namespace Salam.Cms.Web.Features.Home.Models;

using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using Geta.Optimizely.ContentTypeIcons;
using Geta.Optimizely.ContentTypeIcons.Attributes;
using Salam.Cms.Shared.Models.Common;
using Salam.Cms.Web.Features.Common.Models;
using System.ComponentModel.DataAnnotations;

[ContentType(
    DisplayName = "Home Page",
    GUID = "060C7B3A-971D-4632-92C4-B493C2DA8D52",
    Description = "A page designed as a default landing page.",
    GroupName = GroupNames.Content)]
[ContentTypeIcon(FontAwesome.Home)]
public class HomePage : SitePageData
{
    [Display(
        Name = "Main Content",
        Description = "A content area that allows blocks that have been specifically designed as section content.",
        GroupName = GroupNames.Content,
        Order = 10)]
    public virtual ContentArea? MainContent { get; set; }
}