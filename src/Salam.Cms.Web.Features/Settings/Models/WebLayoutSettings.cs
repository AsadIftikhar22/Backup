namespace Salam.Cms.Web.Features.Settings.Models;

using EPiServer.Cms.Shell.UI.ObjectEditing.EditorDescriptors;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Shell.ObjectEditing;
using EPiServer.SpecializedProperties;
using EPiServer.Web;
using Geta.Optimizely.ContentTypeIcons;
using Geta.Optimizely.ContentTypeIcons.Attributes;
using Salam.Cms.Core.Settings.Infrastructure;
using Salam.Cms.Core.Settings.Models;
using Salam.Cms.Shared.Models;
using Salam.Cms.Shared.Models.Common;
using Salam.Cms.Shared.Models.Common.Properties;
using Salam.Cms.Shared.Models.Common.RichText;
using Salam.Cms.Shared.Models.Validation;
using Salam.Cms.Web.Features.Cards.Components;
using Salam.Cms.Web.Features.Cards.Models;
using Salam.Cms.Web.Features.Common.Components.MetaData;
using Salam.Cms.Web.Features.IconLinks.Models;
using Salam.Cms.Web.Features.Navigation.Models;
using Salam.Cms.Web.Features.NotFound.Models;
using Salam.Cms.Web.Features.RedirectRuleBlock.Models;
using Salam.Cms.Web.Features.Showcase.Models;
using Salam.Cms.Web.Features.Support.Models;
using System.ComponentModel.DataAnnotations;

[SettingsContentType(
    DisplayName = "Web Layout Settings",
    GUID = "f2db19fa-9385-4a25-a858-74c67401d35c",
    Description = "Web Layout Settings",
    AvailableInEditMode = true,
    SettingsName = "Web Layout Settings")]
[ContentTypeIcon(FontAwesome5Solid.Cogs)]
public class WebLayoutSettings : SettingsBase
{
    [Display(
       Name = "Logo",
       Description = "Logo",
       GroupName = GroupNames.Content,
       Order = 10)]
    [AllowedTypes(new[] { typeof(ImageData) })]
    [UIHint(UIHint.Image)]
    [CultureSpecific]
    public virtual ContentReference? Logo { get; set; }

    [Display(
   Name = "B2B Website Logo",
   Description = "B2B Website Logo",
   GroupName = GroupNames.Content,
   Order = 10)]
    [AllowedTypes(new[] { typeof(ImageData) })]
    [UIHint(UIHint.Image)]
    [CultureSpecific]
    public virtual ContentReference? B2BLogo { get; set; }

    [Display(
        Name = "Main Page Business Navigation",
        Description = "Main Page Business Navigation",
        GroupName = GroupNames.Content,
        Order = 12)]
    public virtual ContentReference? MainPageBusinessNavigation { get; set; }

    [Display(
       Name = "Main Page Consumer Navigation",
       Description = "Main Page Consumer Navigation",
       GroupName = GroupNames.Content,
       Order = 12)]
    public virtual ContentReference? MainPageConsumerNavigation { get; set; }

    [Display(
            Name = "B2B Search Placeholder Text",
            Description = "B2B Search Placeholder Text",
            GroupName = GroupNames.B2B_Business_Header,
            Order = 10)]
    [CultureSpecific]
    public virtual string? B2bSearchPlaceHolderTxt { get; set; }

    [Display(
        Name = "B2B Search Button Text",
        Description = "B2B Search Button Text",
        GroupName = GroupNames.B2B_Business_Header,
        Order = 10)]
    [CultureSpecific]
    public virtual string? B2bSearchBtnTxt { get; set; }

    [Display(
   Name = "Whole Sale Logo",
   Description = "Whole Sale Logo",
   GroupName = GroupNames.Content,
   Order = 10)]
    [AllowedTypes(new[] { typeof(ImageData) })]
    [UIHint(UIHint.Image)]
    [CultureSpecific]
    public virtual ContentReference? WholeSaleLogo { get; set; }

