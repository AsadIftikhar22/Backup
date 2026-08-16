namespace Salam.Cms.Web.Features.Common.Components.Images.PictureRenderer;

using EPiServer;
using EPiServer.Cms.WelcomeIntegration.Core;
using EPiServer.Cms.WelcomeIntegration.UI;
using EPiServer.Core;
using global::PictureRenderer;
using global::PictureRenderer.Profiles;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Optimizely.Cmp.Client;
using Salam.Cms.Core.Services.Caching;
using Salam.Cms.Core.Services.Images;
using Salam.Cms.Shared.Models.Media;
using Salam.Cms.Web.Features.Common.Components.Images;
using System.Globalization;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

public class PictureRendererViewModelBuilder : IPictureRendererViewModelBuilder
{
    private readonly IContentLoader _contentLoader;
    private readonly IImageUtilityService _imageUtilityService;
    private readonly IDAMAssetIdentityResolver _damAssetIdentityResolver;
    private readonly IDAMAssetMetadataService _damAssetMetadataService;
    private readonly ICmpClient _cmpClient;
    private readonly ILogger<PictureRendererViewModelBuilder> _logger;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ICachingService _cachingService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public PictureRendererViewModelBuilder(
        IContentLoader contentLoader,
        IImageUtilityService imageUtilityService,
        IDAMAssetIdentityResolver damAssetIdentityResolver,
        IDAMAssetMetadataService damAssetMetadataService,
        ICmpClient cmpClient,
        ILogger<PictureRendererViewModelBuilder> logger,
        IHttpClientFactory httpClientFactory,
        ICachingService cachingService,
        IHttpContextAccessor httpContextAccessor
    )
    {
        _contentLoader = contentLoader;
        _imageUtilityService = imageUtilityService;
        _damAssetIdentityResolver = damAssetIdentityResolver;
        _damAssetMetadataService = damAssetMetadataService;
        _cmpClient = cmpClient;
        _logger = logger;
        _httpClientFactory = httpClientFactory;
        _cachingService = cachingService;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<ImageViewModel?> Build(ContentReference imageReference, PictureProfileBase? pictureProfile = null, PictureAttributes? attributes = null, SvgRenderMode svgRenderMode = SvgRenderMode.ImageSrc)
    {
        var stopwatch = System.Diagnostics.Stopwatch.StartNew();

        var model = new ImageViewModel
        {
            ImageReference = imageReference,
            SvgRenderMode = svgRenderMode,
            PictureProfile = pictureProfile
        };

        try
        {
            // DAM detection
            var damDetectionStart = stopwatch.ElapsedMilliseconds;
            var damAssetIdentity = _damAssetIdentityResolver.Get(imageReference);
            _logger.LogDebug("DAM detection took {ElapsedMs}ms for image {ImageRef}",
                stopwatch.ElapsedMilliseconds - damDetectionStart, imageReference);

            if (damAssetIdentity != null && damAssetIdentity.Metadata != null)
            {
                var damProcessingStart = stopwatch.ElapsedMilliseconds;
                await PopulateFromDamAssetAsync(model, damAssetIdentity, svgRenderMode, pictureProfile, attributes);
                _logger.LogDebug("DAM processing took {ElapsedMs}ms for image {ImageRef}",
                    stopwatch.ElapsedMilliseconds - damProcessingStart, imageReference);
            }
            else
            {
                var cmsProcessingStart = stopwatch.ElapsedMilliseconds;
                await PopulateFromCmsAssetAsync(model, imageReference, svgRenderMode, pictureProfile, attributes);
                _logger.LogDebug("CMS processing took {ElapsedMs}ms for image {ImageRef}",
                    stopwatch.ElapsedMilliseconds - cmsProcessingStart, imageReference);
            }

            _logger.LogDebug("Total image processing took {ElapsedMs}ms for image {ImageRef}",
                stopwatch.ElapsedMilliseconds, imageReference);

            // Add Server-Timing header for performance debugging
            if (_httpContextAccessor.HttpContext?.Response != null)
            {
                var serverTimingValue = $"image-processing;desc=\"Image Processing\";dur={stopwatch.ElapsedMilliseconds}";
                _httpContextAccessor.HttpContext.Response.Headers.Append("Server-Timing", serverTimingValue);
            }

            return model;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error building image view model for {ImageRef} after {ElapsedMs}ms",
                imageReference, stopwatch.ElapsedMilliseconds);
            return null;
        }
    }

