namespace Salam.Cms.Shared.Models.Catalogue.Data;

using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using System.ComponentModel.DataAnnotations;


[ContentType(
    DisplayName = "Product Field",
    GUID = "d1a78913-81ef-447d-a0d3-7dcc8cdea3db",
    Description = "Represents a product field.",
    AvailableInEditMode = false)]
public class ProductField : BlockData
{
    [Display(Name = "Value", Order = 10)]
    public virtual string? Value { get; set; }

    [Display(Name = "Description", Order = 20)]
    public virtual string? Description { get; set; }

    [Display(Name = "Tooltip", Order = 20)]
    public virtual string? Tooltip { get; set; }
}
