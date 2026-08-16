namespace Salam.Cms.Web.Features.B2BProductEnquiry.Models;

using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using Geta.Optimizely.ContentTypeIcons;
using Geta.Optimizely.ContentTypeIcons.Attributes;
using Salam.Cms.Shared.Models.Common;
using Salam.Cms.Shared.Models.Pages;
using Salam.Cms.Web.Features.Common.Models;

[ContentType(
    DisplayName = "B2B Product Enquiry page",
    Description = "A flexible page for product enquiry for Tab Service Selection",
    GUID = "55751819-ca5a-448d-95d8-33e19d372d2e",
    GroupName = GroupNames.Content)]
[ContentTypeIcon(FontAwesome5Solid.File)]
public class B2BProductEnquiryPage : B2BSitePageData, INavigationItem
{

}