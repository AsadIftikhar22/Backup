namespace Salam.Cms.Web.Features.SalamIFrameMigrationPage.Controllers;

using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Salam.Cms.Web.Features.SalamIFrameMigrationPage.Models;
using Salam.Cms.Web.Features.SalamIFrameMigrationPage.ViewModels;
public class SalamFormMigrationPageController : PageController<SalamIFrameMigrationPage>
{
    [HttpGet]
    [AcceptVerbs("GET", "HEAD")]
    public IActionResult Index(SalamIFrameMigrationPage currentPage)
    {
        var model = new SalamIFrameMigrationPageViewModel(currentPage);

        return View(model);
    }
}