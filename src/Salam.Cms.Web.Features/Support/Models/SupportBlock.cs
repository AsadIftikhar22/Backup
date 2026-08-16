namespace Salam.Cms.Web.Features.Support.Models;

using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Shell.ObjectEditing;
using EPiServer.Web;
using Salam.Cms.Shared.Models.Common;
using Salam.Cms.Shared.Models.Common.Properties;
using Salam.Cms.Web.Features.Cards.Models;
using Salam.Cms.Web.Features.Common.Interfaces;
using Salam.Cms.Web.Features.Common.Models;
using Salam.Cms.Web.Features.IconLinks.Models;
using Salam.Cms.Web.Features.Support.Enums;
using System.ComponentModel.DataAnnotations;

[ContentType(
    DisplayName = "Support Block",
    GUID = "12194a96-7a14-44d1-8eba-52cd822887f1",
    Description = "Support Block",
    GroupName = GroupNames.Content)]
public class SupportBlock : SiteContentBlock
{
    [Display(
            Name = "Layout",
            Description = "Select the layout of the block.",
            GroupName = GroupNames.Content,
            Order = 5)]
    [CultureSpecific]
    [BackingType(typeof(PropertyNumber))]
    [EditorDescriptor(EditorDescriptorType = typeof(EnumEditorDescriptor<SupportLayoutOption>))]
    public virtual SupportLayoutOption Layout { get; set; }

    [Display(
        Name = "Heading Line One",
        Description = "Heading line one to show in Default and Featured layout.",
        GroupName = GroupNames.Content,
        Order = 5)]
    [CultureSpecific]
    public virtual string? HeadingLineOne { get; set; }

    [Display(
        Name = "Heading Line Two",
        Description = "Heading line two to show in Featured layout.",
        GroupName = GroupNames.Content,
        Order = 10)]
    [CultureSpecific]
    public virtual string? HeadingLineTwo { get; set; }

    [Display(
        Name = "Description",
        Description = "Description to show in Default layout.",
        GroupName = GroupNames.Content,
        Order = 15)]
    [CultureSpecific]
    [UIHint(UIHint.Textarea)]
    public virtual string? Description { get; set; }

    [Display(
         Name = "Items",
         Description = "Content Area for holding a list of Support Items.",
         GroupName = GroupNames.Content,
         Order = 20)]
    [AllowedTypes(new[] { typeof(IconLinkItemBlock), typeof(ISitePageData) })]
    public virtual ContentArea? Items { get; set; }

    [Display(
         Name = "Featured Item",
        Description = "Content Area for holding a single Featured Item. Only shown in Featured layout.",
         GroupName = GroupNames.Content,
         Order = 30)]
    [AllowedTypes(new[] { typeof(CardBlock) })]
    [MaxLength(1)]
    public virtual ContentArea? FeaturedItem { get; set; }

}