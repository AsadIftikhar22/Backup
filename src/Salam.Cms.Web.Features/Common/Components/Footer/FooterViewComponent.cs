namespace Salam.Cms.Web.Features.Common.Components.Footer;

using EPiServer;
using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Salam.Cms.Core.Services.Caching;
using Salam.Cms.Core.Settings.Abstract;
using Salam.Cms.Shared.Models.Extensions;
using Salam.Cms.Web.Features.Common.Helpers;
using Salam.Cms.Web.Features.Navigation.Models;
using Salam.Cms.Web.Features.Settings.Models;
using System.Globalization;

public sealed class FooterViewComponent : BlockComponent<NavigationItemCollectionBlock>
{
    readonly IContentLoader _contentLoader;
    readonly ISettingsManager _settingsManager;
    private readonly ICachingService _cachingService;

    public FooterViewComponent(IContentLoader contentLoader, ISettingsManager settingsManager, ICachingService cachingService)
    {
        _contentLoader = contentLoader;
        _settingsManager = settingsManager;
        _cachingService = cachingService;
    }

    protected override IViewComponentResult InvokeComponent(NavigationItemCollectionBlock currentContent)
    {
        var webLayoutSettings = _settingsManager.GetSettings<WebLayoutSettings>();

        if (webLayoutSettings == null)
        {
            return Content(string.Empty);
        }

        var cacheKey = $"{CacheKeys.Footer}_{CultureInfo.CurrentUICulture.Name}";

        var model = _cachingService.Get<FooterViewModel>(cacheKey);

        if (model != null)
            return View(model);

        var navigationItemCollection = webLayoutSettings.FooterNavigation.GetAllowedReferences();
        var socialLinks = webLayoutSettings.FooterSocialLinks.GetAllowedReferences();
        var legalLinks = webLayoutSettings.FooterLegalLinks;

        model = new FooterViewModel
        {
            NavigationItems = ContentHelper.GetNavigationItemCollection(navigationItemCollection).ToList(),
            FooterLegalLinks = legalLinks,
            FooterSocialLinks = ContentHelper.GetIconLinks(socialLinks).ToList(),
            Logo = webLayoutSettings.Logo,
            CopyrightText = webLayoutSettings.CopyrightText
        };

        _cachingService.Add(model, cacheKey, CacheKeys.MasterKeys.SiteContent);

        return View(model);
    }
}
