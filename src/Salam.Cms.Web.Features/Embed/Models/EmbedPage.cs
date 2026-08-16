namespace Salam.Cms.Web.Features.Embed.Models;

using EPiServer.Core;
using EPiServer.DataAnnotations;
using EPiServer.Shell.ObjectEditing;
using Geta.Optimizely.ContentTypeIcons;
using Geta.Optimizely.ContentTypeIcons.Attributes;
using Salam.Cms.Shared.Models.Common;
using Salam.Cms.Web.Features.Common.Models;
using Stott.Security.Optimizely.Features.Pages;
using System.ComponentModel.DataAnnotations;

[ContentType(
    DisplayName = "Embed Page",
    Description = "A flexible page allowing the use of embedded HTML, JS and CSS alongside content blocks.",
    GUID = "F89D06DB-3532-45F8-AA02-73D0A4A5DADD",
    GroupName = GroupNames.EmbedCode,
    AvailableInEditMode = false)]

[ContentTypeIcon(FontAwesome5Solid.Code)]
public class EmbedPage : SitePageData, IContentSecurityPolicyPage
{
    [Display(
        Name = "Main Content",
        Description = "A content area that allows blocks that have been specifically designed as content or as embedded content.",
        GroupName = GroupNames.Content,
        Order = 20)]
    [AllowedTypes(typeof(EmbedBlock))]
    public virtual ContentArea? MainContent { get; set; }

    [Display(
        Name = "Hide Hero Section",
        Description = "When ticked, the hero section will not be displayed for this page.",
        GroupName = GroupNames.Content,
        Order = 40)]
    public virtual bool HideHeroSection { get; set; }

    [Display(
        Name = "Hide Footer Section",
        Description = "When ticked, the footer section will not be displayed for this page.",
        GroupName = GroupNames.Content,
        Order = 50)]
    public virtual bool HideFooterSection { get; set; }

    [Display(
        Name = "Content Security Policy Sources",
        Description = "The following Content Security Policy Sources will be merged into the global Content Security Policy when visiting this page.",
        GroupName = GroupNames.Security,
        Order = 90)]
    [EditorDescriptor(EditorDescriptorType = typeof(CspSourceMappingEditorDescriptor))]
    public virtual IList<PageCspSourceMapping> ContentSecurityPolicySources { get; set; } = new List<PageCspSourceMapping>();
}