namespace Salam.Cms.Web.Features.DXPB2BConnectivity.Models;

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
    DisplayName = "DXP B2B Connectivity Block",
    GUID = "d5ed16e5-d7d7-4d1d-95f0-940493361377",
    Description = "Displays DXP B2B Connectivity Block",
    GroupName = SystemTabNames.Content)]
[ContentTypeIcon(FontAwesome5Solid.List)]
public class DXPB2BConnectivityBlock : SiteContentBlock
{
    [Display(
        Name = "Connectivity Heading",
        GroupName = SystemTabNames.Content,
        Order = 10)]
    [CultureSpecific]
    public virtual string? Heading { get; set; }

    [Display(
         Name = "Card 1 Heading",
         Description = "Card 1 Heading",
         GroupName = SystemTabNames.Content,
         Order = 20)]
    [CultureSpecific]
    public virtual string? Card1Heading { get; set; }

    [Display(
     Name = "Card 1 List",
     Description = "Card 1 List",
     GroupName = SystemTabNames.Content,
     Order = 30)]
    [CultureSpecific]
    public virtual IList<string>? Card1List { get; set; }

    [Display(
          Name = "Card 2 Heading",
          Description = "Card 2 Heading",
          GroupName = SystemTabNames.Content,
          Order = 40)]
    [CultureSpecific]
    public virtual string? Card2Heading { get; set; }

    [Display(
     Name = "Card 2 List",
     Description = "Card 2 List",
     GroupName = SystemTabNames.Content,
     Order = 50)]
    [CultureSpecific]
    public virtual IList<string>? Card2List { get; set; }
}
