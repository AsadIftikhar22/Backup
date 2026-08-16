namespace Salam.Cms.Web.Features.TelecomItRightsPage.Models;

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
    DisplayName = "Telecom and IT User Rights Regulations Page",
    Description = "Page for Telecom and IT User Rights Regulations with optional custom CSS and HTML.",
    GUID = "B703A8C9-D0E1-4F2A-3B4C-6D7E8F901425",
    GroupName = GroupNames.Content)]
[ContentTypeIcon(FontAwesome5Solid.Gavel)]
public class TelecomItRightsPage : SitePageData
{
    [Display(
        Name = "Main Content",
        Description = "Add Telecom/IT Rights section blocks.",
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
