namespace Salam.Cms.Web.Features.Cards.Models;

using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Shell.ObjectEditing;
using EPiServer.SpecializedProperties;
using EPiServer.Web;
using Geta.Optimizely.ContentTypeIcons;
using Geta.Optimizely.ContentTypeIcons.Attributes;
using Salam.Cms.Shared.Models;
using Salam.Cms.Shared.Models.Common;
using Salam.Cms.Shared.Models.Common.Enums;
using Salam.Cms.Shared.Models.Common.Properties;
using Salam.Cms.Shared.Models.Common.RichText;
using Salam.Cms.Web.Features.Cards.Enums;
using System.ComponentModel.DataAnnotations;

[ContentType(
    DisplayName = "Card Block",
    GUID = "71BBEAB8-5522-4720-8A2D-901A201B3A49",
    Description = "Displays a card and allows the content editor to add content to the card.",
    GroupName = SystemTabNames.Content)]
[ContentTypeIcon(FontAwesome5Solid.List)]
public class CardBlock : BlockData
{
    [Display(
        Name = "Style",
        Description = "Select the style of the card.",
        GroupName = GroupNames.Content,
        Order = 10)]
    [BackingType(typeof(PropertyLongString))]
    [SelectMany(SelectionFactoryType = typeof(EnumSelectionFactory<CardStyleOption>))]
    public virtual string? Style { get; set; }

    [Display(
        Name = "Button Style",
        Description = "Select the style of the card block button.",
        GroupName = GroupNames.Content,
        Order = 15)]
    [BackingType(typeof(PropertyNumber))]
    [EditorDescriptor(EditorDescriptorType = typeof(EnumEditorDescriptor<StyleOption>))]
    public virtual StyleOption ButtonStyle { get; set; }

    [Display(
        Name = "Theme",
        Description = "Select the theme of the card.",
        GroupName = GroupNames.Content,
        Order = 20)]
    [BackingType(typeof(PropertyNumber))]
    [EditorDescriptor(EditorDescriptorType = typeof(EnumEditorDescriptor<ThemeOption>))]
    public virtual ThemeOption Theme { get; set; }

    [Display(
        Name = "Layout",
        Description = "Select the layout of the card.",
        GroupName = GroupNames.Content,
        Order = 30)]
    [BackingType(typeof(PropertyNumber))]
    [EditorDescriptor(EditorDescriptorType = typeof(EnumEditorDescriptor<LayoutOption>))]
    public virtual LayoutOption Layout { get; set; }

    [Display(
        Name = "Media",
        Description = "Select the media for the card.",
        GroupName = GroupNames.Content,
        Order = 40)]
    [CultureSpecific]
    [UIHint(UIHint.Image)]
    public virtual ContentReference? Media { get; set; }

    [Display(
        Name = "Icon",
        Description = "Select the icon for the card.",
        GroupName = GroupNames.Content,
        Order = 50)]
    [CultureSpecific]
    [UIHint(SalamUIHint.IconLibrary)]
    public virtual ContentReference? Icon { get; set; }

    [Display(
    Name = "Is Enquire Product",
    Description = "Is Enquire Product.",
    GroupName = GroupNames.Content,
    Order = 55)]
    public virtual bool IsEnquireProduct {get;set;}

    [Display(
        Name = "Badge Text",
        Description = "Select the badge text for the card.",
        GroupName = GroupNames.Content,
        Order = 60)]
    [CultureSpecific]
    public virtual string? BadgeText { get; set; }

    [Display(
        Name = "Heading",
        Description = "Select the heading for the card.",
        GroupName = GroupNames.Content,
        Order = 70)]
    [CultureSpecific]
    public virtual string? Heading { get; set; }

    [Display(
        Name = "Main Body",
        Description = "Select the main body for the card.",
        GroupName = GroupNames.Content,
        Order = 90)]
    [CultureSpecific]
    [UIHint(RichTextEditors.ReducedEditor)]
    public virtual XhtmlString? MainBody { get; set; }

    [Display(
        Name = "Link Items",
        Description = "Select the link items for the card.",
        GroupName = GroupNames.Content,
        Order = 100)]
    [CultureSpecific]
    public virtual LinkItemCollection? LinkItems { get; set; }

}