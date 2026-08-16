namespace Salam.Cms.Web.Features.Eligibility;

using EPiServer;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Web;
using Salam.Cms.Web.Features.Eligibility.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

[ContentType(
    DisplayName = "Eligibility Check",
    GUID = "B8E3A7E1-2C54-4A90-9D41-6D8E5F2A1001",
    Description = "Displays the eligibility check requirements and Get Started button."
)]
public class EligibilityCheckBlock : BlockData
{
    [Display(
        Name = "Title",
        Description = "Main heading",
        GroupName = SystemTabNames.Content,
        Order = 10)]
    [CultureSpecific]
    public virtual string Title { get; set; }


    [Display(
        Name = "Description",
        Description = "Description shown below the title",
        GroupName = SystemTabNames.Content,
        Order = 20)]
    [UIHint(UIHint.Textarea)]
    [CultureSpecific]
    public virtual string Description { get; set; }


    [Display(
        Name = "Requirements",
        Description = "Eligibility requirements",
        GroupName = SystemTabNames.Content,
        Order = 30)]
    [CultureSpecific]
    [AllowedTypes(typeof(EligibilityRequirementBlock))]
    public virtual ContentArea Requirements { get; set; }


    [Display(
        Name = "Get Started Button Text",
        GroupName = SystemTabNames.Content,
        Order = 40)]
    [CultureSpecific]
    public virtual string ButtonText { get; set; }


    [Display(
        Name = "Get Started Button Link",
        GroupName = SystemTabNames.Content,
        Order = 50)]
    [CultureSpecific]
    public virtual Url ButtonLink { get; set; }
}