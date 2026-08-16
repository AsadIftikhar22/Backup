using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Forms.Implementation.Elements;
using Geta.Optimizely.ContentTypeIcons;
using Geta.Optimizely.ContentTypeIcons.Attributes;
using System.ComponentModel.DataAnnotations;

[ContentType(DisplayName = "B2B Disabled Text Element",
           GUID = "485b9857-b748-4828-a042-6dd66f54fae2",
        Description = "B2B Disabled Text Form element")]

[ContentTypeIcon(FontAwesome5Solid.List)]
public class B2BDisabledTextElementBlock : TextboxElementBlock
{
    [Display(
        Name = "Field Mapping with Email Template",
        Description = "Field Mapping with Email Template",
        GroupName = SystemTabNames.Content,
        Order = 30)]
    [Required]
    public virtual string FieldMapping { get; set; }
}