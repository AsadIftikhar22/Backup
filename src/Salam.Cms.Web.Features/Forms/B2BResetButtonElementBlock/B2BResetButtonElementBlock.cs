namespace Salam.Cms.Web.Features.Forms.B2BResetButtonElementBlock;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Forms.Implementation.Elements;
using Geta.Optimizely.ContentTypeIcons;
using Geta.Optimizely.ContentTypeIcons.Attributes;
using System.ComponentModel.DataAnnotations;

[ContentType(DisplayName = "B2B Reset Element",
        GUID = "73771370-a68b-42ae-b54c-1a6d56e966ca",
        Description = "B2B Reset Form element")]
[ContentTypeIcon(FontAwesome5Solid.List)]
public class B2BResetButtonElementBlock : ResetButtonElementBlock
{
    [Display(
    Name = "Field Mapping with Email Template",
    Description = "Field Mapping with Email Template",
    GroupName = SystemTabNames.Content,
    Order = 30)]
    public virtual string FieldMapping { get; set; }
}