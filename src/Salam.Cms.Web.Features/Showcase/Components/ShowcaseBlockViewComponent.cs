namespace Salam.Cms.Web.Features.Showcase.Components;
using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Salam.Cms.Web.Features.Showcase.Models;
using Salam.Cms.Web.Features.Showcase.ViewModels;

public sealed class ShowcaseBlockViewComponent : BlockComponent<ShowcaseBlock>
{
    public ShowcaseBlockViewComponent() { }

    protected override IViewComponentResult InvokeComponent(ShowcaseBlock currentContent)
    {
        ShowcaseBlockViewModel model = new(currentContent)
        {
        };

        return View(model);
    }
}
