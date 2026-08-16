namespace Salam.Cms.Shared.Models.Extensions;

using EPiServer.SpecializedProperties;
using System.Diagnostics.CodeAnalysis;

public static class LinkItemCollectionExtensions
{
    /// <summary>
    /// Determines if a <see cref="LinkItemCollection"/> is null or empty.
    /// </summary>
    /// <param name="linkItemCollection">The <see cref="LinkItemCollection"/> to validate.</param>
    /// <returns>true or false</returns>
    public static bool IsNullOrEmpty([NotNullWhen(false)] this LinkItemCollection? linkItemCollection)
    {
        return linkItemCollection == null || linkItemCollection.Count == 0;
    }
}