    [Display(
        Name = "Small Logo",
        Description = "Small Logo used mainly on mobile devices.",
        GroupName = GroupNames.Content,
        Order = 20)]
    [AllowedTypes(new[] { typeof(ImageData) })]
    [UIHint(UIHint.Image)]
    [CultureSpecific]
    public virtual ContentReference? LogoSmall { get; set; }

    [Display(
    Name = "Whole Sale Small Logo",
    Description = "Whole Sale Small Logo used mainly on mobile devices.",
    GroupName = GroupNames.Content,
    Order = 20)]
    [AllowedTypes(new[] { typeof(ImageData) })]
    [UIHint(UIHint.Image)]
    [CultureSpecific]
    public virtual ContentReference? WholeSaleLogoSmall { get; set; }

    [Display(
        Name = "Site Name",
        Description = "Site Name",
        GroupName = GroupNames.Content,
        Order = 30)]
    [Required]
    [CultureSpecific]
    public virtual string? SiteName { get; set; }

    [Display(
        Name = "Page Title Order",
        Description = "Determines the order of the Page Title and Site Name within the title element for the page.",
        GroupName = GroupNames.Content,
        Order = 40)]
    [BackingType(typeof(PropertyNumber))]
    [EditorDescriptor(EditorDescriptorType = typeof(EnumEditorDescriptor<PageTitleOrder>))]
    public virtual PageTitleOrder PageTitleOrder { get; set; }

    [Display(
        Name = "Not Found Page",
        Description = "The currently active settings for this site.",
        GroupName = GroupNames.Content,
        Order = 50)]
    [AllowedTypes(typeof(NotFoundPage))]
    public virtual ContentReference? NotFoundPage { get; set; }

    [Display(
        Name = "Google Tag Manager Key",
        Description = "Enter the Google Tag Manager key for use with the code snippets",
        GroupName = GroupNames.SearchEngineOptimization,
        Order = 500)]
    [RegularExpression(@"^(GTM-[0-9A-Z]{1,15})$", ErrorMessage = "The Google Tag Manager Key should be in the format of 'GTM-ABC123'.")]
    public virtual string? TagManagerKey { get; set; }

    [Display(
    Name = "Google Tag Manager Host Name",
    Description = "Google Tag Manager Host Name",
    GroupName = GroupNames.SearchEngineOptimization,
    Order = 600)]
    public virtual string? HostName { get; set; }

    [Display(
    Name = "Google Tag Manager Absolute URL",
    Description = "Google Tag Manager Absolute URL",
    GroupName = GroupNames.SearchEngineOptimization,
    Order = 700)]
    public virtual string? AbsoluteURL { get; set; }

    //Header Items 

    [Display(
        Name = "Top Navigation Menu",
        Description = "Content Area for holding top navigation menu items.",
        GroupName = GroupNames.Header,
        Order = 10)]
    [CultureSpecific]
    public virtual LinkItemCollection TopNavigationMenu { get; set; }
        = new LinkItemCollection();


    [Display(
        Name = "B2B Top Navigation Menu",
        Description = "B2B Content Area for holding top navigation menu items.",
        GroupName = GroupNames.B2B_Business_Header,
        Order = 10)]
    [CultureSpecific]
    public virtual LinkItemCollection B2BTopNavigationMenu { get; set; }
        = new LinkItemCollection();

    [Display(
        Name = "Coverage Button Link",
        Description = "The link for the Coverage button in the top navigation menu",
        GroupName = GroupNames.Header,
        Order = 20)]
    [CultureSpecific]
    public virtual LinkItem CoverageButtonLink { get; set; }


    [Display(
        Name = "B2B Coverage Button Link",
        Description = "The link for the B2B Coverage button in the top navigation menu",
        GroupName = GroupNames.B2B_Business_Header,
        Order = 20)]
    [CultureSpecific]
    public virtual LinkItem B2BCoverageButtonLink { get; set; }

    [Display(
        Name = "Help and support button Link",
        Description = "The link for the Help and Support in the top navigation menu",
        GroupName = GroupNames.B2B_Business_Header,
        Order = 20)]
    [CultureSpecific]
    public virtual LinkItem HelpAndSupportButtonLink { get; set; }= new LinkItem();

