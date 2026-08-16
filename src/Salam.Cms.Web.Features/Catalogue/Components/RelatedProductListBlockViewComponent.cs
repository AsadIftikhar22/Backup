namespace Salam.Cms.Web.Features.Catalogue.Components;

using EPiServer;
using EPiServer.Find;
using EPiServer.Web.Mvc;
using EPiServer.Web.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Salam.Cms.Core.Services.Catalogue;
using Salam.Cms.Core.Settings.Abstract;
using Salam.Cms.Core.Settings.Configuration;
using Salam.Cms.Shared.Models.Catalogue.Models;
using Salam.Cms.Web.Features.Catalogue.Models;
using Salam.Cms.Web.Features.Catalogue.ViewModels;
using Salam.Cms.Web.Features.Common.Helpers;
using Salam.Cms.Web.Features.LanguageSwitcher.Services;
using Salam.Cms.Web.Features.Settings.Models;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

public class RelatedProductListBlockViewComponent : AsyncBlockComponent<RelatedProductListBlock>
{
    private readonly IContentLoader _contentLoader;
    private readonly IClient _findClient;
    private readonly IPageRouteHelper _pageRouteHelper;
    private readonly CatalogueApiSettings _catalogueApiSettings;
    private readonly ILogger<RelatedProductListBlockViewComponent> _logger;
    private readonly ISettingsManager _settingsManager;
    private readonly LanguageService _languageService;
    private readonly IProductQueryService _productQuery;

    public RelatedProductListBlockViewComponent(
        IContentLoader contentLoader,
        IClient client,
        IProductQueryService productQuery,
        IPageRouteHelper pageRouteHelper,
        IOptions<CatalogueApiSettings> catalogueApiSettings,
        ILogger<RelatedProductListBlockViewComponent> logger,
        ISettingsManager settingsManager,
        LanguageService languageService)
    {
        _contentLoader = contentLoader;
        _findClient = client;
        _productQuery = productQuery;
        _pageRouteHelper = pageRouteHelper;
        _catalogueApiSettings = catalogueApiSettings.Value;
        _logger = logger;
        _languageService = languageService;
        _settingsManager = settingsManager;
    }

    protected async override Task<IViewComponentResult> InvokeComponentAsync(RelatedProductListBlock currentContent)
    {
        // This should always be a ProductDetailPage. It is the only page that can contain a ProductSummaryBlock.
        var currentPage = _pageRouteHelper.Page as ProductDetailPage;
        var language = _languageService.GetCurrentLanguage().TwoLetterISOLanguageName;
        if (currentPage == null)
        {
            _logger.LogWarning("ProductSummaryBlockViewComponent used on a page that is not a ProductDetailPage. Current page type: {PageType}", _pageRouteHelper.Page?.GetType().Name);
            return Content(string.Empty);
        }

        // This should always be a ProductLandingPage. ProductDetailPage should always be a child of ProductLandingPage.
        var parentPage = _contentLoader.Get<ProductLandingPage>(currentPage.ParentLink);
        if (parentPage == null)
        {
            _logger.LogWarning("ProductSummaryBlockViewComponent could not load parent ProductLandingPage for ProductDetailPage with ID: {PageId}. ParentLink: {ParentLink}", currentPage.ContentLink.ID, currentPage.ParentLink);
            return Content(string.Empty);
        }

        if (currentPage.ProductId == null)
        {
            _logger.LogWarning("ProductSummaryBlockViewComponent used on a ProductDetailPage with no ProductId. Current page ID: {PageId}", currentPage.ContentLink.ID);
            return Content(string.Empty);
        }

        var selectedCategory = parentPage.ProductCatalogueCategory;

        if (!selectedCategory.HasValue)
        {
            _logger.LogWarning("No Category is configured on the Product Landing Page");
            return Content(string.Empty);
        }
        ProductSku product = null;
        try
        {
            int productid = Convert.ToInt32(currentPage.ProductId);
            var dict = await _productQuery.GetSkusAsync([productid], language);
            dict.TryGetValue(productid, out product);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving product with ID {ProductId} and language {Lang}", currentPage.ProductId, language);
        }

        if (product == null)
        {
            return Content(string.Empty);
        }

        var relatedProducts = new List<ProductSku>();

        var productsDict = await _productQuery.GetSkusAsync(product.RelatedProductIDs, language);
        relatedProducts = productsDict.Values.ToList();


        var productDetailPages = relatedProducts
            .SelectMany(relatedProduct => _contentLoader
                .GetChildren<ProductDetailPage>(parentPage.ContentLink)
                .Where(x => x.ProductId == relatedProduct.Id.ToString())
                .Select(page => new ProductPlanDetailsViewModel
                {
                    ProductId = int.Parse(page.ProductId, CultureInfo.InvariantCulture),
                    PlanDetailsUrl = page.ContentLink
                })
            );

        if (relatedProducts.Count < 0)
        {
            _logger.LogWarning("ProductSummaryBlockViewComponent could not parse ProductId: {ProductId}", currentPage.ProductId);
            return Content(string.Empty);
        }

        var handoffUrl = ContentHelper.GetHandoffUrl(parentPage.HandoffBehavior, language, _catalogueApiSettings);

        var webLayoutSettings = _settingsManager.GetSettings<WebLayoutSettings>();

        var labels = await _productQuery.GetLabelsAsync(language);

        RelatedProductListBlockViewModel model = new(currentContent)
        {
            CurrentPage = currentPage,
            ParentPage = parentPage,
            RelatedProducts = relatedProducts,
            PlanDetailsLinks = productDetailPages,
            HandoffUrl = handoffUrl,
            PlanDetailsText = webLayoutSettings.PlanDetailsText ?? "Plan details",
            ProductButtonText = webLayoutSettings.ProductButtonText ?? "Buy now",
            ViewAllLinkText = webLayoutSettings.ViewAllLinkText ?? "View all",
            Span = currentPage.Span ?? webLayoutSettings.Span,
            DiscountedPriceSpan = currentPage.DiscountedPriceSpan ?? webLayoutSettings.DiscountedPriceSpan,
            VatText = currentPage.VatText ?? webLayoutSettings.VatText,
            Labels = labels.Select(x => new FrontEndLabelInfo()
            {
                AttributeCode = x.AttributeCode,
                DefaultFrontEndLabel = x.DefaultFrontendLabel,
                LabelCultureSpecific = x.FrontEndLabels.SingleOrDefault(y => _productQuery.GetLanguage(y.StoreId).Equals(language))?.Label,
                Language = x.Language
            }).ToList(),
        };

        return View(model);
    }
}
