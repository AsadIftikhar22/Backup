namespace Salam.Cms.Web.Features.ConnectivityRemove.Models;

using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.SpecializedProperties;
using Geta.Optimizely.ContentTypeIcons;
using Geta.Optimizely.ContentTypeIcons.Attributes;
using Salam.Cms.Shared.Models.Common.RichText;
using Salam.Cms.Web.Features.Common.Models;
using System.ComponentModel.DataAnnotations;

[ContentType(
    DisplayName = "DXP B2B Connectivity Remove Block",
    GUID = "2b3c5caf-1c92-46bb-b706-6180152d422b",
    Description = "DXP B2B Connectivity Remove Block",
    GroupName = SystemTabNames.Content)]
[ContentTypeIcon(FontAwesome5Solid.List)]
public class ConnectivityRemoveBlock : SiteContentBlock
{
    [Display(
    Name = "Connectivity Remove Item",
    GroupName = SystemTabNames.Content,
    Order = 10)]
    [CultureSpecific]
    public virtual IList<ConnectivityRemoveChildBlock>? Items { get; set; }
}

[ContentType(
    DisplayName = "DXP B2B Connectivity Remove Block Child Items",
    GUID = "a6f81558-4b21-4e98-b921-eaf0714233e2",
    Description = "DXP B2B Connectivity Remove Block Child Items",
    GroupName = SystemTabNames.Content)]
[ContentTypeIcon(FontAwesome5Solid.List)]
public class ConnectivityRemoveChildBlock : SiteContentBlock
{

    [Display(
        Name = "Connectivity Remove Item Heading",
        GroupName = SystemTabNames.Content,
        Order = 10)]
    [CultureSpecific]
    public virtual string? Heading { get; set; }

    [Display(
         Name = "Connectivity Remove Item Description",
         Description = "Tile Block Description",
         GroupName = SystemTabNames.Content,
         Order = 20)]
    [CultureSpecific]
    [UIHint(RichTextEditors.BasicEditor)]
    public virtual string? Description { get; set; }


    [Display(
         Name = "Connectivity Remove Item Labels",
         Description = "Tile Block Labels",
         GroupName = SystemTabNames.Content,
         Order = 20)]
    [CultureSpecific]
    [UIHint(RichTextEditors.BasicEditor)]
    public virtual IList<string>? Labels { get; set; }


    [Display(
     Name = "Link Item Block",
     Description = "Link Item Block",
     GroupName = SystemTabNames.Content,
     Order = 25)]
    [CultureSpecific]
    public virtual LinkItem? CTAUrl { get; set; }
}