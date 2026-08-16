namespace Salam.Cms.Web.Features.Common.Interfaces;

using EPiServer.Core;
using Salam.Cms.Shared.Models.Pages;
using Salam.Cms.Web.Features.Common.Editor;

/// <summary>
/// The minimum properties for every visitable page.
/// Used in combination with <see cref="SitePageDataUIDescriptor"/> .
/// This allows us to simplify which pages are allowed in main content areas.
/// </summary>
public interface ISitePageData : ISharedPageData
{
    /// <summary>
    /// Gets the Hero Content Area.
    /// Client specific implementation.
    /// </summary>
    ContentArea? HeroArea { get; }

    /// <summary>
    /// Gets the Icon for the page.
    /// Client specific implementation.
    /// </summary> 
    ContentReference? Icon { get; }

    /// <summary>
    /// Gets the fall back Heading for the page.
    /// Client specific implementation.
    /// </summary>
    string? Heading { get; }

    /// <summary>
    /// Gets the Short Name for the page.
    /// Client specific implementation.
    /// </summary>
    string? ShortPageName { get; }

    /// <summary>
    /// Gets the Support Content Area.
    /// Client specific implementation.
    /// </summary>
    ContentArea? SupportContactContent { get; }

    bool HideSupportContactContent { get; }
}