namespace Salam.Cms.Core.Services.Catalogue;

using EPiServer.Find;
using EPiServer.Find.Api;
using Flurl.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Salam.Cms.Core.Services.Caching;
using Salam.Cms.Core.Settings.Configuration;
using Salam.Cms.Shared.Models.Catalogue.Data;
using Salam.Cms.Shared.Models.Catalogue.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class CatalogueApiService : ICatalogueApiService
{
    private readonly ILogger<CatalogueApiService> _logger;
    private readonly IClient _findClient;
    private readonly string _baseUrl;
    private readonly string _baseUrlForMedia;
    private readonly ICachingService _cache;

    const int RetryCount = 20;

    public CatalogueApiService(ILogger<CatalogueApiService> logger, IClient findClient, IOptions<CatalogueApiSettings> apiSettings, ICachingService cache)
    {
        _logger = logger;
        _findClient = findClient;
        _baseUrl = apiSettings.Value.BaseUrl;
        _baseUrlForMedia = apiSettings.Value.BaseUrlForMedia;
        _cache = cache;
    }

    public async Task<List<T>> FetchAndIndexDataAsync<T>(string apiUrl, string languageCode, string languageStore) where T : class
    {
        // Determine product type name for filtering/deletion logic
        string typeNameForFilter = typeof(T) == typeof(Fiber) ? "Fiber" : typeof(T).Name;

        // Grab currently indexed IDs for this type/language
        List<int> existingSkuIdsInIndex;
        try
        {
            var result = await _findClient
                .Search<ProductSku>()
                .Filter(x => x.ProductType.MatchCaseInsensitive(typeNameForFilter))
                .Filter(x => x.Language.Match(languageCode))
                .Select(x => x.Id)
                .Take(1000)
                .GetResultAsync();

            existingSkuIdsInIndex = result.ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching existing ProductSku IDs for type {Type} lang {Lang}", typeNameForFilter, languageCode);
            existingSkuIdsInIndex = new List<int>();
        }

        var currentSourceProductIds = new HashSet<int>();

        var apiUrlFinal = string.Format(apiUrl, languageStore);

        var requestUrl = _baseUrl + apiUrlFinal;

        ApiResponse apiResponse;
        try
        {
            apiResponse = await requestUrl
                .WithTimeout(TimeSpan.FromSeconds(90))
                .WithHeader("Accept", "application/json")
                .WithHeader("Content-Type", "application/json")
                .AllowAnyHttpStatus()
                .PostAsync()
                .ReceiveJson<ApiResponse>();
        }
        catch (FlurlHttpTimeoutException ex)
        {
            _logger.LogError(ex, "API request to {Url} timed out after 90 seconds", requestUrl);
            throw;
        }
        catch (FlurlHttpException ex)
        {
            _logger.LogError(ex, "HTTP error {Status} from {Url}", ex.StatusCode, requestUrl);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error calling {Url}", requestUrl);
            throw;
        }

        if (apiResponse == null || apiResponse?.Items == null || apiResponse?.Items.Count == 0)
        {
            _logger.LogWarning("API response from {RequestUrl} is null or empty!", requestUrl);
            return new List<T>();
        }

        // Index generic T items if still required (business decision)
        var indexedItems = apiResponse.Items.Select(item => Activator.CreateInstance(typeof(T), item, languageCode) as T).ToList();
        await EnsureIndexedAsync(indexedItems);

        // Build ProductSku list for items with robust error handling
        var productSkus = new List<ProductSku>();
        foreach (var srcItem in apiResponse.Items)
        {
            try
            {
                var sku = new ProductSku(srcItem, typeNameForFilter, _baseUrlForMedia, languageCode);
                productSkus.Add(sku);
                currentSourceProductIds.Add(sku.Id);
            }
            catch (Exception mapEx)
            {
                _logger.LogError(mapEx, "Failed to map source item {ItemId} to ProductSku (type {Type} lang {Lang}). Skipping.", srcItem?.Id, typeNameForFilter, languageCode);
            }
        }

        if (productSkus.Any())
            await EnsureIndexedAsync(productSkus);

        // Handle Fiber pricing type SKUs and track their IDs as part of the same type
        await IndexFiberPricingTypesAsync(typeNameForFilter, languageCode, productSkus, currentSourceProductIds);

        // Delete stale IDs
        try
        {
            var idsToDelete = existingSkuIdsInIndex.Except(currentSourceProductIds).ToList();
            if (idsToDelete.Any())
            {
                _logger.LogInformation("{Count} stale ProductSku(s) detected for type {Type} lang {Lang}. Deleting...", idsToDelete.Count, typeNameForFilter, languageCode);

                int batchSize = 100;
                //for (int i = 0; i < idsToDelete.Count; i += batchSize)
                //{
                //    var batch = idsToDelete.Skip(i).Take(batchSize).ToList();
                //    if (!batch.Any()) continue;
                //    try
                //    {
                //        await _findClient.DeleteAsync<ProductSku>(x => x.Id.In(batch));
                //    }
                //    catch (Exception dex)
                //    {
                //        _logger.LogError(dex, "Error deleting stale ProductSku batch (type {Type} lang {Lang}).", typeNameForFilter, languageCode);
                //    }
                //}
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error while deleting stale products for type {Type} lang {Lang}", typeNameForFilter, languageCode);
        }

        var attributes = apiResponse.Attributes.Items.Select(a =>
        {
            a.LanguageRouting = new LanguageRouting(languageCode);
            a.Language = languageCode;
            return a;
        })
            .ToList();
        await EnsureIndexedAsync(attributes);

        var categories = apiResponse.Categories;
        categories.LanguageRouting = new LanguageRouting(languageCode);
        categories.Language = languageCode;

        await EnsureIndexedAsync(categories);

        if (categories.ChildrenData.Any())
        {
            var categories1stLevelToIndex = categories.ChildrenData
                .Select(a =>
                {
                    a.LanguageRouting = new LanguageRouting(languageCode);
                    a.Language = languageCode;
                    return a;
                })
                .ToList();
            await EnsureIndexedAsync<Category>(categories1stLevelToIndex);

            foreach (var category in categories.ChildrenData)
            {
                if (category.ChildrenData.Any())
                {
                    var categories2ndLevelToIndex = category.ChildrenData
                        .Select(a =>
                        {
                            a.LanguageRouting = new LanguageRouting(languageCode);
                            a.Language = languageCode;
                            return a;
                        })
                        .ToList();
                    await EnsureIndexedAsync<Category>(categories2ndLevelToIndex);

                    foreach (var subCategory in category.ChildrenData)
                    {
                        if (subCategory.ChildrenData.Any())
                        {
                            var categories3rdLevelToIndex = subCategory.ChildrenData
                                .Select(a =>
                                {
                                    a.LanguageRouting = new LanguageRouting(languageCode);
                                    a.Language = languageCode;
                                    return a;
                                })
                                .ToList();
                            await EnsureIndexedAsync<Category>(categories3rdLevelToIndex);
                        }
                    }
                }
            }
        }

        // Evict caches for product skus using master key
        _cache.RemoveByMasterKey(CacheKeys.MasterKeys.ProductCatalogue);
        return indexedItems;
    }

    public async Task<List<AddOn>> FetchAndIndexAddOnsAsync(string endpoint, string languageCode, string languageStore)
    {
        var endpointFinal = string.Format(endpoint, languageStore);
        var requestUrl = _baseUrl + endpointFinal;

        ApiResponse apiResponse;
        try
        {
            apiResponse = await requestUrl
                .WithTimeout(TimeSpan.FromSeconds(90))  // Longer timeout
                .WithHeader("Accept", "application/json")
                .WithHeader("Content-Type", "application/json")
                .AllowAnyHttpStatus()
                .PostAsync()
                .ReceiveJson<ApiResponse>();
        }
        catch (FlurlHttpTimeoutException ex)
        {
            _logger.LogError(ex, "API request to {Url} timed out after 90 seconds", requestUrl);
            throw;
        }
        catch (FlurlHttpException ex)
        {
            _logger.LogError(ex, "HTTP error {Status} from {Url}", ex.StatusCode, requestUrl);
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error calling {Url}", requestUrl);
            throw;
        }

        if (apiResponse?.Items == null || !apiResponse.Items.Any()) return new List<AddOn>();

        var indexedItems = apiResponse.Items
            .SelectMany(item =>
            {
                var addOns = item.GetAttribute("AddOns")?.GetAddOns() ?? new List<AddOn>();

                foreach (var addOn in addOns)
                {
                    addOn.Language = languageCode;
                    addOn.Id = $"{item.Id}_{addOn.RecordId}";
                    addOn.CategoryIds = item.ExtensionAttributes?.CategoryLinks.Select(x => int.Parse(x.CategoryId)).ToList();
                    addOn.Name = item.Name;
                    addOn.Sku = $"{item.Sku}_{addOn.RecordId}";
                    addOn.ProductId = item.Id;
                }

                return addOns;
            })
            .ToList();

        await IndexProductItemsAsync(indexedItems, typeof(AddOn).Name, languageCode); //Most add-ons
        await IndexProductItemsAsync(apiResponse, typeof(AddOn).Name, languageCode); //Some idd bundles don't sync unless we also add this
        await EnsureIndexedAsync(indexedItems);

        return indexedItems;
    }

    private async Task IndexProductItemsAsync(List<AddOn> addOns, string typeName, string languageCode)
    {
        var productItems = new List<ProductSku>();

        foreach (var addOn in addOns)
        {
            try
            {
                productItems.Add(new ProductSku(addOn, typeName, languageCode));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to map AddOn to ProductSku. RecordId: {RecordId}", addOn?.RecordId);
            }
        }

        await EnsureIndexedAsync(addOns, productItems);
    }

    private async Task IndexProductItemsAsync(ApiResponse apiResponse, string typeName, string languageCode)
    {
        var productItems = new List<ProductSku>();

        foreach (var item in apiResponse.Items)
        {
            try
            {
                productItems.Add(new ProductSku(item, typeName, _baseUrlForMedia, languageCode));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to map AddOn to ProductSku. Id: {Id}", item?.Id);
            }
        }

        await EnsureIndexedAsync(productItems);
    }

    private async Task EnsureIndexedAsync<T>(List<T> items)
    {
        var retries = 0;

        if (items.Any())
        {
            var result = await _findClient.IndexAsync(items);

            if (result.Items.All(x => x.Ok))
                return;

            while (result.Items.Any(x => !x.Ok) && retries <= RetryCount)
            {
                await Task.Delay(1000);

                result = await _findClient.IndexAsync(items);
                retries++;
            }
        }
    }

    private async Task EnsureIndexedAsync(List<AddOn> addOns, List<ProductSku> productItems)
    {
        var retries = 0;

        if (productItems.Any())
        {
            var result = await _findClient.IndexAsync(productItems);

            if (result.Items.All(x => x.Ok))
                return;

            while (result.Items.All(x => !x.Ok) && retries <= RetryCount)
            {
                await Task.Delay(1000);

                result = await _findClient.IndexAsync(productItems);
                retries++;
            }
        }
    }

    private async Task IndexFiberPricingTypesAsync(string typeName, string languageCode, List<ProductSku> productItems, HashSet<int> idCollector)
    {
        var fiberPricingTypes = productItems
            .Where(x => x.PricingType != null && x.PricingType.Any())
            .SelectMany(item =>
            {
                var pricingTypes = item.PricingType;

                foreach (var pricingType in pricingTypes)
                {
                    pricingType.Id = $"{item.Name}_{pricingType.RecordId}";
                    pricingType.CategoryIds = item.CategoryIds;
                    pricingType.Name = item.Name;
                    pricingType.Language = languageCode;
                    pricingType.ProductId = item.Id;
                    pricingType.Sku = $"{item.Sku}_{pricingType.RecordId}";
                    pricingType.DownloadSpeed = item?.DownloadSpeed;
                    pricingType.UploadSpeed = item?.UploadSpeed;
                    pricingType.CorrelatedId = item?.CorrelatedId;
                    pricingType.extra_month_Free = item?.extra_month_Free;
                    pricingType.free_router = item.free_router;
                    pricingType.InstallationFee = item?.InstallationFee;
                }

                return pricingTypes;
            });

        if (!fiberPricingTypes.Any())
            return;

        var fiberPricingTypeProductSkus = fiberPricingTypes.Select(x => new ProductSku(x, typeName, languageCode)).ToList();

        foreach (var sku in fiberPricingTypeProductSkus)
            idCollector?.Add(sku.Id);

        await EnsureIndexedAsync(fiberPricingTypeProductSkus);
    }

    private async Task EnsureIndexedAsync(List<ProductSku> productSkus)
    {
        var result = await _findClient.IndexAsync(productSkus);

        var retries = 0;

        if (result.Items.All(x => x.Ok))
            return;

        var unindexedItems = productSkus.Where(x => result.Items.Any(y => !y.Ok && y.Id == x.Id.ToString()));

        while (unindexedItems.Any() && retries <= RetryCount)
        {
            await Task.Delay(1000);

            result = await _findClient.IndexAsync(unindexedItems);
            unindexedItems = productSkus.Where(x => result.Items.Any(y => !y.Ok && y.Id == x.Id.ToString()));
            retries++;
        }
    }

    private async Task EnsureIndexedAsync(Category? categories)
    {
        var result = await _findClient.IndexAsync(categories);

        var retries = 0;

        if (result.Ok)
            return;

        while (!result.Ok && retries <= RetryCount)
        {
            await Task.Delay(1000);

            result = await _findClient.IndexAsync(categories);
            retries++;
        }
    }
}
