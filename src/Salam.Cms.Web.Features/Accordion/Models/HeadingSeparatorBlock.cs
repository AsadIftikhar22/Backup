namespace Salam.Cms.Web.Features.Accordion.Models;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using Geta.Optimizely.ContentTypeIcons;
using Geta.Optimizely.ContentTypeIcons.Attributes;
using Salam.Cms.Shared.Models.Common;
using System.ComponentModel.DataAnnotations;

[ContentType(
    DisplayName = "Heading Separator Block",
    GUID = "3a80f469-628d-45f9-9b1e-647ba486f214",
    Description = "",
    GroupName = GroupNames.Content)]
[ContentTypeIcon(FontAwesome5Solid.GripLines)]
public class HeadingSeparatorBlock : BlockData
{

    [Display(
        Name = "Heading",
        GroupName = SystemTabNames.Content,
        Order = 7)]
    [Required]
    [CultureSpecific]
    public virtual string? Heading { get; set; }
}

