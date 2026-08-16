namespace Salam.Cms.Web.Features.Common.Components.Navigation;

using Microsoft.AspNetCore.Mvc;
using Salam.Cms.Core.Services.Caching;
using Salam.Cms.Web.Features.B2BGeneralContent.Models;
using Salam.Cms.Web.Features.Common.Interfaces;
using Salam.Cms.Web.Features.Common.Models;
using Salam.Cms.Web.Features.LanguageSwitcher.Services;
using System.Globalization;

public sealed class NavigationViewComponent : ViewComponent
{
    private readonly INavigationViewModelBuilder _navigationViewModelBuilder;
    private readonly LanguageService _languageService;
    private readonly ICachingService _cachingService;

    public NavigationViewComponent(
        INavigationViewModelBuilder navigationViewModelBuilder,
        LanguageService languageService,
        ICachingService cachingService
    )
    {
        _navigationViewModelBuilder = navigationViewModelBuilder;
        _languageService = languageService;
        _cachingService = cachingService;
    }

    public IViewComponentResult Invoke(ISitePageData currentPage)
    {
        // TODO: Cache won't scale to multiple landing pages (business, wholesale, investors, etc.)
        // Need to revisit this later
        string cacheKey = string.Empty;

        if (currentPage is B2BComponentContentPage)
            cacheKey = $"{CacheKeys.B2B_Navigation}_{CultureInfo.CurrentUICulture.Name}";
        else
            cacheKey = $"{CacheKeys.Navigation}_{CultureInfo.CurrentUICulture.Name}";

            var model = _cachingService.Get<NavigationViewModel>(cacheKey);

            //if (model == null)
            //{
                model = _navigationViewModelBuilder.Build(currentPage, out var isCacheEnabled);

                if (isCacheEnabled)
                {
                        _cachingService.Add(model, cacheKey, CacheKeys.Navigation);
                }
            //}

            // Always set the current page and user language to the model to ensure the view has the correct context
            model.CurrentPage = currentPage;
            model.CurrentLanguage = _languageService.GetCurrentLanguage().Name;

            string viewpath = currentPage switch
            {
                B2BComponentContentPage => "~/Views/Shared/Components/B2BNavigation/Default.cshtml",
                B2bSearchPage.Models.B2bSearchPage => "~/Views/Shared/Components/B2BNavigation/Default.cshtml",
                WholesaleSitePageData => "~/Views/Shared/Components/WholesaleNavigation/Default.cshtml",
                _ => "~/Views/Shared/Components/Navigation/Default.cshtml"
            };
            return View(viewpath, model);
    }
}