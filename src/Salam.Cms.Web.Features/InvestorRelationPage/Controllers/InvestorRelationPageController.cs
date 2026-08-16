namespace Salam.Cms.Web.Features.Home.Controllers;

using EPiServer.Web.Mvc;

using Microsoft.AspNetCore.Mvc;
using Salam.Cms.Web.Features.Home.Models;
using Salam.Cms.Web.Features.Home.ViewModels;

public class InvestorRelationPageController : PageController<InvestorRelationPage>
{
    [HttpGet]
    public IActionResult Index(InvestorRelationPage currentPage)
    {
        var model = new InvestorRelationPageViewModel(currentPage);

        return View(model);
    }
}