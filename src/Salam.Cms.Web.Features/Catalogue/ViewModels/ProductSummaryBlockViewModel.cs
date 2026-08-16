namespace Salam.Cms.Web.Features.Catalogue.ViewModels;

using Salam.Cms.Shared.Models.Catalogue.Models;
using Salam.Cms.Web.Features.Catalogue.Models;
using Salam.Cms.Web.Features.Common.ViewModels;

public class ProductSummaryBlockViewModel : BlockViewModel<ProductSummaryBlock>
{
    public ProductSummaryBlockViewModel(ProductSummaryBlock currentBlock) : base(currentBlock)
    {

    }
    public string currentPageCategory { get; set; }
    public string? SpanWhereValidityIsNull { get; set; }
    public ProductDetailPage? CurrentPage { get; set; }

    public ProductLandingPage? ParentPage { get; set; }

    public ProductSku? Product { get; set; }

    public string? HandoffUrl { get; set; } = string.Empty;

    public string? ProductButtonText { get; set; } = string.Empty;

    public string? BackLinkText { get; set; } = string.Empty;

    public string FooterText { get; set; }

    public List<FrontEndLabelInfo> Labels { get; set; }

    public string Name { get; set; }

    public string? Span { get; set; } = string.Empty;
    public string? DiscountedPriceSpan { get; set; } = string.Empty;
    public string? VatText { get; set; } = string.Empty;

    public int NumberOfFields { get; set; }
    public bool IsPlatformCardVisible { get; set; }
    public string BuyButtonRedirection { get; set; }
}