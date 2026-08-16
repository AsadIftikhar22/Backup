namespace Salam.Cms.Web.Features.Catalogue.ViewModels;

using EPiServer.SpecializedProperties;
using Salam.Cms.Shared.Models.Catalogue.Models;
using System.Collections.Generic;

public class AddOnViewModel
{
    public List<string> VisibleFields { get; set; }

    public List<string> VisibleFieldsFeatured { get; set; }

    public string ProductType { get; set; }

    public string CategoryName { get; set; }

    public ProductSku Product { get; set; }

    public List<ProductPlanDetailsViewModel> PlanDetailsLinks { get; set; }

    public string HandOffUrl { get; set; }

    public string Price { get; set; }

    public string Validity { get; set; }

    public string? PlanDetailsText { get; set; } = string.Empty;

    public string? ProductButtonText { get; set; } = string.Empty;
    public LinkItem BuyNowStaticURL { get; set; }
    public string Name { get; set; }

    public string? Span { get; set; } = string.Empty;

    public string? VatText { get; set; } = string.Empty;

    public string? DataText { get; set; } = string.Empty;

    public string? CallLabel { get; set; } = string.Empty;

    public string? CallAmountText { get; set; } = string.Empty;
}
