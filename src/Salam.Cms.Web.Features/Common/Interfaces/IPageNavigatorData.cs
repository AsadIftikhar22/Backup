namespace Salam.Cms.Web.Features.Common.Interfaces;

/// <summary>
/// Defines properties required for a block type to be included as an item in the Page Navigator.
/// </summary>
public interface IPageNavigatorData
{
    /// <summary>
    /// The text to display in the page navigation link for this block.
    /// </summary>
    string? NavigationTitle { get; set; }
}
