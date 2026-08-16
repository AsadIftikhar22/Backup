namespace Salam.Cms.Web.Features.SolutionsSectionsBlock.Models;

using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.SpecializedProperties;
using Geta.Optimizely.ContentTypeIcons;
using Geta.Optimizely.ContentTypeIcons.Attributes;
using Salam.Cms.Shared.Models.Common.RichText;
using Salam.Cms.Web.Features.Common.Models;
using System.ComponentModel.DataAnnotations;

[ContentType(
    DisplayName = "DXP B2B Solutions Sections Block 4 By 4",
    GUID = "c98f2bd8-cec2-4b45-aa13-ea79d6c7cdbf",
    Description = "DXP B2B Solutions Sections Block 4 By 4",
    GroupName = SystemTabNames.Content)]
[ContentTypeIcon(FontAwesome5Solid.List)]
public class SolutionsSections4By4Block : SiteContentBlock
{
    [Display(
        Name = "Heading",
        GroupName = SystemTabNames.Content,
        Order = 10)]
    [CultureSpecific]
    public virtual string? Heading { get; set; }

    [Display(
         Name = "Description",
         Description = "Description",
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
         Order = 40)]
    [CultureSpecific]
    public virtual string? Card1Description { get; set; }


    [Display(
    Name = "Heading Solution section card 2",
    GroupName = SystemTabNames.Content,
    Order = 50)]
    [CultureSpecific]
    public virtual string? Card2Heading { get; set; }

    [Display(
         Name = "Solution section card 2 Description",
         Description = "Solution section card 2 Description",
         GroupName = SystemTabNames.Content,
         Order = 60)]
    [CultureSpecific]
    public virtual string? Card2Description { get; set; }

    [Display(
    Name = "Heading Solution section card 3",
    GroupName = SystemTabNames.Content,
    Order = 70)]
    [CultureSpecific]
    public virtual string? Card3Heading { get; set; }

    [Display(
         Name = "Solution section card 3 Description",
         Description = "Solution section card 3 Description",
         GroupName = SystemTabNames.Content,
         Order = 80)]
    [CultureSpecific]
    public virtual string? Card3Description { get; set; }

    [Display(
    Name = "Solution section card 4 Heading",
    GroupName = SystemTabNames.Content,
    Order = 90)]
    [CultureSpecific]
    public virtual string? Card4Heading { get; set; }

    [Display(
         Name = "Solution section card 4 Description",
         Description = "Solution section card 4 Description",
         GroupName = SystemTabNames.Content,
         Order = 100)]
    [CultureSpecific]
    public virtual string? Card4Description { get; set; }

    [Display(
        Name = "Solution section card 5 Heading",
        GroupName = SystemTabNames.Content,
        Order = 110)]
    [CultureSpecific]
    public virtual string? Card5Heading { get; set; }

    [Display(
         Name = "Solution section card 5 Description",
         Description = "Solution section card 5 Description",
         GroupName = SystemTabNames.Content,
         Order = 120)]
    [CultureSpecific]
    public virtual string? Card5Description { get; set; }

    [Display(
        Name = "Solution section card 6 Heading",
        GroupName = SystemTabNames.Content,
        Order = 130)]
    [CultureSpecific]
    public virtual string? Card6Heading { get; set; }

    [Display(
         Name = "Solution section card 6 Description",
         Description = "Solution section card 6 Description",
         GroupName = SystemTabNames.Content,
         Order = 140)]
    [CultureSpecific]
    public virtual string? Card6Description { get; set; }

    [Display(
    Name = "Solution section card 7 Heading",
    GroupName = SystemTabNames.Content,
    Order = 130)]
    [CultureSpecific]
    public virtual string? Card7Heading { get; set; }

    [Display(
         Name = "Solution section card 7 Description",
         Description = "Solution section card 7 Description",
         GroupName = SystemTabNames.Content,
         Order = 140)]
    [CultureSpecific]
    public virtual string? Card7Description { get; set; }

    [Display(
    Name = "Solution section card 8 Heading",
    GroupName = SystemTabNames.Content,
    Order = 130)]
    [CultureSpecific]
    public virtual string? Card8Heading { get; set; }

    [Display(
         Name = "Solution section card 8 Description",
         Description = "Solution section card 8 Description",
         GroupName = SystemTabNames.Content,
         Order = 140)]
    [CultureSpecific]
    public virtual string? Card8Description { get; set; }

    [Display(
        Name = "Card Cta Button 1",
        Description = "Card Cta Button 1",
        GroupName = SystemTabNames.Content,
        Order = 150)]
    [CultureSpecific]
    public virtual LinkItem? Card1Cta { get; set; }

    [Display(
     Name = "Card Cta Button 2",
     Description = "Card Cta Button 2",
     GroupName = SystemTabNames.Content,
     Order = 160)]
    [CultureSpecific]
    public virtual LinkItem? Card2Cta { get; set; }

    [Display(
     Name = "Card Cta Button 3",
     Description = "Card Cta Button 3",
     GroupName = SystemTabNames.Content,
     Order = 170)]
    [CultureSpecific]
    public virtual LinkItem? Card3Cta { get; set; }

    [Display(
         Name = "Card Cta Button 4",
         Description = "Card Cta Button 4",
     GroupName = SystemTabNames.Content,
     Order = 180)]
    [CultureSpecific]
    public virtual LinkItem? Card4Cta { get; set; }

    [Display(
     Name = "Card Cta Button 5",
     Description = "Card Cta Button 5",
     GroupName = SystemTabNames.Content,
     Order = 190)]
    [CultureSpecific]
    public virtual LinkItem? Card5Cta { get; set; }

    [Display(
     Name = "Card Cta Button 6",
     Description = "Card Cta Button 6",
     GroupName = SystemTabNames.Content,
     Order = 200)]
    [CultureSpecific]
    public virtual LinkItem? Card6Cta { get; set; }

    [Display(
         Name = "Card Cta Button 7",
         Description = "Card Cta Button 7",
         GroupName = SystemTabNames.Content,
         Order = 200)]
    [CultureSpecific]
    public virtual LinkItem? Card7Cta { get; set; }

    [Display(
         Name = "Card Cta Button 8",
         Description = "Card Cta Button 8",
         GroupName = SystemTabNames.Content,
         Order = 200)]
    [CultureSpecific]
    public virtual LinkItem? Card8Cta { get; set; }
}
