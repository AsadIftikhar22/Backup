using System.Text;
using System.Text.RegularExpressions;

namespace Salam.Cms.Web.Features.Common.Helpers;

/// <summary>
/// Helper class to generate URL-friendly anchor IDs.
/// </summary>
public static class AnchorIdHelper
{
    // Regex to match any character that is NOT a lowercase letter, number, or hyphen.
    private static readonly Regex _invalidCharsRegex = new Regex(@"[^\p{L}\p{N}-]", RegexOptions.Compiled | RegexOptions.CultureInvariant);
    // Regex to replace multiple consecutive hyphens with a single hyphen.
    private static readonly Regex _multipleHyphensRegex = new Regex("-{2,}", RegexOptions.Compiled, TimeSpan.FromSeconds(1));

    /// <summary>
    /// Generates a unique and URL-friendly anchor ID from a title and content link.
    /// </summary>
    /// <param name="contentLink">The content link of the item (used for uniqueness).</param>
    /// <param name="title">The title to base the ID on.</param>
    /// <returns>A URL-friendly anchor ID string, or an empty string if generation is not possible.</returns>
    public static string Generate(string title)
    {
        // Check if title is valid
        if (string.IsNullOrWhiteSpace(title))
        {
            // Return empty string if title is missing, so no ID is rendered
            return string.Empty;
        }

        // 1. Convert to lowercase
        var idBuilder = new StringBuilder(title.ToLowerInvariant());

        // 2. Replace spaces with hyphens
        idBuilder.Replace(' ', '-');

        // 3. Remove invalid characters
        string sanitized = _invalidCharsRegex.Replace(idBuilder.ToString(), string.Empty);

        // 4. Replace multiple hyphens with single hyphen
        sanitized = _multipleHyphensRegex.Replace(sanitized, "-");

        // 5. Trim leading/trailing hyphens
        sanitized = sanitized.Trim('-');

        // Ensure the result is not empty after sanitization
        if (string.IsNullOrWhiteSpace(sanitized))
        {
            return string.Empty; // No valid ID can be generated
        }

        // 6. Append _ and ContentLink.ID for uniqueness
        return $"{sanitized}";
    }
}
