namespace Salam.Cms.Web.Features.InternetCardGrid.Components;

using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Salam.Cms.Web.Features.InternetCards.Models;
using Salam.Cms.Web.Features.InternetCards.ViewModels;

public sealed class CarouselCardBlockViewComponent : BlockComponent<CarouselCardBlock>
{
    public CarouselCardBlockViewComponent()
    {

    }
    protected override IViewComponentResult InvokeComponent(CarouselCardBlock currentContent)
    {
        CarouselCardBlockViewModel model = new(currentContent)
        {
        };

        return View(model);
    }
}