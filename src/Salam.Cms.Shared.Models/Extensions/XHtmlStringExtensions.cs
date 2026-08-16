namespace Salam.Cms.Shared.Models.Extensions;

using EPiServer.Core;
using Microsoft.AspNetCore.Html;
using System.Diagnostics.CodeAnalysis;

public static class XHtmlStringExtensions
{
    /// <summary>
    /// Determines if a <see cref="XhtmlString"/> is null or empty.
    /// </summary>
    /// <param name="richText">The <see cref="XhtmlString"/> to validate.</param>
    /// <returns>true or false</returns>
    public static bool IsNullOrEmpty([NotNullWhen(false)] this XhtmlString? richText)
    {
        // This wrapper methods adds consistent language and helps intellisense understand object states.
        return richText is null || richText.IsEmpty;
    }

    /// <summary>
    /// Replaces placeholders in the format {{placeholder}} with actual values.
    /// </summary>
    /// <param name="richText">The <see cref="XhtmlString"/> containing placeholders.</param>
    /// <param name="replacements">The dictionary of placeholders and their corresponding replacement values.</param>
    /// <returns>A new <see cref="XhtmlString"/> with replaced values.</returns>
    public static HtmlString ReplacePlaceholders(this XhtmlString? richText, Dictionary<string, string> replacements)
    {
        if (richText == null || richText.IsEmpty)
        {
            return new HtmlString("");
        }

        var resultContent = richText.ToString();

        foreach (var pair in replacements)
        {
            var placeholder = $"{{{{{pair.Key}}}}}";
            resultContent = resultContent.Replace(placeholder, pair.Value);
        }

        return new HtmlString(resultContent);
    }

}