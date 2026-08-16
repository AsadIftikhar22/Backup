namespace Salam.Cms.Web.Features.B2bSupportPage.Controllers;

using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Salam.Cms.Web.Features.B2bSupportPage.Models;
using Salam.Cms.Web.Features.B2bSupportPage.ViewModels;
public class B2bSupportPageController : PageController<B2bSupportPage>
{
    [HttpGet]
    [AcceptVerbs("GET", "HEAD")]
    public IActionResult Index(B2bSupportPage currentPage)
    {
        var model = new B2bSupportPageViewModel(currentPage);

        return View(model);
    }
}