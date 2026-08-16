namespace Salam.Cms.Web.Features.Common.Components.PageNavigator.ViewModels;

using System.Collections.Generic;

public class PageNavigatorViewModel
{
    /// <summary>
    /// Represents a single navigation item in the page navigator.
    /// </summary>
    public record PageNavigatorItemViewModel(string Title, string AnchorId);

    /// <summary>
    /// Gets or sets the list of navigation items.
    /// </summary>
    public List<PageNavigatorItemViewModel> Items { get; set; } = new();
}