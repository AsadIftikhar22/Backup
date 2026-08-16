namespace Salam.Cms.Web.Features.Forms.B2BSubmitButtonElementBlock;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Forms.Implementation.Elements;
using Geta.Optimizely.ContentTypeIcons;
using Geta.Optimizely.ContentTypeIcons.Attributes;
using System.ComponentModel.DataAnnotations;

[ContentType(DisplayName = "B2B Submit Element",
        GUID = "08cf3e05-9099-4662-8a43-3380e1de3a99",
        Description = "B2B Submit Form element")]

[ContentTypeIcon(FontAwesome5Solid.List)]
public class B2BSubmitButtonElementBlock : SubmitButtonElementBlock
{
    [Display(
        Name = "SEND Above Button Text",
        Description = "SEND Above Button Text",
        GroupName = SystemTabNames.Content,
        Order = 1)]
    public virtual string SndBtnAboveTxt { get; set; }
}