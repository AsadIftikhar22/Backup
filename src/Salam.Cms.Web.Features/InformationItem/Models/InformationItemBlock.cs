namespace Salam.Cms.Web.Features.InformationItem.Models;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.SpecializedProperties;
using EPiServer.Web;
using Salam.Cms.Shared.Models;
using Salam.Cms.Shared.Models.Common;
using System.ComponentModel.DataAnnotations;


[ContentType(
    DisplayName = "Information Item Block",
    GUID = "7dd284d1-f3e2-43b6-bc6c-84c74e143c6e",
    Description = "Displays an information item.",
    GroupName = SystemTabNames.Content)]
public class InformationItemBlock : BlockData
{
    [Display(
        Name = "Icon",
        Description = "Select the icon for the card.",
        GroupName = GroupNames.Content,
        Order = 10)]
    [UIHint(SalamUIHint.IconLibrary)]
    public virtual ContentReference? Icon { get; set; }

    [Display(
        Name = "Heading",
        GroupName = SystemTabNames.Content,
        Order = 20)]
    [CultureSpecific]
    public virtual string? Heading { get; set; } = string.Empty;

    [Display(
        Name = "Description",
        GroupName = SystemTabNames.Content,
        Order = 30)]
    [CultureSpecific]
    [UIHint(UIHint.Textarea)]
    public virtual string? Description { get; set; } = string.Empty;
    
    [Display(
    Name = "Explore Link",
    Description = "Explore Link",
    GroupName = SystemTabNames.Content,
    Order = 50)]
    [CultureSpecific]
    public virtual LinkItem ExploreLink { get; set; }
}