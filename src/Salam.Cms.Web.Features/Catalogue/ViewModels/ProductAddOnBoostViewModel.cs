namespace Salam.Cms.Web.Features.Catalogue.ViewModels;

using EPiServer.Core;
using EPiServer.SpecializedProperties;
using Salam.Cms.Shared.Models.Catalogue.Models;
using Salam.Cms.Web.Features.Catalogue.Models;
using Salam.Cms.Web.Features.Common.ViewModels;
using System.Collections.Generic;

public class ProductAddOnBoostViewModel : BlockViewModel<ProductAddOnBoostBlock>
{
    public ProductAddOnBoostViewModel(ProductAddOnBoostBlock? currentBlock) : base(currentBlock)
    {
    }
    public IList<string> VisibleFeaturesFields { get; set; } = new List<string>();
    public List<ProductSku> AddOns { get; set; }
    public string? NavigationTitle { get; set; }

    public string Title { get; set; } = string.Empty;

    public bool AddBannerNavigation { get; set; }
    public XhtmlString? Description { get; set; }

    public string? Span { get; set; } = string.Empty;

    public string? VatText { get; set; } = string.Empty;
    public LinkItem BuyNowStaticURL { get; set; }
}

public class ProductAddOnNewAddsOnViewModel : BlockViewModel<ProductAddOnNewAddsOnBlock>
{
    public ProductAddOnNewAddsOnViewModel(ProductAddOnNewAddsOnBlock? currentBlock) : base(currentBlock)
    {
    }
    public IList<string> VisibleFeaturesFields { get; set; } = new List<string>(); 
    public IList<string> VisibleFields { get; set; } = new List<string>();
    public List<ProductSku> AddOns { get; set; }
    public string? NavigationTitle { get; set; }
    public LinkItem BuyNowStaticURL { get; set; }
    public string Title { get; set; } = string.Empty;
    public string DataText { get; set; } = string.Empty;
    public bool AddBannerNavigation { get; set; }
    public XhtmlString? Description { get; set; }

    public string? Span { get; set; } = string.Empty;

    public string? VatText { get; set; } = string.Empty;
}
