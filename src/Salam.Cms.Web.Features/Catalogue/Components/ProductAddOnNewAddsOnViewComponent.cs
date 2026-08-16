namespace Salam.Cms.Web.Features.Catalogue.Components;

using EPiServer.Find;
using EPiServer.Find.Helpers;
using EPiServer.Web.Mvc;
using EPiServer.Web.Routing;
using Microsoft.AspNetCore.Mvc;
using Salam.Cms.Shared.Models.Catalogue.Models;
using Salam.Cms.Shared.Models.Extensions;
using Salam.Cms.Web.Features.Catalogue.Models;
using Salam.Cms.Web.Features.Catalogue.ViewModels;
using Salam.Cms.Web.Features.LanguageSwitcher.Services;

public class ProductAddOnNewAddsOnViewComponent : AsyncBlockComponent<ProductAddOnNewAddsOnBlock>
{
    private const string NewAddsOnSkuPrefix = "NewAddons";

    private readonly IPageRouteHelper _pageRouteHelper;
    private readonly IClient _findClient;
    private readonly LanguageService _languageService;

    public ProductAddOnNewAddsOnViewComponent(IPageRouteHelper pageRouteHelper, IClient findClient, LanguageService languageService)
    {
        _pageRouteHelper = pageRouteHelper;
        _findClient = findClient;
        _languageService = languageService;
    }

    protected async override Task<IViewComponentResult> InvokeComponentAsync(ProductAddOnNewAddsOnBlock currentContent)
    {
        var currentPage = _pageRouteHelper.Page as ProductLandingPage;
        var language = _languageService.GetCurrentLanguage().TwoLetterISOLanguageName;

        if (currentPage == null)
        {
            return Content("This block can be used only inside ProductLandingPage.");
        }

        var query = await _findClient
            .Search<ProductSku>()
            .Filter(addOn => addOn.Language.Match(language))
            .StaticallyCacheFor(TimeSpan.FromSeconds(30))
            .Take(1000)
            .GetResultAsync();

        var addOns = query
            .Where(x => x.Sku.Contains(NewAddsOnSkuPrefix, StringComparison.OrdinalIgnoreCase))
            /*.Where(x => !string.IsNullOrEmpty(x.Data) || !String.IsNullOrEmpty(x.CallsMinutes))*/
            .ToList(); 

        addOns = addOns
            .OrderBy(x => x.Price)
            .ToList();
        var visibleFields = currentPage.ProductSelectorVisibleFieldsNew.IsNullOrEmpty() ? currentPage.ProductSelectorVisibleFields : currentPage.ProductSelectorVisibleFieldsNew;

        ProductAddOnNewAddsOnViewModel model = new(currentContent)
        {
            AddOns = addOns,
            Title = currentPage.ProductSelectorTitle,
            Description = currentPage.ProductSelectorDescription,
            NavigationTitle = currentContent.NavigationTitle,
            Span = currentPage.Span,
            AddBannerNavigation = currentContent.AddBannerNavigation,
            VatText = currentPage.VatText,
            VisibleFeaturesFields = visibleFields,
            BuyNowStaticURL= currentContent?.BuyNowStaticURL,
            VisibleFields = visibleFields,
            DataText = currentPage.DataText,
        };

        return View(model);
    }
}
