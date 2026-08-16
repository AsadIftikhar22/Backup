namespace Salam.Cms.Web.Features.SectionHeading.Models;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Shell.ObjectEditing;
using Salam.Cms.Shared.Models.Common;
using Salam.Cms.Shared.Models.Common.Properties;
using Salam.Cms.Web.Features.Common.Models;
using System.ComponentModel.DataAnnotations;

[ContentType(
    DisplayName = "Section Heading Block",
    GUID = "3f285de0-4b80-47e0-8985-09de347acef9",
    Description = "Section Heading Block",
    GroupName = SystemTabNames.Content)]
public class SectionHeadingBlock : SiteContentBlock
{
    [Display(
        Name = "Line one",
        GroupName = GroupNames.Content,
        Order = 10)]
    [CultureSpecific]
    public virtual string? LineOne { get; set; }

    [Display(
        Name = "Line two",
        GroupName = GroupNames.Content,
        Order = 20)]
    [CultureSpecific]
    public virtual string? LineTwo { get; set; }

    [Display(
        Name = "Style",
        Description = "Style",
        GroupName = GroupNames.Content,
        Order = 30)]
    [CultureSpecific]
    [BackingType(typeof(PropertyNumber))]
    [EditorDescriptor(EditorDescriptorType = typeof(EnumEditorDescriptor<SectionHeadingStyleOption>))]
    public virtual SectionHeadingStyleOption Style { get; set; }
}