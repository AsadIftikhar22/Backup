namespace Salam.Cms.Web.Features.Forms.B2BTextAreaElementBlock;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Forms.Implementation.Elements;
using EPiServer.Shell.ObjectEditing;
using Geta.Optimizely.ContentTypeIcons;
using Geta.Optimizely.ContentTypeIcons.Attributes;
using Salam.Cms.Web.Features.Forms.SelectionFactories;
using System.ComponentModel.DataAnnotations;

[ContentType(DisplayName = "B2B Text Area Element",
               GUID = "6b9b2dce-90fd-43b7-b017-d7da3f1b5eff",
    Description = "B2B Text Area Element")]

[ContentTypeIcon(FontAwesome5Solid.List)]
public class B2BTextAreaElementBlock : TextareaElementBlock
{
    [Display(
    Name = "Field Mapping with Email Template",
    Description = "Field Mapping with Email Template",
    GroupName = SystemTabNames.Content,
    Order = 30)]
    [Required]
    public virtual string FieldMapping { get; set; }


    [Display(
    Name = "Font Size for the Heading",
    Description = "Font Size for the Heading",
    GroupName = SystemTabNames.Content,
    Order = 30)]
    [SelectOne(SelectionFactoryType = typeof(TextAreaHeightSelectionFactory))]
    public virtual string TextAreaHeight { get; set; }
}