    [Display(
    Name = "B2B My Salam Link",
    Description = "The link for the My Salam button B2B in the navigation menu",
    GroupName = GroupNames.B2B_Business_Header,
    Order = 20)]
    [CultureSpecific]
    public virtual LinkItem B2BMySalamLink { get; set; }
    = new LinkItem();


    [Display(
        Name = "B2B My Salam Icon",
        Description = "Select the icon for the My Salam button in the navigation menu.",
        GroupName = GroupNames.B2B_Business_Header,
        Order = 50)]
    [CultureSpecific]
    [UIHint(SalamUIHint.IconLibrary)]
    public virtual ContentReference? B2BMySalamIcon { get; set; }

    [Display(
        Name = "My Salam Link",
        Description = "The link for the My Salam button in the navigation menu",
        GroupName = GroupNames.Header,
        Order = 20)]
    [CultureSpecific]
    public virtual LinkItem MySalamLink { get; set; }
        = new LinkItem();

    [Display(
        Name = "My Salam Icon",
        Description = "Select the icon for the My Salam button in the navigation menu.",
        GroupName = GroupNames.Header,
        Order = 50)]
    [CultureSpecific]
    [UIHint(SalamUIHint.IconLibrary)]
    public virtual ContentReference? MySalamIcon { get; set; }

    //Footer Items 

    [Display(
        Name = "Footer Navigation Items",
        Description = "Content Area for holding footer items.",
        GroupName = GroupNames.Footer,
        Order = 10)]
    [AllowedTypes(new[] { typeof(NavigationItemCollectionBlock) })]
    public virtual ContentArea? FooterNavigation { get; set; }

    [Display(
        Name = "Social Media Links",
        Description = "The list of icon link items to be displayed.",
        GroupName = GroupNames.Footer,
        Order = 40)]
    [AllowedTypes(typeof(IconLinkItemBlock))]
    public virtual ContentArea? FooterSocialLinks { get; set; }

    [Display(
        Name = "Legal Links",
        Description = "The list of icon link items to be displayed.",
        GroupName = GroupNames.Footer,
        Order = 50)]
    [CultureSpecific]
    public virtual LinkItemCollection? FooterLegalLinks { get; set; }

    [Display(
        Name = "Copyright text",
        Description = "Copyright text.",
        GroupName = GroupNames.Footer,
        Order = 60)]
    [CultureSpecific]
    public virtual string? CopyrightText { get; set; }



    #region B2B Business

    [Display(
    Name = "Selected Product Counter",
    Description = "Selected Product Counter",
    GroupName = GroupNames.B2B_Business_Header,
    Order = 61)]
    [CultureSpecific]
    public virtual LinkItem? SelectedProductCounterr { get; set; }

    [Display(
    Name = "B2B Footer Navigation Items",
    Description = "B2B Content Area for holding footer items.",
    GroupName = GroupNames.B2B_Business_Footer,
    Order = 61)]
    [AllowedTypes(new[] { typeof(NavigationItemCollectionBlock) })]
    public virtual ContentArea? B2BFooterNavigation { get; set; }

    [Display(
        Name = "Social Media Links",
        Description = "The list of icon link items to be displayed.",
        GroupName = GroupNames.B2B_Business_Footer,
        Order = 62)]
    [AllowedTypes(typeof(IconLinkItemBlock))]
    public virtual ContentArea? B2BFooterSocialLinks { get; set; }

    [Display(
        Name = "B2B Legal Links",
        Description = "The list of icon link items to be displayed.",
        GroupName = GroupNames.B2B_Business_Footer,
        Order = 64)]
    [CultureSpecific]
    public virtual LinkItemCollection? B2BFooterLegalLinks { get; set; }

    [Display(
    Name = "B2B Copyright text",
    Description = "B2B Copyright text.",
    GroupName = GroupNames.B2B_Business_Footer,
    Order = 66)]
    [CultureSpecific]
    public virtual string? B2BCopyrightText { get; set; }

    [Display(
    Name = "Footer Body",
    Description = "Select the Footer body for the Page e.g Html for components",
    GroupName = GroupNames.B2B_Business_Footer,
    Order = 69)]
    [CultureSpecific]
    [UIHint(RichTextEditors.HtmlEditor)]
    public virtual XhtmlString? B2BFooterHTML { get; set; }

