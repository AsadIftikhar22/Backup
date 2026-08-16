namespace Salam.Cms.Core.Settings.Infrastructure;

using EPiServer;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAccess;
using EPiServer.Framework.Cache;
using EPiServer.Framework.TypeScanner;
using EPiServer.Globalization;
using EPiServer.Logging;
using EPiServer.Security;
using EPiServer.Web;
using Microsoft.AspNetCore.Http;
using Salam.Cms.Core.Settings.Constants;
using Salam.Cms.Core.Settings.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

public class SettingsService : ISettingsService
{
    //public const string GlobalSettingsRootName = "Global Settings Root";
    private readonly IContentRepository _contentRepository;
    private readonly IContentVersionRepository _contentVersionRepository;
    private readonly ContentRootService _contentRootService;
    private readonly IContentTypeRepository _contentTypeRepository;
    private readonly ILogger _log = LogManager.GetLogger();
    private readonly ITypeScannerLookup _typeScannerLookup;
    private readonly IContentEvents _contentEvents;
    private readonly ISiteDefinitionEvents _siteDefinitionEvents;
    private readonly ISiteDefinitionRepository _siteDefinitionRepository;
    private readonly ISiteDefinitionResolver _siteDefinitionResolver;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IContextModeResolver _contextModeResolver;
    private readonly ISynchronizedObjectInstanceCache _synchronizedObjectInstanceCache;

    public SettingsService(
        IContentRepository contentRepository,
        IContentVersionRepository contentVersionRepository,
        ContentRootService contentRootService,
        ITypeScannerLookup typeScannerLookup,
        IContentTypeRepository contentTypeRepository,
        IContentEvents contentEvents,
        ISiteDefinitionEvents siteDefinitionEvents,
        ISiteDefinitionRepository siteDefinitionRepository,
        ISiteDefinitionResolver siteDefinitionResolver,
        IHttpContextAccessor httpContextAccessor,
        IContextModeResolver contextModeResolver,
        ISynchronizedObjectInstanceCache synchronizedObjectInstanceCache)
    {
        _contentRepository = contentRepository;
        _contentVersionRepository = contentVersionRepository;
        _contentRootService = contentRootService;
        _typeScannerLookup = typeScannerLookup;
        _contentTypeRepository = contentTypeRepository;
        _contentEvents = contentEvents;
        _siteDefinitionEvents = siteDefinitionEvents;
        _siteDefinitionRepository = siteDefinitionRepository;
        _siteDefinitionResolver = siteDefinitionResolver;
        _httpContextAccessor = httpContextAccessor;
        _contextModeResolver = contextModeResolver;
        _synchronizedObjectInstanceCache = synchronizedObjectInstanceCache;
    }

    public ConcurrentDictionary<string, Dictionary<Type, SettingsBase>> SiteSettings { get; } = new ConcurrentDictionary<string, Dictionary<Type, SettingsBase>>();

    public ContentReference? GlobalSettingsRoot { get; set; }

    public Dictionary<Type, SettingsBase>? GetSiteSettings(Guid? siteId = null)
    {
        var contentLanguage = ContentLanguage.PreferredCulture.Name;

        if (!siteId.HasValue)
        {
            siteId = ResolveSiteId();

            if (siteId == Guid.Empty)
                return default;
        }

        try
        {
            if (_contextModeResolver.CurrentMode == ContextMode.Edit)
            {
                var settings = GetEditModeSettingsWithCache(siteId.Value.ToString() + $"-common-draft-{contentLanguage}");

                if (settings != null)
                    return settings;

                return GetEditModeSettingsWithCache(siteId.Value.ToString() + "-common-draft-default");
            }
            else
            {
                var settings = GetViewModeSettingsWithCache(siteId.Value.ToString() + $"-{contentLanguage}");

                if (settings != null)
                    return settings;

                return GetViewModeSettingsWithCache(siteId.Value.ToString() + "-default");
            }
        }
        catch (KeyNotFoundException keyNotFoundException)
        {
            _log.Error($"[Settings] {keyNotFoundException.Message}", exception: keyNotFoundException);
        }
        catch (ArgumentNullException argumentNullException)
        {
            _log.Error($"[Settings] {argumentNullException.Message}", exception: argumentNullException);
        }

        return default;
    }

