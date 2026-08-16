namespace Salam.Cms.Web.Features.B2BProductEnquiry.Controllers;

using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Salam.Cms.Web.Features.B2BProductEnquiry.Models;
using Salam.Cms.Web.Features.B2BProductEnquiry.ViewModels;

public class B2BProductEnquiryPageController : PageController<B2BProductEnquiryPage>
{
    [HttpGet]
    public IActionResult Index(B2BProductEnquiryPage currentPage)
    {
        var model = new B2BProductEnquiryPageViewModel(currentPage);

        return View(model);
    }
}