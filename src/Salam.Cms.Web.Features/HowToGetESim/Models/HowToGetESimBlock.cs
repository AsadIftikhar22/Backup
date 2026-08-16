namespace Salam.Cms.Web.Features.HowToGetESim.Models;

using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using Geta.Optimizely.ContentTypeIcons;
using Geta.Optimizely.ContentTypeIcons.Attributes;
using Salam.Cms.Shared.Models.Common;
using Salam.Cms.Shared.Models.Common.RichText;
using Salam.Cms.Web.Features.CallToAction.Abstract;
using Salam.Cms.Web.Features.Common.Interfaces;
using Salam.Cms.Web.Features.Common.Models;
using System.ComponentModel.DataAnnotations;

[ContentType(
    DisplayName = "How To Get ESim Block",
    GUID = "535ed9a5-268b-46e4-9a45-ee12e41a7c63",
    Description = "Displays a card and allows the content editor to add content to the card.",
    GroupName = SystemTabNames.Content)]
[ContentTypeIcon(FontAwesome5Solid.List)]
public class HowToGetESimBlock : SiteContentBlock, IHowToGetESimBlock, IPageNavigatorData
{
    [Display(
    Name = "Block Navigator Title",
    Description = "The title displayed in the page navigator when 'Enable Page Navigator' is activated on a page containing this block in the main content area.",
    GroupName = GroupNames.Navigation,
    Order = 5)]
    [CultureSpecific]
    public virtual string? NavigationTitle { get; set; }

    [Display(
        Name = "Heading",
        Description = "Heading for the Card Blocks.",
        GroupName = GroupNames.Content,
        Order = 10)]
    [CultureSpecific]
    public virtual string? Heading { get; set; }

    [Display(
    Name = "Description",
    Description = "Card Description.",
    GroupName = GroupNames.Content,
    Order = 20)]
    [CultureSpecific]
    [UIHint(RichTextEditors.HtmlEditor)]
    public virtual XhtmlString? MainDescription { get; set; }
}
