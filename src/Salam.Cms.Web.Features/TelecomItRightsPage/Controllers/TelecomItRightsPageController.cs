namespace Salam.Cms.Web.Features.TelecomItRightsPage.Controllers;

using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Salam.Cms.Web.Features.TelecomItRightsPage.Models;
using Salam.Cms.Web.Features.TelecomItRightsPage.ViewModels;

public class TelecomItRightsPageController : PageController<TelecomItRightsPage>
{
    [HttpGet]
    public IActionResult Index(TelecomItRightsPage currentPage)
    {
        var model = new TelecomItRightsPageViewModel(currentPage);
        return View(model);
    }
}
