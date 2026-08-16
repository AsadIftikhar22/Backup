namespace Salam.Cms.Web.Features.Hero.Models;

using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using Geta.Optimizely.ContentTypeIcons;
using Geta.Optimizely.ContentTypeIcons.Attributes;
using Salam.Cms.Shared.Models.Common;
using Salam.Cms.Shared.Models.Common.RichText;
using Salam.Cms.Shared.Models.Validation;
using Salam.Cms.Web.Features.Hero.Abstract;
using System.ComponentModel.DataAnnotations;

[ContentType(
    DisplayName = "Hero Text Only",
    GUID = "4B4A4C06-5841-40A2-8BA1-06B65E6EE01D",
    Description = "A basic hero block with text only.",
    GroupName = SystemTabNames.Content)]
[ContentTypeIcon(FontAwesome5Solid.Mask)]
public class HeroTextOnlyBlock : BlockData, IHeroBlock
{
    [Display(
        Name = "Heading",
        Description = "The Hero block heading.",
        GroupName = GroupNames.Content,
        Order = 20)]
    [CultureSpecific]
    public virtual string? Heading { get; set; }

    [Display(
        Name = "Description",
        Description = "Hero block description.",
        GroupName = GroupNames.Content,
        Order = 40)]
    [CultureSpecific]
    [UIHint(RichTextEditors.BasicEditor)]
    [NoMediaInRichText]
    [NoBlocksInRichText]
    public virtual XhtmlString? Description { get; set; }
}