namespace Salam.Cms.Web.Features.Common.Models;

using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Shell.ObjectEditing;
using EPiServer.Web;
using Geta.Optimizely.Sitemaps.SpecializedProperties;
using Salam.Cms.Shared.Models.Common;
using Salam.Cms.Shared.Models.Media;
using Salam.Cms.Shared.Models.Pages;
using Salam.Cms.Web.Features.Common.Components.MetaData;
using Salam.Cms.Web.Features.Common.Interfaces;
using Salam.Cms.Web.Features.Hero.Abstract;
using Salam.Cms.Web.Features.Support.Models;
using System.ComponentModel.DataAnnotations;

public abstract class SitePageData : PageData, ISitePageData, ISeoMetaData, INavigationItem
{
    [Display(
        Name = "Hero Content",
        Description = "A specific area for hero blocks which will be rendered immediately below the navigation elements.",
        GroupName = GroupNames.Content,
        Order = 4)]
    [Searchable]
    [AllowedTypes(typeof(IHeroBlock))]
    public virtual ContentArea? HeroArea { get; set; }

    [Display(
        Name = "Heading",
        Description = "The page title to show in the hero area as a fallback for when a hero has not been provided.",
        GroupName = GroupNames.Content,
        Order = 5)]
    [Searchable]
    [CultureSpecific]
    public virtual string? Heading { get; set; }

    [Display(
        Name = "Short Page Name",
        Description = "Defines the page title for use in the breadcrumb trail and site-maps. Where not specified the PageName attribute is used",
        GroupName = GroupNames.Content,
        Order = 9)]
    [Searchable]
    [CultureSpecific]
    public virtual string? ShortPageName { get; set; }

    [Display(
        Name = "Mobile Navigation Page Name",
        Description = "Defines the Name of the mobile navigation Product Landing Page link",
        GroupName = GroupNames.Content,
        Order = 10)]
    [Searchable]
    [CultureSpecific]
    public virtual string? MobileName { get; set; }

    [Display(
    Name = "New Page Title",
    Description = "New Page Title",
    GroupName = GroupNames.Content,
    Order = 15)]
    [CultureSpecific]
    public virtual string? NewPageTitle { get; set; }

    [Display(
    Name = "Sort Order for the Navigations",
    Description = "Sort Order for the Navigations",
    GroupName = GroupNames.Settings,
    Order = 310)]
    [CultureSpecific]
    public virtual int SortingOrder { get; set; }
    
    [Display(
        Name = "SEO Title",
        Description = "A meta title to be rendered within the page's title",
        GroupName = GroupNames.SearchEngineOptimization,
        Order = 100)]
    [CultureSpecific]
    public virtual string? MetaTitle { get; set; }

    [Display(
        Name = "SEO Description",
        Description = "The meta description of the page to be shown in search results.",
        GroupName = GroupNames.SearchEngineOptimization,
        Order = 110)]
    [CultureSpecific]
    public virtual string? MetaDescription { get; set; }

    [Display(
        Name = "Social Sharing Title",
        Description = "The title of your content as it should appear when previewed in social media.",
        GroupName = GroupNames.SocialSharing,
        Order = 115)]
    [CultureSpecific]
    public virtual string? SocialSharingTitle { get; set; }

    [Display(
        Name = "Social Sharing Description",
        Description = "A description that concisely summarizes the content when previewed in social media.",
        GroupName = GroupNames.SocialSharing,
        Order = 116)]
    [CultureSpecific]
    public virtual string? SocialSharingDescription { get; set; }

    [Display(
        Name = "Social Sharing Image",
        Description = "The image to displayed when previewing in social media.",
        GroupName = GroupNames.SocialSharing,
        Order = 117)]
    [UIHint(UIHint.Image)]
    [CultureSpecific]
    [AllowedTypes(typeof(ImageContent))]
    public virtual ContentReference? SocialSharingImage { get; set; }

