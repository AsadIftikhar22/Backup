namespace Salam.Cms.Web.Features.BusinessCardDescriptionItems.Models;

using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using Geta.Optimizely.ContentTypeIcons;
using Geta.Optimizely.ContentTypeIcons.Attributes;
using Salam.Cms.Web.Features.Common.Models;
using System.ComponentModel.DataAnnotations;

[ContentType(
    DisplayName = "DXP B2B Business Card Description Items Block",
    GUID = "cd5c21c7-df4e-480a-9d4a-062034ccc586",
    Description = "Displays an DXP B2B Business Card Description Items Block\",",
    GroupName = SystemTabNames.Content)]
[ContentTypeIcon(FontAwesome5Solid.List)]
public class BusinessCardDescriptionItemsBlock : SiteContentBlock
{
    [Display(
        Name = "Heading",
        GroupName = SystemTabNames.Content,
        Order = 10)]
    [CultureSpecific]
    public virtual string? Heading { get; set; }

    [Display(
         Name = "Description",
         Description = "Description",
         GroupName = SystemTabNames.Content,
         Order = 20)]
    public virtual string? Description { get; set; }
}
