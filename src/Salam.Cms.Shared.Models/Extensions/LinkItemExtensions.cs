namespace Salam.Cms.Shared.Models.Extensions;

using EPiServer.SpecializedProperties;
using System.Diagnostics.CodeAnalysis;

public static class LinkItemExtensions
{
    public static bool IsValid([NotNullWhen(true)] this LinkItem? linkItem)
    {
        return linkItem != null &&
               !string.IsNullOrWhiteSpace(linkItem.Href) &&
               !string.IsNullOrWhiteSpace(linkItem.Text);
    }
}
