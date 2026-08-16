namespace Salam.Cms.Shared.Models.Pages;

using EPiServer.Core;

/// <summary>
/// The minimum properties for every visitable page.
/// Used in combination with <see cref="SharedPageDataUIDescriptor"/> .
/// This allows us to simplify which pages are allowed in main content areas.
/// </summary>
public interface ISharedPageData : IContent
{
    /// <summary>
    /// Gets the page name.
    /// Provided by Optimizely framework.
    /// </summary>
    string? PageName { get; }

    /// <summary>
    /// Gets the date the content was published.
    /// Provided by Optimizely framework.
    /// </summary>
    DateTime? StartPublish { get; }

    /// <summary>
    /// Gets the date the content was modified.
    /// Provided by Optimizely framework.
    /// </summary>
    DateTime Changed { get; }

    /// <summary>
    /// Gets the date the content will be expired.
    /// Provided by Optimizely framework.
    /// </summary>
    DateTime? StopPublish { get; }
}