namespace Salam.Cms.Web.Features.Common.Components.Footer;

using EPiServer;
using Microsoft.AspNetCore.Mvc;
using Salam.Cms.Core.Services.Caching;
using Salam.Cms.Core.Settings.Abstract;
using Salam.Cms.Web.Features.Common.Interfaces;
using Salam.Cms.Web.Features.Settings.Models;
using Salam.Cms.Web.Features.Showcase.ViewModels;
using System.Globalization;

public sealed class CookiesBannerViewComponent : ViewComponent
{
    readonly IContentLoader _contentLoader;
    readonly ISettingsManager _settingsManager;
    private readonly ICachingService _cachingService;

    public CookiesBannerViewComponent(IContentLoader contentLoader, ISettingsManager settingsManager, ICachingService cachingService)
    {
        _contentLoader = contentLoader;
        _settingsManager = settingsManager;
        _cachingService = cachingService;
    }
    public IViewComponentResult Invoke(ISitePageData? sitePage)
    {

        var webLayoutSettings = _settingsManager.GetSettings<WebLayoutSettings>();

        if (webLayoutSettings == null)
        {
            return Content(string.Empty);
        }

        var cacheKey = $"{CacheKeys.CookiesBanner}_{CultureInfo.CurrentUICulture.Name}";

        var model = _cachingService.Get<CookiesBannerViewModel>(cacheKey);

        if (model != null)
            return View(model);
        model = CookiesBannerViewModel.FromBlock(webLayoutSettings.CookiesBannerBlock);
        _cachingService.Add(model, cacheKey, CacheKeys.MasterKeys.SiteContent);
        return View("~/Views/Shared/Components/CookiesBanner/Default.cshtml", model);
    }
}