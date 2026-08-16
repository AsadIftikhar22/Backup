namespace Salam.Cms.Web.Features.Catalogue.Components;

using EPiServer;
using EPiServer.Find;
using EPiServer.Find.Cms;
using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Salam.Cms.Core.Services.Catalogue;
using Salam.Cms.Core.Settings.Configuration;
using Salam.Cms.Shared.Models;
using Salam.Cms.Shared.Models.Catalogue.Enums;
using Salam.Cms.Shared.Models.Catalogue.Extensions;
using Salam.Cms.Shared.Models.Catalogue.Models;
using Salam.Cms.Web.Features.Catalogue.Models;
using Salam.Cms.Web.Features.Catalogue.ViewModels;
using Salam.Cms.Web.Features.Common.Helpers;
using Salam.Cms.Web.Features.LanguageSwitcher.Services;
using System.Collections.Generic;

public sealed class FeaturedProductListBlockViewComponent : AsyncBlockComponent<FeaturedProductListBlock>
{
    private readonly IContentLoader _contentLoader;
    private readonly IClient _findClient;
    private readonly IProductQueryService _productQuery;
    private readonly CatalogueApiSettings _catalogueApiSettings;
    private readonly LanguageService _languageService;

    public FeaturedProductListBlockViewComponent(
        IContentLoader contentLoader,
        IClient client,
        IProductQueryService productQuery,
        IOptions<CatalogueApiSettings> catalogueApiSettings,
        LanguageService languageService)
    {
        _contentLoader = contentLoader;
        _findClient = client;
        _productQuery = productQuery;
        _catalogueApiSettings = catalogueApiSettings.Value;
        _languageService = languageService;
    }

    protected async override Task<IViewComponentResult> InvokeComponentAsync(FeaturedProductListBlock currentContent)
    {
        // TODO: Query the products from the Search & Navigation API
        // remaining filtering by product type and query parameter
        var queryOnlyProducts = new List<ProductSku>();
        var products = new List<ProductSku>();
        string productType = currentContent.ProductType.ToString();
        string language = _languageService.GetCurrentLanguage().TwoLetterISOLanguageName;

        if (currentContent.QueryBehaviour == QueryBehaviourOption.ManualOnly
            || currentContent.QueryBehaviour == QueryBehaviourOption.ManualAndQuery)
        {
            var productIds = currentContent.ProductIds ?? new List<string>();
            var intIds = productIds.Select(id => int.TryParse(id, out var n) ? n : 0).Where(n => n != 0).ToList();
            var dict = await _productQuery.GetSkusAsync(intIds, language);
            products = intIds.Select(id => dict.ContainsKey(id) ? dict[id] : null).Where(p => p != null).ToList();
        }

        if (currentContent.QueryBehaviour == QueryBehaviourOption.QueryOnly
            || currentContent.QueryBehaviour == QueryBehaviourOption.ManualAndQuery)
        {
            var queryOnlyProductsFiltering = _findClient
                .Search<ProductSku>()
                .StaticallyCacheFor(TimeSpan.FromSeconds(30))
                .Filter(x => x.Language.Match(language));

            foreach (var queryParam in currentContent.QueryParamaters ?? Enumerable.Empty<QueryParameter>())
            {
                if (queryParam.Key == SalamConstants.CatalogueAPIfields.Name)
                {
                    queryOnlyProductsFiltering = queryOnlyProductsFiltering.FilterByName(queryParam.Value);
                }
                else if (queryParam.Key == SalamConstants.CatalogueAPIfields.Price)
                {
                    List<string> values = queryParam.Value.Split('<').ToList();
                    decimal min = Decimal.Parse(values.First().Trim());
                    decimal max = Decimal.Parse(values.Last().Trim());
                    queryOnlyProductsFiltering = queryOnlyProductsFiltering.FilterByPrice(min, max);
                }
                else if (queryParam.Key == SalamConstants.CatalogueAPIfields.ProductSku)
                {
                    queryOnlyProductsFiltering = queryOnlyProductsFiltering.FilterBySku(queryParam.Value);
                }
            }

            var query = await queryOnlyProductsFiltering
                .FilterByType(productType)
                .Select(x => x.Id)
                .StaticallyCacheFor(TimeSpan.FromSeconds(30))
                .Take(1000)
                .GetResultAsync();

            var queryIds = query.ToList();

            var queryDict = await _productQuery.GetSkusAsync(queryIds, language);
            queryOnlyProducts = queryIds.Select(id => queryDict.ContainsKey(id) ? queryDict[id] : null).Where(p => p != null).ToList();
        }

        var handoffUrl = ContentHelper.GetHandoffUrl(currentContent.HandoffBehavior, language, _catalogueApiSettings);

        FeaturedProductListBlockViewModel model = new(currentContent)
        {
            Products = products.Union(queryOnlyProducts).Take(5).ToList(),
            HandoffUrl = handoffUrl
        };

        return View(model);
    }
}