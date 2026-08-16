namespace Salam.Cms.Web.Features.Accordion.Models;

using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Shell.ObjectEditing;
using EPiServer.SpecializedProperties;
using EPiServer.Web;
using Geta.Optimizely.ContentTypeIcons;
using Geta.Optimizely.ContentTypeIcons.Attributes;
using Salam.Cms.Shared.Models.Common;
using Salam.Cms.Shared.Models.Common.RichText;
using Salam.Cms.Web.Features.Common.Models;
using System.ComponentModel.DataAnnotations;

[ContentType(
    DisplayName = "DXP B2B Accordion Block",
    GUID = "6ebfd152-8587-4af4-9f26-0380d6d91c9e",
    Description = "Displays an accordion and allows the content editor to add content to the accordion.",
    GroupName = SystemTabNames.Content)]
[ContentTypeIcon(FontAwesome5Solid.List)]
public class B2BAccordionBlock : SiteContentBlock
{
    [Display(
        Name = "Heading",
        GroupName = SystemTabNames.Content,
        Order = 10)]
    [CultureSpecific]
    public virtual string? Heading { get; set; }

    [Display(
        Name = "Description",
        GroupName = SystemTabNames.Content,
        Order = 20)]
    [CultureSpecific]
    [UIHint(RichTextEditors.HtmlEditor)]
    public virtual XhtmlString? Description { get; set; }

    [Display(
        Name = "View More Link",
        Description = "The view more link.",
        GroupName = GroupNames.Content,
        Order = 30)]
    [CultureSpecific]
    public virtual LinkItem? ViewMoreLink { get; set; }

    [Display(
         Name = "Accordion Items",
         Description = "Content Area for holding a list of Accordion Items.",
         GroupName = GroupNames.Content,
         Order = 40)]
    [AllowedTypes(new[] { typeof(B2BAccordionItemBlock) })]
    public virtual ContentArea? Items { get; set; }

    [Display(
         Name = "Accordion Background Color",
         GroupName = SystemTabNames.Content,
         Order = 50)]
    [SelectOne(SelectionFactoryType = typeof(ColorSelectionFactory))]
    public virtual string SelectedHexColor { get; set; }

}

