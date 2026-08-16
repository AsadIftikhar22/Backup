namespace Salam.Cms.Web.API.Services;

using EPiServer;
using EPiServer.Core;
using EPiServer.Filters;
using EPiServer.Web;
using Salam.Cms.Core.Services.Caching;
using Salam.Cms.Core.Settings.Abstract;
using Salam.Cms.Shared.Models.Extensions;
using Salam.Cms.Shared.Models.Pages;
using Salam.Cms.Web.Features.ClientResources.Services;
using Salam.Cms.Web.Features.Common.Components.Footer;
using Salam.Cms.Web.Features.Common.Components.Navigation;
using Salam.Cms.Web.Features.Common.Helpers;
using Salam.Cms.Web.Features.Common.Interfaces;
using Salam.Cms.Web.Features.Common.Models;
using Salam.Cms.Web.Features.Home.Models;
using Salam.Cms.Web.Features.Landing.Models;
using Salam.Cms.Web.Features.LanguageSwitcher.Services;
using Salam.Cms.Web.Features.LanguageSwitcher.ViewModels;
using Salam.Cms.Web.Features.Settings.Models;
using Salam.CMS.Web.Data;
using System.Collections.Generic;
using System.Globalization;

public class WebLayoutSettingsImplementation : IWebLayoutSettingsRepo
{
    private readonly IContentLoader _contentLoader;
    private readonly ISettingsManager _settingsManager;
    private readonly LanguageService _languageService;
    private readonly ICachingService _cachingService;
    private readonly IInlineCssService _InlineCssService;

