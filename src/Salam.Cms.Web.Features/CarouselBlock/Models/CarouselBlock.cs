namespace Salam.Cms.Web.Features.InternetCards.Models;

using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Web;
using Geta.Optimizely.ContentTypeIcons;
using Geta.Optimizely.ContentTypeIcons.Attributes;
using Salam.Cms.Shared.Models.Common.RichText;
using Salam.Cms.Web.Features.Common.Models;
using System.ComponentModel.DataAnnotations;

[ContentType(
    DisplayName = "DXP B2B Carousel Block",
    GUID = "d943c0f4-5c8b-418b-a12e-7b70376443ad",
    Description = "Displays an DXP B2B Carousel Block.",
    GroupName = SystemTabNames.Content)]
[ContentTypeIcon(FontAwesome5Solid.List)]
public class CarouselBlock : SiteContentBlock
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
    public virtual IList<CarouselCardBlock>? CarouselCards { get; set; }
}