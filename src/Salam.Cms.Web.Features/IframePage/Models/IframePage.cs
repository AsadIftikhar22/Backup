namespace Salam.Cms.Web.Features.Home.Models;

using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.SpecializedProperties;
using Geta.Optimizely.ContentTypeIcons;
using Geta.Optimizely.ContentTypeIcons.Attributes;
using Salam.Cms.Shared.Models.Common;
using Salam.Cms.Shared.Models.Common.RichText;
using Salam.Cms.Web.Features.Common.Models;
using System.ComponentModel.DataAnnotations;

[ContentType(
    DisplayName = "IFrame Page",
    GUID = "475536f7-3193-4d63-883e-57b6b73e7674",
    Description = "A page designed as a IFrame page.",
    GroupName = GroupNames.Content)]
[ContentTypeIcon(FontAwesome.FlagO)]
public class IframePage : SitePageData
{
    [Display(
        Name = "Main Content",
        Description = "A content area that allows blocks that have been specifically designed as section content.",
        GroupName = GroupNames.Content,
        Order = 10)]
    public virtual ContentArea? MainContent { get; set; }

    [Display(
    Name = "CSS for the pages",
    Description = "Select the main css for the Page",
    GroupName = GroupNames.Content,
    Order = 25)]
    [CultureSpecific]
    [UIHint(RichTextEditors.HtmlEditor)]
    public virtual XhtmlString? HTMLCSS { get; set; }

    [Display(
    Name = "Main Body",
    Description = "Select the main body for the Page e.g Html for components",
    GroupName = GroupNames.Content,
    Order = 30)]
    [CultureSpecific]
    [UIHint(RichTextEditors.HtmlEditor)]
    public virtual XhtmlString? HTMLBody { get; set; }

    [Display(
        Name = "Media",
        Description = "The image or video to display in the call to action block.",
        GroupName = GroupNames.Content,
        Order = 20)]
    [CultureSpecific]
    public virtual ContentReference? Media { get; set; }

    [Display(
    Name = "Image Redirect URL",
    Description = "Image Redirect URL",
    GroupName = GroupNames.Content,
    Order = 20)]
    [CultureSpecific]
    public virtual LinkItem? ImageRedirectURL { get; set; }
}