   [Display(
    Name = "Whole Sale Footer Body",
    Description = "Select the Footer body for the Page e.g Html for components",
    GroupName = GroupNames.WholeSale_Business_Footer,
    Order = 69)]
    [CultureSpecific]
    [UIHint(RichTextEditors.HtmlEditor)]
    public virtual XhtmlString? WholesaleFooterHTML { get; set; }


    [Display(
        Name = "WholeSale Social Media Links",
        Description = "The WholeSale list of icon link items to be displayed.",
        GroupName = GroupNames.WholeSale_Business_Footer,
        Order = 69)]
    [AllowedTypes(typeof(IconLinkItemBlock))]
    public virtual ContentArea? WholeSaleFooterSocialLinks { get; set; }

    [Display(
        Name = "WholeSale Legal Links",
        Description = "The WholeSale list of icon link items to be displayed.",
        GroupName = GroupNames.WholeSale_Business_Footer,
        Order = 69)]
    [CultureSpecific]
    public virtual LinkItemCollection? WholeSaleFooterLegalLinks { get; set; }

    [Display(
    Name = "WholeSale Copyright text",
    Description = "WholeSale Copyright text.",
    GroupName = GroupNames.WholeSale_Business_Footer,
    Order = 69)]
    [CultureSpecific]
    public virtual string? WholeSaleCopyrightText { get; set; }

    [Display(
            Name = "B2B From Email When no Template email is configured",
            Description = "B2B From Email When no Template email is configured",
            GroupName = GroupNames.B2B_Business_Email_Body,
            Order = 69)]
    public virtual string B2BFromEmail { get; set; }

    [Display(
        Name = "Template 1 Email Body",
        Description = "Template 1 Email Body",
        GroupName = GroupNames.B2B_Business_Email_Body,
        Order = 69)]
    [CultureSpecific]
    [UIHint(RichTextEditors.HtmlEditor)]
    public virtual XhtmlString? FreeConsultationForm { get; set; }

    [Display(
    Name = "Template 1 Subject",
    Description = "Template 1 Subject",
    GroupName = GroupNames.B2B_Business_Email_Body,
    Order = 69)]
    [CultureSpecific]
    public virtual string FreeConsultationForm_Subject { get; set; }

    [Display(
            Name = "Template 1 To Email",
            Description = "Template 1 To Email",
            GroupName = GroupNames.B2B_Business_Email_Body,
            Order = 69)]
    [CultureSpecific]
    public virtual string FreeConsultationForm_ToEmail { get; set; }

    [Display(
    Name = "Template 2 Email Body",
    Description = "Template 2 Email Body",
    GroupName = GroupNames.B2B_Business_Email_Body,
    Order = 69)]
    [CultureSpecific]
    [UIHint(RichTextEditors.HtmlEditor)]
    public virtual XhtmlString? ComplaintForm { get; set; }

    [Display(
   Name = "Template 2 Email Subject",
    Description = "Template 2 Email Subject",
    GroupName = GroupNames.B2B_Business_Email_Body,
    Order = 69)]
    [CultureSpecific]
    public virtual string ComplaintForm_Subject { get; set; }

    [Display(
   Name = "Template 2 ToEmail",
    Description = "Template 2 ToEmail",
    GroupName = GroupNames.B2B_Business_Email_Body,
    Order = 69)]
    [CultureSpecific]
    public virtual string ComplaintForm_ToEmail { get; set; }

    [Display(
        Name = "Template 3 Email Body",
        Description = "Template 3 Email Body",
        GroupName = GroupNames.B2B_Business_Email_Body,
        Order = 69)]
    [CultureSpecific]
    [UIHint(RichTextEditors.HtmlEditor)]
    public virtual XhtmlString? CallBackForm { get; set; }


    [Display(
    Name = "Template 3 Form Subject",
    Description = "Template 3 Form Subject",
    GroupName = GroupNames.B2B_Business_Email_Body,
    Order = 69)]
    [CultureSpecific]
    public virtual string CallBackForm_Subject { get; set; }

