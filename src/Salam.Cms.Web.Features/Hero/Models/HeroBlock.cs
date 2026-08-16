namespace Salam.Cms.Web.Features.Hero.Models;

using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Shell.ObjectEditing;
using EPiServer.SpecializedProperties;
using EPiServer.Web;
using Geta.Optimizely.ContentTypeIcons;
using Geta.Optimizely.ContentTypeIcons.Attributes;
using Salam.Cms.Shared.Models.Common;
using Salam.Cms.Shared.Models.Common.Enums;
using Salam.Cms.Shared.Models.Common.Properties;
using Salam.Cms.Shared.Models.Common.RichText;
using Salam.Cms.Web.Features.Hero.Abstract;
using System.ComponentModel.DataAnnotations;

[ContentType(
    DisplayName = "Hero Block",
    GUID = "953544E0-0CBB-4BED-900F-322846FA9F60",
    Description = "This block displays the hero with call to action content side by side with an image.",
    GroupName = SystemTabNames.Content)]
[ContentTypeIcon(FontAwesome5Solid.Columns)]
public class HeroBlock : BlockData, IHeroBlock
{
    [Display(
        Name = "Layout",
        Description = "Set the layout of the block.",
        GroupName = GroupNames.Content,
        Order = 60)]
    [CultureSpecific]
    [BackingType(typeof(PropertyNumber))]
    [EditorDescriptor(EditorDescriptorType = typeof(EnumEditorDescriptor<LayoutOption>))]
    public virtual LayoutOption Layout { get; set; }

    [Display(
        Name = "Badge Text",
        Description = "A badge to display at the top of the block.",
        GroupName = GroupNames.Content,
        Order = 30)]
    [CultureSpecific]
    public virtual string? BadgeText { get; set; }

    [Display(
        Name = "Heading",
        Description = "The heading for the block.",
        GroupName = GroupNames.Content,
        Order = 10)]
    [CultureSpecific]
    public virtual string? Heading { get; set; }

    [Display(
        Name = "Description",
        Description = "The description for the block.",
        GroupName = GroupNames.Content,
        Order = 30)]
    [CultureSpecific]
    [UIHint(RichTextEditors.BasicEditor)]
    public virtual XhtmlString? Description { get; set; }

    [Display(
        Name = "Call To Action Links",
        Description = "Add links to be used for the call to action buttons.",
        GroupName = GroupNames.Content,
        Order = 40)]
    [CultureSpecific]
    public virtual LinkItemCollection? LinkItems { get; set; }

    [Display(
        Name = "Featured Media",
        Description = "The featured media to use when displaying as a block.",
        GroupName = GroupNames.Content,
        Order = 50)]
    [UIHint(UIHint.Image)]
    [CultureSpecific]
    public virtual ContentReference? Media { get; set; }
}