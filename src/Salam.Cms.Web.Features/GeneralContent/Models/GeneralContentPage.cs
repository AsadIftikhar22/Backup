namespace Salam.Cms.Web.Features.GeneralContent.Models;

using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using Geta.Optimizely.ContentTypeIcons;
using Geta.Optimizely.ContentTypeIcons.Attributes;
using Salam.Cms.Shared.Models.Common;
using Salam.Cms.Web.Features.Common.Interfaces;
using Salam.Cms.Web.Features.Common.Models;
using Salam.Cms.Web.Features.Eligibility;
using Salam.Cms.Web.Features.Eligibility.Models;
using System.ComponentModel.DataAnnotations;

[ContentType(
    DisplayName = "General Content Page",
    Description = "A flexible page for general usage that allows all content blocks.",
    GUID = "B14FD34A-EA69-4CE7-849A-EBE6F0541727",
    GroupName = GroupNames.Content)]
[ContentTypeIcon(FontAwesome5Solid.File)]
public class GeneralContentPage : SitePageData, IPageNavigatorEnabled
{
    [Display(
        Name = "Main Content",
        Description = "A content area that allows blocks that have been specifically designed as section content.",
        GroupName = GroupNames.Content,
        Order = 20)]
    [AllowedTypes(typeof(ISiteContentBlock),typeof(EligibilityCheckBlock),typeof(EligibilityRequirementBlock))]
    public virtual ContentArea? MainContent { get; set; }

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

}