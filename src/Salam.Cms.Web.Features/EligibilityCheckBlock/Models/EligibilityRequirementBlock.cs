using EPiServer;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Web;
using System.ComponentModel.DataAnnotations;

namespace Salam.Cms.Web.Features.Eligibility.Models;

[ContentType(
    DisplayName = "Eligibility Requirement",
    GUID = "D91C6B72-3E1A-4B85-8C29-7F4A2E6B2002",
    Description = "A single eligibility requirement."
)]
public class EligibilityRequirementBlock : BlockData
{
    [Display(
        Name = "Title",
        GroupName = SystemTabNames.Content,
        Order = 10)]
    [CultureSpecific]
    [Required]
    public virtual string Title { get; set; }


    [Display(
        Name = "Description",
        GroupName = SystemTabNames.Content,
        Order = 20)]
    [CultureSpecific]
    public virtual string Description { get; set; }


    [Display(
        Name = "Icon",
        Description = "Icon used for this requirement",
        GroupName = SystemTabNames.Content,
        Order = 30)]
    [CultureSpecific]
    [UIHint(UIHint.Image)]
    public virtual ContentReference Icon { get; set; }

    [Display(
        Name = "Information Icon",
        Description = "Information Icon",
        GroupName = SystemTabNames.Content,
        Order = 40)]
    [CultureSpecific]
    [UIHint(UIHint.Image)]
    public virtual ContentReference InformationIcon { get; set; }

    [Display(
    Name = "Information Modal Title",
    GroupName = SystemTabNames.Content,
    Order = 50)]
    [CultureSpecific]
    public virtual string InformationModalTitle { get; set; }

    [Display(
    Name = "Information Modal Description",
    GroupName = SystemTabNames.Content,
    Order = 60)]
    [CultureSpecific]
    public virtual XhtmlString InformationModalDescription { get; set; }

    [Display(
        Name = "Show Information Icon",
        GroupName = SystemTabNames.Content,
        Order = 40)]
    [CultureSpecific]
    public virtual bool ShowInformationIcon { get; set; }

    [Display(
    Name = "i Icon image outside Modal",
    Description = "i Icon image outside Modal",
    GroupName = SystemTabNames.Content,
    Order = 40)]
    [CultureSpecific]
    [UIHint(UIHint.Image)]
    public virtual ContentReference IIcon { get; set; }

    [Display(
        Name = "Show Registration Bar",
        Description = "Shows the registration message below this requirement.",
        GroupName = SystemTabNames.Content,
        Order = 60)]
    [CultureSpecific]
    public virtual bool ShowRegistrationBar { get; set; }


    [Display(
        Name = "Registration Text",
        GroupName = SystemTabNames.Content,
        Order = 70)]
    [CultureSpecific]
    public virtual string RegistrationText { get; set; }


    [Display(
        Name = "Registration Link Text",
        GroupName = SystemTabNames.Content,
        Order = 80)]
    [CultureSpecific]
    public virtual string RegistrationLinkText { get; set; }

    [Display(
    Name = "Registration Modal Title",
    GroupName = SystemTabNames.Content,
    Order = 90)]
    [CultureSpecific]
    public virtual string RegistrationModalTitle { get; set; }
    
    [Display(
        Name = "Registration Modal Description",
        GroupName = SystemTabNames.Content,
        Order = 100)]
    [CultureSpecific]
    public virtual XhtmlString RegistrationModalDescription { get; set; }

    [Display(
            Name = "Registration Modal Image",
            GroupName = SystemTabNames.Content,
            Order = 110)]
    [CultureSpecific]
    [UIHint(UIHint.Image)]
    public virtual ContentReference RegistrationModalImage { get; set; }

    [Display(
        Name = "Registration Modal Link",
        GroupName = SystemTabNames.Content,
        Order = 115)]
    [CultureSpecific]
    public virtual Url RegistrationModalLink { get; set; }

    [Display(
    Name = "Registration Modal Button Text",
    GroupName = SystemTabNames.Content,
    Order = 120)]
    [CultureSpecific]
    public virtual string RegistrationModalButtonText { get; set; }
}