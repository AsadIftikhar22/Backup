using EPiServer.Core;

namespace Salam.Cms.Web.Features.Common.Interfaces;

/// <summary>
/// Defines properties required for a page type to support the Page Navigator feature.
/// </summary>
public interface IPageNavigatorEnabled : IContentData
{
    /// <summary>
    /// The main content area of the page where navigable blocks reside.
    /// </summary>
    ContentArea? MainContent { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the page navigator should be displayed for this page.
    /// </summary>
    bool EnablePageNavigator { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the page navigator should be displayed for this page.
    /// </summary>
    bool EnableCategoryNavigator { get; set; }

}
