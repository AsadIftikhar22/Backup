namespace Salam.Cms.Core.Services.Caching;

using EPiServer.DataAbstraction;
using EPiServer.Framework.Cache;
using EPiServer.Web;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;

/// <summary>
/// This implementation wraps up <see cref="ISynchronizedObjectInstanceCache"/>,
/// which is an Optimizely solution that handles caching across load balanced
/// environments like DXP.
/// 
/// <para>
/// <strong>Master Keys Usage:</strong><br/>
/// Master keys allow you to group related cache entries and clear them all at once.
/// This is the recommended approach instead of iterating over cache keys by prefix.
/// </para>
/// 
/// <example>
/// <code>
/// // Add items with master keys for grouped invalidation
/// _cachingService.Add(navigationData, "nav_en", CacheKeys.MasterKeys.Navigation);
/// _cachingService.Add(footerData, "footer_en", CacheKeys.MasterKeys.SiteContent);
/// _cachingService.Add(productData, "product_123", CacheKeys.MasterKeys.ProductCatalogue);
/// 
/// // Clear all navigation-related cache entries at once
/// _cachingService.RemoveByMasterKey(CacheKeys.MasterKeys.Navigation);
/// 
/// // Clear all product catalogue entries at once
/// _cachingService.RemoveByMasterKey(CacheKeys.MasterKeys.ProductCatalogue);
/// </code>
/// </example>
/// </summary>
/// <inheritdoc cref="ICachingService"/>
public sealed class CachingService : ICachingService
{
    private readonly ISynchronizedObjectInstanceCache _cache;
    private readonly ILanguageBranchRepository _languageBranchRepository;
    private readonly IContextModeResolver _contextModeResolver;
    private readonly ILogger<CachingService> _logger;

    private const string CacheInvalidationSetting = "CustomSettings:CacheInvalidationTimeout";
    private readonly int _cacheDuration;

    public CachingService(
        ISynchronizedObjectInstanceCache cache,
        ILanguageBranchRepository languageBranchRepository,
        IContextModeResolver contextModeResolver,
        IConfiguration configuration,
        ILogger<CachingService> logger)
    {
        _cache = cache;
        _languageBranchRepository = languageBranchRepository;
        _contextModeResolver = contextModeResolver;
        _logger = logger;

        var configuredDuration = configuration.GetValue<string>(CacheInvalidationSetting);
        _cacheDuration = int.TryParse(configuredDuration, out var cacheDurationTime) ? cacheDurationTime : 15;
    }

    public void Add<T>(T? objectToCache, string? cacheKey)
        where T : class
    {
        var defaultPolicy = new CacheEvictionPolicy(
            TimeSpan.FromMinutes(_cacheDuration),
            CacheTimeoutType.Absolute
        );
        Add(objectToCache, cacheKey, defaultPolicy);
    }

    public void Add<T>(T? objectToCache, string? cacheKey, CacheEvictionPolicy cacheEvictionPolicy)
        where T : class
    {
        try
        {
            // Only cache when there is a valid key and object
            if (string.IsNullOrWhiteSpace(cacheKey) || objectToCache == null)
            {
                return;
            }

            // Do not cache if we are in Edit or Preview mode
            if (_contextModeResolver.CurrentMode is ContextMode.Edit or ContextMode.Preview)
            {
                return;
            }

            _cache.Insert(cacheKey, objectToCache, cacheEvictionPolicy);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Failed to add object to cache for a cache key of '{CacheKey}'", cacheKey);
        }
    }

    public void Add<T>(T? objectToCache, string? cacheKey, string? masterKey, TimeSpan? timeSpan = null, CacheTimeoutType? cacheTimeoutType = null)
        where T : class
    {
        try
        {
            // Only cache when there is a valid key and object
            if (string.IsNullOrWhiteSpace(cacheKey) || objectToCache == null)
            {
                return;
            }

            // Do not cache if we are in Edit or Preview mode
            if (_contextModeResolver.CurrentMode is ContextMode.Edit or ContextMode.Preview)
            {
                return;
            }

            var cacheEvictionPolicy = new CacheEvictionPolicy(
                timeSpan ?? TimeSpan.FromMinutes(_cacheDuration),
                cacheTimeoutType ?? CacheTimeoutType.Absolute,
                cacheKeys: null, // No regular cache dependencies
                masterKeys: string.IsNullOrWhiteSpace(masterKey) ? null : new[] { masterKey }
            );

            _cache.Insert(cacheKey, objectToCache, cacheEvictionPolicy);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Failed to add object to cache for a cache key of '{CacheKey}' with master key '{MasterKey}'", cacheKey, masterKey);
        }
    }

    public T? Get<T>(string? cacheKey)
        where T : class
    {
        try
        {
            // Do not access cache if we are in Edit or Preview mode
            if (_contextModeResolver.CurrentMode is ContextMode.Edit or ContextMode.Preview)
            {
                return null;
            }

            return _cache.TryGet(cacheKey, ReadStrategy.Wait, out T result) ? result : default;
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Failed to retrieve an object from cache for a cache key of '{CacheKey}'", cacheKey);
            return default;
        }
    }

    public void Remove(string? cacheKey)
    {
        if (string.IsNullOrWhiteSpace(cacheKey))
        {
            return;
        }

        try
        {
            _cache.Remove(cacheKey);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Failed to remove object from the cache for a cache key of '{CacheKey}'", cacheKey);
        }
    }

    public void RemoveByMasterKey(string? masterKey)
    {
        if (string.IsNullOrWhiteSpace(masterKey))
        {
            return;
        }

        try
        {
            // Removing the master key will automatically remove all cache entries that depend on it
            _cache.Remove(masterKey);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Failed to remove cache entries by master key '{MasterKey}'", masterKey);
        }
    }

    public void RemoveAll()
    {
        try
        {
            // Use master keys to clear all cache groups efficiently
            // This is much more performant than iterating over individual keys
            RemoveByMasterKey(CacheKeys.MasterKeys.SiteContent);
            RemoveByMasterKey(CacheKeys.MasterKeys.Navigation);
            RemoveByMasterKey(CacheKeys.MasterKeys.ProductCatalogue);
            RemoveByMasterKey(CacheKeys.MasterKeys.Media);

            _logger.LogInformation("Successfully cleared all cache using master keys");
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Failed to clear all cache using master keys");
        }
    }
}