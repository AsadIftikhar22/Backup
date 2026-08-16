namespace Salam.Cms.Web.Features.Showcase.Models;

using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Web;
using Geta.Optimizely.ContentTypeIcons;
using Geta.Optimizely.ContentTypeIcons.Attributes;
using Salam.Cms.Shared.Models.Common;
using Salam.Cms.Shared.Models.Common.RichText;
using Salam.Cms.Web.Features.Common.Models;
using System.ComponentModel.DataAnnotations;

[ContentType(
    DisplayName = "Cookies Banner Block",
    GUID = "d22fa085-1984-4bd2-9395-0ef7b421bcff",
    Description = "Displays a Cookie banner.",
    GroupName = SystemTabNames.Content)]
[ContentTypeIcon(FontAwesome5Solid.List)]
public class CookiesBannerBlock : SiteBlockData
{

    [Display(
        Name = "Cookie Banner Heading",
        Description = "Cookie Banner Heading",
        GroupName = GroupNames.Content,
        Order = 10)]
    [CultureSpecific]
    public virtual string? CookieBannerHeading { get; set; }

    [Display(
        Name = "Cookie Banner Description",
        Description = "Main paragraph text below the heading",
        GroupName = GroupNames.Content,
        Order = 20)]
    [UIHint(RichTextEditors.ReducedEditor)]
    [CultureSpecific]
    public virtual string? CookieBannerDescription { get; set; }

    [Display(
        Name = "Reject Button Text",
        Description = "Text for the 'Reject All' button",
        GroupName = GroupNames.Content,
        Order = 30)]
    [CultureSpecific]
    public virtual string? RejectButtonText { get; set; }

    [Display(
        Name = "Customize Button Text",
        Description = "Text for the 'Customize Settings' button",
        GroupName = GroupNames.Content,
        Order = 40)]
    [CultureSpecific]
    public virtual string? CustomizeButtonText { get; set; }

    [Display(
        Name = "Accept Button Text",
        Description = "Text for the 'Accept All' button",
        GroupName = GroupNames.Content,
        Order = 50)]
    [CultureSpecific]
    public virtual string? AcceptButtonText { get; set; }

    [Display(
        Name = "Preferences Heading",
        Description = "Heading for the cookie preferences popup",
        GroupName = GroupNames.Content,
        Order = 60)]
    [CultureSpecific]
    public virtual string? PreferencesHeading { get; set; }

    [Display(
        Name = "Essential Cookies Title",
        Description = "Label for essential cookies option",
        GroupName = GroupNames.Content,
        Order = 70)]
    [CultureSpecific]
    public virtual string? EssentialCookiesTitle { get; set; }

    [Display(
    Name = "Essential Cookies Sub Title",
    Description = "Label for essential cookies option",
    GroupName = GroupNames.Content,
    Order = 75)]
    [CultureSpecific]
    public virtual string? EssentialCookiesSubTitle { get; set; }

    [Display(
        Name = "Essential Cookies Description",
        Description = "Description text for essential cookies",
        GroupName = GroupNames.Content,
        Order = 80)]
    [UIHint(RichTextEditors.ReducedEditor)]
    [CultureSpecific]
    public virtual string? EssentialCookiesDescription { get; set; }

    [Display(
        Name = "Analytics Cookies Title",
        Description = "Label for analytics cookies option",
        GroupName = GroupNames.Content,
        Order = 90)]
    [CultureSpecific]
    public virtual string? AnalyticsCookiesTitle { get; set; }

    [Display(
        Name = "Analytics Cookies Description",
        Description = "Description text for analytics cookies",
        GroupName = GroupNames.Content,
        Order = 100)]
    [UIHint(RichTextEditors.ReducedEditor)]
    [CultureSpecific]
    public virtual string? AnalyticsCookiesDescription { get; set; }

    [Display(
        Name = "Marketing Cookies Title",
        Description = "Label for marketing cookies option",
        GroupName = GroupNames.Content,
        Order = 110)]
    [CultureSpecific]
    public virtual string? MarketingCookiesTitle { get; set; }

    [Display(
        Name = "Marketing Cookies Description",
        Description = "Description text for marketing cookies",
        GroupName = GroupNames.Content,
        Order = 120)]
    [UIHint(RichTextEditors.ReducedEditor)]
    [CultureSpecific]
    public virtual string? MarketingCookiesDescription { get; set; }

    [Display(
        Name = "Reject All Preferences Button Text",
        Description = "Text for the reject all button in preferences popup",
        GroupName = GroupNames.Content,
        Order = 130)]
    [CultureSpecific]
    public virtual string? RejectAllPreferencesText { get; set; }

    [Display(
        Name = "Save Preferences Button Text",
        Description = "Text for the save preferences button",
        GroupName = GroupNames.Content,
        Order = 140)]
    [CultureSpecific]
    public virtual string? SavePreferencesText { get; set; }

    [Display(
        Name = "Accept All Preferences Button Text",
        Description = "Text for the accept all button in preferences popup",
        GroupName = GroupNames.Content,
        Order = 150)]
    [CultureSpecific]
    public virtual string? AcceptAllPreferencesText { get; set; }

}