    [Display(
            Name = "Template 3 To Email",
            Description = "Template 3 To Email",
            GroupName = GroupNames.B2B_Business_Email_Body,
            Order = 69)]
    [CultureSpecific]
    public virtual string CallBackForm_ToEmail { get; set; }


    [Display(
    Name = "Template 4 Email Body",
    Description = "Template 4 Email Body",
    GroupName = GroupNames.B2B_Business_Email_Body,
    Order = 69)]
    [CultureSpecific]
    [UIHint(RichTextEditors.HtmlEditor)]
    public virtual XhtmlString? Template4FormBody { get; set; }

    [Display(
    Name = "Template 4 Form Subject",
    Description = "Template 4 Form Subject",
    GroupName = GroupNames.B2B_Business_Email_Body,
    Order = 69)]
    [CultureSpecific]
    public virtual string Template4Form_Subject { get; set; }

    [Display(
            Name = "Template 4 To Email",
            Description = "Template 4 To Email",
            GroupName = GroupNames.B2B_Business_Email_Body,
            Order = 69)]
    [CultureSpecific]
    public virtual string Template4Form_ToEmail { get; set; }


    [Display(
  Name = "Template 5 Email Body",
  Description = "Template 5 Email Body",
  GroupName = GroupNames.B2B_Business_Email_Body,
  Order = 69)]
    [CultureSpecific]
    [UIHint(RichTextEditors.HtmlEditor)]
    public virtual XhtmlString? Template5FormBody { get; set; }

    [Display(
    Name = "Template 5 Form Subject",
    Description = "Template 5 Form Subject",
    GroupName = GroupNames.B2B_Business_Email_Body,
    Order = 69)]
    [CultureSpecific]
    public virtual string Template5Form_Subject { get; set; }

    [Display(
            Name = "Template 5 To Email",
            Description = "Template 5 To Email",
            GroupName = GroupNames.B2B_Business_Email_Body,
            Order = 69)]
    [CultureSpecific]
    public virtual string Template5Form_ToEmail { get; set; }

    [Display(
        Name = "B2B Form Submission API Base URL",
        Description = "B2B Form Submission API Base URL",
        GroupName = GroupNames.B2B_Business_Email_Body,
        Order = 69)]
    public virtual string APIbaseURL { get; set; }

    #endregion
    //Support Block 

    [Display(
        Name = "Contact Information",
        Description = "Default contact information for the site.",
        GroupName = GroupNames.Contact,
        Order = 70)]
    [AllowedTypes(typeof(SupportBlock))]
    public virtual ContentArea? SupportContactContent { get; set; }

    //Site Strings

    [Display(
       Name = "Product Footer Text",
       Description = "Product Footer Text",
       GroupName = GroupNames.SiteStrings,
       Order = 10)]
    [CultureSpecific]
    public virtual string? ProductFooterText { get; set; }

    [Display(
        Name = "View All Link Text",
        Description = "Text displayed in view all links on the site.",
        GroupName = GroupNames.SiteStrings,
        Order = 20)]
    [CultureSpecific]
    public virtual string? ViewAllLinkText { get; set; } = string.Empty;

    [Display(
        Name = "Back Link Text",
        Description = "Text displayed in back links on the site.",
        GroupName = GroupNames.SiteStrings,
        Order = 30)]
    [CultureSpecific]
    public virtual string? BackLinkText { get; set; } = string.Empty;

    [Display(
        Name = "Languages Navigation Item Text",
        Description = "Text displayed in the navigation item for Languages.",
        GroupName = GroupNames.SiteStrings,
        Order = 40)]
    [CultureSpecific]
    public virtual string? LanguagesNavItemText { get; set; } = string.Empty;

    [Display(
    Name = "B2B Languages Navigation Item Text",
    Description = "Text displayed in the B2B  navigation item for Languages.",
    GroupName = GroupNames.SiteStrings,
    Order = 40)]
    [CultureSpecific]
    public virtual string? B2BLanguagesNavItemText { get; set; } = string.Empty;

