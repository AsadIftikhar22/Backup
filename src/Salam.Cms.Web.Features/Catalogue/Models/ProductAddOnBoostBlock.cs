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
    DisplayName = "AddOn / Boost Block",
    Description = "A block that displays boost addons.",
    GUID = "75eb90c8-b23d-48ac-8b5a-e44a9b454b73",
    GroupName = GroupNames.Content)]
[ContentTypeIcon(FontAwesome5Solid.ListAlt)]
public class ProductAddOnBoostBlock : SiteBlockData, IPageNavigatorData
{
    [Display(
        Name = "Page Navigator Title",
        Description = "The title displayed in the page navigator when 'Enable Page Navigator' is activated on a page containing this block in the main content area.",
        GroupName = GroupNames.Navigation,
        Order = 5)]
    [CultureSpecific]
    public virtual string? NavigationTitle { get; set; }

    [Display(
    Name = "Add Banner Navigation",
    Description = "Add Banner Navigation",
    GroupName = GroupNames.Navigation,
    Order = 10)]
    public virtual bool AddBannerNavigation { get; set; }

    [Display(
        Name = "Buy Now Static URL",
        Description = "Buy Now Static URL",
        GroupName = GroupNames.Content,
        Order = 15)]
    [CultureSpecific]
    public virtual LinkItem? BuyNowStaticURL { get; set; }
}
