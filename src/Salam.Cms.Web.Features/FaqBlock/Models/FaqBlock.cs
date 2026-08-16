namespace Salam.Cms.Web.Features.FaqBlock.Models;

using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using Geta.Optimizely.ContentTypeIcons;
using Geta.Optimizely.ContentTypeIcons.Attributes;
using Salam.Cms.Shared.Models.Common;
using Salam.Cms.Web.Features.Common.Models;
using System.ComponentModel.DataAnnotations;

[ContentType(
    DisplayName = "FAQ Block",
    GUID = "B2C3D4E5-F6A7-4B8C-9D0E-1F2A3B4C5D6E",
    Description = "A block that displays a list of frequently asked questions.",
    GroupName = SystemTabNames.Content)]
[ContentTypeIcon(FontAwesome5Solid.QuestionCircle)]
public class FaqBlock : SiteContentBlock
{
    [Display(
        Name = "Heading",
        Description = "Optional heading for the FAQ section.",
        GroupName = GroupNames.Content,
        Order = 10)]
    [CultureSpecific]
    public virtual string? Heading { get; set; }

    [Display(
        Name = "Description",
        Description = "Optional description text for the FAQ section.",
        GroupName = GroupNames.Content,
        Order = 20)]
    [CultureSpecific]
    public virtual string? Description { get; set; }

    [Display(
        Name = "FAQ Items",
        Description = "Add Q&As here: click 'Select Content' then 'Create new' (or +) and choose 'FAQ Item Block'. Fill Question and Answer, save. Repeat to add more.",
        GroupName = GroupNames.Content,
        Order = 30)]
    [AllowedTypes(typeof(FaqItemBlock))]
    public virtual ContentArea? Items { get; set; }
}

