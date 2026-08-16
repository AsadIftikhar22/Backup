namespace Salam.Cms.Web.Features.IconLinks.Models;

using AngleSharp.Css.Dom;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Web;
using Geta.Optimizely.ContentTypeIcons;
using Geta.Optimizely.ContentTypeIcons.Attributes;
using Salam.Cms.Shared.Models.Common;
using Salam.Cms.Shared.Models.Common.RichText;
using Salam.Cms.Web.Features.Common.Interfaces;
using Salam.Cms.Web.Features.Common.Models;
using System.ComponentModel.DataAnnotations;

[ContentType(
    DisplayName = "Icon Link Item List Block",
    GUID = "50145190-2986-4cd8-b66b-aa045e54f35f",
    Description = "Displays a list of icon link items.",
    GroupName = SystemTabNames.Content)]
[ContentTypeIcon(FontAwesome5Solid.List)]
public class IconLinkItemListBlock : SiteContentBlock
{
    [Display(
        Name = "Heading",
        Description = "The heading to be displayed with the icon link items.",
        GroupName = GroupNames.Content,
        Order = 10)]
    [CultureSpecific]
    public virtual string? Heading { get; set; }

    [Display(
        Name = "Description",
        Description = "The description to be displayed with the icon link items.",
        GroupName = GroupNames.Content,
        Order = 20)]
    [CultureSpecific]
    [UIHint(RichTextEditors.ReducedEditor)]
    public virtual XhtmlString? Description { get; set; }

    [Display(
        Name = "Icon Link Items",
        Description = "The list of icon link items to be displayed.",
        GroupName = GroupNames.Content,
        Order = 30)]
    [AllowedTypes(typeof(IconLinkItemBlock), typeof(ISitePageData))]
    public virtual ContentArea? Items { get; set; }

    [Display(
    Name = "New DXP UI",
    Description = "New DXP UI",
    GroupName = GroupNames.Content,
    Order = 40)]
    public virtual bool NewDXPUI { get; set; }

    [Display(
            Name = "Is B2b Layout",
            Description = "Is B2b Layout",
            GroupName = GroupNames.BusinessComponentTab,
            Order = 50)]
    public virtual bool IsB2bLayout { get; set; }

    [Display(
        Name = "B2B Heading",
        Description = "B2B Heading",
        GroupName = GroupNames.BusinessComponentTab,
        Order = 60)]
    [CultureSpecific]
    public virtual string B2BHeading { get; set; }
}
