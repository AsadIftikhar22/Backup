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
using Salam.Cms.Shared.Models.Extensions;
using Salam.Cms.Web.Features.Catalogue.Models;
using Salam.Cms.Web.Features.Catalogue.ViewModels;
using Salam.Cms.Web.Features.Common.Helpers;
using Salam.Cms.Web.Features.LanguageSwitcher.Services;
using Salam.Cms.Web.Features.Settings.Models;
using System.Collections.Generic;
using System.Globalization;

public sealed class ProductSelectorBlockViewComponent : AsyncBlockComponent<ProductSelectorBlock>
{
    private readonly IContentLoader _contentLoader;
    private readonly IClient _findClient;
    private readonly IProductQueryService _productQuery;
    private readonly IPageRouteHelper _pageRouteHelper;
    private readonly CatalogueApiSettings _catalogueApiSettings;
    private readonly LanguageService _languageService;
    private readonly ISettingsManager _settingsManager;
    private readonly ILogger<ProductSelectorBlockViewComponent> _logger;

    public ProductSelectorBlockViewComponent(
        IContentLoader contentLoader,
        IClient client,
        IProductQueryService productQuery,
        IPageRouteHelper pageRouteHelper,
        IOptions<CatalogueApiSettings> catalogueApiSettings,
        ISettingsManager settingsManager,
        LanguageService languageService,
        ILogger<ProductSelectorBlockViewComponent> logger)
    {
        _contentLoader = contentLoader;
        _findClient = client;
        _productQuery = productQuery;
        _pageRouteHelper = pageRouteHelper;
        _catalogueApiSettings = catalogueApiSettings.Value;
        _languageService = languageService;
        _settingsManager = settingsManager;
        _logger = logger;
    }

