namespace Salam.Cms.Web.Features.B2BGeneralContent.Models;

using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Web;
using Geta.Optimizely.ContentTypeIcons;
using Geta.Optimizely.ContentTypeIcons.Attributes;
using Salam.Cms.Shared.Models.Common;
using Salam.Cms.Shared.Models.Common.RichText;
using Salam.Cms.Shared.Models.Pages;
using Salam.Cms.Shared.Models.Pages.Container;
using Salam.Cms.Web.Features.Common.Interfaces;
using Salam.Cms.Web.Features.Common.Models;
using Salam.Cms.Web.Features.Home.Models;
using System.ComponentModel.DataAnnotations;

[ContentType(
    DisplayName = "B2B Component Content Page",
    Description = "A flexible page for general usage that allows all content blocks.",
    GUID = "2f84968f-60ed-4e16-84a9-9f11c0cbcc34",
    GroupName = GroupNames.Content)]
[ContentTypeIcon(FontAwesome5Solid.File)]
[AvailableContentTypes(
    Include = new[]
    {
   typeof(ContainerPage),
        typeof(Salam.Cms.Web.Features.B2bSearchPage.Models.B2bSearchPage),
        typeof(B2BComponentContentPage),
        typeof(InvestorRelationPage)
    })]
public class B2BComponentContentPage : B2BSitePageData, INavigationItem, IPageNavigatorEnabled
{
    [Display(
        Name = "Main Content",
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
    Name = "Enable Category Navigator",
    Description = "Toggle the Category navigator, which links to sections of the main content area. The Page Navigator Title on the block will serve as the link text.",
    GroupName = GroupNames.Navigation,
    Order = 45)]
    public virtual bool EnableCategoryNavigator { get; set; }
    
    [Display(
    Name = "End Page Main Content",
    Description = "A content area that allows blocks that have been specifically designed as section content.",
    GroupName = GroupNames.Content,
    Order = 50)]
    [AllowedTypes(typeof(ISiteContentBlock))]
    public virtual ContentArea? EndPageMainContent { get; set; }

    [Display(
    Name = "Background Image",
    Description = "Select the media for the Background.",
    GroupName = SystemTabNames.Content,
    Order = 70)]
    [CultureSpecific]
    [UIHint(UIHint.Image)]
    public virtual ContentReference? BackgroundImage { get; set; }
}