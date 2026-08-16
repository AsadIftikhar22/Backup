namespace Salam.Cms.Web.Features.NotFound.Models;

using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using Geta.Optimizely.ContentTypeIcons;
using Geta.Optimizely.ContentTypeIcons.Attributes;
using Salam.Cms.Shared.Models.Common;
using Salam.Cms.Shared.Models.Validation;
using Salam.Cms.Web.Features.Common.Models;
using Salam.Cms.Web.Features.Hero.Abstract;
using System.ComponentModel.DataAnnotations;

[ContentType(
    DisplayName = "Not Found Page",
    GUID = "DED7CA59-ED4A-4399-9E46-D6D1BE95ADB2",
    Description = "A page designed as a default not found page.",
    GroupName = GroupNames.Content)]
[ContentTypeIcon(FontAwesome.ExclamationTriangle)]
public class NotFoundPage : SitePageData
{
    [Display(
        Name = "Hero Content",
        Description = "A specific area for hero blocks which will be rendered immediately below the navigation elements.",
        GroupName = GroupNames.Content,
        Order = 10)]
    [CultureSpecific]
    [MaxElements(1)]
    [AllowedTypes(typeof(IHeroBlock))]
    public override ContentArea? HeroArea { get; set; }

    [Display(
        Name = "Main Content",
        Description = "A content area that allows blocks that have been specifically designed as content.",
        GroupName = GroupNames.Content,
        Order = 20)]
    [AllowedTypes(AllowedTypes = new[] { typeof(SiteContentBlock) })]
    public virtual ContentArea? MainContent { get; set; }

}