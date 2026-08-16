namespace Salam.Cms.Web.Features.B2bSearchPage.Models;

using EPiServer.Core;
using EPiServer.DataAnnotations;
using Geta.Optimizely.ContentTypeIcons;
using Geta.Optimizely.ContentTypeIcons.Attributes;
using Salam.Cms.Shared.Models.Common;
using Salam.Cms.Shared.Models.Common.RichText;
using Salam.Cms.Shared.Models.Pages;
using Salam.Cms.Shared.Models.Pages.Container;
using Salam.Cms.Web.Features.Common.Interfaces;
using Salam.Cms.Web.Features.Common.Models;
using System.ComponentModel.DataAnnotations;

[ContentType(
    DisplayName = "Business Search Page",
    GUID = "042028b4-cadf-40c9-b60a-24ee6a11f961",
    GroupName = GroupNames.Content)]
[ContentTypeIcon(FontAwesome5Solid.MapSigns)]
public class B2bSearchPage : B2BSitePageData, INavigationItem, IPageNavigatorEnabled
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

    [Display(
    Name = "Parent page content reference",
    Description = "A content area that allows Parent page content reference",
    GroupName = GroupNames.Content,
    Order = 60)]
    public virtual ContentReference? ParentPageContentReference { get; set; }

    [Display(
        Name = "Search Button text",
        Description = "Search Button text",
        GroupName = GroupNames.Content,
        Order = 70)]
    [CultureSpecific]
    public virtual string? SearchBtnTxt { get; set; }

    [Display(
    Name = "Search Input Placeholder",
    Description = "Search Input Placeholder",
    GroupName = GroupNames.Content,
    Order = 75)]
    [CultureSpecific]
    public virtual string? SearchInputPlaceholdertxt { get; set; }

    [Display(
    Name = "No Result Found text",
    Description = "No Result Found Button text",
    GroupName = GroupNames.Content,
    Order = 80)]
    [CultureSpecific]
    public virtual string? NoResultFoundTxt { get; set; }


    [Display(
    Name = "Result Found text",
    Description = "Result Found text",
    GroupName = GroupNames.Content,
    Order = 90)]
    [CultureSpecific]
    public virtual string? ResultFoundTxt { get; set; }
}