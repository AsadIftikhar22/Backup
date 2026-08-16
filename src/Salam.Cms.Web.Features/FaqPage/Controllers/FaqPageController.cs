namespace Salam.Cms.Web.Features.FaqPage.Controllers;

using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Salam.Cms.Web.Features.FaqPage.Models;
using Salam.Cms.Web.Features.FaqPage.ViewModels;

public class FaqPageController : PageController<FaqPage>
{
    [HttpGet]
    public IActionResult Index(FaqPage currentPage)
    {
        var model = new FaqPageViewModel(currentPage);

        return View(model);
    }
}

