namespace Salam.Cms.Web.Features.Catalogue.Models;

using EPiServer.Core;
using EPiServer.DataAnnotations;
using EPiServer.SpecializedProperties;
using Geta.Optimizely.ContentTypeIcons;
using Geta.Optimizely.ContentTypeIcons.Attributes;
using Salam.Cms.Shared.Models.Catalogue.Data;
using Salam.Cms.Shared.Models.Common;
using System.ComponentModel.DataAnnotations;

[ContentType(
    DisplayName = "Product Block",
    Description = "A block that displays product selector on the page.",
    GUID = "bf03f4d9-c33c-45c7-a306-6a696cf2c269",
    GroupName = GroupNames.Content)]
[ContentTypeIcon(FontAwesome5Solid.ListAlt)]
public class ProductBlock : BlockData
{
    [Display(
        Name = "Name",
        Description = "Name displayed on the product block",
        GroupName = GroupNames.Content,
        Order = 10)]
    [CultureSpecific]
    public virtual string? Name { get; set; }

    [Display(
        Name = "Price",
        Description = "Price displayed on the product block",
        GroupName = GroupNames.Content,
        Order = 20)]
    [CultureSpecific]
    public virtual string? Price { get; set; }

    [Display(
        Name = "Period",
        Description = "Period for the price displayed on the product block",
        GroupName = GroupNames.Content,
        Order = 20)]
    [CultureSpecific]
    public virtual string? Period { get; set; }

    [Display(
        Name = "Product Fields",
        Description = "List of product fields displayed on the product block",
        GroupName = GroupNames.Content,
        Order = 30)]
    [CultureSpecific]
    public virtual IList<ProductField>? ProductFields { get; set; }

    [Display(
        Name = "Buy Now Button Link",
        Description = "The link used when the 'Buy Now' button is clicked",
        GroupName = GroupNames.Content,
        Order = 40)]
    [CultureSpecific]
    public virtual LinkItem? BuyNowLink { get; set; }
}
