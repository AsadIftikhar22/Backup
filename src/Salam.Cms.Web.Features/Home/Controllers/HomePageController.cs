namespace Salam.Cms.Web.Features.Home.Controllers;

using EPiServer.Web.Mvc;

using Microsoft.AspNetCore.Mvc;
using Salam.Cms.Web.Features.Home.Models;
using Salam.Cms.Web.Features.Home.ViewModels;

public class HomePageController : PageController<HomePage>
{
    [HttpGet]
    public IActionResult Index(HomePage currentPage)
    {
        var model = new HomePageViewModel(currentPage);

        return View(model);
    }
}