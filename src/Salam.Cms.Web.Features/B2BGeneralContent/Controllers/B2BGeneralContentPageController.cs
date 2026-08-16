namespace Salam.Cms.Web.Features.B2BGeneralContent.Controllers;

using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Salam.Cms.Web.Features.B2BGeneralContent.Models;
using Salam.Cms.Web.Features.B2BGeneralContent.ViewModels;

public class B2BGeneralContentPageController : PageController<B2BGeneralContentPage>
{
    [HttpGet]
    public IActionResult Index(B2BGeneralContentPage currentPage)
    {
        var model = new B2BGeneralContentPageViewModel(currentPage);

        return View(model);
    }
}