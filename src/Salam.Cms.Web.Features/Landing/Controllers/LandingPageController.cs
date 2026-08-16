namespace Salam.Cms.Web.Features.Landing.Controllers;

using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Salam.Cms.Web.Features.Landing.Models;
using Salam.Cms.Web.Features.Landing.ViewModels;

public class LandingPageController : PageController<LandingPage>
{
    [HttpGet]
    [AcceptVerbs("GET", "HEAD")]
    public IActionResult Index(LandingPage currentPage)
    {
        var model = new LandingPageViewModel(currentPage);

        return View(model);
    }
}