    private async Task PopulateFromDamAssetAsync(ImageViewModel model, DAMAssetIdentity damIdentity, SvgRenderMode svgRenderMode, PictureProfileBase? profile, PictureAttributes? attributes)
    {
        var assetGuid = GetAssetGuid(damIdentity);
        if (!assetGuid.HasValue)
        {
            _logger.LogError("Could not determine Asset GUID for DAM asset {DamAssetUri}", damIdentity.DAMAssetUri);
            return;
        }

        // Cache key for DAM metadata
        // Build cache key for DAM metadata
        var cacheKey = $"{CacheKeys.DamMetadata}_{assetGuid.Value}_{CultureInfo.CurrentCulture.Name}";

        // Try to get metadata from cache
        var damMetaData = _cachingService.Get<DamAssetInfo>(cacheKey);

        if (damMetaData == null)
        {
            _logger.LogDebug("Cache miss for DAM metadata {AssetGuid}, fetching from service", assetGuid.Value);

            damMetaData = await _damAssetMetadataService.GetAssetMetadata(assetGuid.Value);

            if (damMetaData == null)
            {
                _logger.LogError("Failed to retrieve DAM metadata for asset GUID {AssetGuid}", assetGuid.Value);
                return;
            }

            _cachingService.Add(damMetaData, cacheKey, CacheKeys.MasterKeys.DamMetadata);
        }

        // For DAM images, we need to create a base URL that PictureRenderer can work with
        // The PictureRenderer will append width parameters, so we'll use a custom approach
        model.DamImageUrl = CreateDamCompatibleUrl(damMetaData.Url);

        if (string.IsNullOrEmpty(model.DamImageUrl))
        {
            _logger.LogWarning("Public URL for DAM asset GUID {AssetGuid} is empty.", assetGuid.Value);
        }
        // Alt Text Resolution for DAM
        model.AltText = damMetaData.AltText;
        if (string.IsNullOrWhiteSpace(model.AltText))
        {
            model.AltText = damMetaData.Title;
        }
        if (string.IsNullOrWhiteSpace(model.AltText))
        {
            _logger.LogWarning("DAM Asset {AssetGuid} has no AltText or Title in its metadata.", assetGuid.Value);
            model.AltText = string.Empty;
        }
        model.ImageContent = null;
        if (damMetaData.MimeType?.Contains("svg", StringComparison.OrdinalIgnoreCase) == true)
        {
            model.IsVectorImage = true;
            model.SvgRenderMode = svgRenderMode;
            if (svgRenderMode == SvgRenderMode.Inline && !string.IsNullOrEmpty(model.DamImageUrl))
            {
                // Fetch SVG content from DAM URL
                try
                {
                    var client = _httpClientFactory.CreateClient();
                    client.Timeout = TimeSpan.FromSeconds(10); // Set reasonable timeout
                    var svgContent = await client.GetStringAsync(model.DamImageUrl);
                    // Optionally sanitize SVG here
                    model.RawVectorImageContent = new Microsoft.AspNetCore.Html.HtmlString(svgContent);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to fetch inline SVG from DAM URL: {Url}", model.DamImageUrl);
                }
            }
        }
        else
        {
            model.IsVectorImage = false;
        }
        ApplyProfileSettingsToModel(model, profile, attributes);
    }

    /// <summary>
    /// Creates a DAM-compatible URL that works with PictureRenderer
    /// This ensures proper query parameter formatting for Optimizely DAM
    /// </summary>
    private string CreateDamCompatibleUrl(string originalDamUrl)
    {
        // We are relying on pre-configured renditions from DAM.
        // The URL from DAM (damMetaData.Url) should be used as-is as it points to a specific rendition.
        // No further transformation or cleaning by this method is needed.
        return originalDamUrl;
    }

    private async Task PopulateFromCmsAssetAsync(ImageViewModel model, ContentReference cmsAssetRef, SvgRenderMode svgRenderMode, PictureProfileBase? profile, PictureAttributes? attributes)
    {
        if (!_contentLoader.TryGet(cmsAssetRef, out IContent? content) || content is not IImageContent imageContent)
        {
            _logger.LogError("Could not load IImageContent for CMS asset {ContentLink}", cmsAssetRef);
            return;
        }
        model.ImageContent = imageContent;
        model.DamImageUrl = null;
        // Alt Text Resolution for CMS
        string? cmsAltText = null;
        // Try to get 'AlternateText' property via reflection
        var altProp = imageContent.GetType().GetProperty("AlternateText", BindingFlags.Public | BindingFlags.Instance);
        if (altProp != null)
        {
            var value = altProp.GetValue(imageContent) as string;
            if (!string.IsNullOrWhiteSpace(value))
            {
                cmsAltText = value;
            }
        }
        if (string.IsNullOrWhiteSpace(cmsAltText))
        {
            cmsAltText = content.Name;
            _logger.LogInformation("CMS Asset {ContentLink} is using its Name as fallback AltText.", cmsAssetRef);
        }
        model.AltText = cmsAltText ?? string.Empty;
        if (imageContent is VectorImageContent vectorImage)
        {
            model.IsVectorImage = true;
            model.SvgRenderMode = svgRenderMode;
            if (svgRenderMode == SvgRenderMode.Inline)
            {
                var svgContent = await _imageUtilityService.ConvertImageToRawContentAsync(vectorImage);
                model.RawVectorImageContent = svgContent;
            }
        }
        else
        {
            model.IsVectorImage = false;
        }
        ApplyProfileSettingsToModel(model, profile, attributes);
    }

    private void ApplyProfileSettingsToModel(ImageViewModel model, PictureProfileBase? profile, PictureAttributes? attributes)
    {
        model.PictureProfile = profile;
        model.LazyLoading = LazyLoading.Browser;
        model.ImgFetchPriority = FetchPriority.Auto;
        if (profile != null && attributes != null)
        {
            model.ImgFetchPriority = attributes.ImgFetchPriority;
            model.LazyLoading = attributes.LazyLoading;
        }
    }

    // Helper to extract asset GUID from DAMAssetIdentity (based on Optimizely docs)
    private static Guid? GetAssetGuid(DAMAssetIdentity assetIdentity)
    {
        // Try to parse as direct GUID (rendition)
        if (Guid.TryParse(assetIdentity.DAMAssetUri.Segments.Last(), out var guid))
            return guid;
        // Try to parse as base64-encoded image GUID
        try
        {
            var assetId = assetIdentity.DAMAssetUri.Segments.Last();
            var decodedId = Encoding.UTF8.GetString(Convert.FromBase64String(assetId)).Split('=')[1];
            if (Guid.TryParse(decodedId, out var imageGuid))
                return imageGuid;
        }
        catch { }
        return null;
    }
}
