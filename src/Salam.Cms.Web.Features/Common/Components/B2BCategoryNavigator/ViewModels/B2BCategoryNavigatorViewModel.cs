namespace Salam.Cms.Web.Features.Common.Components.B2BCategoryNavigator.ViewModels;

using System.Collections.Generic;

public class B2BCategoryNavigatorViewModel
{
    /// <summary>
    /// Represents a single navigation item in the page navigator.
    /// </summary>
    public record B2BCategoryNavigatorItemViewModel(string Title, string AnchorId);

    /// <summary>
    /// Gets or sets the list of navigation items.
    /// </summary>
    public List<B2BCategoryNavigatorItemViewModel> Items { get; set; } = new();
}