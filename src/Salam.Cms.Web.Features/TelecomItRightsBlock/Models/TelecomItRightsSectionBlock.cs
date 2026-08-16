namespace Salam.Cms.Web.Features.TelecomItRightsBlock.Models;

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
    DisplayName = "Telecom/IT Rights Section Block",
    GUID = "C814B9D0-E1F2-4A3B-5C6D-7E8F90142636",
    Description = "Telecom and IT User Rights Regulations section with introduction and accordion parts.",
    GroupName = SystemTabNames.Content)]
[ContentTypeIcon(FontAwesome5Solid.Gavel)]
public class TelecomItRightsSectionBlock : SiteContentBlock
{
    [Display(
        Name = "Heading",
        Description = "Main heading (e.g. Telecom and IT User Rights Regulations).",
        GroupName = GroupNames.Content,
        Order = 10)]
    [CultureSpecific]
    public virtual string? Heading { get; set; }

    [Display(
        Name = "Introduction Title",
        Description = "Introduction section title (e.g. Introduction).",
        GroupName = GroupNames.Content,
        Order = 20)]
    [CultureSpecific]
    public virtual string? IntroductionTitle { get; set; }

    [Display(
        Name = "Introduction Content",
        Description = "Introduction text and lists. Use RTE for paragraphs and numbered lists.",
        GroupName = GroupNames.Content,
        Order = 30)]
    [CultureSpecific]
    [UIHint(RichTextEditors.HtmlEditor)]
    public virtual XhtmlString? IntroductionContent { get; set; }

    [Display(
        Name = "Accordion Parts",
        Description = "Add parts: Select Content → Create new → Telecom/IT Rights Accordion Part. Set first part 'Expand by default' to Yes.",
        GroupName = GroupNames.Content,
        Order = 40)]
    [AllowedTypes(typeof(TelecomItRightsAccordionItemBlock))]
    public virtual ContentArea? AccordionItems { get; set; }
}
