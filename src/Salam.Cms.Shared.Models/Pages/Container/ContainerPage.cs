namespace Salam.Cms.Shared.Models.Pages.Container;

using EPiServer.Core;
using EPiServer.DataAnnotations;
using EPiServer.Web;
using Geta.Optimizely.ContentTypeIcons;
using Geta.Optimizely.ContentTypeIcons.Attributes;
using Salam.Cms.Shared.Models.Common;
using Salam.Cms.Shared.Models.Pages;
using System.ComponentModel.DataAnnotations;

/// <summary>
/// Used to logically group pages in the page tree
/// </summary>
[ContentType(
    DisplayName = "Container Page",
    Description = "A page that is used to group other pages together.",
    GUID = "1b9251dc-c60b-4287-b307-537e3e35a61e",
    GroupName = GroupNames.Specialized)]
[ContentTypeIcon(FontAwesome5Solid.Folder)]
public class ContainerPage : PageData, IContainerPage, INavigationItem
{
    [Display(
        Name = "Icon",
        Description = "The icon to be used for the page.",
        GroupName = GroupNames.AlternateDisplay,
        Order = 10)]
    [UIHint(UIHint.Image)]
    public virtual ContentReference? Icon { get; set; }

    [Display(
        Name = "Mobile Navigation Page Name",
        Description = "Defines the Name of the mobile navigation Product Landing Page link",
        GroupName = GroupNames.Content,
        Order = 10)]
    [Searchable]
    [CultureSpecific]
    public virtual string? MobileName { get; set; }

    [Display(
        Name = "Sort Order for the Navigations",
        Description = "Sort Order for the Navigations",
        GroupName = GroupNames.Settings,
        Order = 3000)]
    [Searchable]
    [CultureSpecific]
    public virtual int SortingOrder { get; set; }

    [Display(
   Name = "New Page Title",
   Description = "New Page Title",
   GroupName = GroupNames.Content,
   Order = 15)]
    [CultureSpecific]
    public virtual string? NewPageTitle { get; set; }
}
