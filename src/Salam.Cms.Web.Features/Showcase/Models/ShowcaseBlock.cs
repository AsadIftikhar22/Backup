namespace Salam.Cms.Web.Features.Showcase.Models;

using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using Geta.Optimizely.ContentTypeIcons;
using Geta.Optimizely.ContentTypeIcons.Attributes;
using Salam.Cms.Shared.Models.Common;
using Salam.Cms.Web.Features.Common.Models;
using System.ComponentModel.DataAnnotations;

[ContentType(
    DisplayName = "ShowCase Block",
    GUID = "0B138EE9-5576-4FA3-9C61-15C2E8FD7769",
    Description = "Displays a card and allows the content editor to add content to the card.",
    GroupName = SystemTabNames.Content)]
[ContentTypeIcon(FontAwesome5Solid.List)]
public class ShowcaseBlock : SiteContentBlock
{
    [Display(
        Name = "Showcase Item Blocks",
        Description = "Content Area for holding a list of Card Blocks.",
        GroupName = GroupNames.Content,
        Order = 10)]
    [AllowedTypes(new[] { typeof(ShowcaseItemBlock) })]
    public virtual ContentArea? Items { get; set; }

    [Display(
    Name = "Is B2B Layout",
    Description = "Is B2B Layout",
    GroupName = GroupNames.BusinessComponentTab,
    Order = 10)]
    public virtual bool IsB2bLayOut { get; set; }

}