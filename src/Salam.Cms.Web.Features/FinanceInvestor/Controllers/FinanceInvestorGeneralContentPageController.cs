namespace Salam.Cms.Web.Features.FinanceInvestorGeneralContent.Controllers;

using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Salam.Cms.Web.Features.FinanceInvestorGeneralContent.Models;
using Salam.Cms.Web.Features.FinanceInvestorGeneralContent.ViewModels;

public class FinanceInvestorGeneralContentPageController : PageController<FinanceInvestorGeneralContentPage>
{
    [HttpGet]
    public IActionResult Index(FinanceInvestorGeneralContentPage currentPage)
    {
        var model = new FinanceInvestorGeneralContentPageViewModel(currentPage);

        return View(model);
    }
}