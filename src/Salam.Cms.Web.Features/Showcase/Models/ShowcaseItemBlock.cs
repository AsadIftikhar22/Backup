namespace Salam.Cms.Web.Features.Showcase.Models;

using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Web;
using Geta.Optimizely.ContentTypeIcons;
using Geta.Optimizely.ContentTypeIcons.Attributes;
using Salam.Cms.Shared.Models.Common;
using Salam.Cms.Shared.Models.Common.RichText;
using System.ComponentModel.DataAnnotations;

[ContentType(
    DisplayName = "Showcase Item Block",
    GUID = "EB7C67F9-07FA-46BD-A613-09180470F4F2",
    Description = "Displays a card and allows the content editor to add content to the card.",
    GroupName = SystemTabNames.Content)]
[ContentTypeIcon(FontAwesome5Solid.List)]
public class ShowcaseItemBlock : BlockData
{
    [Display(
        Name = "Heading",
        Description = "Select the heading for the card.",
        GroupName = GroupNames.Content,
        Order = 10)]
    [CultureSpecific]
    public virtual string? Heading { get; set; }

    [Display(
        Name = "Main Body",
        Description = "Select the main body for the card.",
        GroupName = GroupNames.Content,
        Order = 20)]
    [CultureSpecific]
    [UIHint(RichTextEditors.ReducedEditor)]
    public virtual XhtmlString? MainBody { get; set; }

}