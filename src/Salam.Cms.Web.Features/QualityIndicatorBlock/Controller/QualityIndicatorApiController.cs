using EPiServer;
using EPiServer.Core;
using Microsoft.AspNetCore.Mvc;
using Salam.Cms.Web.Features.SolutionsSectionsBlock.Models;

[Route("QualityIndicatorApi")]
public class QualityIndicatorApiController : Controller
{
    private readonly IContentLoader _contentLoader;

    public QualityIndicatorApiController(IContentLoader contentLoader)
    {
        _contentLoader = contentLoader;
    }
    [HttpGet("Testing")]
    public string Testing()
    {
        return "Quarter API is working";
    }
    [HttpGet("GetQuarters")]
    public IActionResult GetQuarters(int blockContentId, int year)
    {
        var blockRef = new ContentReference(blockContentId);
        if (!_contentLoader.TryGet(blockRef, out QualityIndicatorBlock block))
        {
            return NotFound();
        }

        var list = block.Reports
            .Where(r => r.Year == year)
            .Select(r => new
            {
                QuarterlyTab = r.QuarterlyTab,
                PdfUrl = Url.Content(r.FileUrl?.Href ?? "")
            })
            .ToList();

        return Json(list);
    }
}
