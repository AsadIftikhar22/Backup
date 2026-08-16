namespace Salam.Cms.Web.Features.Navigation.Models;

using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.SpecializedProperties;
using Geta.Optimizely.ContentTypeIcons;
using Geta.Optimizely.ContentTypeIcons.Attributes;
using Salam.Cms.Shared.Models.Common;
using System.ComponentModel.DataAnnotations;

[ContentType(
    DisplayName = "Navigation Item Collection Block",
    GUID = "35adce78-e1de-4640-bdab-dcfd5ef75ddf",
    Description = "Navigation Item Collection Block",
    GroupName = SystemTabNames.Content)]
[ContentTypeIcon(FontAwesome5Solid.List)]
public class NavigationItemCollectionBlock : BlockData
{
    [Display(
        Name = "Heading",
        GroupName = GroupNames.Content,
        Order = 10)]
    [CultureSpecific]
    public virtual string? Heading { get; set; }

    [Display(
        Name = "Links",
        Description = "The collection of links for the block.",
        GroupName = GroupNames.Content,
        Order = 20)]
    [CultureSpecific]
    public virtual LinkItemCollection? Links { get; set; }
}
