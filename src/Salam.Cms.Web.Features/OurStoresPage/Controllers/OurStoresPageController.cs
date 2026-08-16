namespace Salam.Cms.Web.Features.OurStoresPage.Controllers;

using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Salam.Cms.Web.Features.OurStoresPage.Models;
using Salam.Cms.Web.Features.OurStoresPage.ViewModels;

public class OurStoresPageController : PageController<OurStoresPage>
{
    [HttpGet]
    public IActionResult Index(OurStoresPage currentPage)
    {
        var model = new OurStoresPageViewModel(currentPage);
        return View(model);
    }
}
