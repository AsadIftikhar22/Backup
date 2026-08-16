
namespace Salam.Cms.Core.Services.Catalogue;

using EPiServer.Find;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Salam.Cms.Core.Services.Caching;
using Salam.Cms.Core.Settings.Configuration;
using Salam.Cms.Shared.Models.Catalogue.Data;
using Salam.Cms.Shared.Models.Catalogue.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public sealed class ProductQueryService : IProductQueryService
{
    private readonly IClient _client;
    private readonly ICachingService _cache;
    private readonly IHttpContextAccessor _http;
    private readonly ILogger<ProductQueryService> _logger;
    private readonly List<LanguageSettings> _languageSettings;

    public ProductQueryService(IClient client, ICachingService cache, IHttpContextAccessor http, ILogger<ProductQueryService> logger, IOptions<CatalogueApiSettings> catalogueApiSettings)
    {
        _client = client;
        _cache = cache;
        _http = http;
        _logger = logger;
        _languageSettings = catalogueApiSettings.Value.Languages;
    }

    public async Task<IReadOnlyDictionary<int, ProductSku>> GetSkusAsync(IEnumerable<int> ids, string language)
    {
        var idList = ids?.Distinct().ToList() ?? new List<int>();

        if (!idList.Any())
        {
            _logger.LogInformation("ProductQueryService: GetSkusAsync - No product IDs provided, returning empty dictionary");
            return new Dictionary<int, ProductSku>();
        }

        var cacheKey = CacheKeys.CreateProductSkusCacheKey(idList, language);

        var ctx = _http.HttpContext;
        if (ctx != null && ctx.Items.TryGetValue(cacheKey, out var o) && o is IReadOnlyDictionary<int, ProductSku> dictInRequest)
        {
            _logger.LogInformation("ProductQueryService: Cache HIT - Request-level cache for {Count} products with language {Language}",
                dictInRequest.Count, language);
            return dictInRequest;
        }

        // Global synched cache
        var cached = _cache.Get<IReadOnlyDictionary<int, ProductSku>>(cacheKey);
        if (cached != null && cached.Any())
        {

            if (ctx != null)
            {
                ctx.Items[cacheKey] = cached;
                _logger.LogDebug("ProductQueryService: Added global cache result to request-level cache");
            }

            return cached;
        }

        _logger.LogDebug("ProductQueryService: Global cache MISS");
        _logger.LogInformation("ProductQueryService: Cache MISS - Fetching {Count} products from Find with language {Language}",
            idList.Count, language);

        var resultDict = new Dictionary<int, ProductSku>();
        const int batch = 100;
        for (int i = 0; i < idList.Count; i += batch)
        {
            var segment = idList.Skip(i).Take(batch).ToList();
            _logger.LogDebug("ProductQueryService: Fetching batch {BatchNumber} with {Count} product IDs: [{BatchIds}]",
                (i / batch) + 1, segment.Count, string.Join(", ", segment.Take(10)) + (segment.Count > 10 ? "..." : ""));

            var query = _client.Search<ProductSku>()
                               .Filter(x => x.Language.Match(language))
                               .Filter(x => x.Id.In(segment))
                               .StaticallyCacheFor(TimeSpan.FromSeconds(30))
                               .Take(segment.Count);

            var segmentResults = await query.GetResultAsync();


            if (segmentResults.Count() < segment.Count)
            {
                var missingIds = segment.Except(segmentResults.Select(x => x.Id)).ToList();
                _logger.LogWarning("ProductQueryService: {Count} products not found in batch {BatchNumber}: [{MissingIds}]",
                    missingIds.Count, (i / batch) + 1, string.Join(", ", missingIds.Take(10)) + (missingIds.Count > 10 ? "..." : ""));
            }

            foreach (var sku in segmentResults)
            {
                resultDict[sku.Id] = sku;
            }
        }

        _logger.LogDebug("ProductQueryService: Adding {Count} products to cache with key: {CacheKey}", resultDict.Count, cacheKey);
        _cache.Add(resultDict, cacheKey, CacheKeys.MasterKeys.ProductCatalogue);

        if (ctx != null)
        {
            ctx.Items[cacheKey] = resultDict;
            _logger.LogDebug("ProductQueryService: Added Find result to request-level cache");
        }

        return resultDict;
    }

    public async Task<List<AttributeDefinition>> GetLabelsAsync(string language)
    {
        var cacheKey = $"{CacheKeys.LabelsPrefix}_{language}";

        var cached = _cache.Get<List<AttributeDefinition>>(cacheKey);

        if (cached != null)
            return cached;

        var labelsQuery = await _client
            .Search<AttributeDefinition>()
            .Filter(x => x.Language.Match(language))
            .StaticallyCacheFor(TimeSpan.FromSeconds(30))
            .Take(1000)
            .GetResultAsync();

        var labels = labelsQuery.ToList();

        _cache.Add(labels, cacheKey, CacheKeys.MasterKeys.ProductCatalogue);

        return labels;
    }

    public async Task<List<Category>> GetCategoriesAsync(int categoryId, string language)
    {
        //var cacheKey = $"{CacheKeys.CategoriesPrefix}{categoryId}_{language}";

        //var cached = _cache.Get<List<Category>>(cacheKey);

        //if (cached != null)
         //   return cached;

        var categoryQuery = await _client
            .Search<Category>()
            .Filter(x => x.Language.Match(language))
            .Filter(x => x.Id.Match(categoryId))
            .StaticallyCacheFor(TimeSpan.FromSeconds(30))
            .GetResultAsync();

        var categories = categoryQuery.ToList();

        //_cache.Add(categories, cacheKey, CacheKeys.MasterKeys.ProductCategoryCatalogue);

        return categories;
    }

    public string GetLanguage(int storeId)
    {
        return _languageSettings.SingleOrDefault(x => x.StoreId == storeId)?.LanguageCode;
    }
}