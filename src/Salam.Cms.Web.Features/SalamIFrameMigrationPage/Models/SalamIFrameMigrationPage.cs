namespace Salam.Cms.Web.Features.SalamIFrameMigrationPage.Models;

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
    DisplayName = "Salam Iframe Migration page theme",
    Description = "Salam Iframe Migration page theme",
    GUID = "e352a676-5227-436c-8a2c-7f1c1825ad02",
    GroupName = GroupNames.Content)]
[ContentTypeIcon(FontAwesome5Solid.MapSigns)]
public class SalamIFrameMigrationPage : B2BSitePageData, IPageNavigatorEnabled
{
    [Display(
        Name = "Main Content",
        Description = "A content area that allows blocks that have been specifically designed as section content.",
        GroupName = GroupNames.Content,
        Order = 20)]
    [AllowedTypes(typeof(ISiteContentBlock), typeof(BlockData))]
    public virtual ContentArea? MainContent { get; set; }

    [Display(
        Name = "Enable Page Navigator",
        Description = "Toggle the page navigator, which links to sections of the main content area. The Page Navigator Title on the block will serve as the link text.",
        GroupName = GroupNames.Navigation,
        Order = 40)]
    public virtual bool EnablePageNavigator { get; set; }

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
    Name = "End Page Main Content",
    Description = "A content area that allows blocks that have been specifically designed as section content.",
    GroupName = GroupNames.Content,
    Order = 50)]
    [AllowedTypes(typeof(ISiteContentBlock))]
    public virtual ContentArea? EndMainContent { get; set; }
}