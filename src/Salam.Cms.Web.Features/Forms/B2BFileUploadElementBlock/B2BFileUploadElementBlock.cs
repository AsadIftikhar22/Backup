namespace Salam.Cms.Web.Features.Forms.B2BFileUploadElementBlock;

using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Forms.Implementation.Elements;
using Geta.Optimizely.ContentTypeIcons;
using Geta.Optimizely.ContentTypeIcons.Attributes;
using System.ComponentModel.DataAnnotations;

[ContentType(DisplayName = "B2B File Upload Element",
      GUID = "84064983-cbab-43d9-a55b-d984c3261c7b",
   Description = "B2B File Upload Form element")]

[ContentTypeIcon(FontAwesome5Solid.List)]
public class B2BFileUploadElementBlock : FileUploadElementBlock
{
    public virtual string FileName { get; set; }
    public virtual int FileSize { get; set; } // Use int if FileSize is stored as an integer

    [Display(
    Name = "Field Mapping with Email Template",
    Description = "Field Mapping with Email Template",
    GroupName = SystemTabNames.Content,
    Order = 30)]
    [Required]
    public virtual string FieldMapping { get; set; }

    [Display(
        Name = "Highlighted Text",
        Description = "Highlighted Text",
        GroupName = SystemTabNames.Content,
        Order = 40)]
    [CultureSpecific]
    public virtual string HighlightedText { get; set; }

    [Display(
        Name = "Highlighted Text Span",
        Description = "Highlighted Text Span",
        GroupName = SystemTabNames.Content,
        Order = 50)]
    [CultureSpecific]
    public virtual string HighlightedTextSpan { get; set; }
}