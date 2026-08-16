namespace Salam.Cms.Web.Features.Catalogue.ViewModels;

using Salam.Cms.Shared.Models.Catalogue.Models;

public class ProductSkuViewModel
{
    public ProductSku Item { get; set; } = new ProductSku();

    public string? HandoffUrl { get; set; } = string.Empty;
}
