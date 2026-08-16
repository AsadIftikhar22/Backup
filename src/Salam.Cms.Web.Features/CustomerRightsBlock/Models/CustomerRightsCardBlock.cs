namespace Salam.Cms.Web.Features.CustomerRightsBlock.Models;

using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Web;
using Geta.Optimizely.ContentTypeIcons;
using Geta.Optimizely.ContentTypeIcons.Attributes;
using Salam.Cms.Shared.Models.Common;
using Salam.Cms.Shared.Models.Common.RichText;
using System.ComponentModel.DataAnnotations;

[ContentType(
    DisplayName = "Customer Rights Card Block",
    GUID = "F723A8B9-D0E1-4C2F-3A4B-5C6D7E8F9012",
    Description = "One information card with image, title, description and link. Add via Customer Rights Section Block → Cards.",
    GroupName = SystemTabNames.Content)]
[ContentTypeIcon(FontAwesome5Solid.IdCard)]
public class CustomerRightsCardBlock : BlockData
{
    [Display(
        Name = "Image",
        Description = "Card icon/image.",
        GroupName = GroupNames.Content,
        Order = 10)]
    [CultureSpecific]
    [UIHint(UIHint.Image)]
    public virtual ContentReference? Image { get; set; }

    [Display(
        Name = "Title",
        Description = "Card heading.",
        GroupName = GroupNames.Content,
        Order = 20)]
    [CultureSpecific]
    public virtual string? Title { get; set; }

    [Display(
        Name = "Description",
        Description = "Short description under the title.",
        GroupName = GroupNames.Content,
        Order = 30)]
    [CultureSpecific]
    [UIHint(RichTextEditors.HtmlEditor)]
    public virtual XhtmlString? Description { get; set; }

    [Display(
        Name = "Link (page)",
        Description = "Optional internal page link. If set, this is used instead of Link URL.",
        GroupName = GroupNames.Content,
        Order = 40)]
    [CultureSpecific]
    public virtual ContentReference? Link { get; set; }

    [Display(
        Name = "Link URL",
        Description = "URL for the card link when not using a page (external or path).",
        GroupName = GroupNames.Content,
        Order = 45)]
    [CultureSpecific]
    public virtual string? LinkUrl { get; set; }

    [Display(
        Name = "Open in new tab",
        Description = "Open link in new tab.",
        GroupName = GroupNames.Content,
        Order = 50)]
    public virtual bool LinkNewTab { get; set; }
}
