namespace Salam.Cms.Web.Features.Forms.B2BSelectDropdownBlock;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Forms.Implementation.Elements;
using Geta.Optimizely.ContentTypeIcons;
using Geta.Optimizely.ContentTypeIcons.Attributes;
using System.ComponentModel.DataAnnotations;

[ContentType(DisplayName = "B2B Select Dropdown Element",
           GUID = "83fe4290-f734-4b98-9435-1d281b28346e",
        Description = "B2B Select Dropdown element")]
[ContentTypeIcon(FontAwesome5Solid.List)]
public class B2BSelectDropdownBlock : SelectionElementBlock
{
    [Display(
    Name = "Field Mapping with Email Template",
    Description = "Field Mapping with Email Template",
    GroupName = SystemTabNames.Content,
    Order = 30)]
    [Required]
    public virtual string FieldMapping { get; set; }
}
