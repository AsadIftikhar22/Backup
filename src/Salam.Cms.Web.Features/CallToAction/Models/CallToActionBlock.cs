namespace Salam.Cms.Web.Features.CallToAction.Models;

using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.SpecializedProperties;
using Geta.Optimizely.ContentTypeIcons;
using Geta.Optimizely.ContentTypeIcons.Attributes;
using Salam.Cms.Shared.Models.Common;
using Salam.Cms.Shared.Models.Common.RichText;
using Salam.Cms.Web.Features.CallToAction.Abstract;
using Salam.Cms.Web.Features.Common.Models;
using System.ComponentModel.DataAnnotations;

[ContentType(
    DisplayName = "Call To Action",
    Description = "A block that allows for a call to action to be rendered inline on pages.",
    GUID = "b53a7015-3237-4641-8ed2-77bddbd9a2da",
    GroupName = GroupNames.Content)]
[ContentTypeIcon(FontAwesome5Solid.Phone)]
public class CallToActionBlock : SiteContentBlock, ICallToAction
{
    [Display(
        Name = "Media",
        Description = "The image or video to display in the call to action block.",
        GroupName = GroupNames.Content,
        Order = 20)]
    [CultureSpecific]
    [Required]
    public virtual ContentReference? Media { get; set; }

    [Display(
        Name = "Badge Text",
        Description = "A badge to display at the top of the block.",
        GroupName = GroupNames.Content,
        Order = 30)]
    [CultureSpecific]
    public virtual string? BadgeText { get; set; }

    [Display(
        Name = "Heading Line One",
        Description = "The first line of the heading.",
        GroupName = GroupNames.Content,
        Order = 40)]
    [CultureSpecific]
    [Required]
    public virtual string? HeadingLineOne { get; set; }

    [Display(
        Name = "Heading Line Two",
        Description = "The second line of the heading.",
        GroupName = GroupNames.Content,
        Order = 50)]
    [CultureSpecific]
    public virtual string? HeadingLineTwo { get; set; }

    [Display(
        Name = "Main Body",
        Description = "The main body of the call to action block.",
        GroupName = GroupNames.Content,
        Order = 60)]
    [CultureSpecific]
    [UIHint(RichTextEditors.ReducedEditor)]
    public virtual XhtmlString? MainBody { get; set; }

    [Display(
        Name = "Link Items",
        Description = "A collection of links to display in the call to action block.",
        GroupName = GroupNames.Content,
        Order = 70)]
    [CultureSpecific]
    public virtual LinkItemCollection? LinkItems { get; set; }

    public override void SetDefaultValues(ContentType contentType)
    {
        base.SetDefaultValues(contentType);
    }
}
