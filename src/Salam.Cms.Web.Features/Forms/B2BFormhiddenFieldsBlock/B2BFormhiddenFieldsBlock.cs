using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Forms.Implementation.Elements;
using Geta.Optimizely.ContentTypeIcons;
using Geta.Optimizely.ContentTypeIcons.Attributes;
using System.ComponentModel.DataAnnotations;

[ContentType(DisplayName = "B2B Hidden Fields",
           GUID = "443dbbd4-0ebb-4005-8fc9-9df4109fd17c",
        Description = "B2B Hidden Fields")]
[ContentTypeIcon(FontAwesome5Solid.List)]
public class B2BFormhiddenFieldsBlock : PredefinedHiddenElementBlock
{
    [Display(
            Name = "Hidden Field Name",
            Description = "Hidden Field Name",
            GroupName = SystemTabNames.Content,
            Order = 10)]
    public virtual string HiddenFieldName { get; set; }

    [Display(
        Name = "Field Mapping with Email Template",
        Description = "Field Mapping with Email Template",
        GroupName = SystemTabNames.Content,
        Order = 30)]
    [Required]
    public virtual string FieldMapping { get; set; }

}