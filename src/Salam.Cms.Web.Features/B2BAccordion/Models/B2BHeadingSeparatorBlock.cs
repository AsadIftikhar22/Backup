namespace Salam.Cms.Web.Features.Accordion.Models;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using Geta.Optimizely.ContentTypeIcons;
using Geta.Optimizely.ContentTypeIcons.Attributes;
using Salam.Cms.Shared.Models.Common;
using System.ComponentModel.DataAnnotations;

[ContentType(
    DisplayName = "DXP B2B Heading Separator Block",
    GUID = "cc855f41-955a-45d7-9375-6cecbebce6ec",
    Description = "",
    GroupName = GroupNames.Content)]
[ContentTypeIcon(FontAwesome5Solid.GripLines)]
public class B2BHeadingSeparatorBlock : BlockData
{

    [Display(
        Name = "Heading",
        GroupName = SystemTabNames.Content,
        Order = 7)]
    [Required]
    [CultureSpecific]
    public virtual string? Heading { get; set; }
}

