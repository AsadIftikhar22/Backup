namespace Salam.Cms.Shared.Models.Extensions;

using EPiServer;
using EPiServer.Core;
using EPiServer.Web;
using EPiServer.Web.Mvc.Html;
using Microsoft.AspNetCore.Mvc;
using Salam.Cms.Shared.Models.Extensions;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

public static class UrlExtensions
{
    /// <summary>
    /// Determines if a <see cref="Url"/> is null or empty.
    /// </summary>
    /// <param name="url">The <see cref="Url"/> to validate.</param>
    /// <returns>true or false</returns>
    public static bool IsNullOrEmpty([NotNullWhen(false)] this Url? url)
    {
        return url == null || url.IsEmpty();
    }

    public static string AbsoluteUrl(this IUrlHelper urlHelper, ContentReference? contentReference)
    {
        return contentReference.IsNullOrEmpty() ? string.Empty : urlHelper.AbsoluteUrl(urlHelper.ContentUrl(contentReference));
    }

    public static string AbsoluteUrl(this IUrlHelper urlHelper, string relativeUrl)
    {
        if (string.IsNullOrWhiteSpace(relativeUrl))
        {
            return string.Empty;
        }

        if (Uri.TryCreate(relativeUrl, UriKind.RelativeOrAbsolute, out var uri) && !uri.IsAbsoluteUri)
        {
            var url = urlHelper.ContentUrl(relativeUrl);
            var httpContext = urlHelper.ActionContext?.HttpContext;
            var currentCulture = CultureInfo.CurrentUICulture.Name;

            var hostDefinition = SiteDefinition.Current?.Hosts
                .Where(x => x.Type == HostDefinitionType.Primary)
                .FirstOrDefault(x => x.Language?.Name.Equals(currentCulture, StringComparison.OrdinalIgnoreCase) == true)
                ?? SiteDefinition.Current?.Hosts.Where(x => x.Type == HostDefinitionType.Primary).FirstOrDefault();

            if (httpContext != null && hostDefinition != null)
            {
                var uriBuilder = new UriBuilder(
                    hostDefinition.UseSecureConnection == true ? "https" : "http",
                    hostDefinition.Authority.Hostname,
                    hostDefinition.Authority.Port,
                    url);

                return uriBuilder.Uri.ToString();
            }
        }

        return relativeUrl;
    }
}