namespace Salam.Cms.Web.Features.SolutionsSectionsBlock.Models;

using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.SpecializedProperties;
using Geta.Optimizely.ContentTypeIcons;
using Geta.Optimizely.ContentTypeIcons.Attributes;
using Salam.Cms.Shared.Models;
using Salam.Cms.Shared.Models.Common;
using Salam.Cms.Shared.Models.Common.RichText;
using Salam.Cms.Web.Features.Common.Models;
using System.ComponentModel.DataAnnotations;

[ContentType(
    DisplayName = "DXP B2B 2 By 2 Solutions Block",
    GUID = "bc80908e-e052-4fb1-a434-2830cccf2da0",
    Description = "DXP B2B 2 By 2 Solutions Block",
    GroupName = SystemTabNames.Content)]
[ContentTypeIcon(FontAwesome5Solid.List)]
public class DXPB2BSolutionsSectionsBlock : SiteContentBlock
{
    [Display(
        Name = "Center Heading",
        GroupName = SystemTabNames.Content,
        Order = 10)]
    [CultureSpecific]
    public virtual string? Heading { get; set; }

    [Display(
         Name = "Center Description",
         Description = "Center Description",
         GroupName = SystemTabNames.Content,
         Order = 20)]
    [CultureSpecific]
    public virtual string? Description { get; set; }

    [Display(
    Name = "Heading Solution section card 1",
    GroupName = SystemTabNames.Content,
    Order = 30)]
    [CultureSpecific]
    public virtual string? Card1Heading { get; set; }

    [Display(
         Name = "Solution section card 1 Description",
         Description = "Solution section card 1 Description",
         GroupName = SystemTabNames.Content,
         Order = 35)]
    [CultureSpecific]
    public virtual string? Card1Description { get; set; }

    [Display(
    Name = "Card Cta Button 1",
    Description = "Card Cta Button 1",
    GroupName = SystemTabNames.Content,
    Order = 40)]
    [CultureSpecific]
    public virtual LinkItem? Card1Cta { get; set; }


    [Display(
    Name = "Heading Solution section card 2",
    GroupName = SystemTabNames.Content,
    Order = 45)]
    [CultureSpecific]
    public virtual string? Card2Heading { get; set; }

    [Display(
         Name = "Solution section card 2 Description",
         Description = "Solution section card 2 Description",
         GroupName = SystemTabNames.Content,
         Order = 50)]
    [CultureSpecific]
    public virtual string? Card2Description { get; set; }

    [Display(
         Name = "Card Cta Button 2",
         Description = "Card Cta Button 2",
         GroupName = SystemTabNames.Content,
         Order = 40)]
    [CultureSpecific]
    public virtual LinkItem? Card2Cta { get; set; }

    [Display(
        Name = "Heading Solution section card 3",
        GroupName = SystemTabNames.Content,
        Order = 45)]
    [CultureSpecific]
    public virtual string? Card3Heading { get; set; }

    [Display(
         Name = "Solution section card 3 Description",
         Description = "Solution section card 3 Description",
         GroupName = SystemTabNames.Content,
         Order = 50)]
    [CultureSpecific]
    public virtual string? Card3Description { get; set; }

    [Display(
         Name = "Card Cta Button 3",
         Description = "Card Cta Button 3",
         GroupName = SystemTabNames.Content,
         Order = 55)]
    [CultureSpecific]
    public virtual LinkItem? Card3Cta { get; set; }

    [Display(
    Name = "Solution section card 4 Heading",
    GroupName = SystemTabNames.Content,
    Order = 60)]
    [CultureSpecific]
    public virtual string? Card4Heading { get; set; }

    [Display(
         Name = "Solution section card 4 Description",
         Description = "Solution section card 4 Description",
         GroupName = SystemTabNames.Content,
         Order = 65)]
    [CultureSpecific]
    public virtual string? Card4Description { get; set; }

    [Display(
         Name = "Card Cta Button 4",
         Description = "Card Cta Button 4",
     GroupName = SystemTabNames.Content,
     Order = 70)]
    [CultureSpecific]
    public virtual LinkItem? Card4Cta { get; set; }

    [Display(
        Name = "Icon 1",
        Description = "The icon to be displayed on card 1.",
        GroupName = GroupNames.SolutionsBlockIcons,
        Order = 10)]
    [UIHint(SalamUIHint.IconLibrary)]
    public virtual ContentReference? Icon1 { get; set; }
    
    [Display(
    Name = "Icon 2",
    Description = "The icon to be displayed on card 2.",
    GroupName = GroupNames.SolutionsBlockIcons,
    Order = 10)]
    [UIHint(SalamUIHint.IconLibrary)]
    public virtual ContentReference? Icon2 { get; set; }

    [Display(
    Name = "Icon 3",
    Description = "The icon to be displayed on card 3.",
    GroupName = GroupNames.SolutionsBlockIcons,
    Order = 10)]
    [UIHint(SalamUIHint.IconLibrary)]
    public virtual ContentReference? Icon3 { get; set; }

    [Display(
    Name = "Icon 4",
    Description = "The icon to be displayed on card 4.",
    GroupName = GroupNames.SolutionsBlockIcons,
    Order = 10)]
    [UIHint(SalamUIHint.IconLibrary)]
    public virtual ContentReference? Icon4 { get; set; }
}
