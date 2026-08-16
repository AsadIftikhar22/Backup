namespace Salam.Cms.Web.Features.Catalogue.Models;

using EPiServer.Core;
using EPiServer.DataAnnotations;
using EPiServer.Shell.ObjectEditing;
using EPiServer.Web;
using Geta.Optimizely.ContentTypeIcons;
using Geta.Optimizely.ContentTypeIcons.Attributes;
using Salam.Cms.Shared.Models.Catalogue.Enums;
using Salam.Cms.Shared.Models.Common;
using Salam.Cms.Shared.Models.Common.Properties;
using Salam.Cms.Web.Features.Common.Interfaces;
using Salam.Cms.Web.Features.Common.Models;
using System.ComponentModel.DataAnnotations;

[ContentType(
    DisplayName = "Product Landing Page",
    Description = "A page that is used to display a product landing page.",
    GUID = "a96cbba3-f940-4a7b-ba05-4dd0e9a15330",
    GroupName = GroupNames.Content)]
[ContentTypeIcon(FontAwesome5Solid.File)]
[AvailableContentTypes(Include = new[] { typeof(ProductDetailPage), typeof(ProductLandingPage) })]
public class ProductLandingPage : SitePageData, IPageNavigatorEnabled
{
    [Display(
    Name = "Enable Category Navigator",
    Description = "Toggle the Category navigator, which links to sections of the main content area. The Page Navigator Title on the block will serve as the link text.",
    GroupName = GroupNames.Navigation,
    Order = 45)]
    public virtual bool EnableCategoryNavigator { get; set; }

    [Display(
        Name = "Top Content",
        Description = "A specific area for content blocks which will be rendered immediately below the hero area.",
        GroupName = GroupNames.Content,
        Order = 20)]
    public virtual ContentArea? TopContent { get; set; }

    [Display(
        Name = "Main Content",
        Description = "A content area that allows blocks that have been specifically designed as section content.",
        GroupName = GroupNames.Content,
        Order = 30)]
    [AllowedTypes(typeof(ISiteContentBlock), typeof(ProductSelectorBlock), typeof(ProductAddOnBoostBlock), typeof(ProductAddOnNewAddsOnBlock))]
    public virtual ContentArea? MainContent { get; set; }

    [Display(
        Name = "Product Detail Fallback Content",
        Description = "A content area that allows blocks that have been specifically designed as fallback content for Product Detail Pages.",
        GroupName = GroupNames.Content,
        Order = 40)]
    [AllowedTypes(typeof(ISiteContentBlock), typeof(RelatedProductListBlock))]
    public virtual ContentArea? ProductDetailFallbackContent { get; set; }

    [Display(
        Name = "Enable Page Navigator",
        Description = "Toggle the page navigator, which links to sections of the main content area. The Page Navigator Title on the block will serve as the link text.",
        GroupName = GroupNames.Navigation,
        Order = 50)]
    public virtual bool EnablePageNavigator { get; set; }

    [Display(
        Name = "Title",
        Description = "Product Selector Title.",
        GroupName = GroupNames.ProductSelector,
        Order = 50)]
    [CultureSpecific]
    public virtual string? ProductSelectorTitle { get; set; } = string.Empty;

    [Display(
        Name = "Description",
        Description = "Product Selector Description.",
        GroupName = GroupNames.ProductSelector,
        Order = 60)]
    [CultureSpecific]
    public virtual XhtmlString? ProductSelectorDescription { get; set; }

    [Display(
        Name = "Product Selector Override Content",
        Description = "A content area that allows blocks that have been specifically designed as fallback content for Product Detail Pages.",
        GroupName = GroupNames.ProductSelector,
        Order = 65)]
    [AllowedTypes(typeof(ProductBlock))]
    [CultureSpecific]
    public virtual ContentArea? OverrideProductList { get; set; }
    

    [Display(
        Name = "Handoff Behaviour",
        Description = "The handoff behaviour to use when handing off products."
                        + SalamConstants.PropertyDescriptions.LineBreak
                        + "None: No handoff behaviour is applied."
                        + SalamConstants.PropertyDescriptions.LineBreak
                        + "Plan: Initiate plan handoff."
                        + SalamConstants.PropertyDescriptions.LineBreak
                        + "Device: Initiate device handoff.",
        GroupName = GroupNames.ProductSelector,
        Order = 70)]
    [BackingType(typeof(PropertyNumber))]
    [EditorDescriptor(EditorDescriptorType = typeof(EnumEditorDescriptor<HandoffOption>))]
    public virtual HandoffOption HandoffBehavior { get; set; }

    [Display(
        Name = "Visible Fields (Old)",
        Description = "Product Selector Visible Fields.",
        GroupName = GroupNames.ProductSelector,
        Order = 80)]
    [AutoSuggestSelection(typeof(ProductVisibleFieldsSelectionQuery), AllowCustomValues = false)]
    [Obsolete("Phasing this out eventually")]
    public virtual IList<string> ProductSelectorVisibleFields { get; set; } = new List<string>();

