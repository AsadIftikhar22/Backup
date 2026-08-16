namespace Salam.Cms.Web.Features.Hero.Models;

using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using Geta.Optimizely.ContentTypeIcons;
using Geta.Optimizely.ContentTypeIcons.Attributes;
using Salam.Cms.Web.Features.Common.Models;
using Salam.Cms.Web.Features.Hero.Abstract;
using System.ComponentModel.DataAnnotations;

[ContentType(
    DisplayName = "Hero Landing Block",
    GUID = "96F0B234-DADD-4852-A144-FBECB3B93D75",
    Description = "A block that shows hero items as a carousel to be used on landing pages.",
    GroupName = SystemTabNames.Content)]
[ContentTypeIcon(FontAwesome5Solid.Mask)]
public class HeroLandingBlock : SiteContentBlock, IHeroBlock
{
    [Display(
        Name = "Hero Items",
        Description = "Add at least one hero item block.",
        GroupName = SystemTabNames.Content,
        Order = 10)]
    [AllowedTypes(typeof(HeroBlock))]
    public virtual ContentArea? Items { get; set; }

}
