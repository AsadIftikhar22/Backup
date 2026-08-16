namespace Salam.Cms.Web.Features.Catalogue.Models;

using EPiServer.Core;
using EPiServer.DataAnnotations;
using EPiServer.Shell.ObjectEditing;
using Geta.Optimizely.ContentTypeIcons;
using Geta.Optimizely.ContentTypeIcons.Attributes;
using Salam.Cms.Shared.Models.Common;
using Salam.Cms.Shared.Models.Common.Properties;
using Salam.Cms.Web.Features.Common.Interfaces;
using Salam.Cms.Web.Features.Common.Models;
using System.ComponentModel.DataAnnotations;

[ContentType(
    DisplayName = "Product Detail Page",
    Description = "A page that is used to display product detail.",
    GUID = "602b90d9-af7a-415b-a6c7-30d0ba895165",
    GroupName = GroupNames.Content)]
[ContentTypeIcon(FontAwesome5Solid.File)]
public class ProductDetailPage : SitePageData, IPageNavigatorEnabled
{
    [Display(
    Name = "Enable Category Navigator",
    Description = "Toggle the Category navigator, which links to sections of the main content area. The Page Navigator Title on the block will serve as the link text.",
    GroupName = GroupNames.Navigation,
    Order = 45)]
    public virtual bool EnableCategoryNavigator { get; set; }

    [Display(
        Name = "Main Content",
        Description = "A content area that allows blocks that have been specifically designed as section content.",
        GroupName = GroupNames.Content,
        Order = 30)]
    [AllowedTypes(typeof(ISiteContentBlock), typeof(ProductSummaryBlock), typeof(RelatedProductListBlock))]
    public virtual ContentArea? MainContent { get; set; }

    [Display(
        Name = "Disable Fallback Content",
        Description = "If enabled, the fallback content will not be displayed from the product landing page.",
        GroupName = GroupNames.Content,
        Order = 35)]
    public virtual bool DisableFallbackContent { get; set; }

    [Display(
        Name = "Enable Page Navigator",
        Description = "Toggle the page navigator, which links to sections of the main content area. The Page Navigator Title on the block will serve as the link text.",
        GroupName = GroupNames.Navigation,
        Order = 40)]
    public virtual bool EnablePageNavigator { get; set; }

    [Display(
        Name = "Product",
        Description = "Select the product to display.",
        GroupName = GroupNames.ProductSettings,
        Order = 50)]
    [AutoSuggestSelection(typeof(ProductSelectionQuery), AllowCustomValues = false)]
    public virtual string? ProductId { get; set; }

    [Display(
        Name = "Plan Card Time Span",
        Description = "The time span shown on the product block. eg: SAR/Monthly",
        GroupName = GroupNames.ProductSettings,
        Order = 130)]
    [CultureSpecific]
    public virtual string? Span { get; set; }

    [Display(
     Name = "Discoutned Plan Card Time Span",
     Description = "The time shown in the product blocks. eg: SAR",
     GroupName = GroupNames.ProductSettings,
     Order = 131)]
    [CultureSpecific]
    public virtual string? DiscountedPriceSpan { get; set; }

    [Display(
        Name = "Plan Card Vat Inclusive Text",
        Description = "The text shown on the product blocks plan card when there is VAT.",
        GroupName = GroupNames.ProductSettings,
        Order = 140)]
    [CultureSpecific]
    public virtual string? VatText { get; set; }


    [Display(
    Name = "Social Text Update",
    Description = "Social Text Update",
    GroupName = GroupNames.ProductSettings,
    Order = 150)]
    [CultureSpecific]
    public virtual string? SocialTxtUpdate { get; set; }
}