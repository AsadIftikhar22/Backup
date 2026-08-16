namespace Salam.Cms.Web.Features.Catalogue.Models;

using EPiServer.DataAnnotations;
using EPiServer.SpecializedProperties;
using Geta.Optimizely.ContentTypeIcons;
using Geta.Optimizely.ContentTypeIcons.Attributes;
using Salam.Cms.Shared.Models.Common;
using Salam.Cms.Web.Features.Common.Interfaces;
using Salam.Cms.Web.Features.Common.Models;
using System.ComponentModel.DataAnnotations;

[ContentType(
    DisplayName = "Product Selector",
    Description = "A block that displays product selector on the page.",
    GUID = "f045d02e-cc23-4d9d-acb9-37ebc2163abf",
    GroupName = GroupNames.Content)]
[ContentTypeIcon(FontAwesome5Solid.ListAlt)]
public class ProductSelectorBlock : SiteBlockData, IPageNavigatorData
{
    [Display(
        Name = "Page Navigator Title",
        Description = "The title displayed in the page navigator when 'Enable Page Navigator' is activated on a page containing this block in the main content area.",
        GroupName = GroupNames.Navigation,
        Order = 5)]
    [CultureSpecific]
    public virtual string? NavigationTitle { get; set; }

    [Display(
            Name = "Buy Now Static URL",
            Description = "Buy Now Static URL",
            GroupName = GroupNames.Content,
            Order = 10)]
    [CultureSpecific]
    public virtual LinkItem? BuyNowStaticURL { get; set; }

    [Display(
        Name = "Exclusive Text",
        Description = "Exclusive Text",
        GroupName = GroupNames.Content,
        Order = 10)]
    [CultureSpecific]
    public virtual string? ExclusiveText { get; set; }
}