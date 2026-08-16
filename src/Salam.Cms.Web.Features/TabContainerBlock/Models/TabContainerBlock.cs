namespace Salam.Cms.Web.Features.TabContainer.Models;

using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.SpecializedProperties;
using Geta.Optimizely.ContentTypeIcons;
using Geta.Optimizely.ContentTypeIcons.Attributes;
using Salam.Cms.Shared.Models.Common.RichText;
using Salam.Cms.Web.Features.Common.Models;
using Salam.Cms.Web.Features.InternetCards.Models;
using System.ComponentModel.DataAnnotations;

[ContentType(
    DisplayName = "DXP B2B Internet CardBlock Tab Container",
    GUID = "3c34accb-8256-4021-9bb6-93ea96649b76",
    Description = "Displays a DXP B2B Internet Card Grid Items which are dropped on Internet Card Grid Tab Container",
    GroupName = SystemTabNames.Content)]
[ContentTypeIcon(FontAwesome5Solid.List)]
public class TabContainerBlock : SiteContentBlock
{
    [Display(
         Name = "Is Default",
         Description = "Is Default",
         GroupName = SystemTabNames.Content,
         Order = 20)]
    public virtual bool IsDefault { get; set; }

    [Display(
       Name = "Internet Card Blocks",
       Description = "Content Area for holding a list of Internet Card Items.",
       GroupName = SystemTabNames.Content,
       Order = 20)]
    [AllowedTypes(new[] { typeof(InternetCardsBlock) })]
    public virtual ContentArea? Items { get; set; }
}
