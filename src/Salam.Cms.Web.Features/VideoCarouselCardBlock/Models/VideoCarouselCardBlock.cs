namespace Salam.Cms.Web.Features.VideoCarouselCard.Models;

using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Web;
using Geta.Optimizely.ContentTypeIcons;
using Geta.Optimizely.ContentTypeIcons.Attributes;
using Salam.Cms.Web.Features.Common.Models;
using System.ComponentModel.DataAnnotations;

[ContentType(
    DisplayName = "DXP B2B Video Carousel Card Block Items",
    GUID = "b183d784-b23c-427a-9cc6-22024d29e070",
    Description = "Displays DXP B2B Video Carousel Card Block Items",
    GroupName = SystemTabNames.Content)]
[ContentTypeIcon(FontAwesome5Solid.List)]
public class VideoCarouselCardBlock : SiteContentBlock
{
    [Display(
        Name = "Heading",
        GroupName = SystemTabNames.Content,
        Order = 10)]
    [CultureSpecific]
    public virtual string? Heading { get; set; }

    [Display(
        Name = "Cards Videos Upload to the Digital Asset Library",
        Description = "Select the media for the Cards Videos Upload to the Digital Asset Library",
        GroupName = SystemTabNames.Content,
        Order = 30)]
    [CultureSpecific]
    [UIHint(UIHint.Video)]
    public virtual ContentReference? CardVideoCMP { get; set; }

    [Display(
    Name = "Cards Videos Iframe URL",
    Description = "Select the media for the Cards Videos from IFrame URL",
    GroupName = SystemTabNames.Content,
    Order = 40)]
    [CultureSpecific]
    public virtual string IframeEmbed { get; set; }
}