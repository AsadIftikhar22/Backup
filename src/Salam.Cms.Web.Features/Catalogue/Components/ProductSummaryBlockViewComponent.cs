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
using Salam.Cms.Shared.Models.Catalogue.Enums;
using Salam.Cms.Shared.Models.Catalogue.Models;
using Salam.Cms.Web.Features.Catalogue.Models;
using Salam.Cms.Web.Features.Catalogue.ViewModels;
using Salam.Cms.Web.Features.Common.Helpers;
using Salam.Cms.Web.Features.LanguageSwitcher.Services;
using Salam.Cms.Web.Features.Settings.Models;
using System.Globalization;

public class ProductSummaryBlockViewComponent : AsyncBlockComponent<ProductSummaryBlock>
{
    private readonly IContentLoader _contentLoader;
    private readonly IClient _findClient;
    private readonly IProductQueryService _productQuery;
    private readonly IPageRouteHelper _pageRouteHelper;
    private readonly CatalogueApiSettings _catalogueApiSettings;
    private readonly ILogger<ProductSummaryBlockViewComponent> _logger;
    private readonly LanguageService _languageService;
    private readonly ISettingsManager _settingsManager;

    public ProductSummaryBlockViewComponent(
        IContentLoader contentLoader,
        IClient findClient,
        IProductQueryService productQuery,
        IPageRouteHelper pageRouteHelper,
        IOptions<CatalogueApiSettings> catalogueApiSettings,
        ILogger<ProductSummaryBlockViewComponent> logger,
        ISettingsManager settingsManager,
        LanguageService languageService)
    {
        _contentLoader = contentLoader;
        _findClient = findClient;
        _productQuery = productQuery;
        _pageRouteHelper = pageRouteHelper;
        _catalogueApiSettings = catalogueApiSettings.Value;
        _logger = logger;
        _languageService = languageService;
        _settingsManager = settingsManager;
    }

    protected async override Task<IViewComponentResult> InvokeComponentAsync(ProductSummaryBlock currentContent)
    {
        // This should always be a ProductDetailPage. It is the only page that can contain a ProductSummaryBlock.
        var currentPage = _pageRouteHelper.Page as ProductDetailPage;
        var language = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;

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
            // We can still proceed as the view might handle a null ParentPage.
        }
        //Find the Product Landing Page to fetch the buynow url for Visitors
        var productId = Int32.Parse(currentPage.ProductId, CultureInfo.InvariantCulture);
        ProductSku product = null;
        try
        {
            var dict = await _productQuery.GetSkusAsync([productId], language);
            dict.TryGetValue(productId, out product);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving product with ID {ProductId} and language {Lang}", productId, language);
        }

        if (product == null)
        {
            return Content(string.Empty);
        }

        var handoffUrl = ContentHelper.GetHandoffUrl(parentPage?.HandoffBehavior ?? HandoffOption.None, language, _catalogueApiSettings);

        var webLayoutSettings = _settingsManager.GetSettings<WebLayoutSettings>();

        var attributeDefinitions = await _productQuery.GetLabelsAsync(language);

        var category = await _productQuery.GetCategoriesAsync(product.CategoryIds.First(), language);

        if (category == null)
        {
            return Content("No product catalogue category is selected on this ProductLandingPage.");
        }

        var categoryName = category.FirstOrDefault()?.Name;

        if (webLayoutSettings.CategoryTranslations != null && webLayoutSettings.CategoryTranslations.Any())
        {
            categoryName = webLayoutSettings.CategoryTranslations.SingleOrDefault(x => x.Key.Trim().Equals(categoryName.Trim(), StringComparison.OrdinalIgnoreCase))?.Value ?? category.FirstOrDefault()?.Name;
        }

        var productTypes = parentPage?.ProductSummaryVisibleFields
            .Select(item =>
            {
                var parts = ContentHelper.SplitExtensionForVisibleFields(item);
                return new
                {
                    ProductType = parts[0],
                    VisibleField = parts.Length > 1 ? parts[1] : string.Empty
                };
            })
            .GroupBy(x => x.ProductType);

        var productType = productTypes?
            .Where(type => type.Key.Equals(product?.ProductType))
            .First();

        var visibleFields = productType.Select(x => x.VisibleField).ToList();

        var numberOfFields = visibleFields
            .Where(x => ContentHelper.ShouldRenderVisibleField(x, product).ShouldRender)
            .Count();

        var labels = attributeDefinitions.Select(x => new FrontEndLabelInfo()
        {
            AttributeCode = x.AttributeCode,
            DefaultFrontEndLabel = x.DefaultFrontendLabel,
            LabelCultureSpecific = x.FrontEndLabels.SingleOrDefault(y => _productQuery.GetLanguage(y.StoreId).Equals(language))?.Label,
            Language = x.Language
        }).ToList();

        product.Labels = labels;
        product.BuyButtonRedirection = parentPage?.BuyButtonRedirection ?? string.Empty;
        ProductSummaryBlockViewModel model = new(currentContent)
        {
            CurrentPage = currentPage,
            ParentPage = parentPage,
            Product = product,
            HandoffUrl = handoffUrl,
            FooterText = currentPage?.SocialTxtUpdate
                                 ?? webLayoutSettings?.ProductFooterText
                                 ?? string.Empty,
            ProductButtonText = webLayoutSettings?.ProductButtonText ?? "Buy now",
            BackLinkText = webLayoutSettings?.BackLinkText ?? "Back",
            Name = currentContent?.BatchCategory ?? categoryName,
            Span = currentPage.Span ?? webLayoutSettings.Span,
            DiscountedPriceSpan = currentPage.DiscountedPriceSpan ?? webLayoutSettings.DiscountedPriceSpan,
            VatText = currentPage.VatText ?? webLayoutSettings.VatText,
            NumberOfFields = numberOfFields,
            Labels = labels,
            IsPlatformCardVisible = currentContent.IsPlatformCardVisible,
            SpanWhereValidityIsNull = parentPage.SpanWhereValidityIsNull
        };

        return View(model);
    }
}
