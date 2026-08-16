namespace Salam.Cms.Web.Features.InternetCardGrid.Components;

using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Salam.Cms.Web.Features.InternetCards.Models;
using Salam.Cms.Web.Features.InternetCards.ViewModels;

public sealed class CarouselBlockViewComponent : BlockComponent<CarouselBlock>
{
    public CarouselBlockViewComponent()
    {

    }
    protected override IViewComponentResult InvokeComponent(CarouselBlock currentContent)
    {
        CarouselBlockViewModel model = new(currentContent)
        {
        };

        return View(model);
    }
}