    public WebLayoutSettingsImplementation(IContentLoader contentLoader,
        ISettingsManager settingsManager,
        LanguageService languageService,
        ICachingService cachingService,
        IInlineCssService InlineCssService
        )
    {
        _contentLoader = contentLoader;
        _settingsManager = settingsManager;
        _languageService = languageService;
        _cachingService = cachingService;
        _InlineCssService = InlineCssService;
    }
    /// <summary>
    /// GetFormEmailBody
    /// </summary>
    /// <param name="cultureInfo"></param>
    /// <param name="RequestForm"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public EmailBodyResponse GetFormEmailBody(CultureInfo cultureInfo, string p_FormType)
    {
        var obj_EmailBodyResponse = new EmailBodyResponse();
        try
        {
            var webLayoutSettings = _settingsManager.GetSettings<WebLayoutSettings>(CultureInfo.CurrentUICulture);
            if (!String.IsNullOrEmpty(webLayoutSettings.B2BFromEmail))
                obj_EmailBodyResponse.FromEmail = webLayoutSettings.B2BFromEmail;
            if (!String.IsNullOrEmpty(webLayoutSettings.APIbaseURL))
                obj_EmailBodyResponse.APIbaseURL = webLayoutSettings.APIbaseURL;

            if (p_FormType != null)
            {
                if (p_FormType == FormType.Template1.Value)
                {
                        obj_EmailBodyResponse.EmailBody = webLayoutSettings?.FreeConsultationForm;
                        obj_EmailBodyResponse.Emailsubject = webLayoutSettings?.FreeConsultationForm_Subject;
                        obj_EmailBodyResponse.ToEmail = webLayoutSettings?.FreeConsultationForm_ToEmail;
                }
                if (p_FormType == FormType.Template2.Value)
                {
                        obj_EmailBodyResponse.EmailBody = webLayoutSettings?.ComplaintForm;
                        obj_EmailBodyResponse.Emailsubject = webLayoutSettings?.ComplaintForm_Subject;
                        obj_EmailBodyResponse.ToEmail = webLayoutSettings?.ComplaintForm_ToEmail;
                }
                if (p_FormType == FormType.Template3.Value)
                {
                        obj_EmailBodyResponse.EmailBody = webLayoutSettings?.CallBackForm;
                        obj_EmailBodyResponse.Emailsubject = webLayoutSettings?.CallBackForm_Subject;
                        obj_EmailBodyResponse.ToEmail = webLayoutSettings?.CallBackForm_ToEmail;
                }
                if (p_FormType == FormType.Template4.Value)
                {
                    obj_EmailBodyResponse.EmailBody = webLayoutSettings?.Template4FormBody;
                    obj_EmailBodyResponse.Emailsubject = webLayoutSettings?.Template4Form_Subject;
                    obj_EmailBodyResponse.ToEmail = webLayoutSettings?.Template4Form_ToEmail;
                }
                if (p_FormType == FormType.Template5.Value)
                {
                    obj_EmailBodyResponse.EmailBody = webLayoutSettings?.Template5FormBody;
                    obj_EmailBodyResponse.Emailsubject = webLayoutSettings?.Template5Form_Subject;
                    obj_EmailBodyResponse.ToEmail = webLayoutSettings?.Template5Form_ToEmail;
                }

                if (p_FormType == FormType.SolutionForm.Value)
                {
                    obj_EmailBodyResponse.EmailBody = webLayoutSettings?.SolutionFormHtml;
                    obj_EmailBodyResponse.Emailsubject = webLayoutSettings?.SolutionEmailForm_Subject;
                    obj_EmailBodyResponse.ToEmail = webLayoutSettings?.SolutionEmailForm_ToEmail;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception Message is {ex.Message} and StackTrace is {ex.StackTrace}");
            throw new ArgumentException($"Exception message is {ex.Message} and Stacktrace is {ex.StackTrace}");
        }

        return obj_EmailBodyResponse;
    }

    public int GetProductEnquireLimit()
    {
        var webLayoutSettings = _settingsManager.GetSettings<WebLayoutSettings>();
        return webLayoutSettings?.MaxProductEnquireLimit ?? 10;
    }
    /// <summary>
    /// GetAllWebLayoutSettings
    /// </summary>
    /// <param name="cultureInfo"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public CentralizedLayout GetAllWebLayoutSettings(CultureInfo cultureInfo)
    {
        CentralizedLayout objCentralizedLayout = new CentralizedLayout();
        try
        {
            objCentralizedLayout.ExecuteStaticFiles = _InlineCssService;

            var footercacheKey = $"{CacheKeys.Footer}_{cultureInfo}";
            var _footerCacheResult = _cachingService.Get<FooterViewModel>(footercacheKey);

            var navigationcacheKey = $"{CacheKeys.Navigation}_{cultureInfo}";
            var _headerCacheResult = _cachingService.Get<NavigationViewModel>(navigationcacheKey);

            //if (_footerCacheResult is { } && _headerCacheResult is { })
            //{
            //    objCentralizedLayout.navigationViewModel = _headerCacheResult;
            //    objCentralizedLayout.footerViewModel = _footerCacheResult;
            //    return objCentralizedLayout;
            //}

            var languages = _languageService.GetAvailableLanguages()
                .Select(x => new LanguageItem
                {
                    DisplayName = x.Name.ToUpperInvariant(),
                    Value = x.Name
                });



            Console.WriteLine($"Languages are {languages}");
            NavigationViewModel objNavigationViewModels = new NavigationViewModel();
            var webLayoutSettings = _settingsManager.GetSettings<WebLayoutSettings>(CultureInfo.CurrentUICulture);
            var startPage = _contentLoader.Get<HomePage>(SiteDefinition.Current.StartPage,
                                                       CultureInfo.CurrentUICulture);

            var currentPage = _contentLoader.Get<ISitePageData>(startPage.ContentLink,
                                            CultureInfo.CurrentUICulture);

            LandingPage? landingPage = GetLandingPage(currentPage);

            if (landingPage == null)
            {
                // get the first landing page under the start page for now
                // TODO: Ideally this should be configurable in WebLayoutSettings
                landingPage = _contentLoader
                    .GetChildren<LandingPage>(ContentReference.StartPage, CultureInfo.CurrentUICulture)
                    .FirstOrDefault();
            }

            var pages = _contentLoader
                .GetChildren<INavigationItem>(landingPage?.ContentLink, CultureInfo.CurrentUICulture)
                .Where(pageItem => pageItem.VisibleInMenu)
                .ToList();

            var childPages2ndLevelDict = new Dictionary<ContentReference, List<INavigationItem>>();
            var childPages3rdLevelDict = new Dictionary<ContentReference, List<INavigationItem>>();

            foreach (var page in pages)
            {
                var childPages2ndLevel = _contentLoader
                    .GetChildren<INavigationItem>(page.ContentLink, CultureInfo.CurrentUICulture)
                    .Where(p => p.VisibleInMenu)
                    .ToList();

                foreach (var childPage in childPages2ndLevel)
                {
                    var childPages3rdLevel = _contentLoader
                        .GetChildren<INavigationItem>(childPage.ContentLink, CultureInfo.CurrentUICulture)
                        .Where(p => p.VisibleInMenu)
                        .ToList();

                    childPages3rdLevelDict.Add(childPage.ContentLink, childPages3rdLevel);
                }

                childPages2ndLevelDict.Add(page.ContentLink, childPages2ndLevel);
            }

            var socialLinks = webLayoutSettings.FooterSocialLinks.GetAllowedReferences();

            var children = FilterForVisitor.Filter(
                _contentLoader.GetChildren<ISitePageData>(ContentReference.StartPage, CultureInfo.CurrentUICulture)
            );

            var topNavLinks = children
                .Cast<SitePageData>()
                .Where(x => x.VisibleInMenu)
                .ToList();

            var legalLinks = webLayoutSettings.FooterLegalLinks;
            var b2blegalLinks = webLayoutSettings.B2BFooterLegalLinks;
            objNavigationViewModels = new NavigationViewModel
            {
                TopLinks = topNavLinks,
                CoverageButtonLink = webLayoutSettings.CoverageButtonLink,
                B2BCoverageButtonLink = webLayoutSettings.B2BCoverageButtonLink,
                HelpAndSupportButtonLink = webLayoutSettings.HelpAndSupportButtonLink,
                MySalamIcon = webLayoutSettings.MySalamIcon,
                MySalamLink = webLayoutSettings.MySalamLink,
                B2BMySalamIcon = webLayoutSettings.B2BMySalamIcon,
                SelectedProductCounterr=webLayoutSettings?.SelectedProductCounterr,
                B2BMySalamLink = webLayoutSettings.B2BMySalamLink,
                TopNavigationMenu = webLayoutSettings.TopNavigationMenu,
                B2BTopNavigationMenu = webLayoutSettings.B2BTopNavigationMenu,
                Pages = pages,
                ChildPages2ndLevel = childPages2ndLevelDict,
                ChildPages3rdLevel = childPages3rdLevelDict,
                FooterSocialLinks = ContentHelper.GetIconLinks(socialLinks, CultureInfo.CurrentUICulture).ToList(),
                Logo = webLayoutSettings.Logo,
                LogoSmall = webLayoutSettings.LogoSmall,
                CopyrightText = webLayoutSettings.CopyrightText,
                Languages = languages,
                LanguagesNavItemText = webLayoutSettings.LanguagesNavItemText,
                B2BLanguagesNavItemText = webLayoutSettings.B2BLanguagesNavItemText,
                FooterLegalLinks = legalLinks,
                CurrentPage = currentPage
            };
            //For Footer

            var navigationItemCollection = webLayoutSettings.FooterNavigation.GetAllowedReferences();
            var footersocialLinks = webLayoutSettings.FooterSocialLinks.GetAllowedReferences();
            var footerlegalLinks = webLayoutSettings.FooterLegalLinks;

            var objFooterViewModel = new FooterViewModel
            {
                NavigationItems = ContentHelper.GetNavigationItemCollection(navigationItemCollection, CultureInfo.CurrentUICulture).ToList(),
                FooterLegalLinks = legalLinks,
                FooterSocialLinks = ContentHelper.GetIconLinks(socialLinks, CultureInfo.CurrentUICulture).ToList(),
                Logo = webLayoutSettings.Logo,
                CopyrightText = webLayoutSettings.CopyrightText
            };

            _cachingService.Add(objNavigationViewModels, navigationcacheKey, CacheKeys.MasterKeys.SiteContent);
            objCentralizedLayout.navigationViewModel = objNavigationViewModels;

            _cachingService.Add(objFooterViewModel, footercacheKey, CacheKeys.MasterKeys.SiteContent);
            objCentralizedLayout.footerViewModel = objFooterViewModel;

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception Message is {ex.Message} and StackTrace is {ex.StackTrace}");
            throw new ArgumentException($"Exception message is {ex.Message} and Stacktrace is {ex.StackTrace}");
        }
        return objCentralizedLayout;
    }

    private LandingPage? GetLandingPage(ISitePageData currentPage)
    {
        if (currentPage is LandingPage page)
        {
            return page;
        }
        return _contentLoader.GetAncestors(currentPage.ContentLink)
            .OfType<LandingPage>()
            .FirstOrDefault();
    }
}
