namespace Salam.Cms.Shared.Models.Extensions;

using EPiServer;
using System.Web;

public static class UrlBuilderExtensions
{
    /// <summary>
    /// Conditionally add a value to the <see cref="UrlBuilder"/> if both the key and the value have been defined.
    /// </summary>
    /// <param name="urlBuilder"></param>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public static void TryAddQuery(this UrlBuilder urlBuilder, string? key, object? value)
    {
        var stringValue = value?.ToString();
        if (!string.IsNullOrWhiteSpace(stringValue) && !string.IsNullOrWhiteSpace(key))
        {
            urlBuilder.QueryCollection[key] = HttpUtility.UrlEncode(stringValue);
        }
    }
}