namespace Salam.Cms.Shared.Models.Helpers;
public interface IPlaceholderReplacer
{
    string ReplacePlaceholders(string? textToCheck, PlaceholderType placeholderType);
}