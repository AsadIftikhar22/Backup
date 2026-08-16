namespace Salam.Cms.Web.Features.FaqBlock.Models;

using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using Geta.Optimizely.ContentTypeIcons;
using Geta.Optimizely.ContentTypeIcons.Attributes;
using Salam.Cms.Shared.Models.Common;
using Salam.Cms.Shared.Models.Common.RichText;
using System.ComponentModel.DataAnnotations;

[ContentType(
    DisplayName = "FAQ Item Block (Q&A)",
    GUID = "C3D4E5F6-A7B8-4C9D-0E1F-2A3B4C5D6E7F",
    Description = "One question and answer. Add via FAQ Block → Items → Select Content → Create new.",
    GroupName = SystemTabNames.Content)]
[ContentTypeIcon(FontAwesome5Solid.Question)]
public class FaqItemBlock : BlockData
{
    [Display(
        Name = "Question",
        Description = "The question text.",
        GroupName = GroupNames.Content,
        Order = 10)]
    [Required]
    [CultureSpecific]
    public virtual string? Question { get; set; }

    [Display(
        Name = "Answer",
        Description = "The answer text. You can use rich text formatting.",
        GroupName = GroupNames.Content,
        Order = 20)]
    [Required]
    [CultureSpecific]
    [UIHint(RichTextEditors.HtmlEditor)]
    public virtual XhtmlString? Answer { get; set; }
}

