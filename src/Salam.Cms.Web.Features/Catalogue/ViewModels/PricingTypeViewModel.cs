namespace Salam.Cms.Web.Features.Catalogue.ViewModels;

using Salam.Cms.Shared.Models.Catalogue.Enums;
using Salam.Cms.Shared.Models.Catalogue.Models;
using System.Collections.Generic;

public class PricingTypeViewModel
{
    public List<string> VisibleFields { get; set; }

    public string ProductType { get; set; }

    public PricingType PricingType { get; set; }

    public ProductSku Product { get; set; }

    public List<ProductPlanDetailsViewModel> PlanDetailsLinks { get; set; }

    public string HandOffUrl { get; set; }
}
