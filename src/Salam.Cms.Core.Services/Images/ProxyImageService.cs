namespace Salam.Cms.Web.Infrastructure.Services;

using EPiServer.Framework.Cache;
using Microsoft.Extensions.Options;
using Salam.Cms.Core.Services.Caching;
using Salam.Cms.Core.Services.Images;
using Salam.Cms.Core.Settings.Configuration;
using Salam.Cms.Shared.Models.Images;
using System;
using System.Net.Http;
using System.Threading.Tasks;





/// <summary>
/// Service to safely proxy images from HTTP sources to serve over HTTPS
/// </summary>
public sealed class ProxyImageService : IProxyImageService
{
    private readonly IHttpClientFactory _httpFactory;
    private readonly ICachingService _cachingService;
    private readonly ImageProxySettings _options;

    /// <summary>
    /// Initializes a new instance of the <see cref="ProxyImageService"/>
    /// </summary>
    public ProxyImageService(
        IHttpClientFactory httpFactory,
        ICachingService cachingService,
        IOptions<ImageProxySettings> options)
    {
        _httpFactory = httpFactory;
        _cachingService = cachingService;
        _options = options.Value;
    }

    /// <inheritdoc/>
    public async Task<ProxyImageResult> FetchAsync(string url)
    {
        // Validate the URL - must be absolute, on an allowed host, and using HTTP
        if (!Uri.TryCreate(url, UriKind.Absolute, out var uri) ||
            !_options.AllowedHosts.Contains(uri.Host, StringComparer.OrdinalIgnoreCase) ||
            !uri.Scheme.Equals("http", StringComparison.OrdinalIgnoreCase))
        {
            return ProxyImageResult.Invalid;
        }

        // Check if the image is already cached
        var cachedImage = _cachingService.Get<ProxyImageResult>(url);

        if (cachedImage != null)
        {
            return cachedImage with { FromCache = true };
        }

        // Fetch the image from the source
        var client = _httpFactory.CreateClient("image-proxy");

        try
        {
            using var resp = await client.GetAsync(uri, HttpCompletionOption.ResponseHeadersRead);

            // Ensure it's a successful response and it contains an image
            if (!resp.IsSuccessStatusCode ||
                resp.Content.Headers.ContentType == null ||
                !resp.Content.Headers.ContentType.MediaType!.StartsWith("image/", StringComparison.OrdinalIgnoreCase))
            {
                return ProxyImageResult.Failed;
            }

            // Read the image data
            var bytes = await resp.Content.ReadAsByteArrayAsync();
            var result = new ProxyImageResult(bytes, resp.Content.Headers.ContentType.ToString());

            // Set cache expiration policy
            var cacheEvictionPolicy = new CacheEvictionPolicy(TimeSpan.FromSeconds(_options.CacheDurationSeconds), CacheTimeoutType.Absolute);

            // Cache the result
            _cachingService.Add(result, url, cacheEvictionPolicy);

            return result;
        }
        catch (Exception)
        {
            return ProxyImageResult.Failed;
        }
    }
}