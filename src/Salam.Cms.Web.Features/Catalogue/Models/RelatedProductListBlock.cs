namespace Salam.Cms.Web.Features.Catalogue.Models;

using EPiServer.Core;
using EPiServer.DataAnnotations;
using EPiServer.Web;
using Geta.Optimizely.ContentTypeIcons;
using Geta.Optimizely.ContentTypeIcons.Attributes;
using Salam.Cms.Shared.Models.Common;
using Salam.Cms.Shared.Models.Common.RichText;
using Salam.Cms.Web.Features.Common.Interfaces;
using Salam.Cms.Web.Features.Common.Models;
using System.ComponentModel.DataAnnotations;

[ContentType(
    DisplayName = "Related Product List",
    Description = "A block that allows for a related product list to be rendered inline on pages.",
    GUID = "302e9500-567f-42c3-8ea5-c3f2156a4e3c",
    GroupName = GroupNames.Content)]
[ContentTypeIcon(FontAwesome5Solid.CartPlus)]
public class RelatedProductListBlock : SiteBlockData, IPageNavigatorData
{
    [Display(
        Name = "Page Navigator Title",
        Description = "The title displayed in the page navigator when 'Enable Page Navigator' is activated on a page containing this block in the main content area.",
        GroupName = GroupNames.Navigation,
        Order = 5)]
    [CultureSpecific]
    public virtual string? NavigationTitle { get; set; }

    [Display(
        Name = "Heading",
        Description = "The heading to show next to the product list.",
        GroupName = GroupNames.Content,
        Order = 10)]
    [CultureSpecific]
    public virtual string? Heading { get; set; } = string.Empty;

    [Display(
        Name = "Description",
        Description = "The description to show next to the product list.",
        GroupName = GroupNames.Content,
        Order = 15)]
    [CultureSpecific]
    [UIHint(RichTextEditors.ReducedEditor)]
    public virtual XhtmlString? Description { get; set; }
}
