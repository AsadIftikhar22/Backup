namespace Salam.Cms.Web.Features.OurStoresBlock.Models;

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
    DisplayName = "Our Stores Section Block",
    GUID = "F935B0E1-A2C3-4D5E-6F7A-8B9C0D1E2F3A",
    Description = "Our Stores locations section with heading and two columns (left/right).",
    GroupName = SystemTabNames.Content)]
[ContentTypeIcon(FontAwesome5Solid.MapMarkerAlt)]
public class OurStoresSectionBlock : SiteContentBlock
{
    [Display(
        Name = "Heading",
        Description = "Section heading (e.g. Our Stores Locations).",
        GroupName = GroupNames.Content,
        Order = 10)]
    [CultureSpecific]
    public virtual string? Heading { get; set; }

    [Display(
        Name = "Left Column Content",
        Description = "Stores list for left column (regions: Riyadh, Central, North, South). Use headings (h3/h4), paragraphs and links.",
        GroupName = GroupNames.Content,
        Order = 20)]
    [CultureSpecific]
    [UIHint(RichTextEditors.HtmlEditor)]
    public virtual XhtmlString? LeftColumnContent { get; set; }

    [Display(
        Name = "Right Column Content",
        Description = "Stores list for right column (regions: Eastern, Western). Use headings (h3/h4), paragraphs and links.",
        GroupName = GroupNames.Content,
        Order = 30)]
    [CultureSpecific]
    [UIHint(RichTextEditors.HtmlEditor)]
    public virtual XhtmlString? RightColumnContent { get; set; }
}
