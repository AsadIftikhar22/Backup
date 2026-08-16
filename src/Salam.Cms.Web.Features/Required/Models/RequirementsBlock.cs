namespace Salam.Cms.Web.Features.Required.Models;

using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.SpecializedProperties;
using EPiServer.Web;
using Salam.Cms.Shared.Models.Common;
using Salam.Cms.Web.Features.Cards.Models;
using Salam.Cms.Web.Features.Common.Models;
using Salam.Cms.Web.Features.InformationItem.Models;
using System.ComponentModel.DataAnnotations;

[ContentType(
    DisplayName = "Requirements Block",
    GUID = "6AAAEA43-D0BC-449E-910E-B0595479772C",
    Description = "Requirements Block",
    GroupName = GroupNames.Content)]
public class RequirementsBlock : SiteContentBlock
{
    [Display(
        Name = "Heading",
        GroupName = SystemTabNames.Content,
        Order = 10)]
    [Required]
    [CultureSpecific]
    public virtual string? Heading { get; set; }

    [Display(
        Name = "Description",
        Description = "Requirements Block Description Text",
        GroupName = GroupNames.Content,
        Order = 20)]
    [CultureSpecific]
    [UIHint(UIHint.Textarea)]
    public virtual string? Description { get; set; }

    [Display(
        Name = "Items",
        Description = "Content Area for holding a list of Support Items.",
        GroupName = GroupNames.Content,
        Order = 30)]
    [AllowedTypes(new[] { typeof(InformationItemBlock) })]
    public virtual ContentArea? Items { get; set; }

    [Display(
         Name = "Featured Item",
         Description = "Content Area for holding Featured item.",
         GroupName = GroupNames.Content,
         Order = 40)]
    [AllowedTypes(new[] { typeof(CardBlock) })]
    [MaxLength(1)]
    public virtual ContentArea? FeaturedItem { get; set; }

    [Display(
        Name = "Is B2B Details",
        Description = "Is B2B Details",
        GroupName = SystemTabNames.Content,
        Order = 50)]
    public virtual bool isB2B { get; set; }
}