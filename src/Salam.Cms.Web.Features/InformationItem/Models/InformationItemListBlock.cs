namespace Salam.Cms.Web.Features.InformationItem.Models;

using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Shell.ObjectEditing;
using Geta.Optimizely.ContentTypeIcons;
using Geta.Optimizely.ContentTypeIcons.Attributes;
using Salam.Cms.Shared.Models.Common;
using Salam.Cms.Shared.Models.Common.Properties;
using Salam.Cms.Web.Features.Common.Interfaces;
using Salam.Cms.Web.Features.Common.Models;
using Salam.Cms.Web.Features.InformationItem.Enums;
using System.ComponentModel.DataAnnotations;

[ContentType(
    DisplayName = "Information Item List Block",
    GUID = "0dde10a3-a6f2-4d69-b928-33a1df951c66",
    Description = "Displays a list of information items.",
    GroupName = SystemTabNames.Content)]
[ContentTypeIcon(FontAwesome5Solid.List)]
public class InformationItemListBlock : SiteContentBlock
{
    [Display(
        Name = "Style",
        Description = "Select the style of the card.",
        GroupName = GroupNames.Content,
        Order = 10)]
    [BackingType(typeof(PropertyNumber))]
    [EditorDescriptor(EditorDescriptorType = typeof(EnumEditorDescriptor<InformationItemListBlockStyleOption>))]
    public virtual InformationItemListBlockStyleOption Style { get; set; }

    [Display(
        Name = "Width Option",
        Description = "Select whether the card should apply max width or not",
        GroupName = GroupNames.Content,
        Order = 15)]
    [BackingType(typeof(PropertyNumber))]
    [EditorDescriptor(EditorDescriptorType = typeof(EnumEditorDescriptor<InformationItemListWidthOption>))]
    public virtual InformationItemListWidthOption MaxWidth { get; set; }

    [Display(
        Name = "Information Items",
        Description = "The list of information items to be displayed.",
        GroupName = GroupNames.Content,
        Order = 20)]
    [AllowedTypes(typeof(InformationItemBlock), typeof(ISitePageData))]
    public virtual ContentArea? Items { get; set; }
}