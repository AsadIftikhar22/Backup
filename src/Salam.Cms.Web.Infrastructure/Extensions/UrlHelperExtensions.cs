namespace Salam.Cms.Web.Infrastructure.Extensions;

using Microsoft.AspNetCore.Mvc;
using System;

/// <summary>
/// Extensions for IUrlHelper to handle image proxying
/// </summary>
public static class UrlHelperExtensions
{
    /// <summary>
    /// Converts an image URL to a proxied URL if it's an HTTP URL
    /// </summary>
    /// <param name="urlHelper">The URL helper</param>
    /// <param name="originalUrl">The original image URL</param>
    /// <returns>The proxied URL if the original is HTTP, otherwise the original URL</returns>
    public static string ToProxiedImage(this IUrlHelper urlHelper, string? originalUrl)
    {
        if (string.IsNullOrWhiteSpace(originalUrl))
        {
            return string.Empty;
        }

        // Only proxy HTTP URLs, leave HTTPS and relative URLs as they are
        if (originalUrl.StartsWith("http://", StringComparison.OrdinalIgnoreCase))
        {
            return urlHelper.Content($"~/image-proxy?url={Uri.EscapeDataString(originalUrl)}");
        }

        return originalUrl;
    }
}