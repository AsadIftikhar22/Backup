namespace Salam.Cms.Web.Features.Accordion.Models;

using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using Geta.Optimizely.ContentTypeIcons;
using Geta.Optimizely.ContentTypeIcons.Attributes;
using Salam.Cms.Web.Features.Common.Models;
using System.ComponentModel.DataAnnotations;

[ContentType(
    DisplayName = "Accordion Advanced Block",
    GUID = "5a0d0418-73f8-496c-829c-f6c17b7c2546",
    Description = "Displays an advanced accordion and allows the content editor to add content to the accordion.",
    GroupName = SystemTabNames.Content)]
[ContentTypeIcon(FontAwesome5Solid.List)]
public class AccordionAdvancedBlock : SiteContentBlock
{
    [Display(
        Name = "Heading",
        GroupName = SystemTabNames.Content,
        Order = 10)]
    [CultureSpecific]
    public virtual string? Heading { get; set; }

    [Display(
         Name = "Accordion Items",
         Description = "Content Area for holding a list of Accordion Items.",
         GroupName = SystemTabNames.Content,
         Order = 20)]
    [AllowedTypes(new[] { typeof(AccordionItemBlock), typeof(HeadingSeparatorBlock) })]
    public virtual ContentArea? Items { get; set; }
}
