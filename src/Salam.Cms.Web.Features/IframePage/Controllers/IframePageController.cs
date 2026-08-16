namespace Salam.Cms.Web.Features.Home.Controllers;

using EPiServer.Web.Mvc;

using Microsoft.AspNetCore.Mvc;
using Salam.Cms.Web.Features.Home.Models;
using Salam.Cms.Web.Features.Home.ViewModels;

public class IframePageController : PageController<IframePage>
{
    [HttpGet]
    public IActionResult Index(IframePage currentPage)
    {
        var model = new IframePageViewModel(currentPage);

        return View(model);
    }
}