    public T? GetSiteSettings<T>(Guid? siteId = null) where T : SettingsBase
    {
        try
        {
            var dictionary = GetSiteSettings(siteId);
            if (dictionary != default && dictionary.TryGetValue(typeof(T), out var settingsObject))
                return (T)settingsObject;
        }
        catch (KeyNotFoundException keyNotFoundException)
        {
            _log.Error($"[Settings] {keyNotFoundException.Message}", exception: keyNotFoundException);
        }
        catch (ArgumentNullException argumentNullException)
        {
            _log.Error($"[Settings] {argumentNullException.Message}", exception: argumentNullException);
        }
        catch (NullReferenceException nullReferenceException)
        {
            _log.Error($"[Settings] {nullReferenceException.Message}", exception: nullReferenceException);
        }
        return default;
    }

    private Dictionary<Type, SettingsBase> GetEditModeSettingsWithCache(string cacheKey)
    {
        return _synchronizedObjectInstanceCache.ReadThrough<Dictionary<Type, SettingsBase>>(
            $"EditModeSettings:{cacheKey}",
            () =>
            {
                UpdateSettings();

                if (SiteSettings.TryGetValue(cacheKey, out var siteSettings))
                    return siteSettings;

                return default;
            },
            (result) => new CacheEvictionPolicy(TimeSpan.FromMinutes(1), CacheTimeoutType.Absolute, null, new[] { SettingsConstants.MASTERKEY }),
            ReadStrategy.Wait
        );
    }

    private Dictionary<Type, SettingsBase> GetViewModeSettingsWithCache(string cacheKey)
    {
        return _synchronizedObjectInstanceCache.ReadThrough<Dictionary<Type, SettingsBase>>(
            $"ViewModeSettings:{cacheKey}",
            () =>
            {
                UpdateSettings();

                if (SiteSettings.TryGetValue(cacheKey, out var siteSettings))
                    return siteSettings;

                return default;
            },
            (result) => new CacheEvictionPolicy(TimeSpan.FromMinutes(10), CacheTimeoutType.Absolute, null, new[] { SettingsConstants.MASTERKEY }),
            ReadStrategy.Wait
        );
    }

    public void UpdateSettings(Guid siteId, SettingsBase content, bool isContentNotPublished)
    {
        UpdateSettings(siteId, content, isContentNotPublished, content.Language);
        if (!ContentLanguage.PreferredCulture.Equals(content.Language))
        {
            UpdateSettings(siteId, content, isContentNotPublished, ContentLanguage.PreferredCulture);
        }
    }

    public void UpdateSettings(Guid siteId, SettingsBase content, bool isContentNotPublished, CultureInfo language)
    {
        var contentType = content.GetOriginalType();
        var contentLanguage = language.Name;
        try
        {
            // Make sure the dictionaries exist
            if (!SiteSettings.ContainsKey($"{siteId}-default"))
                SiteSettings[$"{siteId}-default"] = new Dictionary<Type, SettingsBase>();
            if (!SiteSettings.ContainsKey($"{siteId}-common-draft-default"))
                SiteSettings[$"{siteId}-common-draft-default"] = new Dictionary<Type, SettingsBase>();
            if (!SiteSettings.ContainsKey($"{siteId}-{contentLanguage}"))
                SiteSettings[$"{siteId}-{contentLanguage}"] = new Dictionary<Type, SettingsBase>();
            if (!SiteSettings.ContainsKey($"{siteId}-common-draft-{contentLanguage}"))
                SiteSettings[$"{siteId}-common-draft-{contentLanguage}"] = new Dictionary<Type, SettingsBase>();

            // If this is a draft version, add it as such
            if (isContentNotPublished)
            {
                if (!SiteSettings[$"{siteId}-common-draft-default"].ContainsKey(contentType))
                    SiteSettings[$"{siteId}-common-draft-default"][contentType] = content;
                SiteSettings[$"{siteId}-common-draft-{contentLanguage}"][contentType] = content;
            }

            // Otherwise add it as public version
            else
            {
                if (!SiteSettings[$"{siteId}-default"].ContainsKey(contentType))
                    SiteSettings[$"{siteId}-default"][contentType] = content;
                SiteSettings[siteId.ToString() + $"-{contentLanguage}"][contentType] = content;
            }
        }
        catch (KeyNotFoundException keyNotFoundException)
        {
            _log.Error($"[Settings] {keyNotFoundException.Message}", exception: keyNotFoundException);
        }
        catch (ArgumentNullException argumentNullException)
        {
            _log.Error($"[Settings] {argumentNullException.Message}", exception: argumentNullException);
        }
    }

