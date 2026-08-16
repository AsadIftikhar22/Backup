namespace Salam.Cms.Web.Features.RedirectRuleBlock.Models;

using EPiServer;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.ServiceLocation;
using Geta.Optimizely.ContentTypeIcons;
using Geta.Optimizely.ContentTypeIcons.Attributes;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Salam.Cms.Core.Settings.Abstract;
using Salam.Cms.Core.Settings.Services;
using Salam.Cms.Web.Features.Common.Models;
using Salam.Cms.Web.Features.Settings.Models;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;

[ContentType(
    DisplayName = "URL Redirect Rule New",
    GUID = "439eca16-ffad-4344-8a71-3a58ceb73460",
    Description = "URL Redirect Rule New",
    GroupName = SystemTabNames.Content)]
[ContentTypeIcon(FontAwesome5Solid.ListOl)]
public class RedirecttRuleBlock : SiteContentBlock
{
    [Display(Name = "Source Url")]
    public virtual string SourceUrl { get; set; }

    [Display(Name = "Target Url")]
    public virtual string TargetUrl { get; set; }

}
[ContentType(
    DisplayName = "URL Redirect Rule",
    GUID = "96f45a7f-1153-4913-88a4-f416ca8548a7",
    Description = "URL Redirect Rule",
    GroupName = SystemTabNames.Content)]
[ContentTypeIcon(FontAwesome5Solid.ListOl)]
public class RedirectRuleBlock : SiteContentBlock
{
    [Display(Name = "Source Url")]
    public virtual string SourceUrl { get; set; }

    [Display(Name = "Target Url")]
    public virtual string TargetUrl { get; set; }

    [Display(Name = "Is Permanent (301)")]
    public virtual bool IsPermanent { get; set; }
}
public interface IRedirectRepository
{
    List<RedirecttRuleBlock> GetAllDXPRepositoriesSlug();
    List<RedirectRuleBlock> GetAll();

}

public class RedirectRepository : IRedirectRepository
{
    private readonly IContentLoader _contentLoader;

    public RedirectRepository(IContentLoader contentLoader)
    {
        _contentLoader = contentLoader;
    }
    

    public List<RedirecttRuleBlock> GetAllDXPRepositoriesSlug()
    {
        var _settingsManager = ServiceLocator.Current.GetInstance<ISettingsManager>();
        var webLayoutSettings = _settingsManager.GetSettings<WebLayoutSettings>();

        return webLayoutSettings?.RedirectRuleBlockDXPSlug?.ToList()
        ?? new List<RedirecttRuleBlock>();
    }
    public List<RedirectRuleBlock> GetAll()
    {
        var _settingsManager = ServiceLocator.Current.GetInstance<ISettingsManager>();
        var webLayoutSettings = _settingsManager.GetSettings<WebLayoutSettings>();

        return webLayoutSettings?.RedirectRuleBlock?.ToList()
        ?? new List<RedirectRuleBlock>();
    }
}