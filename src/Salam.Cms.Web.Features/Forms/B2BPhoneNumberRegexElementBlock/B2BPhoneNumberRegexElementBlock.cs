namespace Salam.Cms.Web.Features.Forms.B2BNumberElementBlock;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Forms.Implementation.Elements;
using Geta.Optimizely.ContentTypeIcons;
using Geta.Optimizely.ContentTypeIcons.Attributes;
using System.ComponentModel.DataAnnotations;

[ContentType(DisplayName = "B2B Phone Number Regex Form element",
         GUID = "f7cc3685-e835-4f00-a12f-e6ce5485ada8",
         Description = "B2B Phone Number Regex Form element")]
[ContentTypeIcon(FontAwesome5Solid.List)]
public class B2BPhoneNumberRegexElementBlock : TextboxElementBlock
{
    [Display(
    Name = "Field Mapping with Email Template",
    Description = "Field Mapping with Email Template",
    GroupName = SystemTabNames.Content,
    Order = 30)]
    [Required]
    public virtual string FieldMapping { get; set; }
}