namespace Salam.Cms.Web.Features.InternetCardGrid.Models;

using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Shell.ViewComposition.Containers;
using Geta.Optimizely.ContentTypeIcons;
using Geta.Optimizely.ContentTypeIcons.Attributes;
using Salam.Cms.Web.Features.Common.Interfaces;
using Salam.Cms.Web.Features.Common.Models;
using Salam.Cms.Web.Features.InternetCards.Models;
using Salam.Cms.Web.Features.TabContainer.Models;
using System.ComponentModel.DataAnnotations;

[ContentType(
    DisplayName = "DXP B2B Internet Card Grid Block",
    GUID = "3c828d2d-d623-4639-af14-d4c09e1884fa",
    Description = "Displays an DXP B2B Internet Cards Grid Block and allows the content editor to add content to the Internet Card.",
    GroupName = SystemTabNames.Content)]
[ContentTypeIcon(FontAwesome5Solid.List)]
public class InternetCardGridBlock : SiteContentBlock
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
        Name = "Internet Card Blocks",
        Description = "Content Area for holding a list of Internet Card Items.",
        GroupName = SystemTabNames.Content,
        Order = 20)]
    [AllowedTypes(new[] { typeof(InternetCardsBlock) })]
    public virtual ContentArea? Items { get; set; }

    [Display(
         Name = "Internet Card Items Tab Container",
         Description = "Content Area for holding a list of Internet Card Tab Container.",
         GroupName = SystemTabNames.Content,
         Order = 20)]
    [AllowedTypes(new[] { typeof(TabContainerBlock) })]
    public virtual ContentArea TabContainer {get;set;}
}