    [Display(
    Name = "Salam Navigation Item Text",
    Description = "Text displayed in the navigation item for Salam.",
    GroupName = GroupNames.SiteStrings,
    Order = 50)]
    [CultureSpecific]
    public virtual string? SalamNavItemText { get; set; } = string.Empty;


    [Display(
    Name = "Salam Navigation Item Text",
    Description = "Text displayed in the navigation item for Salam.",
    GroupName = GroupNames.SiteStrings,
    Order = 55)]
    [CultureSpecific]
    public virtual IList<LinkItem>? SalamNavItems { get; set; }

    // Product Settings

    [Display(
        Name = "Product Button Text",
        Description = "Text displayed in the button on the Product Selector Block",
        GroupName = GroupNames.ProductSettings,
        Order = 10)]
    [CultureSpecific]
    public virtual string? ProductButtonText { get; set; } = string.Empty;

    [Display(
        Name = "Plan Details Button Text",
        Description = "Text displayed in the button on the Product Selector Block",
        GroupName = GroupNames.ProductSettings,
        Order = 20)]
    [CultureSpecific]
    public virtual string? PlanDetailsText { get; set; } = string.Empty;

    [Display(
        Name = "Plan Card Time Span",
        Description = "The time shown in the product blocks. eg: SAR/Month",
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
        Name = "Category Translations",
        Description = "Arabic text translations for categories",
        GroupName = GroupNames.ProductSettings,
        Order = 140)]
    [EditorDescriptor(EditorDescriptorType = typeof(CollectionEditorDescriptor<KeyValue>))]
    [UniqueKey]
    [CultureSpecific]
    public virtual IList<KeyValue> CategoryTranslations { get; set; }

    [Display(
    Name = "Cookies Banner Block",
    Description = "Cookies Banner Block",
    GroupName = GroupNames.CookiesBanner,
    Order = 150)]
    public virtual CookiesBannerBlock CookiesBannerBlock { get; set; }

    [Display(
        Name = "Is Cookie Banner needed",
        Description = "Is Cookie Banner needed",
        GroupName = GroupNames.Content,
        Order = 160)]
    public virtual bool HideCookieBanner { get; set; }

    [Display(
         Name = "Card List Block View",
         Description = "Card List Block View",
         GroupName = GroupNames.B2B_Business_Footer,
         Order = 170)]
    public virtual CardListBlock CardListBlockView { get; set; }

    [Display(
    Name = "Selected Product Remove Button B2B",
    Description = "Selected Product Remove Button B2B",
    GroupName = GroupNames.B2B_GeneralContent,
    Order = 190)]
    [CultureSpecific]
    public virtual LinkItem? RemoveProductBtn { get; set; }

    [Display(
            Name = "Save Product Enquire Message",
            Description = "Save Product Enquire Message",
            GroupName = GroupNames.B2B_GeneralContent,
            Order = 200)]
    [CultureSpecific]
    public virtual string? SaveProductEnquireMessage { get; set; }


    [Display(
            Name = "No Product Selected",
            Description = "No Product Selected",
            GroupName = GroupNames.B2B_GeneralContent,
            Order = 201)]
    [CultureSpecific]
    public virtual string? NoProductSelectedMessage { get; set; }

    [Display(
        Name = "Remove Product Enquire Message",
        Description = "Remove Product Enquire Message",
        GroupName = GroupNames.B2B_GeneralContent,
        Order = 200)]
    [CultureSpecific]
    public virtual string? RemoveProductEnquireMessage { get; set; }

    [Display(
    Name = "Product Already Exist Message",
    Description = "Product Already Exist Message",
    GroupName = GroupNames.B2B_GeneralContent,
    Order = 210)]
    [CultureSpecific]
    public virtual string? ProductAlreadyExistMessage { get; set; }

    [Display(
    Name = "Maximum Product Enquire Limit",
    Description = "Maximum Product Enquire Limit",
    GroupName = GroupNames.B2B_GeneralContent,
    Order = 220)]
    [CultureSpecific]
    public virtual int? MaxProductEnquireLimit { get; set; }

    [Display(
        Name = "Product Limit Message",
        Description = "Product Limit Message",
        GroupName = GroupNames.B2B_GeneralContent,
        Order = 230)]
    [CultureSpecific]
    public virtual string? ProductLimitMessage { get; set; }