    [Display(
        Name = "Featured Visible Fields",
        Description = "Featured Product Selector Visible Fields.",
        GroupName = GroupNames.ProductSelector,
        Order = 83)]
    [BackingType(typeof(PropertyCollection<string>))]
    [AutoSuggestSelection(typeof(ProductVisibleFieldsSelectionQuery), AllowCustomValues = false)]
    public virtual IList<string> ProductSelectorVisibleFieldsFeatured { get; set; } = [];

    [Display(
        Name = "Visible Fields",
        Description = "Product Selector Visible Fields.",
        GroupName = GroupNames.ProductSelector,
        Order = 85)]
    [BackingType(typeof(PropertyCollection<string>))]
    [AutoSuggestSelection(typeof(ProductVisibleFieldsSelectionQuery), AllowCustomValues = false)]
    public virtual IList<string> ProductSelectorVisibleFieldsNew { get; set; } = [];

    [Display(
        Name = "Category",
        Description = "Product Catalogue Category.",
        GroupName = GroupNames.ProductSelector,
        Order = 90)]
    [AutoSuggestSelection(typeof(CategorySelectionQuery), AllowCustomValues = false)]
    public virtual int? ProductCatalogueCategory { get; set; }

    [Display(
        Name = "Disable Tabs",
        Description = "Shows a flat list of products with no tabs if enabled. This only applies to categories that have sub-categories, such as IDD, Prepaid plans etc.",
        GroupName = GroupNames.ProductSelector,
        Order = 100)]
    public virtual bool AreTabsDisabled { get; set; }

    [Display(
        Name = "Disable Footer",
        Description = "Hides the footer when checked",
        GroupName = GroupNames.ProductSelector,
        Order = 110)]
    public virtual bool IsFooterDisabled { get; set; }

    [Display(
        Name = "Product Footer Text",
        Description = "Footer Text",
        GroupName = GroupNames.ProductSelector,
        Order = 110)]
    [CultureSpecific]
    public virtual string? FooterText { get; set; }

    [Display(
        Name = "Dynamic Products",
        Description = "Renders only the products added here, use this if there are no detail pages",
        GroupName = GroupNames.ProductSelector,
        Order = 120)]
    [AutoSuggestSelection(typeof(ProductSelectionQuery), AllowCustomValues = false)]
    [BackingType(typeof(PropertyCollection<int>))]
    public virtual IList<int> DynamicProducts { get; set; }

    [Display(
        Name = "Visible Fields",
        Description = "Product Summary Visible Fields.",
        GroupName = GroupNames.ProductSummary,
        Order = 90)]
    [AutoSuggestSelection(typeof(ProductVisibleFieldsSelectionQuery), AllowCustomValues = false)]
    public virtual IList<string> ProductSummaryVisibleFields { get; set; } = new List<string>();

    [Display(
        Name = "Plan Card Time Span",
        Description = "The time span shown on the product block. eg: SAR/Monthly For",
        GroupName = GroupNames.ProductSelector,
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
    Name = "Plan Card Time Span Where validity is empty",
    Description = "The time span shown on the product block. eg: SAR/Monthly",
    GroupName = GroupNames.ProductSelector,
    Order = 135)]
    [CultureSpecific]
    public virtual string? SpanWhereValidityIsNull { get; set; }

    [Display(
        Name = "Plan Card Vat Inclusive Text",
        Description = "The text shown on the product blocks plan card when there is VAT.",
        GroupName = GroupNames.ProductSelector,
        Order = 140)]
    [CultureSpecific]
    public virtual string? VatText { get; set; }

    [Display(
        Name = "Plan Card Badge Text",
        Description = "Select the main body for the card. Text of [[FreeTime]] will be replaced with the products 'Free Time' value. Text of [[PackageDuration]] will be replaced with the products 'PackageDuration' value.",

        GroupName = GroupNames.ProductSelector,
        Order = 150)]
    [CultureSpecific]
    [UIHint(UIHint.Textarea)]
    public virtual string? BadgeText { get; set; }

    [Display(
        Name = "Data Label",
        Description = "The text used in the product selector plan card for the data atribute",
        GroupName = GroupNames.ProductSelector,
        Order = 155)]
    [CultureSpecific]
    public virtual string? DataText { get; set; }

    [Display(
        Name = "Call Label",
        Description = "The text used in the product selector plan card when there is an unlimited amount of calls",
        GroupName = GroupNames.ProductSelector,
        Order = 160)]
    [CultureSpecific]
    public virtual string? CallLabel { get; set; }

    [Display(
        Name = "Call Amount Text",
        Description = "The text used in the product selector plan card when there is a amount 'of calls' used",

        GroupName = GroupNames.ProductSelector,
        Order = 170)]
    [CultureSpecific]
    [UIHint(UIHint.Textarea)]
    public virtual string? CallAmountText { get; set; }

    [Display(
            Name = "Buy Button Redirection",
            Description = "The text used in the product selector plan card when there is Buy Now Link Override for Visitor",
            GroupName = GroupNames.VisitorProductSelectorOverride,
            Order = 175)]
    [CultureSpecific]
    public virtual string? BuyButtonRedirection { get; set; }
}