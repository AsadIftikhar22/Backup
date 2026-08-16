namespace Salam.Cms.Web.Features.B2BComponentContent.Controllers;

using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Salam.Cms.Web.Features.B2BComponentContent.ViewModels;
using Salam.Cms.Web.Features.B2BGeneralContent.Models;

public class B2BComponentContentPageController : PageController<B2BComponentContentPage>
{
    [HttpGet]
    public IActionResult Index(B2BComponentContentPage currentPage)
    {
        var model = new B2BComponentContentPageViewModel(currentPage);

        return View(model);
    }
}