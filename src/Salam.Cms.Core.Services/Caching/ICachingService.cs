namespace Salam.Cms.Core.Services.Caching;

using EPiServer.Framework.Cache;

/// <summary>
/// A service that wraps the up caching within the CMS solution.
/// </summary>
public interface ICachingService
{
    /// <summary>
    /// Attempts to add an object to the cache with default settings.
    /// </summary>
    /// <typeparam name="T">The type of the object to be cached</typeparam>
    /// <param name="objectToCache">The object to cache</param>
    /// <param name="cacheKey">The cache key</param>
    void Add<T>(T? objectToCache, string? cacheKey) where T : class;

    /// <summary>
    /// Attempts to add an object to the cache with a custom cache eviction policy.
    /// </summary>
    /// <typeparam name="T">The type of the object to be cached</typeparam>
    /// <param name="objectToCache">The object to cache</param>
    /// <param name="cacheKey">The cache key</param>
    /// <param name="cacheEvictionPolicy">The cache eviction policy</param>
    void Add<T>(T? objectToCache, string? cacheKey, CacheEvictionPolicy cacheEvictionPolicy) where T : class;

    /// <summary>
    /// Attempts to add an object to the cache with master key support for grouped cache invalidation.
    /// </summary>
    /// <typeparam name="T">The type of the object to be cached</typeparam>
    /// <param name="objectToCache">The object to cache</param>
    /// <param name="cacheKey">The cache key</param>
    /// <param name="masterKey">The master key for grouping cache entries</param>
    /// <param name="timeSpan">The time span for the cache (optional, defaults to configured duration)</param>
    /// <param name="cacheTimeoutType">The cache timeout type (optional, defaults to Absolute)</param>
    void Add<T>(T? objectToCache, string? cacheKey, string? masterKey, TimeSpan? timeSpan = null, CacheTimeoutType? cacheTimeoutType = null) where T : class;

    /// <summary>
    /// Attempts to retrieve an object from the cache.
    /// Returns a null if the object cannot be retrieved.
    /// </summary>
    /// <typeparam name="T">The type of the object to be retrieved from the cache</typeparam>
    /// <param name="cacheKey">The cache key</param>
    /// <returns>The cached object or a null</returns>
    T? Get<T>(string? cacheKey) where T : class;

    /// <summary>
    /// Attempts to remove an object from the cache.
    /// </summary>
    /// <param name="cacheKey">The cache key</param>
    void Remove(string? cacheKey);

    /// <summary>
    /// Removes all cache entries associated with the specified master key.
    /// This is the recommended way to clear groups of related cache entries.
    /// </summary>
    /// <param name="masterKey">The master key to remove</param>
    void RemoveByMasterKey(string? masterKey);

    /// <summary>
    /// Attempts to remove all known cache objects.
    /// </summary>
    void RemoveAll();
}