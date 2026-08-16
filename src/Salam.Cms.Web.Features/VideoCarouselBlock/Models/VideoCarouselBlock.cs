namespace Salam.Cms.Web.Features.VideoCarousel.Models;

using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Web;
using Geta.Optimizely.ContentTypeIcons;
using Geta.Optimizely.ContentTypeIcons.Attributes;
using Salam.Cms.Shared.Models.Common.RichText;
using Salam.Cms.Web.Features.Common.Models;
using Salam.Cms.Web.Features.VideoCarouselCard.Models;
using System.ComponentModel.DataAnnotations;

[ContentType(
    DisplayName = "DXP B2B Video Carousel Block",
    GUID = "17051d2f-e2d3-482c-a10c-4b8ba9824886",
    Description = "Displays an DXP B2B Video Carousel Block.",
    GroupName = SystemTabNames.Content)]
[ContentTypeIcon(FontAwesome5Solid.List)]
public class VideoCarouselBlock : SiteContentBlock
{
    [Display(
        Name = "Heading",
        GroupName = SystemTabNames.Content,
        Order = 10)]
    [CultureSpecific]
    public virtual string? Heading { get; set; }

    [Display(
         Name = "Description",
         Description = "Description",
         GroupName = SystemTabNames.Content,
         Order = 20)]
    [CultureSpecific]
    [UIHint(RichTextEditors.HtmlEditor)]
    public virtual XhtmlString? Description { get; set; }

    [Display(
             Name = "Carousel Cards Items",
             Description = "Carousel Cards Items",
             GroupName = SystemTabNames.Content,
             Order = 30)]
    [CultureSpecific]
    public virtual IList<VideoCarouselCardBlock>? VideoCarouselCards { get; set; }
}