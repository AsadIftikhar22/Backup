namespace Salam.Cms.Web.Features.CustomerRightsBlock.Models;

using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using Geta.Optimizely.ContentTypeIcons;
using Geta.Optimizely.ContentTypeIcons.Attributes;
using Salam.Cms.Shared.Models.Common;
using Salam.Cms.Shared.Models.Common.RichText;
using Salam.Cms.Web.Features.Common.Models;
using System.ComponentModel.DataAnnotations;

[ContentType(
    DisplayName = "Customer Rights Section Block",
    GUID = "E612F7A8-C9D0-4B1E-2F3A-4B5C6D7E8F90",
    Description = "Customer Rights and Responsibilities section with heading, introduction and cards.",
    GroupName = SystemTabNames.Content)]
[ContentTypeIcon(FontAwesome5Solid.UserShield)]
public class CustomerRightsSectionBlock : SiteContentBlock
{
    [Display(
        Name = "Heading",
        Description = "Section heading (e.g. Customer Rights and Responsibilities).",
        GroupName = GroupNames.Content,
        Order = 10)]
    [CultureSpecific]
    public virtual string? Heading { get; set; }

    [Display(
        Name = "Introduction",
        Description = "Intro text. Use rich text for bold (e.g. Salam Mobile).",
        GroupName = GroupNames.Content,
        Order = 20)]
    [CultureSpecific]
    [UIHint(RichTextEditors.HtmlEditor)]
    public virtual XhtmlString? Introduction { get; set; }

    [Display(
        Name = "Cards",
        Description = "Add cards: click Select Content then Create new and choose Customer Rights Card Block.",
        GroupName = GroupNames.Content,
        Order = 30)]
    [AllowedTypes(typeof(CustomerRightsCardBlock))]
    public virtual ContentArea? Cards { get; set; }
}
