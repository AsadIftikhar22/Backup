namespace Salam.Cms.Web.Features.SolutionsSectionsBlock.Models;

using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.SpecializedProperties;
using EPiServer.Web;
using Geta.Optimizely.ContentTypeIcons;
using Geta.Optimizely.ContentTypeIcons.Attributes;
using Salam.Cms.Web.Features.Common.Models;
using System.ComponentModel.DataAnnotations;

[ContentType(
    DisplayName = "DXP B2B Image Banner container block with Cards",
    GUID = "7418f339-491f-40cf-abbd-c4ee9f4c940e",
    Description = "DXP B2B Image Banner container block with Cards",
    GroupName = SystemTabNames.Content)]
[ContentTypeIcon(FontAwesome5Solid.List)]
public class ImageBannerContainerBlock : SiteContentBlock
{

    [Display(
    Name = "Image Card Block List",
    Description = "Image Card Block List",
    GroupName = SystemTabNames.Content,
    Order = 10)]
    [CultureSpecific]
    public virtual IList<ImageCardBlock>? ImageCardBlockList { get; set; }

}

[ContentType(
    DisplayName = "Image Card Block",
    GUID = "a314e01f-3e20-4f9d-a9f7-7e14f1b80266",
    Description = "Image Card Block with title",
    GroupName = SystemTabNames.Content)]
[ContentTypeIcon(FontAwesome5Solid.List)]
public class ImageCardBlock : SiteContentBlock
{
    [Display(
        Name = "Image Card Heading",
        Description = "Image Card Heading",
        GroupName = SystemTabNames.Content,
        Order = 100)]
    [CultureSpecific]
    public virtual string? Heading { get; set; }

    [Display(
        Name = "Solution section card 4 Description",
        Description = "Solution section card 4 Description",
        GroupName = SystemTabNames.Content,
        Order = 100)]
    [CultureSpecific]
    public virtual string? Description { get; set; }

    [Display(
    Name = "Cards Image",
    Description = "Select the media for the Cards Image.",
    GroupName = SystemTabNames.Content,
    Order = 30)]
    [CultureSpecific]
    [UIHint(UIHint.Image)]
    public virtual ContentReference? CardImage { get; set; }

    [Display(
        Name = "Explore Link",
        Description = "Explore Link",
        GroupName = SystemTabNames.Content,
        Order = 50)]
    [CultureSpecific]
    public virtual LinkItem ExploreLink { get; set; }
}