    [Display(
        Name = "Social Sharing Image Alt Text",
        Description = "The alt text for the image when previewing in social media.",
        GroupName = GroupNames.SocialSharing,
        Order = 118)]
    [CultureSpecific]
    public virtual string? SocialSharingImageAltText { get; set; }

    [Display(
        Name = "Content Creator X (Twitter) Account",
        Description = "The X (Twitter) account for author/creator of this content.",
        GroupName = GroupNames.SocialSharing,
        Order = 119)]
    [CultureSpecific]
    public virtual string? TwitterCardCreator { get; set; }

    [Display(
        Name = "SEO Robots",
        Description = "Page meta robots tag value.",
        GroupName = GroupNames.SearchEngineOptimization,
        Order = 120)]
    [SelectOne(SelectionFactoryType = typeof(MetaRobotsSelectionFactory))]
    public virtual string? MetaRobots { get; set; }

    [Display(
        Name = "Alternate Canonical Url",
        Description = "The content page to define as the Canonical Reference for this page. If no page is selected, this page will refer to itself as it's Canonical Reference.",
        GroupName = GroupNames.SearchEngineOptimization,
        Order = 130)]
    [AllowedTypes(typeof(SitePageData))]
    [CultureSpecific]
    public virtual ContentReference? AlternateCanonicalLink { get; set; }

    [Display(
        Name = "Sitemap Values",
        Description = "Indicates whether to include this page in the XML sitemap, and set the change frequency and priority.",
        GroupName = GroupNames.SearchEngineOptimization,
        Order = 200)]
    [UIHint("SeoSitemap")]
    [BackingType(typeof(PropertySEOSitemaps))]
    public virtual string? SEOSitemaps { get; set; }

    [Display(
        Name = "Include in HTML Sitemap",
        Description = "Indicates whether to include this page in the HTML Sitemap.",
        GroupName = GroupNames.SearchEngineOptimization,
        Order = 210)]
    public virtual bool IncludeInHtmlSitemap { get; set; }

    [Display(
        Order = 300,
        Name = "Exclude From Search Results",
        Description = "Defines if content is excluded from Optimizely Search & Navigation queries.",
        GroupName = GroupNames.Settings)]

    public virtual bool ExcludeFromSearchResults { get; set; }

    [ScaffoldColumn(false)]
    public virtual bool RenderAlternativeLinks => true;

    [Display(
        Name = "Icon",
        Description = "The icon to be used for the page.",
        GroupName = GroupNames.Header,
        Order = 10)]
    [UIHint(UIHint.Image)]
    public virtual ContentReference? Icon { get; set; }

    [Display(
        Name = "Support Contact Content",
        Description = "A content area that allows blocks that have been specifically designed as support contact content.",
        GroupName = GroupNames.Contact,
        Order = 10)]
    [Searchable]
    [AllowedTypes(typeof(SupportBlock))]
    public virtual ContentArea? SupportContactContent { get; set; }

    [Display(
        Name = "Hide Support Contact Content",
        Description = "When ticked, the support contact content will not be displayed for this page.",
        GroupName = GroupNames.Contact,
        Order = 20)]

    public virtual bool HideSupportContactContent { get; set; }

    [Display(
    Name = "Top Navigation Order",
    Description = "Top Navigation Order",
    GroupName = GroupNames.NavigationBusinessSettings,
    Order = 200)]
    public virtual int? TopNavigationLinkOrder { get; set; }

    [Display(
            Name = "Navigation Override Name",
            Description = "Navigation Override Name",
            GroupName = GroupNames.NavigationBusinessSettings,
            Order = 200)]
    public virtual string? NavigationOverRideName { get; set; }

    public override void SetDefaultValues(ContentType contentType)
    {
        base.SetDefaultValues(contentType);

        ExcludeFromSearchResults = false;
        IncludeInHtmlSitemap = true;

        var siteMap = new PropertySEOSitemaps
        {
            Enabled = true
        };

        siteMap.Serialize();
        SEOSitemaps = siteMap.ToString();
    }
}