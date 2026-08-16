namespace Salam.Cms.Web.Features.Forms.B2BFormHintBlock;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Forms.Implementation.Elements;
using EPiServer.Shell.ObjectEditing;
using Geta.Optimizely.ContentTypeIcons;
using Geta.Optimizely.ContentTypeIcons.Attributes;
using Salam.Cms.Web.Features.Forms.SelectionFactories;
using System.ComponentModel.DataAnnotations;

[ContentType(DisplayName = "B2B Form Text Field Having Hint Block When label is provided ",
    GUID = "cf62cc85-a40a-412f-b1b4-925db83a76a7",
 Description = "B2B Form Text Field Having Hint Block When label is provided")]
[ContentTypeIcon(FontAwesome5Solid.List)]
public class B2BFormTextHintBlock : TextboxElementBlock
{
    [Display(GroupName = SystemTabNames.Content,
    Name = "Show Icon Hint",
    Description = "Show Icon Hint",
    Order = 10)]
    public virtual bool ShowIconHint { get; set; }

    [Display(GroupName = SystemTabNames.Content,
    Name = "Small Hint Text",
    Description = "Small Hint Text",
    Order = 20)]
    public virtual string SmallHintMsg { get; set; }

    [Display(GroupName = SystemTabNames.Content,
    Name = "Icon Hint Text",
    Description = "Icon Hint Text",
    Order = 20)]
    public virtual string HintTextFullSize { get; set; }

    [Display(
            Name = "Hint Message Color for the field",
            Description = "Hint Message Color for the field",
            GroupName = SystemTabNames.Content,
            Order = 30)]
    [SelectOne(SelectionFactoryType = typeof(LabelColorSelectionFactory))]
    public virtual string HintTextFontColor { get; set; }

    [Display(
        Name = "Hint Message Font Size for the field",
        Description = "Hint Message Font Size for the field",
        GroupName = SystemTabNames.Content,
        Order = 30)]
    [SelectOne(SelectionFactoryType = typeof(FontSizeSelectionFactory))]
    public virtual string HintTextFontSize { get; set; }
    [Display(
      Name = "Field Mapping with Email Template",
      Description = "Field Mapping with Email Template",
      GroupName = SystemTabNames.Content,
      Order = 30)]
    [Required]
    public virtual string FieldMapping { get; set; }
}

