namespace Salam.Cms.Web.Features.InfrastructuresCardItems.Models;

using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using Geta.Optimizely.ContentTypeIcons;
using Geta.Optimizely.ContentTypeIcons.Attributes;
using Salam.Cms.Web.Features.Common.Models;
using System.ComponentModel.DataAnnotations;

[ContentType(
    DisplayName = "DXP B2B Infrastructures Card Item Block",
    GUID = "37ab409d-dcbe-41ce-b087-050f71bb639a",
    Description = "Displays an DXP B2B Infrastructures Card Item Block",
    GroupName = SystemTabNames.Content)]
[ContentTypeIcon(FontAwesome5Solid.List)]
public class InfrastructuresCardItemsBlock : SiteContentBlock
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
    [CultureSpecific]
    public virtual string? Description { get; set; }

    [Display(
     Name = "Number",
     Description = "Number",
     GroupName = SystemTabNames.Content,
     Order = 30)]
    [CultureSpecific]
    public virtual string? Number { get; set; }
}
