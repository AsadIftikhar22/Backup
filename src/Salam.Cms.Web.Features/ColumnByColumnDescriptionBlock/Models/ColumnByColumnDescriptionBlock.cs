namespace Salam.Cms.Web.Features.ColumnByColumnDescriptionBlock.Models;

using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using Geta.Optimizely.ContentTypeIcons;
using Geta.Optimizely.ContentTypeIcons.Attributes;
using Salam.Cms.Shared.Models.Common.RichText;
using Salam.Cms.Web.Features.Common.Models;
using System.ComponentModel.DataAnnotations;

[ContentType(
    DisplayName = "DXP B2B Column By Column Description Title Block",
    GUID = "765757ea-3c05-4506-ba85-52dccc7cdbc2",
    Description = "DXP B2B Column By Column Description Title Block",
    GroupName = SystemTabNames.Content)]
[ContentTypeIcon(FontAwesome5Solid.List)]
public class ColumnByColumnDescriptionBlock : SiteContentBlock
{
    [Display(
        Name = "Title Heading",
        GroupName = SystemTabNames.Content,
        Order = 10)]
    [CultureSpecific]
    public virtual string? Heading { get; set; }

    [Display(
         Name = "Title Description",
         Description = "Title Description",
         GroupName = SystemTabNames.Content,
         Order = 20)]
    [CultureSpecific]
    [UIHint(RichTextEditors.BasicEditor)]
    public virtual string? Description { get; set; }
}