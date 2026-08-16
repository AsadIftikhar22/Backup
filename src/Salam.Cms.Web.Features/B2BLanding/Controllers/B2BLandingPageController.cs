namespace Salam.Cms.Web.Features.B2BLanding.Controllers;

using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Salam.Cms.Web.Features.B2BLanding.Models;
using Salam.Cms.Web.Features.B2BLanding.ViewModels;
public class B2BLandingPageController : PageController<B2BLandingPage>
{
    [HttpGet]
    [AcceptVerbs("GET", "HEAD")]
    public IActionResult Index(B2BLandingPage currentPage)
    {
        var model = new B2BLandingPageViewModel(currentPage);

        return View(model);
    }
}