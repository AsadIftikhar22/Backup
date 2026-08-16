namespace Salam.Cms.Web.Features.WholesaleLanding.Controllers;

using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Salam.Cms.Web.Features.WholesaleLanding.Models;
using Salam.Cms.Web.Features.WholesaleLanding.ViewModels;
public class WholesaleLandingPageController : PageController<WholesaleLandingPage>
{
    [HttpGet]
    [AcceptVerbs("GET", "HEAD")]
    public IActionResult Index(WholesaleLandingPage currentPage)
    {
        var model = new WholesaleLandingViewModel(currentPage);

        return View(model);
    }
}