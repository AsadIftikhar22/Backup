namespace Salam.Cms.Shared.Models.Extensions;

using EPiServer;
using EPiServer.Core;
using EPiServer.ServiceLocation;
using EPiServer.Web.Routing;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Salam.Cms.Shared.Models.Media;
using System.Diagnostics.CodeAnalysis;

public static class ContentReferenceExtensions
{
    /// <summary>
    /// Determines if a <see cref="ContentReference"/> is null or empty.
    /// </summary>
    /// <param name="contentReference">The <see cref="ContentReference"/> to validate.</param>
    /// <returns>true or false</returns>
    public static bool IsNullOrEmpty([NotNullWhen(false)] this ContentReference? contentReference)
    {
        // This wrapper methods adds consistent language and helps intellisense understand object states.
        return ContentReference.IsNullOrEmpty(contentReference);
    }

    /// <summary>
    /// Attempts to retrieve a content item using the <see cref="IContentLoader"/>.
    /// Returns a null if the object is null or cannot be cast onto the requested type.
    /// This extension method is intended for use only in areas where the default DI framework cannot be used.
    /// </summary>
    /// <typeparam name="TContent">Must inherit <see cref="IContent"/></typeparam>
    /// <param name="contentLink">The <see cref="ContentReference"/> for the requested item.</param>
    /// <returns></returns>
    public static TContent? GetContent<TContent>(this ContentReference? contentLink)
        where TContent : IContent
    {
        if (contentLink.IsNullOrEmpty())
        {
            return default;
        }

        var contentLoader = ServiceLocator.Current.GetInstance<IContentLoader>();

        return contentLoader.TryGet<TContent>(contentLink, out var contentItem) ? contentItem : default;
    }

    /// <summary>
    /// Attempts to retrieve the Alt Text for a <see cref="IImageContent"/> if the <paramref name="contentReference"/> relates to an image.
    /// </summary>
    /// <param name="contentReference"></param>
    /// <returns>The Alt Text of an image is one can be loaded or an empty string.</returns>
    public static string GetImageAltText(this ContentReference? contentReference)
    {
        var imageContent = contentReference.GetContent<IImageContent>();

        return string.IsNullOrWhiteSpace(imageContent?.AltText) ? string.Empty : imageContent.AltText;
    }

    public static string ToAbsoluteUrl(this ContentReference contentLink, HttpContext httpContext)
    {
        var urlResolver = httpContext.RequestServices.GetService<UrlResolver>();

        if (urlResolver == null)
        {
            throw new InvalidOperationException("UrlResolver service is not available.");
        }

        var url = urlResolver.GetUrl(contentLink);

        if (!string.IsNullOrEmpty(url) && Uri.TryCreate(url, UriKind.RelativeOrAbsolute, out var uri))
        {
            if (uri.IsAbsoluteUri)
            {
                return url;
            }

            var request = httpContext.Request;
            var absoluteUrl = $"{request.Scheme}://{request.Host}{url}";
            return absoluteUrl;
        }

        return string.Empty;
    }

    public static IEnumerable<TContent> FilterByType<TContent>(this IList<ContentReference> contentReferences) where TContent : IContent
    {
        var contentLoader = ServiceLocator.Current.GetInstance<IContentLoader>();

        return contentReferences
            .Select(contentReference => contentLoader.Get<IContent>(contentReference))
            .OfType<TContent>();
    }
}