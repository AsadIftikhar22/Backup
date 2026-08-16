namespace Salam.Cms.Web.Features.Catalogue.Controllers;

using EPiServer;
using EPiServer.Core;
using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Salam.Cms.Web.Features.Catalogue.Models;
using Salam.Cms.Web.Features.Catalogue.ViewModels;

public class ProductDetailPageController : PageController<ProductDetailPage>
{
    private readonly IContentLoader _contentLoader;

    public ProductDetailPageController(IContentLoader contentLoader)
    {
        _contentLoader = contentLoader;
    }

    [HttpGet]
    public IActionResult Index(ProductDetailPage currentPage)
    {
        var model = new ProductDetailPageViewModel(currentPage);
        var combinedContentArea = new ContentArea();

        // Add items from the current page's main content.
        AddNonNullItemsToArea(currentPage.MainContent, combinedContentArea);

        // If fallback content is not disabled, try to add items from the parent page.
        if (!currentPage.DisableFallbackContent)
        {
            var parentPage = _contentLoader.Get<ProductLandingPage>(currentPage?.ParentLink);
            if (parentPage != null) // Check if the parent page was successfully loaded.
            {
                AddNonNullItemsToArea(parentPage.ProductDetailFallbackContent, combinedContentArea);
            }
        }

        // If any items were added to the combinedContentArea, assign it to the model.
        if (combinedContentArea.Items.Count > 0)
        {
            model.ProxyMainContent = combinedContentArea;
        }

        return View(model);
    }

    private void AddNonNullItemsToArea(ContentArea sourceArea, ContentArea targetArea)
    {
        if (sourceArea == null)
        {
            return;
        }

        foreach (var contentAreaItem in sourceArea.FilteredItems)
        {
            if (contentAreaItem != null)
            {
                targetArea.Items.Add(contentAreaItem);
            }
        }
    }
}