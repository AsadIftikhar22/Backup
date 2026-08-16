namespace Salam.Cms.Web.Features.InternetCardGrid.Components;

using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Salam.Cms.Web.Features.InternetCards.Models;
using Salam.Cms.Web.Features.InternetCards.ViewModels;

public sealed class InternetCardsBlockViewComponent : BlockComponent<InternetCardsBlock>
{
    public InternetCardsBlockViewComponent()
    {

    }
    protected override IViewComponentResult InvokeComponent(InternetCardsBlock currentContent)
    {
        InternetCardsBlockViewModel model = new(currentContent)
        {
        };

        return View(model);
    }
}