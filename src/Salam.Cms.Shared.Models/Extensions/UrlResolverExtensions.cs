namespace Salam.Cms.Shared.Models.Extensions;

using EPiServer;
using EPiServer.Core;
using EPiServer.Web.Internal;
using EPiServer.Web.Routing;
using Microsoft.Extensions.Primitives;
using Salam.Cms.Shared.Models.Media;
using System.Web;

public static class UrlResolverExtensions
{
    /// <summary>
    /// Resolves an URL using routing for a <see cref="T:EPiServer.Url" />
    /// </summary>
    /// <param name="urlResolver">The URL Resolver instance that this method extends</param>
    /// <param name="url">The url to resolve</param>
    /// <returns>The URL or an empty string</returns>
    public static string ContentUrl(this IUrlResolver urlResolver, Url? url)
    {
        if (url.IsNullOrEmpty())
        {
            return string.Empty;
        }

        return url.IsAbsoluteUri ? url.ToString() : UrlEncoder.Encode(urlResolver.GetUrl(url.ToString()) ?? url.ToString());
    }

    /// <summary>
    /// Resolves an URL using routing for a string using a <see cref="T:EPiServer.Url" />
    /// </summary>
    /// <param name="urlResolver">The URL Resolver instance that this method extends</param>
    /// <param name="url">The url to resolve</param>
    /// <returns>The URL or an empty string</returns>
    public static string ContentUrl(this IUrlResolver urlResolver, string? url)
    {
        return string.IsNullOrWhiteSpace(url) ? string.Empty : urlResolver.ContentUrl(new Url(url));
    }

    /// <summary>
    /// Resolves a URL using routing for <paramref name="contentReference"/>.
    /// <paramref name="queryValue"/> is only appended is both <paramref name="queryKey"/> and <paramref name="queryValue"/> are set.
    /// </summary>
    /// <param name="urlResolver">The URL Resolver instance that this method extends.</param>
    /// <param name="contentReference">The content reference for the page.</param>
    /// <param name="queryKey">The query string name.</param>
    /// <param name="queryValue">The query string value.</param>
    /// <returns></returns>
    public static string ContentUrlWithQuery(
    this IUrlResolver urlResolver,
    ContentReference? contentReference,
    string queryKey,
    object queryValue,
    Dictionary<string, StringValues>? existingParameters = null)
    {
        if (contentReference.IsNullOrEmpty())
        {
            return string.Empty;
        }

        var queryStringValue = queryValue switch
        {
            int integerValue => integerValue.ToString(),
            string stringValue => HttpUtility.UrlEncode(stringValue),
            ContentReference contentValue => contentValue.ID.ToString(),
            _ => string.Empty,
        };

        var url = urlResolver.GetUrl(contentReference);
        var urlBuilder = new UrlBuilder(url);

        if (existingParameters != null)
        {
            foreach (var key in existingParameters.Keys)
            {
                foreach (var value in existingParameters[key])
                {
                    urlBuilder.TryAddQuery(key, value);
                }
            }
        }

        if (!string.IsNullOrWhiteSpace(queryKey) && !string.IsNullOrWhiteSpace(queryStringValue))
        {
            urlBuilder.TryAddQuery(queryKey, queryStringValue);
        }

        return urlBuilder.ToString();
    }

    /// <summary>
    /// Builds an image url with size and quality requirements.
    /// If both width & height are provided, then a resize mode of crop will be used.
    /// If quality is not provided, then a default of 90% will be used.
    /// </summary>
    /// <param name="urlResolver">The URL Resolver instance that this method extends.</param>
    /// <param name="image">The image content to be add a resized url to.</param>
    /// <param name="width">An optional width constraint</param>
    /// <param name="height">An optional height constraint</param>
    /// <param name="quality">An optional quality constraint. Defaults to 90.</param>
    /// <returns></returns>
    public static string ImageUrl(
        this IUrlResolver urlResolver,
        IImageContent? image,
        int? width = null,
        int? height = null,
        int? quality = 90)
    {
        var resizeMode = width.HasValue && height.HasValue ? "crop" : null;

        switch (image)
        {
            case ImageContent imageContent:
                var urlBuilder = new UrlBuilder(urlResolver.GetUrl(imageContent.ContentLink));
                urlBuilder.TryAddQuery("width", width);
                urlBuilder.TryAddQuery("height", height);
                urlBuilder.TryAddQuery("quality", quality);
                urlBuilder.TryAddQuery("rxy", imageContent.ImageFocalPoint?.Replace('|', ','));
                urlBuilder.TryAddQuery("rmode", resizeMode);

                return urlBuilder.ToString();

            case VectorImageContent vectorImageContent:
                return urlResolver.GetUrl(vectorImageContent.ContentLink);

            default:
                return string.Empty;
        }
    }
}