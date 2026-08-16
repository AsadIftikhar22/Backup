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
    DisplayName = "DXP B2B Accordion Item Block",
    GUID = "bd5a5d67-efd4-43a4-a315-b5618875a52c",
    Description = "This block can only be added to a Content Accordion Block.",
    GroupName = SystemTabNames.Content)]
[ContentTypeIcon(FontAwesome5Solid.RulerHorizontal)]
public class B2BAccordionItemBlock : BlockData
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
    [UIHint(RichTextEditors.FullEditor)]
    public virtual XhtmlString? Description { get; set; }

    [Display(
        Name = "Links",
        Description = "The collection of links for the block.",
        GroupName = GroupNames.Content,
        Order = 30)]
    [CultureSpecific]
    public virtual LinkItemCollection? Links { get; set; }
}