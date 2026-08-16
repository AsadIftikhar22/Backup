namespace Salam.Cms.Web.Features.FaqPage.Models;

using EPiServer.Core;
using EPiServer.DataAnnotations;
using Geta.Optimizely.ContentTypeIcons;
using Geta.Optimizely.ContentTypeIcons.Attributes;
using Salam.Cms.Shared.Models.Common;
using Salam.Cms.Web.Features.Common.Interfaces;
using Salam.Cms.Web.Features.Common.Models;
using System.ComponentModel.DataAnnotations;

[ContentType(
    DisplayName = "FAQ Page",
    Description = "A page for displaying frequently asked questions.",
    GUID = "A1B2C3D4-E5F6-4A7B-8C9D-0E1F2A3B4C5D",
    GroupName = GroupNames.Content)]
[ContentTypeIcon(FontAwesome5Solid.QuestionCircle)]
public class FaqPage : SitePageData, IPageNavigatorEnabled
{
    [Display(
    Name = "Enable Category Navigator",
    Description = "Toggle the Category navigator, which links to sections of the main content area. The Page Navigator Title on the block will serve as the link text.",
    GroupName = GroupNames.Navigation,
    Order = 45)]
    public virtual bool EnableCategoryNavigator { get; set; }

    [Display(
        Name = "Main Content",
        Description = "A content area that allows FAQ blocks and other content blocks.",
        GroupName = GroupNames.Content,
        Order = 20)]
    [AllowedTypes(typeof(ISiteContentBlock))]
    public virtual ContentArea? MainContent { get; set; }

    [Display(
        Name = "Enable Page Navigator",
        Description = "Toggle the page navigator, which links to sections of the main content area. The Page Navigator Title on the block will serve as the link text.",
        GroupName = GroupNames.Navigation,
        Order = 40)]
    public virtual bool EnablePageNavigator { get; set; }
}

