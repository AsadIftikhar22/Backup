namespace Salam.Cms.Web.Features.UserRightsBlock.Models;

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
    DisplayName = "User Rights Section Block",
    GUID = "B713A8C9-D0E1-4F2A-3B4C-6D7E8F901424",
    Description = "User Rights & Responsibilities section with heading and two RTE sections.",
    GroupName = SystemTabNames.Content)]
[ContentTypeIcon(FontAwesome5Solid.ListOl)]
public class UserRightsSectionBlock : SiteContentBlock
{
    [Display(
        Name = "Heading",
        Description = "Main heading (e.g. User Rights & Responsibilities).",
        GroupName = GroupNames.Content,
        Order = 10)]
    [CultureSpecific]
    public virtual string? Heading { get; set; }

    [Display(
        Name = "Section 1 Title",
        Description = "First section heading (e.g. User Rights:).",
        GroupName = GroupNames.Content,
        Order = 20)]
    [CultureSpecific]
    public virtual string? Section1Title { get; set; }

    [Display(
        Name = "Section 1 Content",
        Description = "Content for first section (use lists, paragraphs).",
        GroupName = GroupNames.Content,
        Order = 30)]
    [CultureSpecific]
    [UIHint(RichTextEditors.HtmlEditor)]
    public virtual XhtmlString? Section1Content { get; set; }

    [Display(
        Name = "Section 2 Title",
        Description = "Second section heading (e.g. User Responsibilities:).",
        GroupName = GroupNames.Content,
        Order = 40)]
    [CultureSpecific]
    public virtual string? Section2Title { get; set; }

    [Display(
        Name = "Section 2 Content",
        Description = "Content for second section (use lists, paragraphs).",
        GroupName = GroupNames.Content,
        Order = 50)]
    [CultureSpecific]
    [UIHint(RichTextEditors.HtmlEditor)]
    public virtual XhtmlString? Section2Content { get; set; }
}