    protected async override Task<IViewComponentResult> InvokeComponentAsync(ProductSelectorBlock currentContent)
    {
        var currentPage = _pageRouteHelper.Page as ProductLandingPage;
        var language = _languageService.GetCurrentLanguage().TwoLetterISOLanguageName;

        if (currentPage == null)
        {
            return Content("This block can be used only inside ProductLandingPage.");
        }

        var selectedCategory = currentPage.ProductCatalogueCategory;

        if (selectedCategory == null || !selectedCategory.HasValue)
        {
            return Content("No product catalogue category is selected on this ProductLandingPage.");
        }

        var webLayoutSettings = _settingsManager.GetSettings<WebLayoutSettings>();

        var categories = await _productQuery.GetCategoriesAsync(selectedCategory.GetValueOrDefault(), language);

        var category = categories.SingleOrDefault();

        if (category == null)
        {
            return Content("No product catalogue category is selected on this ProductLandingPage.");
        }

        if (webLayoutSettings.CategoryTranslations != null && webLayoutSettings.CategoryTranslations.Any())
        {
            category.Name = webLayoutSettings.CategoryTranslations.SingleOrDefault(x => x.Key.Trim().Equals(category.Name.Trim(), StringComparison.OrdinalIgnoreCase))?.Value ?? category.Name;

            if (category.ChildrenData.Any())
            {
                foreach (var child in category.ChildrenData)
                {
                    child.Name = webLayoutSettings.CategoryTranslations.SingleOrDefault(x => x.Key.Trim().Equals(child.Name.Trim(), StringComparison.OrdinalIgnoreCase))?.Value ?? child.Name;
                }
            }
        }

        var handoffUrl = ContentHelper.GetHandoffUrl(currentPage.HandoffBehavior, language, _catalogueApiSettings);

        var productDetailPagesQuery = _contentLoader.GetChildren<ProductDetailPage>(currentPage?.ContentLink);
        var productDetailPages = productDetailPagesQuery
            .Where(x => !string.IsNullOrEmpty(x.ProductId))
            .Select(page => new ProductPlanDetailsViewModel()
            {
                ProductId = int.Parse(page.ProductId, CultureInfo.InvariantCulture),
                PlanDetailsUrl = page.ContentLink
            })
            .ToList();

        var footerText = currentPage?.FooterText ?? webLayoutSettings.ProductFooterText;

        var visibleFields = currentPage.ProductSelectorVisibleFieldsNew.IsNullOrEmpty() ? currentPage.ProductSelectorVisibleFields : currentPage.ProductSelectorVisibleFieldsNew;

        if (currentPage.OverrideProductList != null && currentPage.OverrideProductList.Items.Any())
        {
            var overrideProducts = new List<ProductBlock>();

            foreach (var item in currentPage.OverrideProductList.FilteredItems)
            {
                if (_contentLoader.TryGet(item.ContentLink, out ProductBlock product))
                {
                    overrideProducts.Add(product);
                }
            }

            overrideProducts = overrideProducts.OrderBy(x => x.Price).ToList();

            var overrideModel = new ProductSelectorBlockViewModel(currentContent)
            {
                CurrentPage = currentPage,
                Title = currentPage.ProductSelectorTitle,
                Description = currentPage.ProductSelectorDescription,
                NavigationTitle = currentContent.NavigationTitle,
                OverrideProducts = overrideProducts,
                AreTabsDisabled = currentPage.AreTabsDisabled,
                IsFooterDisabled = currentPage.IsFooterDisabled,
                ShowFlags = !string.IsNullOrEmpty(currentPage.FooterText),
                ProductCatalogueCategory = category,
                PlanDetailsText = webLayoutSettings.PlanDetailsText,
                ProductButtonText = webLayoutSettings.ProductButtonText,
                PlanDetailsLinks = productDetailPages,
                HandoffUrl = handoffUrl,
                VisibleFields = visibleFields,
                VisibleFieldsFeatured = currentPage.ProductSelectorVisibleFieldsFeatured,
                FooterText = footerText,
                DiscountedPriceSpan = currentPage.DiscountedPriceSpan ?? webLayoutSettings.DiscountedPriceSpan,
                Span = currentPage.Span ?? webLayoutSettings.Span,
                SpanWhereValidityIsNull = currentPage.SpanWhereValidityIsNull,
                VatText = currentPage.VatText ?? webLayoutSettings.VatText,
                BadgeText = currentPage.BadgeText,
                BuyButtonRedirection = currentPage.BuyButtonRedirection,
                BuyNowStaticURL = currentContent?.BuyNowStaticURL,
                IsMultipleTabNavigation = currentPage.IsMultipleTabNavigation
            };

            return View(overrideModel);
        }
        else if (currentPage.DynamicProducts != null && currentPage.DynamicProducts.Any())
        {
            var labels = await _productQuery.GetLabelsAsync(language);

            var productsDict = await _productQuery.GetSkusAsync(currentPage.DynamicProducts, language);

            var products = productsDict
                .Select(x => x.Value)
                .OrderBy(x => x.Price)
                .ToList();

            ProductSelectorBlockViewModel model = new(currentContent)
            {
                Title = currentPage.ProductSelectorTitle,
                Description = currentPage.ProductSelectorDescription,
                VisibleFields = visibleFields,
                VisibleFieldsFeatured = currentPage.ProductSelectorVisibleFieldsFeatured,
                NavigationTitle = currentContent.NavigationTitle,
                ProductCatalogueCategory = category,
                Products = products,
                PlanDetailsLinks = productDetailPages,
                HandoffUrl = handoffUrl,
                AreTabsDisabled = currentPage.AreTabsDisabled,
                IsFooterDisabled = currentPage.IsFooterDisabled,
                ShowFlags = !string.IsNullOrEmpty(currentPage.FooterText),
                FooterText = footerText,
                PlanDetailsText = webLayoutSettings.PlanDetailsText,
                ProductButtonText = webLayoutSettings.ProductButtonText,
                Labels = labels.Select(x => new FrontEndLabelInfo()
                {
                    AttributeCode = x.AttributeCode,
                    DefaultFrontEndLabel = x.DefaultFrontendLabel,
                    LabelCultureSpecific = x.FrontEndLabels.SingleOrDefault(y => _productQuery.GetLanguage(y.StoreId).Equals(language))?.Label,
                    Language = x.Language
                }).ToList(),
                Span = currentPage.Span ?? webLayoutSettings.Span,
                DiscountedPriceSpan = currentPage.DiscountedPriceSpan ?? webLayoutSettings.DiscountedPriceSpan,
                SpanWhereValidityIsNull = currentPage.SpanWhereValidityIsNull,
                VatText = currentPage.VatText ?? webLayoutSettings.VatText,
                BadgeText = currentPage.BadgeText,
                DataText = currentPage.DataText,
                CallLabel = currentPage.CallLabel,
                CallAmountText = currentPage.CallAmountText,
                BuyButtonRedirection = currentPage.BuyButtonRedirection,
                BuyNowStaticURL = currentContent?.BuyNowStaticURL,
                IsMultipleTabNavigation = currentPage.IsMultipleTabNavigation
            };

            return View(model);
        }
        else
        {
            var labels = await _productQuery.GetLabelsAsync(language);

            var productsDict = await _productQuery.GetSkusAsync(productDetailPages.Select(x => x.ProductId), language);

            var products = productsDict
                .Select(x => x.Value)
                .OrderBy(x => x.Price)
                .ToList();

            ProductSelectorBlockViewModel model = new(currentContent)
            {
                Title = currentPage.ProductSelectorTitle,
                Description = currentPage.ProductSelectorDescription,
                VisibleFields = visibleFields,
                VisibleFieldsFeatured = currentPage.ProductSelectorVisibleFieldsFeatured,
                NavigationTitle = currentContent.NavigationTitle,
                ProductCatalogueCategory = category,
                Products = products,
                PlanDetailsLinks = productDetailPages,
                HandoffUrl = handoffUrl,
                AreTabsDisabled = currentPage.AreTabsDisabled,
                IsFooterDisabled = currentPage.IsFooterDisabled,
                ShowFlags = !string.IsNullOrEmpty(currentPage.FooterText),
                FooterText = footerText,
                PlanDetailsText = webLayoutSettings.PlanDetailsText,
                BuyNowStaticURL = currentContent?.BuyNowStaticURL,
                ProductButtonText = webLayoutSettings.ProductButtonText,
                Labels = labels.Select(x => new FrontEndLabelInfo()
                {
                    AttributeCode = x.AttributeCode,
                    DefaultFrontEndLabel = x.DefaultFrontendLabel,
                    LabelCultureSpecific = x.FrontEndLabels.SingleOrDefault(y => _productQuery.GetLanguage(y.StoreId).Equals(language))?.Label,
                    Language = x.Language
                }).ToList(),
                Span = currentPage.Span ?? webLayoutSettings.Span,
                DiscountedPriceSpan = currentPage.DiscountedPriceSpan ?? webLayoutSettings.Span,
                SpanWhereValidityIsNull = currentPage.SpanWhereValidityIsNull,
                VatText = currentPage.VatText ?? webLayoutSettings.VatText,
                BadgeText = currentPage.BadgeText,
                DataText = currentPage.DataText,
                CallLabel = currentPage.CallLabel,
                CallAmountText = currentPage.CallAmountText,
                BuyButtonRedirection = currentPage.BuyButtonRedirection,
                IsMultipleTabNavigation=currentPage.IsMultipleTabNavigation
            };

            return View(model);
        }
    }
}