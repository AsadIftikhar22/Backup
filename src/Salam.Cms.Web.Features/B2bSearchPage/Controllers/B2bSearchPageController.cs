using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Salam.Cms.Web.Features.B2bSearchPage.Models;

public class B2bSearchPageController : PageController<B2bSearchPage>
{
    private readonly SearchService _searchService;

    public B2bSearchPageController(SearchService searchService)
    {
        _searchService = searchService;
    }

    [HttpGet]
    [AcceptVerbs("GET", "HEAD")]
    public IActionResult Index(B2bSearchPage currentPage, string q)
    {
        var model = new B2bSearchPageViewModel(currentPage);

        if (!string.IsNullOrWhiteSpace(q))
        {
            // Call the search service and populate the view model
            model.SearchResults = _searchService.SearchContent(q);
        }

        return View(model);
    }
}
