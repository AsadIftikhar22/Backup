namespace Salam.Cms.Web.Features.Common.Components.Navigation;

using EPiServer;
using EPiServer.Core;
using EPiServer.Filters;
using EPiServer.Find;
using Microsoft.Extensions.Logging;
using Salam.Cms.Core.Settings.Abstract;
using Salam.Cms.Shared.Models.Extensions;
using Salam.Cms.Shared.Models.Pages;
using Salam.Cms.Web.Features.B2BGeneralContent.Models;
using Salam.Cms.Web.Features.B2BLanding.Models;
using Salam.Cms.Web.Features.Common.Helpers;
using Salam.Cms.Web.Features.Common.Interfaces;
using Salam.Cms.Web.Features.Common.Models;
using Salam.Cms.Web.Features.Landing.Models;
using Salam.Cms.Web.Features.LanguageSwitcher.Services;
using Salam.Cms.Web.Features.LanguageSwitcher.ViewModels;
using Salam.Cms.Web.Features.Settings.Models;
using System.Linq;

public class NavigationViewModelBuilder : INavigationViewModelBuilder
{
    private readonly IContentLoader _contentLoader;
    private readonly ISettingsManager _settingsManager;
    private readonly LanguageService _languageService;
    private readonly ILogger<NavigationViewModelBuilder> _logger;

    public NavigationViewModelBuilder(IContentLoader contentLoader, ISettingsManager settingsManager, LanguageService languageService, ILogger<NavigationViewModelBuilder> logger)
    {
        _contentLoader = contentLoader;
        _settingsManager = settingsManager;
        _languageService = languageService;
        _logger = logger;
    }

    public NavigationViewModel Build(ISitePageData currentPage, out bool isCacheEnabled)
    {
        var webLayoutSettings = _settingsManager.GetSettings<WebLayoutSettings>();

        if (webLayoutSettings == null)
        {
            isCacheEnabled = false;
            return new NavigationViewModel();
        }

        var languages = _languageService.GetAvailableLanguages()
            .Select(x => new LanguageItem
            {
                DisplayName = x.Name.ToUpperInvariant(),
                Value = x.Name
            });

        SitePageData? landingPage = GetLandingPage(currentPage);
        ContentReference navigationRoot = (ContentReference)null;
        if (ContentReference.IsNullOrEmpty(ContentReference.StartPage))
        {
            isCacheEnabled = false;

            _logger.LogError("StartPage ContentReference is null or empty in NavigationViewModelBuilder.");
            return new NavigationViewModel
            {
                Languages = languages,
            };
        }
        ContentReference B2BOrConsumer = (ContentReference)null;


        if (landingPage is not B2BComponentContentPage)
        {
            B2BOrConsumer = webLayoutSettings.MainPageConsumerNavigation;
        }
        else
        {
            B2BOrConsumer = webLayoutSettings.MainPageBusinessNavigation;

        }

        if (B2BOrConsumer != null && !ContentReference.IsNullOrEmpty(B2BOrConsumer))
        {
            navigationRoot = B2BOrConsumer;
        }
        else if (landingPage != null)
        {
            navigationRoot = landingPage.ContentLink;
        }

        //if (landingPage == null || ContentReference.IsNullOrEmpty(landingPage))
        //{
        //    _logger.LogWarning($"LandingPage '{landingPage}' encountered with a null or empty ContentLink in NavigationViewModelBuilder.");

        //    isCacheEnabled = false;

        //    return new NavigationViewModel
        //    {
        //        Languages = languages,
        //    };
        //}

        var pages = _contentLoader
            .GetChildren<INavigationItem>(navigationRoot)
            .Where(pageItem => pageItem.VisibleInMenu)
            .OrderBy(x => x.SortingOrder > 0 ? x.SortingOrder : int.MaxValue)
            .ToList();

        var childPages2ndLevelDict = new Dictionary<ContentReference, List<INavigationItem>>();
        var childPages3rdLevelDict = new Dictionary<ContentReference, List<INavigationItem>>();

        foreach (var page in pages)
        {
            var childPages2ndLevel = _contentLoader
                .GetChildren<INavigationItem>(page.ContentLink)
                .Where(p => p.VisibleInMenu)
                .ToList();

            foreach (var childPage in childPages2ndLevel)
            {
                var childPages3rdLevel = _contentLoader
                    .GetChildren<INavigationItem>(childPage.ContentLink)
                    .Where(p => p.VisibleInMenu)
                    .ToList();

                childPages3rdLevelDict.Add(childPage.ContentLink, childPages3rdLevel);
            }

            childPages2ndLevelDict.Add(page.ContentLink, childPages2ndLevel);
        }

        var socialLinks = webLayoutSettings.FooterSocialLinks.GetAllowedReferences();

        var children = FilterForVisitor.Filter(
            _contentLoader.GetChildren<ISitePageData>(ContentReference.StartPage)
        );

        var topNavLinks = children
            .Cast<SitePageData>()
            .Where(x => x.VisibleInMenu)
            .ToList();

        var legalLinks = webLayoutSettings.FooterLegalLinks;

        isCacheEnabled = true;

        var model = new NavigationViewModel
        {
            TopLinks = topNavLinks,
            CoverageButtonLink = webLayoutSettings.CoverageButtonLink,
            HelpAndSupportButtonLink = webLayoutSettings.HelpAndSupportButtonLink,
            SelectedProductCounterr = webLayoutSettings?.SelectedProductCounterr,
            MySalamIcon = webLayoutSettings.MySalamIcon,
            MySalamLink = webLayoutSettings.MySalamLink,
            TopNavigationMenu = webLayoutSettings.TopNavigationMenu,
            Pages = pages,
            ChildPages2ndLevel = childPages2ndLevelDict,
            ChildPages3rdLevel = childPages3rdLevelDict,
            FooterSocialLinks = ContentHelper.GetIconLinks(socialLinks).ToList(),
            Logo = webLayoutSettings.Logo,
            WholeSaleLogo = webLayoutSettings.WholeSaleLogo,
            LogoSmall = webLayoutSettings.LogoSmall,
            WholeSaleLogoSmall = webLayoutSettings.WholeSaleLogoSmall,
            CopyrightText = webLayoutSettings.CopyrightText,
            Languages = languages,
            LanguagesNavItemText = webLayoutSettings.LanguagesNavItemText,
            SalamNavItemText = webLayoutSettings.SalamNavItemText,
            SalamNavItems = webLayoutSettings.SalamNavItems,
            FooterLegalLinks = legalLinks,
            B2bSearchPlaceHolderTxt = webLayoutSettings?.B2bSearchPlaceHolderTxt,
            B2bSearchBtnTxt = webLayoutSettings?.B2bSearchBtnTxt,
            B2BCoverageButtonLink = webLayoutSettings.B2BCoverageButtonLink
        };

        return model;
    }

    private SitePageData? GetLandingPage(ISitePageData currentPage)
    {
        dynamic landingpage = (SitePageData)null;

        if (currentPage is LandingPage page)
        {
            return page;
        }
        if (currentPage is B2BComponentContentPage b2bpage)
        {
            return b2bpage;
        }
        if (landingpage == null)
        {
            var ancestors = _contentLoader.GetAncestors(currentPage.ContentLink)
          .OfType<SitePageData>()
          .ToList();

            return ancestors.FirstOrDefault(x => x is LandingPage || x is B2BComponentContentPage)
                   ?? ancestors.FirstOrDefault();
        }
        return landingpage;
    }
}
