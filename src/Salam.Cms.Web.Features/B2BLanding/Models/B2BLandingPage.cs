namespace Salam.Cms.Web.Features.B2BLanding.Models;

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
    DisplayName = "Business Landing Page",
    Description = " A Business landing page is a page that is designed to be the first point of contact for visitors. It typically contains a clear call to action and is optimized for conversion.",
    GUID = "29a0813f-e1f7-4682-a690-2be608ca1579",
    GroupName = GroupNames.Content)]
[ContentTypeIcon(FontAwesome5Solid.MapSigns)]
public class B2BLandingPage : B2BSitePageData, IPageNavigatorEnabled
{}