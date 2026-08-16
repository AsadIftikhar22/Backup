namespace Salam.Cms.Web.Features.Common.Components.StickyBanner.Models;

using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Web;
using Geta.Optimizely.ContentTypeIcons;
using Geta.Optimizely.ContentTypeIcons.Attributes;
using Salam.Cms.Shared.Models.Common;
using Salam.Cms.Web.Features.Common.Models;
using System.ComponentModel.DataAnnotations;

[ContentType(
    DisplayName = "Sticky Banner Block",
    GUID = "2e3a66d3-56bf-4d90-af44-d772e986ebb1",
    Description = "Displays a sticky banner block.",
    GroupName = SystemTabNames.Content)]
[ContentTypeIcon(FontAwesome5Solid.List)]
public class StickyBannerBlock : SiteContentBlock
{
    [Display(
            Name = "Sticky Banner heading",
            Description = "Sticky Banner heading",
            GroupName = GroupNames.Content,
            Order = 10)]
    public virtual string headline { get; set; }

    [Display(
            Name = "Sticky Banner Sub heading",
            Description = "Sticky Banner Sub heading",
            GroupName = GroupNames.Content,
            Order = 20)]
    public virtual string subheadline { get; set; }

    [Display(
         Name = "Sticky Banner Sub heading Terms",
         Description = "Sticky Banner Sub heading Terms",
         GroupName = GroupNames.Content,
         Order = 25)]
    public virtual string TermsAndCondition { get; set; }

    [Display(
            Name = "Sticky Banner CTA Text",
            Description = "Sticky Banner CTA Text",
            GroupName = GroupNames.Content,
            Order = 30)]
    public virtual string ctaText { get; set; }

    [Display(
        Name = "Sticky Banner CTA URL",
        Description = "Sticky Banner CTA URL",
        GroupName = GroupNames.Content,
        Order = 30)]
    public virtual string ctaURL { get; set; }

    [Display(
            Name = "Sticky Banner Background Image",
            Description = "Select the media for the Sticky Banner Background Image.",
            GroupName = GroupNames.Content,
            Order = 40)]
    [CultureSpecific]
    [UIHint(UIHint.Image)]
    public virtual ContentReference? bgImageSrc { get; set; }

    [Display(
            Name = "Sticky Banner Logo Image",
            Description = "Select the media for the Sticky Banner Logo Image.",
            GroupName = GroupNames.Content,
            Order = 50)]
    [CultureSpecific]
    [UIHint(UIHint.Image)]
    public virtual ContentReference? logoSrc { get; set; }


    [Display(
            Name = "Sticky Banner Powered Logo Image",
            Description = "Select the media for the Sticky Banner Powered Logo Image.",
            GroupName = GroupNames.Content,
            Order = 60)]
    [CultureSpecific]
    [UIHint(UIHint.Image)]
    public virtual ContentReference? poweredByLogoSrc { get; set; }

    [Display(
        Name = "Sticky Banner Powered By Text",
        Description = "Select the media for the Sticky Banner Powered By Text.",
        GroupName = GroupNames.Content,
        Order = 70)]
    [CultureSpecific]
    [UIHint(UIHint.Image)]
    public virtual string? poweredByText { get; set; }


}