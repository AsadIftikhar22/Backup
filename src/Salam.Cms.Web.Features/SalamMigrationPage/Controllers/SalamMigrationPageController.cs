namespace Salam.Cms.Web.Features.SalamMigrationPage.Controllers;

using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Salam.Cms.Web.Features.SalamMigrationPage.Models;
using Salam.Cms.Web.Features.SalamMigrationPage.ViewModels;
public class SalamMigrationPageController : PageController<SalamMigrationPage>
{
    [HttpGet]
    [AcceptVerbs("GET", "HEAD")]
    public IActionResult Index(SalamMigrationPage currentPage)
    {
        var model = new SalamMigrationPageViewModel(currentPage);

        return View(model);
    }
}