namespace Salam.Cms.Web.Features.IconLinks.Models;

using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.SpecializedProperties;
using EPiServer.Web;
using Geta.Optimizely.ContentTypeIcons;
using Geta.Optimizely.ContentTypeIcons.Attributes;
using Salam.Cms.Shared.Models;
using Salam.Cms.Shared.Models.Common;
using System.ComponentModel.DataAnnotations;

[ContentType(
    DisplayName = "Icon Link Item Block",
    GUID = "6ea6bc15-ad49-474b-8e0c-2a9a4a6e16e3",
    Description = "Displays an icon link item.",
    GroupName = SystemTabNames.Content)]
[ContentTypeIcon(FontAwesome5Solid.Icons)]
public class IconLinkItemBlock : BlockData
{
    [Display(
        Name = "Icon",
        Description = "The icon to be displayed.",
        GroupName = GroupNames.Content,
        Order = 10)]
    [UIHint(SalamUIHint.IconLibrary)]
    public virtual ContentReference? Icon { get; set; }

    [Display(
        Name = "Link",
        Description = "The link to be displayed.",
        GroupName = GroupNames.Content,
        Order = 20)]
    [CultureSpecific]
    public virtual LinkItem? Link { get; set; }

    [Display(
        Name = "Is B2b Layout",
        Description = "Is B2b Layout",
        GroupName = GroupNames.BusinessComponentTab,
        Order = 30)]
    public virtual bool IsB2bLayout { get; set; }
}
