namespace Salam.Cms.Web.Features.Forms.B2BDateInputElementBlock;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Forms.Implementation.Elements;
using Geta.Optimizely.ContentTypeIcons;
using Geta.Optimizely.ContentTypeIcons.Attributes;
using System.ComponentModel.DataAnnotations;

[ContentType(DisplayName = "B2B Date Input Element",
                  GUID = "ba78a315-17ca-4c2a-949f-de8dffd05220",
       Description = "B2B Date Input Element")]
[ContentTypeIcon(FontAwesome5Solid.List)]
public class B2BDateInputElementBlock : TextboxElementBlock
{
    [Display(
           Name = "Event Start Time",
           GroupName = SystemTabNames.Content,
           Order = 20)]
    public virtual DateTime DateTimeProperty{ get; set; }

    [Display(
        Name = "Field Mapping with Email Template",
        Description = "Field Mapping with Email Template",
        GroupName = SystemTabNames.Content,
        Order = 30)]
    [Required]
    public virtual string FieldMapping { get; set; }

}
