namespace Salam.Cms.Shared.Models.Common;

using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Security;
using System.ComponentModel.DataAnnotations;

[GroupDefinitions]
public static class GroupNames
{
    [Display(Order = 10)]
    public const string Content = SystemTabNames.Content;

    [Display(Order = 12)]
    public const string SelectedProducts = "Selected Products";

    [Display(Order = 14)]
    public const string ProductSettings = "Product Settings";

    [Display(Order = 15)]
    public const string ProductSelector = "Product Selector";

    [Display(Order = 16)]
    public const string ProductSummary = "Product Summary";

    [Display(Order = 17)]
    public const string VisitorProductSelectorOverride = "Visitor Product Selector OverRide";

    [Display(Order = 20)]
    public const string Filter = "Filter";

    [Display(Order = 30)]
    public const string Teaser = "Teaser";

    [Display(Order = 40)]
    public const string SearchEngineOptimization = "SEO";

    [Display(Order = 50)]
    public const string SocialSharing = "Social Media";

    [Display(Order = 60)]
    public const string Header = "Header";

    [Display(Order = 70)]
    public const string Footer = "Footer";

    [Display(Order = 75)]
    public const string Navigation = "Navigation";

    [Display(Order = 80)]
    public const string Search = "Search";

    [Display(Order = 85)]
    public const string Contact = "Contact";

    [Display(Order = 90)]
    public const string EmbedCode = "Embed Code";

    [Display(Order = 100)]
    public const string DataAnalytics = "Data Analytics";

    [RequiredAccess(AccessLevel.Administer)]
    [Display(Order = 110)]
    public const string Security = "Security";

    [Display(Order = 115)]
    public const string Email = "Email";

    [Display(Order = 120)]
    public const string RssConfiguration = "RSS Configuration";

    [Display(Order = 125)]
    public const string Carousel = "Carousel";

    [Display(Order = 130)]
    public const string Section = "Section Settings";

    [Display(Order = 135)]
    public const string AlternateDisplay = "Alternate Display";

    [Display(Order = 140)]
    public const string Specialized = "Specialized";

    [Display(Order = 150)]
    public const string SiteStrings = "Site Strings";

    [Display(Order = 999)]
    public const string Settings = SystemTabNames.Settings;

    [Display(Order = 998)]
    public const string RedirectURL = "Redirect URL";

    [Display(Order = 1000)]
    public const string B2B_Business_Footer = "Business Footer Settings";

    [Display(Order = 1000)]
    public const string B2B_GeneralContent = "Business General Setting Content";

    [Display(Order = 1001)]
    public const string B2B_Business_Email_Body = "B2B Business Email Body";

    [Display(Order = 1002)]
    public const string B2B_Call_Back_Form = "B2B Call Back Form";

    [Display(Order = 1003)]
    public const string B2B_Free_Consultation_Form = "B2B Free Consultation Form";

    [Display(Order = 1004)]
    public const string WholeSale_Business_Footer = "WholeSale Footer Settings";

    [Display(Order = 1005)]
    public const string CookiesBanner = "Cookies Banner";

    [Display(Order = 1006)]
    public const string B2B_Business_Header = "Business Header Settings";

    [Display(Order = 1007)]
    public const string NavigationBusinessSettings = "Top Navigation Business Settings";

    [Display(Order = 1008)]
    public const string SolutionsBlockIcons = "Solutions Block Icons";

    [Display(Order = 1009)]
    public const string BusinessComponentTab = "Business Component Tab";
}
