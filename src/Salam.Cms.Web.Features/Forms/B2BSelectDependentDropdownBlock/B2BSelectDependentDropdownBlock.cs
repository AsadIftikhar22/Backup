namespace Salam.Cms.Web.Features.Forms.B2BSelectDropdownBlock;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Forms.Implementation.Elements;
using Geta.Optimizely.ContentTypeIcons;
using Geta.Optimizely.ContentTypeIcons.Attributes;
using System.ComponentModel.DataAnnotations;

[ContentType(DisplayName = "B2B Select Dependent Dropdown Element",
           GUID = "a0807803-b6db-4ff7-81bf-d3413ee44aa9",
        Description = "B2B Select Dependent Dropdown element")]
[ContentTypeIcon(FontAwesome5Solid.List)]
public class B2BSelectDependentDropdownBlock : SelectionElementBlock
{
    [Display(
        Name = "Sub Category Label",
        Description = "Sub Category Label",
        GroupName = SystemTabNames.Content,
        Order = 10)]
    [Required]
    public virtual string SubCategoryLabel { get; set; }

    [Display(
    Name = "Field Mapping with Email Template",
    Description = "Field Mapping with Email Template",
    GroupName = SystemTabNames.Content,
    Order = 30)]
    [Required]
    public virtual string FieldMapping { get; set; }

    [Display(
        Name = "Sub Category Reference",
        Description = "Sub Category Reference",
        GroupName = SystemTabNames.Content,
        Order = 30)]
    [CultureSpecific]
    public virtual ContentReference SubCategoryFolderrReference { get; set; }
}

[ContentType(DisplayName = "Dropdown Options Block")]
public class DropdownOptionsBlock : BlockData
{
    public virtual string? Category { get; set; }
    public virtual string? Value { get; set; }
    public virtual string? tier3 { get; set; }
    public virtual string? tier1 { get; set; }
    public virtual string? Label { get; set; }
    public virtual string? Placeholder { get; set; }
    public virtual string? className { get; set; }
    public virtual string? typeOfComplaint { get; set; }
    public virtual int? maxlength { get; set; }

}
