namespace Salam.Cms.Web.Features.Cards.Models;

using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Shell.ObjectEditing;
using Geta.Optimizely.ContentTypeIcons;
using Geta.Optimizely.ContentTypeIcons.Attributes;
using Salam.Cms.Shared.Models.Common;
using Salam.Cms.Shared.Models.Common.Enums;
using Salam.Cms.Shared.Models.Common.Properties;
using Salam.Cms.Web.Features.Cards.Enums;
using Salam.Cms.Web.Features.Common.Models;
using System.ComponentModel.DataAnnotations;

[ContentType(
    DisplayName = "Card List Block",
    GUID = "8A7FE27A-4908-43D9-992B-ED6F85D3FC95",
    Description = "Displays a card list and allows the content editor to create a list of cards",
    GroupName = SystemTabNames.Content)]
[ContentTypeIcon(FontAwesome5Solid.List)]
public class CardListBlock : SiteContentBlock
{
    [Display(
        Name = "Style Preset",
        Description = "Determines whether the card styling should be dictated by the Card or Card List Block",
        GroupName = GroupNames.Content,
        Order = 5)]
    [BackingType(typeof(PropertyNumber))]
    [EditorDescriptor(EditorDescriptorType = typeof(EnumEditorDescriptor<CardListStylePresetOption>))]
    public virtual CardListStylePresetOption StylePreset { get; set; }

    [Display(
        Name = "Style",
        Description = "Select the style of the card list.",
        GroupName = GroupNames.Content,
        Order = 10)]
    [BackingType(typeof(PropertyLongString))]
    [SelectMany(SelectionFactoryType = typeof(EnumSelectionFactory<CardStyleOption>))]
    public virtual string? Style { get; set; }

    [Display(
        Name = "Button Style",
        Description = "Select the style of the card list button.",
        GroupName = GroupNames.Content,
        Order = 15)]
    [BackingType(typeof(PropertyNumber))]
    [EditorDescriptor(EditorDescriptorType = typeof(EnumEditorDescriptor<StyleOption>))]
    public virtual StyleOption ButtonStyle { get; set; }

    [Display(
        Name = "Theme",
        Description = "Select the theme of the card list.",
        GroupName = GroupNames.Content,
        Order = 20)]
    [BackingType(typeof(PropertyNumber))]
    [EditorDescriptor(EditorDescriptorType = typeof(EnumEditorDescriptor<CardListThemeOption>))]
    public virtual CardListThemeOption Theme { get; set; }

    [Display(
        Name = "Layout",
        Description = "Select the layout of the card list.",
        GroupName = GroupNames.Content,
        Order = 30)]
    [BackingType(typeof(PropertyNumber))]
    [EditorDescriptor(EditorDescriptorType = typeof(EnumEditorDescriptor<CardListLayoutOption>))]
    public virtual CardListLayoutOption Layout { get; set; }

    [Display(
        Name = "Card Blocks",
        Description = "Content Area for holding a list of Card Blocks.",
        GroupName = GroupNames.Content,
        Order = 40)]
    [AllowedTypes(new[] { typeof(CardBlock) })]
    public virtual ContentArea? Items { get; set; }
}
