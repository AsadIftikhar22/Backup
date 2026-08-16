namespace Salam.Cms.Web.Features.InternetCards.Models;

using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.SpecializedProperties;
using EPiServer.Web;
using Geta.Optimizely.ContentTypeIcons;
using Geta.Optimizely.ContentTypeIcons.Attributes;
using Salam.Cms.Shared.Models.Common.RichText;
using Salam.Cms.Web.Features.Common.Models;
using System.ComponentModel.DataAnnotations;

[ContentType(
    DisplayName = "Carousel Card Block",
    GUID = "585dbff7-a09f-44f4-90e3-1d94ed3397a1",
    Description = "Displays an Business Carousel Card Block and allows the content editor to add content to the Carousel.",
    GroupName = SystemTabNames.Content)]
[ContentTypeIcon(FontAwesome5Solid.List)]
public class CarouselCardBlock : SiteContentBlock
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
    [ScaffoldColumn(false)]
    [UIHint(RichTextEditors.HtmlEditor)]
    public virtual XhtmlString? Description { get; set; }

    [Display(
     Name = "Description",
     Description = "Description",
     GroupName = SystemTabNames.Content,
     Order = 20)]
    [CultureSpecific]
    public virtual string? Description2 { get; set; }
    
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


    [Display(
     Name = "Carousel Paragraph Height",
     Description = "Carousel Paragraph Height",
     GroupName = SystemTabNames.Content,
     Order = 60)]
    [CultureSpecific]
    public virtual string? CarouselParagraphHeight { get; set; }

    [Display(
     Name = "Carousel Card Content Height",
     Description = "Carousel Card Content Height",
     GroupName = SystemTabNames.Content,
     Order = 20)]
    [CultureSpecific]
    public virtual string? CarouselCardContentHeight { get; set; }

}