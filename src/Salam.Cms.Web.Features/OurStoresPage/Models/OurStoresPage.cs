namespace Salam.Cms.Web.Features.OurStoresPage.Models;

using EPiServer.Core;
using EPiServer.DataAnnotations;
using Geta.Optimizely.ContentTypeIcons;
using Geta.Optimizely.ContentTypeIcons.Attributes;
using Salam.Cms.Shared.Models.Common;
using Salam.Cms.Shared.Models.Common.RichText;
using Salam.Cms.Web.Features.Common.Interfaces;
using Salam.Cms.Web.Features.Common.Models;
using System.ComponentModel.DataAnnotations;

[ContentType(
    DisplayName = "Our Stores Page",
    Description = "Page for Our Stores locations with optional custom CSS and HTML.",
    GUID = "E824A9D0-F1B2-4C3D-5E6F-7A8B9C0D1E2F",
    GroupName = GroupNames.Content)]
[ContentTypeIcon(FontAwesome5Solid.MapMarkerAlt)]
public class OurStoresPage : SitePageData
{
    [Display(
        Name = "Main Content",
        Description = "Add Our Stores section block.",
        GroupName = GroupNames.Content,
        Order = 20)]
    [AllowedTypes(typeof(ISiteContentBlock))]
    public virtual ContentArea? MainContent { get; set; }

    [Display(
        Name = "Custom CSS",
        Description = "Optional custom CSS for this page.",
        GroupName = GroupNames.Content,
        Order = 25)]
    [CultureSpecific]
    [UIHint(RichTextEditors.HtmlEditor)]
    public virtual XhtmlString? CustomCss { get; set; }

    [Display(
        Name = "Custom HTML",
        Description = "Optional custom HTML body content.",
        GroupName = GroupNames.Content,
        Order = 30)]
    [CultureSpecific]
    [UIHint(RichTextEditors.HtmlEditor)]
    public virtual XhtmlString? CustomHtml { get; set; }
}
