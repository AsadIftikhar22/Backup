using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Salam.Cms.Web.Features.Common.Helpers;

namespace Salam.Cms.Web.Features.Common.Extensions;

/// <summary>
/// Provides extension methods for IHtmlHelper specific to common web features.
/// </summary>
public static class HtmlHelperExtensions
{
    /// <summary>
    /// Generates the 'id' attribute for page navigator anchors if applicable,
    /// based on the provided navigation title string.
    /// </summary>
    /// <typeparam name="TModel">The type of the model (not directly used but required for extension method context).</typeparam>
    /// <param name="helper">The HTML helper instance.</param>
    /// <param name="navigationTitle">The string to use for generating the anchor ID. Can be null or empty.</param>
    /// <returns>An HtmlString containing 'id="generated-id"' or an empty HtmlString.</returns>
    public static IHtmlContent PageNavigatorAnchorAttribute<TModel>(
        this IHtmlHelper<TModel> helper,
        string? navigationTitle) // Made navigationTitle nullable
    {
        // Ensure the provided title is not null or empty
        if (string.IsNullOrEmpty(navigationTitle))
        {
            return HtmlString.Empty;
        }

        // Generate the ID using the helper
        string anchorId = AnchorIdHelper.Generate(navigationTitle);

        if (!string.IsNullOrEmpty(anchorId))
        {
            // Return the id attribute directly
            return new HtmlString($"id=\"{anchorId}\"");
        }

        return HtmlString.Empty;
    }
}
