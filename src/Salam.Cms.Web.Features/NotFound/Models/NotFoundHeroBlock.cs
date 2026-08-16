namespace Salam.Cms.Web.Features.NotFound.Models;

using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.SpecializedProperties;
using EPiServer.Web;
using Geta.Optimizely.ContentTypeIcons;
using Geta.Optimizely.ContentTypeIcons.Attributes;
using Salam.Cms.Shared.Models.Common;
using Salam.Cms.Shared.Models.Common.RichText;
using System.ComponentModel.DataAnnotations;

[ContentType(
    DisplayName = "Not Found Hero Block",
    GUID = "4632E043-E6FB-42CF-A86C-CAAC9FA488BD",
    Description = "A Hero Block specifically designed for usage on the Not Found Page.",
    GroupName = SystemTabNames.Content)]
[ContentTypeIcon(FontAwesome.ExclamationTriangle)]
public class NotFoundHeroBlock : BlockData
{
    [Display(
        Name = "Title",
        Description = "The title to show as a h1 element.",
        GroupName = GroupNames.Content,
        Order = 10)]
    [CultureSpecific]
    public virtual string? Title { get; set; }

    [Display(
        Name = "Main Body",
        Description = "The main body content of the block.",
        GroupName = GroupNames.Content,
        Order = 20)]
    [CultureSpecific]
    [UIHint(RichTextEditors.FullEditor)]
    public virtual XhtmlString? MainBody { get; set; }

    [Display(
    Name = "Secondary Body",
    Description = "The secondary body text of the block.",
    GroupName = SystemTabNames.Content,
    Order = 30)]
    [CultureSpecific]
    public virtual XhtmlString? SecondaryBody { get; set; }

    [Display(
            Name = "Links",
            Description = "The collection of links for the block.",
            GroupName = GroupNames.Content,
            Order = 40)]
    [CultureSpecific]
    public virtual LinkItemCollection? Links { get; set; }

    [Display(
        Name = "Image",
        Description = "The image to render to the side of the title.",
        GroupName = GroupNames.Content,
        Order = 50)]
    [UIHint(UIHint.Image)]
    [CultureSpecific]
    public virtual ContentReference? Image { get; set; }
}