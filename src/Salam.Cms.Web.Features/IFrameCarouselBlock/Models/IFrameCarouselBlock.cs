namespace Salam.Cms.Web.Features.IFrameCarouselBlock.Models;

using EPiServer;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using Geta.Optimizely.ContentTypeIcons;
using Geta.Optimizely.ContentTypeIcons.Attributes;
using Salam.Cms.Shared.Models.Common;
using Salam.Cms.Web.Features.Common.Models;
using System.ComponentModel.DataAnnotations;

[ContentType(
    DisplayName = "IFrame Carousel Block Items",
    GUID = "2e5a7b8c-0621-4168-9d96-b57d66aa3dee",
    Description = "A block that displays a IFrame Carousel Block Items",
    GroupName = SystemTabNames.Content)]
[ContentTypeIcon(FontAwesome5Solid.QuestionCircle)]
public class IFrameCarouselBlockItems : SiteContentBlock
{
    [Display(Name = "Image", Order = 10)]
    [CultureSpecific]
    public virtual ContentReference Image { get; set; }
    [CultureSpecific]

    [Display(Name = "Alt Text", Order = 20)]
    public virtual string AltText { get; set; }

    [Display(Name = "Slide Link", Order = 30)]
    [CultureSpecific]
    public virtual Url Link { get; set; }

    [Display(Name = "Open Link In New Tab", Order = 40)]
    [CultureSpecific]
    public virtual bool OpenInNewTab { get; set; }
}
[ContentType(
    DisplayName = "IFrame Carousel Block",
    GUID = "4a2e51b0-b2fd-4448-9c4e-377fd63ed32f",
    Description = "A block that displays a IFrame Carousel Block",
    GroupName = SystemTabNames.Content)]
[ContentTypeIcon(FontAwesome5Solid.QuestionCircle)]
public class IFrameCarouselBlock : SiteContentBlock
{
    [Display(Name = "Heading", Order = 10)]
    public virtual string Heading { get; set; }

    [Display(Name = "Description", Order = 20)]
    public virtual string Description { get; set; }

    [Display(Name = "Carousel Slides", Order = 30)]
    public virtual IList<IFrameCarouselBlockItems> Items { get; set; }
}