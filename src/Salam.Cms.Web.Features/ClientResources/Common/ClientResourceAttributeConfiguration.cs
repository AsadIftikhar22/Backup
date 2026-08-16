namespace Salam.Cms.Web.Features.ClientResources.Common;

using System.ComponentModel.DataAnnotations;

public sealed class ClientResourceAttributeConfiguration
{
    [Display(
        Name = "Attribute",
        Order = 10)]
    [Required]
    public string? Key { get; set; }

    [Display(
        Name = "Value",
        Order = 20)]
    [Required]
    public string? Value { get; set; }
}
