namespace Salam.Cms.Web.Features.Common.Components.MetaData;

using EPiServer.DataAbstraction;

using Microsoft.AspNetCore.Mvc;
using Salam.Cms.Core.Settings.Abstract;
using Salam.Cms.Shared.Models.Extensions;
using Salam.Cms.Shared.Models.Helpers;
using Salam.Cms.Web.Features.Common.Models;
using Salam.Cms.Web.Features.Settings.Models;

public class SocialMetaDataViewComponent : ViewComponent
{
    private readonly ISettingsManager _settingsManager;

    private readonly IContentTypeRepository _contentTypeRepository;

    private readonly IValueFallbackHandler _valueFallbackHandler;

    public SocialMetaDataViewComponent(
        ISettingsManager settingsManager,
        IContentTypeRepository contentTypeRepository,
        IValueFallbackHandler valueFallbackHandler)
    {
        _settingsManager = settingsManager;
        _contentTypeRepository = contentTypeRepository;
        _valueFallbackHandler = valueFallbackHandler;
    }

    public IViewComponentResult Invoke(SitePageData? sitePage)
    {
        if (sitePage == null)
        {
            return Content(string.Empty);
        }

        var model = BuildModel(sitePage);

        return View(model);
    }

    private SocialMetaDataViewModel BuildModel(SitePageData sitePageData)
    {
        var contentTypeName = string.Empty;
        if (_contentTypeRepository.TryGet(sitePageData.ContentTypeID, out var contentType))
        {
            contentTypeName = contentType.Name;
        }

        var description = _valueFallbackHandler.GetBest(sitePageData.SocialSharingDescription, sitePageData.MetaDescription);
        var imageAlt = _valueFallbackHandler.GetBest(sitePageData.SocialSharingImageAltText, sitePageData.SocialSharingTitle, sitePageData.MetaTitle);

        var webLayoutSettings = _settingsManager.GetSettings<WebLayoutSettings>();

        return new SocialMetaDataViewModel
        {
            Title = GetPageTitle(sitePageData, webLayoutSettings),
            Description = description,
            ImageReference = _valueFallbackHandler.GetBest(sitePageData.SocialSharingImage),
            ImageAltText = imageAlt,
            PageReference = sitePageData.ContentLink,
            SiteName = webLayoutSettings.SiteName ?? string.Empty,
            TypeName = contentTypeName,
            Creator = sitePageData.TwitterCardCreator ?? string.Empty,
        };
    }

    private string GetPageTitle(SitePageData sitePageData, WebLayoutSettings webLayoutSettings)
    {
        var contentTitle = _valueFallbackHandler.GetBest(
            sitePageData.SocialSharingTitle,
            sitePageData.MetaTitle,
            sitePageData.Heading,
            sitePageData.Name);

        if (string.IsNullOrWhiteSpace(webLayoutSettings?.SiteName))
        {
            return contentTitle;
        }

        return webLayoutSettings.PageTitleOrder switch
        {
            PageTitleOrder.PageTitleThenSiteName => $"{contentTitle} | {webLayoutSettings.SiteName}",
            PageTitleOrder.SiteNameThenPageTitle => $"{webLayoutSettings.SiteName} | {contentTitle}",
            _ => contentTitle,
        };
    }
}