namespace Salam.Cms.Web.Features.TileListBlock.Models;

using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.SpecializedProperties;
using Geta.Optimizely.ContentTypeIcons;
using Geta.Optimizely.ContentTypeIcons.Attributes;
using Salam.Cms.Shared.Models.Common.RichText;
using Salam.Cms.Web.Features.Common.Models;
using System.ComponentModel.DataAnnotations;

[ContentType(
    DisplayName = "DXP B2B Tile List Block",
    GUID = "11c59b97-e69a-4564-bebe-925081d5ccb2",
    Description = "Displays an DXP B2B Tile List Block and allows the content editor to add content to the Solutions Sections Block.",
    GroupName = SystemTabNames.Content)]
[ContentTypeIcon(FontAwesome5Solid.List)]
public class TileListBlock : SiteContentBlock
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
     Name = "Tile List Item",
     Description = "Tile List Item",
     GroupName = SystemTabNames.Content,
     Order = 20)]
    [AllowedTypes(new[] { typeof(TileListItemBlock) })]
    public virtual ContentArea? Items { get; set; }

}

[ContentType(
    DisplayName = "DXP B2B Tile List Item Block",
    GUID = "314f2de3-e1c1-49bb-9938-5b4050971670",
    Description = "Displays an DXP B2B Tile List Item Block and allows the content editor to add content to the Tile List Container",
    GroupName = SystemTabNames.Content)]
[ContentTypeIcon(FontAwesome5Solid.List)]
public class TileListItemBlock : SiteContentBlock
{
    [Display(
    Name = "Tile Item Heading",
    GroupName = SystemTabNames.Content,
    Order = 10)]
    [CultureSpecific]
    public virtual string? Heading { get; set; }

    [Display(
         Name = "Tile Item Description",
         Description = "Tile Item Description",
         GroupName = SystemTabNames.Content,
         Order = 20)]
    [CultureSpecific]
    [UIHint(RichTextEditors.BasicEditor)]
    public virtual string? Description { get; set; }

    [Display(
        Name = "Tile List Button CTA",
        Description = "Tile List Button CTA",
        GroupName = SystemTabNames.Content,
        Order = 40)]
    [CultureSpecific]
    public virtual LinkItem? Cta { get; set; }
}