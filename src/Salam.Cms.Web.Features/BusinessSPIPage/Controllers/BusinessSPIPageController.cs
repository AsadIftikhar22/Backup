namespace Salam.Cms.Web.Features.B2bSalamPage.Controllers;

using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Salam.Cms.Web.Features.B2bSalamPage.Models;
using Salam.Cms.Web.Features.B2bSalamPage.ViewModels;
public class BusinessSPIPageController : PageController<BusinessSPIPage>
{
    [HttpGet]
    [AcceptVerbs("GET", "HEAD")]
    public IActionResult Index(BusinessSPIPage currentPage)
    {
        var model = new BusinessSPIPageViewModel(currentPage);

        return View(model);
    }
}