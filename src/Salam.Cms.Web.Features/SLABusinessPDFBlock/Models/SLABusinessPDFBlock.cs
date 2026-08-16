namespace Salam.Cms.Web.Features.SLABusinessPDFBlock.Models;

using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.SpecializedProperties;
using Geta.Optimizely.ContentTypeIcons;
using Geta.Optimizely.ContentTypeIcons.Attributes;
using Salam.Cms.Shared.Models.Common.RichText;
using Salam.Cms.Web.Features.Common.Models;
using System.ComponentModel.DataAnnotations;

[ContentType(
    DisplayName = "DXP B2B SLA Business PDF Block",
    GUID = "741d2767-cc64-4050-87ea-0c415f841852",
    Description = "Displays an DXP B2B SLA Business PDF.",
    GroupName = SystemTabNames.Content)]
[ContentTypeIcon(FontAwesome5Solid.List)]
public class SLABusinessPDFBlock : SiteContentBlock
{
    [Display(
        Name = "Tile Block Main Heading",
        GroupName = SystemTabNames.Content,
        Order = 10)]
    [CultureSpecific]
    public virtual string? Heading { get; set; }

    [Display(
         Name = "Tile Block Main Description",
         Description = "Tile Block Main Description",
         GroupName = SystemTabNames.Content,
         Order = 20)]
    [CultureSpecific]
    [UIHint(RichTextEditors.BasicEditor)]
    public virtual string? Description { get; set; }

    [Display(
     Name = "Link Item Block",
     Description = "Link Item Block",
     GroupName = SystemTabNames.Content,
     Order = 25)]
    [CultureSpecific]
    public virtual LinkItem? CTAUrl { get; set; }
}