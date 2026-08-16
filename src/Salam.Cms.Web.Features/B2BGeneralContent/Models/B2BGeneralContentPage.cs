namespace Salam.Cms.Web.Features.B2BGeneralContent.Models;

using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Forms.Implementation.Elements;
using Geta.Optimizely.ContentTypeIcons;
using Geta.Optimizely.ContentTypeIcons.Attributes;
using Salam.Cms.Shared.Models.Common;
using Salam.Cms.Shared.Models.Common.RichText;
using Salam.Cms.Shared.Models.Pages;
using Salam.Cms.Web.Features.Common.Interfaces;
using Salam.Cms.Web.Features.Common.Models;
using Salam.Cms.Web.Features.Forms.B2BFormContainerBlock;
using System.ComponentModel.DataAnnotations;

[ContentType(
    DisplayName = "B2B General Content Page",
    Description = "A flexible page for general usage that allows all content blocks.",
    GUID = "cdb833b7-ec91-4a09-b8c8-3fe902034ef9",
    GroupName = GroupNames.Content)]
[ContentTypeIcon(FontAwesome5Solid.File)]
public class B2BGeneralContentPage : B2BSitePageData, INavigationItem
{
    
}