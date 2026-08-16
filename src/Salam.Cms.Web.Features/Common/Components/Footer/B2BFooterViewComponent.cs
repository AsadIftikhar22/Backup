namespace Salam.Cms.Web.Features.Common.Components.Footer;
using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Salam.Cms.Core.Services.Caching;
using Salam.Cms.Core.Settings.Abstract;
using Salam.Cms.Shared.Models.Extensions;
using Salam.Cms.Web.Features.Common.Helpers;
using Salam.Cms.Web.Features.Navigation.Models;
using Salam.Cms.Web.Features.Settings.Models;
using System.Globalization;

public sealed class B2BFooterViewComponent : BlockComponent<NavigationItemCollectionBlock>
{
    readonly ISettingsManager _settingsManager;
    private readonly ICachingService _cachingService;

    public B2BFooterViewComponent(ISettingsManager settingsManager, ICachingService cachingService)
    {
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

        var cacheKey = $"{CacheKeys.B2B_Footer}_{CultureInfo.CurrentUICulture.Name}";

        var model = _cachingService.Get<FooterViewModel>(cacheKey);

        if (model != null)
            return View(model);

        var navigationItemCollection = webLayoutSettings.B2BFooterNavigation.GetAllowedReferences() ?? webLayoutSettings.FooterNavigation.GetAllowedReferences();
        var socialLinks = webLayoutSettings.B2BFooterSocialLinks.GetAllowedReferences() ?? webLayoutSettings.FooterSocialLinks.GetAllowedReferences();
        var legalLinks = webLayoutSettings.B2BFooterLegalLinks ?? webLayoutSettings.FooterLegalLinks;

        model = new FooterViewModel
        {
            NavigationItems = ContentHelper.GetNavigationItemCollection(navigationItemCollection).ToList(),
            FooterLegalLinks = legalLinks,
            FooterSocialLinks = ContentHelper.GetIconLinks(socialLinks).ToList(),
            Logo = webLayoutSettings.B2BLogo ?? webLayoutSettings.Logo,
            CopyrightText = webLayoutSettings.B2BCopyrightText ?? webLayoutSettings.CopyrightText,
            b2bFooterHtml=webLayoutSettings?.B2BFooterHTML
        };

        _cachingService.Add(model, cacheKey, CacheKeys.MasterKeys.SiteContent);
        string FooterView = "~/Views/Shared/Components/B2BFooter/Default.cshtml";
        return View(FooterView, model);
    }

}