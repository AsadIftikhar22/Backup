namespace Salam.Cms.Web.Features.UserRightsPage.Models;

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
    DisplayName = "User Rights Page",
    Description = "Page for User Rights & Responsibilities with optional custom CSS and HTML.",
    GUID = "A602F7B8-C9D0-4E1F-2A3B-5C6D7E8F9013",
    GroupName = GroupNames.Content)]
[ContentTypeIcon(FontAwesome5Solid.UserCheck)]
public class UserRightsPage : SitePageData
{
    [Display(
        Name = "Main Content",
        Description = "Add User Rights section blocks and other content.",
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