    public void InitializeSettings()
    {
        try
        {
            RegisterContentRoots();
        }
        catch (NotSupportedException notSupportedException)
        {
            _log.Error($"[Settings] {notSupportedException.Message}", exception: notSupportedException);
            throw;
        }

        _contentEvents.PublishedContent += PublishedContent;
        _contentEvents.SavedContent += SavedContent;
        _siteDefinitionEvents.SiteCreated += SiteCreated;
        _siteDefinitionEvents.SiteUpdated += SiteUpdated;
        _siteDefinitionEvents.SiteDeleted += SiteDeleted;
    }

    public void UnintializeSettings()
    {
        _contentEvents.PublishedContent -= PublishedContent;
        _contentEvents.SavedContent -= SavedContent;
        _siteDefinitionEvents.SiteCreated -= SiteCreated;
        _siteDefinitionEvents.SiteUpdated -= SiteUpdated;
        _siteDefinitionEvents.SiteDeleted -= SiteDeleted;
    }

    public void UpdateSettings()
    {
        var root = _contentRepository.GetItems(_contentRootService.List(), new LoaderOptions())
             .FirstOrDefault(x => x.ContentGuid == SettingsFolder.SettingsRootGuid);

        if (root == null)
            return;

        GlobalSettingsRoot = root.ContentLink;
        var children = _contentRepository.GetChildren<SettingsFolder>(GlobalSettingsRoot).ToList();
        foreach (var site in _siteDefinitionRepository.List())
        {
            var folder = children.Find(x => x.Name.Equals(site.Name, StringComparison.InvariantCultureIgnoreCase));
            if (folder != null)
            {
                foreach (var child in _contentRepository.GetChildren<SettingsBase>(folder.ContentLink))
                {
                    // Add child
                    UpdateSettings(site.Id, child, false, child.Language);

                    // Add all published languages & drafts
                    foreach (var branch in child.ExistingLanguages)
                    {
                        // Branch, if different from initially loaded
                        if (!branch.Equals(child.Language))
                        {
                            var branch_child = _contentRepository.Get<SettingsBase>(child.ContentLink, branch);
                            if (branch_child != null) UpdateSettings(site.Id, branch_child, false, branch);
                        }

                        var draftContentLink = _contentVersionRepository.LoadCommonDraft(child.ContentLink, branch.Name);
                        if (draftContentLink != null)
                        {
                            var settingsDraft = _contentRepository.Get<SettingsBase>(draftContentLink.ContentLink);
                            UpdateSettings(site.Id, settingsDraft, true, branch);
                        }

                    }
                }
                continue;
            }
            CreateSiteFolder(site);
        }
    }

    private void RegisterContentRoots()
    {
        var registeredRoots = _contentRepository.GetItems(_contentRootService.List(), new LoaderOptions());
        var settingsRootRegistered = registeredRoots.Any(x => x.ContentGuid == SettingsFolder.SettingsRootGuid && x.Name.Equals(SettingsFolder.SettingsRootName));

        if (!settingsRootRegistered)
        {
            _contentRootService.Register<SettingsFolder>(SettingsFolder.SettingsRootName, SettingsFolder.SettingsRootGuid, ContentReference.RootPage);
        }

        UpdateSettings();
    }