    [Display(
        Name = "Product Enquire Redirect Page URL",
        Description = "Product Enquire Redirect Page URL",
        GroupName = GroupNames.B2B_GeneralContent,
        Order = 240)]
    [CultureSpecific]
    public virtual LinkItem? RedirectProductEnquirePageURL { get; set; }

    [Display(
        Name = "Product Enquire Back Page URL",
        Description = "Product Enquire Back Page URL",
        GroupName = GroupNames.B2B_GeneralContent,
        Order = 240)]
    [CultureSpecific]
    public virtual LinkItem? BackPageRedirectURL { get; set; }

    [Display(
    Name = "Redirect Rule Block DXP SLug",
    Description = "Redirect Rule Block DXP SLug",
    GroupName = GroupNames.RedirectURL,
    Order = 250)]
    public virtual IList<RedirecttRuleBlock>? RedirectRuleBlockDXPSlug { get; set; }

    [Display(
            Name = "Redirect Rule Block",
            Description = "Redirect Rule Block",
            GroupName = GroupNames.RedirectURL,
            Order = 250)]
    public virtual IList<RedirectRuleBlock>? RedirectRuleBlock { get; set; }

    [Display(
        Name = "Redirect Rule DXP Slug",
        Description = "Redirect Rule DXP Slug",
        GroupName = GroupNames.RedirectURL,
        Order = 290)]
    public virtual bool RedirectRuleDxpSlug { get; set; }

    [Display(
            Name = "Redirect Should Apply URL List",
            Description = "Redirect Should Apply URL List",
            GroupName = GroupNames.RedirectURL,
            Order = 250)]
    public virtual IList<string>?  RedirectShouldApplyURL { get; set; }

    [Display(
        Name = "Redirect Should Not Apply URL List",
        Description = "Redirect Should Not Apply URL List",
        GroupName = GroupNames.RedirectURL,
        Order = 250)]
    public virtual IList<string>? RedirectShouldNotApplyURL { get; set; }


    [Display(
            Name = "B2B From Email Solution Form",
            Description = "B2B From Email Solution Form",
            GroupName = GroupNames.B2B_Business_Email_Body,
            Order = 251)]
    public virtual string B2EmailSolutionForm { get; set; }

    [Display(
        Name = "Solution Form  Email Body",
        Description = "Solution Form  Email Body",
        GroupName = GroupNames.B2B_Business_Email_Body,
        Order = 252)]
    [CultureSpecific]
    [UIHint(RichTextEditors.HtmlEditor)]
    public virtual XhtmlString? SolutionFormHtml { get; set; }

    [Display(
    Name = "Solution Email Form Subject",
    Description = "Solution Email Form Subject",
    GroupName = GroupNames.B2B_Business_Email_Body,
    Order = 255)]
    [CultureSpecific]
    public virtual string SolutionEmailForm_Subject { get; set; }

    [Display(
        Name = "Solution To Email",
        Description = "Solution To Email",
        GroupName = GroupNames.B2B_Business_Email_Body,
        Order = 69)]
    [CultureSpecific]
    public virtual string SolutionEmailForm_ToEmail { get; set; }

    [CultureSpecific]
    [Display(
    Name = "Canonical Base URL",
    GroupName = SystemTabNames.Settings,
    Order = 900)]
    public virtual string CanonicalBaseUrl { get; set; }

    [CultureSpecific]
    [Display(
    Name = "Hreflang Initial URL",
    GroupName = SystemTabNames.Settings,
    Order = 910)]
    public virtual string HreflangInitialUrl { get; set; }

    [CultureSpecific]
    [Display(
        Name = "Hreflang Initial URL (AR)",
        GroupName = SystemTabNames.Settings,
        Order = 920)]
    public virtual string HreflangInitialUrlAr { get; set; }

    [Display(
        Name = "DXP Slug Optimizely Source List",
        Description = "DXP Slug Optimizely Source List",
        GroupName = GroupNames.Settings,
        Order = 980)]
    public virtual IList<string>? DXPSlugOptimizelySource { get; set; }
}
