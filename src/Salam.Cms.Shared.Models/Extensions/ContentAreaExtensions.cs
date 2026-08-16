namespace Salam.Cms.Shared.Models.Extensions;

using EPiServer;
using EPiServer.Core;
using System.Diagnostics.CodeAnalysis;

public static class ContentAreaExtensions
{
    /// <summary>
    /// Always returns a collection of <see cref="ContentReference"/> for a <see cref="ContentArea"/>
    /// with respect for the application of visitor groups.
    /// </summary>
    /// <param name="contentArea">The content area to query.</param>
    /// <returns>A collection of Content References.</returns>
    public static IList<ContentReference> GetAllowedReferences(this ContentArea? contentArea)
    {
        // Always prefer the FilteredItems as this allows for personalization.
        // Otherwise fall back to unfiltered items and then an empty list.
        return contentArea?.FilteredItems?.Select(x => x.ContentLink).ToList() ??
               contentArea?.Items?.Select(x => x.ContentLink).ToList() ??
               new List<ContentReference>(0);
    }

    /// <summary>
    /// Determines if a <see cref="ContentArea"/> is null or empty.
    /// </summary>
    /// <param name="contentArea">The <see cref="ContentArea"/> to validate.</param>
    /// <returns>true or false</returns>
    public static bool IsNullOrEmpty([NotNullWhen(false)] this ContentArea? contentArea)
    {
        // This wrapper methods adds consistent language and helps intellisense understand object states.
        return contentArea?.Items == null || contentArea.Items.Count == 0;
    }

    /// <summary>
    /// Fetch content items from a contentArea by type
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="contentArea"></param>
    /// <param name="contentLoader"></param>
    /// <returns></returns>
    public static List<T> GetFilteredItemsOfType<T>(this ContentArea contentArea, IContentLoader contentLoader) where T : IContentData
    {
        var items = new List<T>();

        if (contentArea.IsNullOrEmpty())
        {
            return items;
        }

        foreach (var contentAreaItem in contentArea.FilteredItems)
        {
            IContentData item;
            if (!contentLoader.TryGet(contentAreaItem.ContentLink, out item))
            {
                continue;
            }
            if (item is T)
            {
                items.Add((T)item);
            }
        }

        return items;
    }
}