    private void CreateSiteFolder(SiteDefinition siteDefinition)
    {
        var folder = _contentRepository.GetDefault<SettingsFolder>(GlobalSettingsRoot);
        folder.Name = siteDefinition.Name;
        var reference = _contentRepository.Save(folder, SaveAction.Publish, AccessLevel.NoAccess);

        var settingsModelTypes = _typeScannerLookup.AllTypes
            .Where(t => t.GetCustomAttributes(typeof(SettingsContentTypeAttribute), false).Length > 0);

        foreach (var settingsType in settingsModelTypes)
        {
            if (!(settingsType.GetCustomAttributes(typeof(SettingsContentTypeAttribute), false)
                .FirstOrDefault() is SettingsContentTypeAttribute attribute))
            {
                continue;
            }

            var contentType = _contentTypeRepository.Load(settingsType);
            var newSettings = _contentRepository.GetDefault<SettingsBase>(reference, contentType.ID);
            newSettings.Name = attribute.SettingsName;
            _contentRepository.Save(newSettings, SaveAction.Publish, AccessLevel.NoAccess);
            UpdateSettings(siteDefinition.Id, newSettings, false);
        }
    }

    private void SiteCreated(object? sender, SiteDefinitionEventArgs e)
    {
        if (_contentRepository.GetChildren<SettingsFolder>(GlobalSettingsRoot)
            .Any(x => x.Name.Equals(e.Site.Name, StringComparison.InvariantCultureIgnoreCase)))
        {
            return;
        }

        CreateSiteFolder(e.Site);
        _synchronizedObjectInstanceCache.Remove(SettingsConstants.MASTERKEY);
    }

    private void SiteDeleted(object? sender, SiteDefinitionEventArgs e)
    {
        var folder = _contentRepository.GetChildren<SettingsFolder>(GlobalSettingsRoot)
            .FirstOrDefault(x => x.Name.Equals(e.Site.Name, StringComparison.InvariantCultureIgnoreCase));

        if (folder == null)
        {
            return;
        }

        _contentRepository.Delete(folder.ContentLink, true, AccessLevel.NoAccess);
        _synchronizedObjectInstanceCache.Remove(SettingsConstants.MASTERKEY);
    }

    private void SiteUpdated(object? sender, SiteDefinitionEventArgs e)
    {
        if (e is SiteDefinitionUpdatedEventArgs updatedArgs)
        {
            var prevSite = updatedArgs.PreviousSite;
            var updatedSite = updatedArgs.Site;
            var settingsRoot = GlobalSettingsRoot;
            var currentSettingsFolder = _contentRepository.GetChildren<IContent>(settingsRoot).FirstOrDefault(x => x.Name.Equals(prevSite.Name, StringComparison.InvariantCultureIgnoreCase)) as ContentFolder;
            if (currentSettingsFolder != null)
            {
                var cloneFolder = currentSettingsFolder.CreateWritableClone();
                cloneFolder.Name = updatedSite.Name;
                _contentRepository.Save(cloneFolder);
                return;
            }

            CreateSiteFolder(e.Site);
            _synchronizedObjectInstanceCache.Remove(SettingsConstants.MASTERKEY);
        }
    }

    private void PublishedContent(object? sender, ContentEventArgs e)
    {
        if (e == null)
            return;

        if (e.Content is SettingsBase settings)
        {
            var parent = _contentRepository.Get<IContent>(settings.ParentLink);
            var site = _siteDefinitionRepository.Get(parent.Name);

            var id = site?.Id;
            if (id == null || id == Guid.Empty)
                return;

            UpdateSettings(id.Value, settings, false);
            _synchronizedObjectInstanceCache.Remove(SettingsConstants.MASTERKEY);
        }
    }

    private void SavedContent(object? sender, ContentEventArgs e)
    {
        if (e == null)
            return;

        if (e.Content is SettingsBase settings)
        {
            var id = ResolveSiteId();
            if (id == Guid.Empty)
                return;

            UpdateSettings(id, settings, true);
            _synchronizedObjectInstanceCache.Remove(SettingsConstants.MASTERKEY);
        }
    }

    private Guid ResolveSiteId()
    {
        var request = _httpContextAccessor.HttpContext?.Request;
        if (request == null)
            return Guid.Empty;
        var site = _siteDefinitionResolver.GetByHostname(request.Host.Host, false, out var hostname);
        return site == null ? Guid.Empty : site.Id;
    }
}
