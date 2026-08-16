namespace Salam.Cms.Web.Features.InternetCards.Models;

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
    DisplayName = "DXP B2B Internet CardBlock",
    GUID = "6b999ae1-c976-40bb-84bb-9146a0bb2750",
    Description = "Displays a DXP B2B Internet Card Grid Items which are dropped on Internet Card Grid",
    GroupName = SystemTabNames.Content)]
[ContentTypeIcon(FontAwesome5Solid.List)]
public class InternetCardsBlock : SiteContentBlock
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
    [UIHint(RichTextEditors.HtmlEditor)]
    public virtual XhtmlString? Description { get; set; }

    [Display(
     Name = "Labels",
     Description = "Labels",
     GroupName = SystemTabNames.Content,
     Order = 30)]
    [CultureSpecific]
    public virtual IList<string>? Labels { get; set; }

    [Display(
   Name = "Internet List",
   Description = "Internet List",
   GroupName = SystemTabNames.Content,
   Order = 30)]
    [CultureSpecific]
    public virtual IList<string>? InternetList { get; set; }

    //[Display(
    //     Name = "View Detail Cta Button",
    //     Description = "View Detail Cta Button",
    //     GroupName = SystemTabNames.Content,
    //     Order = 40)]
    //[CultureSpecific]
    //public virtual LinkItem? ViewDetailCtaButton { get; set; }

    [Display(
     Name = "Enquire Cta Button",
     Description = "Enquire Cta Button",
     GroupName = SystemTabNames.Content,
     Order = 60)]
    [CultureSpecific]
    public virtual LinkItem? EnquireCtaButton { get; set; }
}
