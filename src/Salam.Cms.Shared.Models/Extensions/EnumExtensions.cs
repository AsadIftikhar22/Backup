namespace Salam.Cms.Shared.Models.Extensions;

using Humanizer;

public static class EnumExtensions
{
    public static string ToDescription(this Enum enumValue)
    {
        return enumValue.Humanize().Transform(To.TitleCase) ?? string.Empty;
    }
}