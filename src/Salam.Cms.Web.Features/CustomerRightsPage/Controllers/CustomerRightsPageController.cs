namespace Salam.Cms.Web.Features.CustomerRightsPage.Controllers;

using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Salam.Cms.Web.Features.CustomerRightsPage.Models;
using Salam.Cms.Web.Features.CustomerRightsPage.ViewModels;

public class CustomerRightsPageController : PageController<CustomerRightsPage>
{
    [HttpGet]
    public IActionResult Index(CustomerRightsPage currentPage)
    {
        var model = new CustomerRightsPageViewModel(currentPage);
        return View(model);
    }
}
