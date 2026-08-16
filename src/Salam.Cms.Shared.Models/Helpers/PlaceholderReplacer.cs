namespace Salam.Cms.Shared.Models.Helpers;

using System.Text.RegularExpressions;

public sealed class PlaceholderReplacer : IPlaceholderReplacer
{
    private const string AsteriskRegex = @"\*([^*]+)\*";
    private const string PercentageRegex = @"\%([^%]+)\%";
    private const string AsteriskCssClass = "text--bright-blue-2";
    private const string PercentageCssClass = "text--bright-pink";

    public string ReplacePlaceholders(string? textToCheck, PlaceholderType placeholderType)
    {
        var source = textToCheck ?? string.Empty;

        return placeholderType switch
        {
            PlaceholderType.Asterisk => ApplyReplacements(source, AsteriskRegex, AsteriskCssClass),
            PlaceholderType.Percentage => ApplyReplacements(source, PercentageRegex, PercentageCssClass),
            _ => source,
        };
    }

    private static string ApplyReplacements(string inputValue, string pattern, string cssClass)
    {
        var regex = new Regex(pattern, RegexOptions.None, TimeSpan.FromMilliseconds(100));
        if (!regex.IsMatch(inputValue))
        {
            return inputValue;
        }

        return regex.Replace(inputValue, delegate (Match match)
        {
            var matchedString = match.ToString();

            var middle = matchedString.Substring(1, matchedString.Length - 2);

            return $"<span class=\"{cssClass}\">{middle}</span>";
        });
    }
}