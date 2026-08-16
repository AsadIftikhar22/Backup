namespace Salam.Cms.Web.Features.Accordion.Models;

using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.SpecializedProperties;
using Geta.Optimizely.ContentTypeIcons;
using Geta.Optimizely.ContentTypeIcons.Attributes;
using Salam.Cms.Shared.Models.Common;
using Salam.Cms.Shared.Models.Common.RichText;
using System.ComponentModel.DataAnnotations;

[ContentType(
    DisplayName = "Accordion Item Block",
    GUID = "F9C2529E-21C1-4BD7-A58F-438FBC8066CD",
    Description = "This block can only be added to a Content Accordion Block.",
    GroupName = SystemTabNames.Content)]
[ContentTypeIcon(FontAwesome5Solid.RulerHorizontal)]
public class AccordionItemBlock : BlockData
{
    [Display(
        Name = "Heading",
        Description = "The heading of the block.",
        GroupName = GroupNames.Content,
        Order = 10)]
    [CultureSpecific]
    public virtual string? Heading { get; set; }

    [Display(
        Name = "Description",
        Description = "Add content into this property to display on the Accordion item",
        GroupName = GroupNames.Content,
        Order = 20)]
    [CultureSpecific]
    [UIHint(RichTextEditors.HtmlEditor)]
    public virtual XhtmlString? Description { get; set; }

    [Display(
        Name = "Links",
        Description = "The collection of links for the block.",
        GroupName = GroupNames.Content,
        Order = 30)]
    [CultureSpecific]
    public virtual LinkItemCollection? Links { get; set; }
}