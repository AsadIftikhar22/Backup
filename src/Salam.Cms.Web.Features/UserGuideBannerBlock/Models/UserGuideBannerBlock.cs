namespace Salam.Cms.Web.Features.SolutionsSectionsBlock.Models;

using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.SpecializedProperties;
using EPiServer.Web;
using Geta.Optimizely.ContentTypeIcons;
using Geta.Optimizely.ContentTypeIcons.Attributes;
using Salam.Cms.Shared.Models.Common.RichText;
using Salam.Cms.Web.Features.Common.Models;
using System.ComponentModel.DataAnnotations;

[ContentType(
    DisplayName = "DXP B2B User Guide Banner Block",
    GUID = "d7e9ac85-68d3-4b6c-96e6-d7fea8fbc025",
    Description = "Displays DXP B2B  User Guide Banner Block.",
    GroupName = SystemTabNames.Content)]
[ContentTypeIcon(FontAwesome5Solid.List)]
public class UserGuideBannerBlock : SiteContentBlock
{
    [Display(
        Name = "User Guide Banner Heading",
        GroupName = SystemTabNames.Content,
        Order = 10)]
    [CultureSpecific]
    public virtual string? Heading { get; set; }

    [Display(
         Name = "User Guide Banner Description",
         Description = "User Guide Banner Description",
         GroupName = SystemTabNames.Content,
         Order = 20)]
    [CultureSpecific]
    [UIHint(RichTextEditors.BasicEditor)]
    public virtual string? Description { get; set; }

    [Display(
    Name = "User Guide Image",
    Description = "Select the media for the User Guide Image.",
    GroupName = SystemTabNames.Content,
    Order = 30)]
    [CultureSpecific]
    [UIHint(UIHint.Image)]
    public virtual ContentReference? UserGuideImage { get; set; }

    [Display(
        Name = "User Guide Banner Button",
        Description = "User Guide Banner Button",
        GroupName = SystemTabNames.Content,
        Order = 40)]
    [CultureSpecific]
    public virtual LinkItem? UserGuideBannerCta { get; set; }
}
