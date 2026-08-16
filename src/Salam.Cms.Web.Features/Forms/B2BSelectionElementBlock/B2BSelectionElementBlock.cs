namespace Salam.Cms.Web.Features.Forms.B2BSelectionElementBlock;

using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Forms.Implementation.Elements;
using Geta.Optimizely.ContentTypeIcons;
using Geta.Optimizely.ContentTypeIcons.Attributes;
using System.ComponentModel.DataAnnotations;

[ContentType(DisplayName = "B2B Selection Form element",
         GUID = "84014983-cbaa-43d9-a55b-d984c1261c7b",
         Description = "B2B Selection Form element")]
[ContentTypeIcon(FontAwesome5Solid.List)]
public class B2BSelectionElementBlock : SelectionElementBlock
{
    [Display(
    Name = "Field Mapping with Email Template",
    Description = "Field Mapping with Email Template",
    GroupName = SystemTabNames.Content,
    Order = 30)]
    public virtual string FieldMapping { get; set; }
}