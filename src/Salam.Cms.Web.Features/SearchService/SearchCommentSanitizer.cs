namespace Salam.Cms.Web.Features.SearchCommentSanitizer;

using System.Text.RegularExpressions;

public static class SearchCommentSanitizer
{
    public static string RemoveHtmlComments(string input, string searchText)
    {
        if (string.IsNullOrWhiteSpace(input))
            return input;

        var cleaned = Regex.Replace(
            input,
            @"<!--[\s\S]*?-->",
            string.Empty,
            RegexOptions.Singleline
        ).Trim();

        // if comment removal makes it irrelevant → return empty
        if (string.IsNullOrWhiteSpace(cleaned))
            return string.Empty;

        // optional safety: remove broken comment start
        cleaned = Regex.Replace(cleaned, @"<!--[\s\S]*$", string.Empty).Trim();

        // FINAL CHECK: must still contain search text after cleanup
        if (!string.IsNullOrWhiteSpace(searchText) &&
            cleaned.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) < 0)
        {
            return string.Empty;
        }

        return cleaned;
    }
}