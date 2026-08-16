namespace Salam.Cms.Web.Features.Forms.B2BNumberElementBlock;

using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Forms.Implementation.Elements;
using Geta.Optimizely.ContentTypeIcons;
using Geta.Optimizely.ContentTypeIcons.Attributes;
using Salam.Cms.Web.Features.Forms.ComplaintTabFormContainerBlock;
using System.ComponentModel.DataAnnotations;

[ContentType(DisplayName = "B2B Number Element",
           GUID = "38d86e38-44ac-4fc9-baf5-21aa0ded1325",
        Description = "B2B Number Element")]
[ContentTypeIcon(FontAwesome5Solid.List)]
public class B2BNumberElementBlock : NumberElementBlock
{
    [Display(
      Name = "Field Mapping with Email Template",
      Description = "Field Mapping with Email Template",
      GroupName = SystemTabNames.Content,
      Order = 30)]
    [Required]
    public virtual string FieldMapping { get; set; }

    [Display(
      Name = "Note Protection Channel",
      Description = "Note Protection Channel",
      GroupName = SystemTabNames.Content,
      Order = 30)]
    [CultureSpecific]
    public virtual string NoteProtectionChannel { get; set; }

    [Display(
          Name = "Regex Pattern",
          Description = "Regex Pattern",
          GroupName = SystemTabNames.Content,
          Order = 30)]
    [CultureSpecific]
    public virtual string RegexPattern { get; set; }
    public virtual string OTPNumber { get; set; }
    public virtual ComplaintModelElementBlock ComplaintModelElementBlock { get; set; }
}
