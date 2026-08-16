namespace Salam.Cms.Core.Services.Images;
using Ganss.Xss;
using Microsoft.AspNetCore.Html;
using Salam.Cms.Core.Services.Caching;
using Salam.Cms.Shared.Models.Common.Components;
using Salam.Cms.Shared.Models.Media;
using System.Threading.Tasks;

public class ImageUtilityService : IImageUtilityService
{
    readonly IHtmlSanitizer _htmlSanitizer;
    readonly IBlobOperations _blobOperations;
    readonly ICachingService _cachingService;

    public ImageUtilityService(IHtmlSanitizer htmlSanitizer, IBlobOperations blobOperations, ICachingService cachingService)
    {
        _htmlSanitizer = htmlSanitizer;
        _blobOperations = blobOperations;
        _cachingService = cachingService;
    }

    public async Task<HtmlString> ConvertImageToRawContentAsync(VectorImageContent vectorImageContent)
    {
        try
        {
            // Add caching using ICachingService and CacheKeys.SvgContent
            if (vectorImageContent == null || vectorImageContent.BinaryData == null)
            {
                return new HtmlString(string.Empty);
            }

            // Generate a cache key using the SvgContent prefix and the blob's unique ID
            string cacheKey = $"{CacheKeys.SvgContent}:{vectorImageContent.BinaryData.ID}";

            // Try to get from cache
            var cachedSvg = _cachingService.Get<string>(cacheKey);
            if (!string.IsNullOrEmpty(cachedSvg))
            {
                return new HtmlString(cachedSvg);
            }

            var fileInfo = await _blobOperations.AsFileInfoAsync(vectorImageContent.BinaryData);

            string svgContent;

            if (fileInfo == null || !fileInfo.Exists)
            {
                return new HtmlString(string.Empty);
            }

            using (var stream = fileInfo.CreateReadStream())
            using (var reader = new StreamReader(stream))
            {
                svgContent = await reader.ReadToEndAsync();
            }

            // Sanitizer is already configured in constructor - no need to modify collections here
            var sanitizedSvg = _htmlSanitizer.Sanitize(svgContent);
            _cachingService.Add(sanitizedSvg, cacheKey, CacheKeys.MasterKeys.Media);

            return new HtmlString(sanitizedSvg);
        }
        catch (DirectoryNotFoundException)
        {
            return new HtmlString(string.Empty);
        }
    }
}
