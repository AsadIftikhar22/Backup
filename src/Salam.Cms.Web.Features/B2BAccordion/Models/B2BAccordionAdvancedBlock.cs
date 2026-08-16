namespace Salam.Cms.Web.Features.Accordion.Models;

using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Shell.ObjectEditing;
using Geta.Optimizely.ContentTypeIcons;
using Geta.Optimizely.ContentTypeIcons.Attributes;
using Salam.Cms.Web.Features.Common.Models;
using System.ComponentModel.DataAnnotations;

[ContentType(
    DisplayName = "DXP B2B Accordion Advanced Block",
    GUID = "b5b5c83e-e2f1-443d-8351-eb14ac26b8db",
    Description = "Displays an DXP B2B advanced accordion and allows the content editor to add content to the accordion.",
    GroupName = SystemTabNames.Content)]
[ContentTypeIcon(FontAwesome5Solid.List)]
public class B2BAccordionAdvancedBlock : SiteContentBlock
{
    [Display(
        Name = "Heading",
        GroupName = SystemTabNames.Content,
        Order = 10)]
    [CultureSpecific]
    public virtual string? Heading { get; set; }


    [Display(
     Name = "Accordion Background Color",
     GroupName = SystemTabNames.Content,
     Order = 30)]
    [SelectOne(SelectionFactoryType = typeof(ColorSelectionFactory))]
    public virtual string SelectedHexColor { get; set; }


    [Display(
         Name = "Accordion Items",
         Description = "Content Area for holding a list of Accordion Items.",
         GroupName = SystemTabNames.Content,
         Order = 20)]
    [AllowedTypes(new[] { typeof(B2BAccordionItemBlock), typeof(B2BHeadingSeparatorBlock) })]
    public virtual ContentArea? Items { get; set; }
}

public class ColorSelectionFactory : ISelectionFactory
{
    public IEnumerable<ISelectItem> GetSelections(ExtendedMetadata metadata)
    {
        yield return new SelectItem { Text = "Light Green", Value = "#e6fde7" };
        yield return new SelectItem { Text = "White", Value = "#fff" };
    }
}
