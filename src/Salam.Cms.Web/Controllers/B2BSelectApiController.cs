namespace Salam.Cms.Web.Controllers;

using EPiServer;
using EPiServer.Core;
using Microsoft.AspNetCore.Mvc;
using Salam.Cms.Web.Features.Forms.B2BSelectDropdownBlock;
using System.Globalization;

[Route("api/B2BSelectApi")]
public class B2BSelectApiController : Controller
{
    private readonly IContentLoader _contentLoader;

    public B2BSelectApiController(IContentLoader contentLoader)
    {
        _contentLoader = contentLoader;
    }

    [HttpGet]
    public IActionResult GetSubCategoryItems(ContentReference folderRef, string categoryValue,string lang)
    {
        if (ContentReference.IsNullOrEmpty(folderRef))
            return Json(new List<object>());
        var culture = new CultureInfo(lang);
        var loaderOptions = new LoaderOptions();
        loaderOptions.Add(LanguageLoaderOption.Specific(culture));
        var categoryfolder = _contentLoader.GetChildren<IContent>(folderRef).Where(b => !string.IsNullOrWhiteSpace(b.Name?.Trim()) &&
                    b.Name?.Trim()?.Equals(categoryValue.Trim(), StringComparison.OrdinalIgnoreCase) == true).FirstOrDefault();
        var items = _contentLoader.GetChildren<DropdownOptionsBlock>(categoryfolder?.ContentLink,loaderOptions).Select(x => new { Text = x.Category
                                                ,Value = x.Value
                                                ,Placeholder = x.Placeholder
                                                , Labels=x.Label 
                                                ,className=x.className
                                                ,maxlength=x.maxlength
                                                ,typeOfComplaint=x.typeOfComplaint
                                                ,tier3=x.tier3
                                                ,tier1=x.tier1
        });
        return Json(items);
    }
}
