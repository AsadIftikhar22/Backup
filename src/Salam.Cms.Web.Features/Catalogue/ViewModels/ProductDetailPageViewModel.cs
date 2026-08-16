namespace Salam.Cms.Web.Features.Catalogue.ViewModels;

using EPiServer.Core;
using Salam.Cms.Web.Features.Catalogue.Models;
using Salam.Cms.Web.Features.Common.ViewModels;

public class ProductDetailPageViewModel : SitePageViewModel<ProductDetailPage>
{
    public ProductDetailPageViewModel(ProductDetailPage currentPage) : base(currentPage)
    {
    }

    public ContentArea? ProxyMainContent { get; set; }
}