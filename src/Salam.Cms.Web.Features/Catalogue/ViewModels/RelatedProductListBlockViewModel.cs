namespace Salam.Cms.Web.Features.Catalogue.ViewModels;

using Salam.Cms.Shared.Models.Catalogue.Models;
using Salam.Cms.Web.Features.Catalogue.Models;
using Salam.Cms.Web.Features.Common.ViewModels;
using System.Collections.Generic;

public class RelatedProductListBlockViewModel : BlockViewModel<RelatedProductListBlock>
{
    // public IList<ProductSku> Products { get; set; }

    public RelatedProductListBlockViewModel(RelatedProductListBlock currentBlock) : base(currentBlock)
    {
        // Products = new List<ProductSku>();
    }

    //public RelatedProductListBlockViewModel(FeaturedProductListBlock currentBlock, IList<ProductSku> products, IList<ProductSku> manualProducts) : base(currentBlock)
    //{
    //    Products = manualProducts.Union(products).Take(5).ToList();
    //}

    public ProductDetailPage? CurrentPage { get; set; }
    public ProductLandingPage? ParentPage { get; set; }
    public List<ProductSku> RelatedProducts { get; internal set; }
    public IEnumerable<ProductPlanDetailsViewModel> PlanDetailsLinks { get; set; } = new List<ProductPlanDetailsViewModel>();
    public string? HandoffUrl { get; set; } = string.Empty;
    public string? ProductButtonText { get; set; } = string.Empty;
    public string? PlanDetailsText { get; set; } = string.Empty;
    public string? ViewAllLinkText { get; set; } = string.Empty;
    public string? Span { get; set; } = string.Empty;
    public string? DiscountedPriceSpan { get; set; } = string.Empty;
    public string? VatText { get; set; } = string.Empty;
    public string? BadgeText { get; set; }
    public List<FrontEndLabelInfo> Labels { get; set; }

}
