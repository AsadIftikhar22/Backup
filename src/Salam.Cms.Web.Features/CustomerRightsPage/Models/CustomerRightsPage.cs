namespace Salam.Cms.Web.Features.CustomerRightsPage.Models;

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
    DisplayName = "Customer Rights Page",
    Description = "Page for Customer Rights and Responsibilities with optional custom CSS and HTML.",
    GUID = "D501E6F7-B8C9-4A0D-1E2F-3A4B5C6D7E8F",
    GroupName = GroupNames.Content)]
[ContentTypeIcon(FontAwesome5Solid.UserShield)]
public class CustomerRightsPage : SitePageData
{
    [Display(
        Name = "Main Content",
        Description = "Add Customer Rights section blocks and other content.",
        GroupName = GroupNames.Content,
        Order = 20)]
    [AllowedTypes(typeof(ISiteContentBlock))]
    public virtual ContentArea? MainContent { get; set; }

    [Display(
        Name = "Custom CSS",
        Description = "Optional custom CSS for this page. Use to design/format content.",
        GroupName = GroupNames.Content,
        Order = 25)]
    [CultureSpecific]
    [UIHint(RichTextEditors.HtmlEditor)]
    public virtual XhtmlString? CustomCss { get; set; }

    [Display(
        Name = "Custom HTML",
        Description = "Optional custom HTML body content (e.g. extra sections).",
        GroupName = GroupNames.Content,
        Order = 30)]
    [CultureSpecific]
    [UIHint(RichTextEditors.HtmlEditor)]
    public virtual XhtmlString? CustomHtml { get; set; }
}
