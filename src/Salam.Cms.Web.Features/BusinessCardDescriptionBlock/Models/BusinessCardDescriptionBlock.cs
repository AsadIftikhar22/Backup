namespace Salam.Cms.Web.Features.BusinessCardDescription.Models;

using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using Geta.Optimizely.ContentTypeIcons;
using Geta.Optimizely.ContentTypeIcons.Attributes;
using Salam.Cms.Web.Features.BusinessCardDescriptionItems.Models;
using Salam.Cms.Web.Features.Common.Models;
using System.ComponentModel.DataAnnotations;

[ContentType(
    DisplayName = "DXP B2B Trust Signals Block",
    GUID = "f6506eb4-9ad8-4fbb-8d85-0953edace799",
    Description = "Displays an DXP B2B Trust Signal Block\",",
    GroupName = SystemTabNames.Content)]
[ContentTypeIcon(FontAwesome5Solid.List)]
public class BusinessCardDescriptionBlock : SiteContentBlock
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
       Name = "Business Cards Description Items",
       Description = "Business Cards Description Items",
       GroupName = SystemTabNames.Content,
       Order = 30)]
    [CultureSpecific]
    public virtual IList<BusinessCardDescriptionItemsBlock> businessCardsItems { get; set; }
}
