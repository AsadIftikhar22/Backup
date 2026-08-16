namespace Salam.Cms.Web.Features.Catalogue.Controllers;

using EPiServer.Web.Mvc;

using Microsoft.AspNetCore.Mvc;
using Salam.Cms.Web.Features.Catalogue.Models;
using Salam.Cms.Web.Features.Catalogue.ViewModels;

public class ProductLandingPageController : PageController<ProductLandingPage>
{
    [HttpGet]
    public IActionResult Index(ProductLandingPage currentPage)
    {
        var model = new ProductLandingPageViewModel(currentPage);

        return View(model);
    }
}