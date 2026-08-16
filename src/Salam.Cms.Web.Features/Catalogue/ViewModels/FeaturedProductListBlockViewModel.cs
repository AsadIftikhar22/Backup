namespace Salam.Cms.Web.Features.Catalogue.ViewModels;

using Salam.Cms.Shared.Models.Catalogue.Models;
using Salam.Cms.Web.Features.Catalogue.Models;
using Salam.Cms.Web.Features.Common.ViewModels;

public class FeaturedProductListBlockViewModel : BlockViewModel<FeaturedProductListBlock>
{
    public IList<ProductSku> Products { get; set; }

    public string? HandoffUrl { get; set; } = string.Empty;

    public FeaturedProductListBlockViewModel(FeaturedProductListBlock currentBlock) : base(currentBlock)
    {
        Products = new List<ProductSku>();
    }
    public FeaturedProductListBlockViewModel(FeaturedProductListBlock currentBlock, IList<ProductSku> products, IList<ProductSku> manualProducts) : base(currentBlock)
    {
        Products = manualProducts.Union(products).Take(5).ToList();
    }
}
