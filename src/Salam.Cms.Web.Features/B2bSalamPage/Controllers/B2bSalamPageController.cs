namespace Salam.Cms.Web.Features.B2bSalamPage.Controllers;

using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Salam.Cms.Web.Features.B2bSalamPage.Models;
using Salam.Cms.Web.Features.B2bSalamPage.ViewModels;
public class B2bSalamPageController : PageController<B2bSalamPage>
{
    [HttpGet]
    [AcceptVerbs("GET", "HEAD")]
    public IActionResult Index(B2bSalamPage currentPage)
    {
        var model = new B2bSalamPageViewModel(currentPage);

        return View(model);
    }
}