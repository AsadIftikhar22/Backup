namespace Salam.Cms.Web.Features.TelecomItRightsBlock.Models;

using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using Geta.Optimizely.ContentTypeIcons;
using Geta.Optimizely.ContentTypeIcons.Attributes;
using Salam.Cms.Shared.Models.Common;
using Salam.Cms.Shared.Models.Common.RichText;
using System.ComponentModel.DataAnnotations;

[ContentType(
    DisplayName = "Telecom/IT Rights Accordion Part",
    GUID = "D925C0E1-F2A3-4B5C-6D7E-8F9014273847",
    Description = "One accordion part (e.g. PART 1 – GENERAL). Add via Telecom/IT Rights Section Block → Accordion Parts.",
    GroupName = SystemTabNames.Content)]
[ContentTypeIcon(FontAwesome5Solid.ChevronDown)]
public class TelecomItRightsAccordionItemBlock : BlockData
{
    [Display(
        Name = "Title",
        Description = "Part title (e.g. PART 1 – GENERAL).",
        GroupName = GroupNames.Content,
        Order = 10)]
    [CultureSpecific]
    public virtual string? Title { get; set; }

    [Display(
        Name = "Body",
        Description = "Part content (articles, paragraphs).",
        GroupName = GroupNames.Content,
        Order = 20)]
    [CultureSpecific]
    [UIHint(RichTextEditors.HtmlEditor)]
    public virtual XhtmlString? Body { get; set; }

    [Display(
        Name = "Expand by default",
        Description = "Open this part on page load (typically only Part 1).",
        GroupName = GroupNames.Content,
        Order = 30)]
    public virtual bool ExpandByDefault { get; set; }
}
