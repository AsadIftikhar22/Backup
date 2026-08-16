namespace Salam.Cms.Web.Features.GeneralContent.Controllers;

using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Salam.Cms.Web.Features.GeneralContent.Models;
using Salam.Cms.Web.Features.GeneralContent.ViewModels;

public class GeneralContentPageController : PageController<GeneralContentPage>
{
    [HttpGet]
    public IActionResult Index(GeneralContentPage currentPage)
    {
        var model = new GeneralContentPageViewModel(currentPage);

        return View(model);
    }
}