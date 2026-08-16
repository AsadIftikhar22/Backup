namespace Salam.Cms.Web.Features.Hero.Components;

using EPiServer.Core;
using Microsoft.AspNetCore.Mvc;

public sealed class HeroViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(PageData? page)
    {
        if (page is null)
        {
            return Content(string.Empty);
        }

        return View("Default", page);
    }
}