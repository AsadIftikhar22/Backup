namespace Salam.Cms.Web.Features.Forms.B2BImageChoiceElementBlock;

using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Forms.Implementation.Elements;
using Geta.Optimizely.ContentTypeIcons;
using Geta.Optimizely.ContentTypeIcons.Attributes;
using System.ComponentModel.DataAnnotations;

[ContentType(DisplayName = "B2B Image Upload Element",
             GUID = "84014983-cbab-43d9-a55b-d984c3261c7b",
             Description = "B2B Image Upload Form element")]
[ContentTypeIcon(FontAwesome5Solid.List)]
public class B2BImageChoiceElementBlock : ImageChoiceElementBlock
{
    [Display(
    Name = "Field Mapping with Email Template",
    Description = "Field Mapping with Email Template",
    GroupName = SystemTabNames.Content,
    Order = 30)]
    [Required]
    public virtual string FieldMapping { get; set; }
}