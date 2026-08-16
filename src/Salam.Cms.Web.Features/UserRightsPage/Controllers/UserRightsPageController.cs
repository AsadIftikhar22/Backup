namespace Salam.Cms.Web.Features.UserRightsPage.Controllers;

using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Salam.Cms.Web.Features.UserRightsPage.Models;
using Salam.Cms.Web.Features.UserRightsPage.ViewModels;

public class UserRightsPageController : PageController<UserRightsPage>
{
    [HttpGet]
    public IActionResult Index(UserRightsPage currentPage)
    {
        var model = new UserRightsPageViewModel(currentPage);
        return View(model);
    }
}
