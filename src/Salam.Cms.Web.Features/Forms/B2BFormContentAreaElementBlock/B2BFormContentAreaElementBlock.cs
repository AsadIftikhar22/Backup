namespace Salam.Cms.Web.Features.Forms.B2BFormContentAreaElementBlock;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Forms.Implementation.Elements;
using Geta.Optimizely.ContentTypeIcons;
using Geta.Optimizely.ContentTypeIcons.Attributes;
using System.ComponentModel.DataAnnotations;

[ContentType(DisplayName = "B2B Form Content Area Element",
    GUID = "b9f285a0-6eaa-49c6-9b20-e54d6eac2ee2",
 Description = "B2B Form Content Area Element to render blocks on forms")]
[ContentTypeIcon(FontAwesome5Solid.List)]
public class B2BFormContentAreaElementBlock : ParagraphTextElementBlock
{
    [Display(GroupName = SystemTabNames.Content,
    Name = "Content Area",
    Description = "Drop Blocks here",
    Order = 10)]
    public virtual ContentArea Blocks { get; set; }

}