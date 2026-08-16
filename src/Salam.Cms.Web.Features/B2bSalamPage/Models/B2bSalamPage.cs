namespace Salam.Cms.Web.Features.B2bSalamPage.Models;

using EPiServer.Core;
using EPiServer.DataAnnotations;
using Geta.Optimizely.ContentTypeIcons;
using Geta.Optimizely.ContentTypeIcons.Attributes;
using Salam.Cms.Shared.Models.Common;
using Salam.Cms.Shared.Models.Common.RichText;
using Salam.Cms.Shared.Models.Pages;
using Salam.Cms.Web.Features.Common.Interfaces;
using Salam.Cms.Web.Features.Common.Models;
using System.ComponentModel.DataAnnotations;

[ContentType(
    DisplayName = "Business Salam Footer Pages",
    Description = " A Business Salam Footer page is a page that is designed to be the first point of contact for B2b Supports." +
    " It typically contains only content without Header and Footer",
    GUID = "2e8c76e6-7948-4f83-9ddc-2f46cd8d3334",
    GroupName = GroupNames.Content)]
[ContentTypeIcon(FontAwesome5Solid.MapSigns)]
public class B2bSalamPage : B2BSitePageData, INavigationItem, IPageNavigatorEnabled
{
    [Display(
       Name = "Start Page Main Content",
       Description = "A content area that allows blocks that have been specifically designed as section content.",
       GroupName = GroupNames.Content,
       Order = 20)]
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
        Name = "Enable Page Navigator",
        Description = "Toggle the page navigator, which links to sections of the main content area. The Page Navigator Title on the block will serve as the link text.",
        GroupName = GroupNames.Navigation,
        Order = 40)]
    public virtual bool EnablePageNavigator { get; set; }

    [Display(
    Name = "End Page Main Content",
    Description = "A content area that allows blocks that have been specifically designed as section content.",
    GroupName = GroupNames.Content,
    Order = 50)]
    [AllowedTypes(typeof(ISiteContentBlock))]
    public virtual ContentArea? EndPageMainContent { get; set; }
}