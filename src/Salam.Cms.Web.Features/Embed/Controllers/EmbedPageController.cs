namespace Salam.Cms.Web.Features.Embed.Controllers;

using EPiServer.Web.Mvc;

using Microsoft.AspNetCore.Mvc;
using Salam.Cms.Web.Features.Embed.Abstract;
using Salam.Cms.Web.Features.Embed.Models;

public sealed class EmbedPageController : PageController<EmbedPage>
{
    private readonly IEmbedPageViewModelBuilder _viewModelBuilder;

    public EmbedPageController(IEmbedPageViewModelBuilder viewModelBuilder)
    {
        _viewModelBuilder = viewModelBuilder;
    }

    [HttpGet]
    public IActionResult Index(EmbedPage currentPage)
    {
        var model = _viewModelBuilder.WithContent(currentPage).Build();

        return View(model);
    }
}