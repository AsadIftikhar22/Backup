namespace Salam.Cms.Web.Features.Forms.B2BChoiceElementBlock;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Forms.Implementation.Elements;
using Geta.Optimizely.ContentTypeIcons;
using Geta.Optimizely.ContentTypeIcons.Attributes;
using System.ComponentModel.DataAnnotations;

[ContentType(DisplayName = "B2B Choice Element",
           GUID = "84064983-cbdb-43d9-a55b-d984c3261c7b",
        Description = "B2B Choice Form element")]

[ContentTypeIcon(FontAwesome5Solid.List)]
public class B2BChoiceElementBlock : ChoiceElementBlock
{
    [Display(
    Name = "Field Mapping with Email Template",
    Description = "Field Mapping with Email Template",
    GroupName = SystemTabNames.Content,
    Order = 30)]
    public virtual string FieldMapping { get; set; }
    [Display(
          Name = "Increase label height",
          Description = "Increase label height",
          GroupName = SystemTabNames.Content,
          Order = 40)]
    public virtual bool IncreaseLabelHeight { get; set; }
    [Display(
            Name = "Label Position Css",
            Description = "Label Position Css",
            GroupName = SystemTabNames.Content,
            Order = 40)]
    public virtual string LabelPositionCss { get; set; }
}