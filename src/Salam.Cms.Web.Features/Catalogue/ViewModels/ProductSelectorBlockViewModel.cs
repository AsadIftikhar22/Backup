namespace Salam.Cms.Web.Features.Catalogue.ViewModels;

using EPiServer.Core;
using EPiServer.SpecializedProperties;
using Salam.Cms.Shared.Models.Catalogue.Data;
using Salam.Cms.Shared.Models.Catalogue.Models;
using Salam.Cms.Web.Features.Catalogue.Models;
using Salam.Cms.Web.Features.Common.ViewModels;

public class ProductSelectorBlockViewModel : BlockViewModel<ProductSelectorBlock>
{
    public PageData? CurrentPage { get; set; }

    public string Title { get; set; } = string.Empty;

    public XhtmlString? Description { get; set; }

    public IList<ProductSku> Products { get; set; } = new List<ProductSku>();

    public IList<string> VisibleFieldsFeatured { get; set; } = new List<string>();

    public IList<string> VisibleFields { get; set; } = new List<string>();

    public List<FrontEndLabelInfo> Labels { get; set; }

    public Category ProductCatalogueCategory { get; set; }

    public string? NavigationTitle { get; set; }

    public string? HandoffUrl { get; set; } = string.Empty;

    public string? ProductButtonText { get; set; } = string.Empty;

    public string? PlanDetailsText { get; set; } = string.Empty;
    public LinkItem BuyNowStaticURL { get; set; }
    public IList<ProductPlanDetailsViewModel> PlanDetailsLinks { get; set; } = new List<ProductPlanDetailsViewModel>();         
    public bool AreTabsDisabled { get; set; }

    public bool IsFooterDisabled { get; set; }

    public bool ShowFlags { get; set; }

    public string? FooterText { get; set; }

    public IList<ProductBlock> OverrideProducts { get; set; } = new List<ProductBlock>();
    public string? Span { get; set; }
    public string? DiscountedPriceSpan { get; set; }
    public string? SpanWhereValidityIsNull { get; set; }

    public string? VatText { get; set; }

    public string? BadgeText { get; set; }

    public string? DataText { get; set; }

    public string? CallLabel { get; set; }

    public string? CallAmountText { get; set; }
    public string? BuyButtonRedirection { get; set; }

    public ProductSelectorBlockViewModel(ProductSelectorBlock currentBlock) : base(currentBlock)
    {
        Products = new List<ProductSku>();
    }

    public ProductSelectorBlockViewModel(ProductSelectorBlock currentBlock, IList<ProductSku> products) : base(currentBlock)
    {
        Products = products;
    }
}
