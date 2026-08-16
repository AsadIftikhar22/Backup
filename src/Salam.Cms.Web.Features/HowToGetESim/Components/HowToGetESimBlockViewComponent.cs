namespace Salam.Cms.Web.Features.HowToGetESim.Components;
using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Salam.Cms.Web.Features.HowToGetESim.Models;
using Salam.Cms.Web.Features.HowToGetESim.ViewModels;

public sealed class HowToGetESimBlockViewComponent : BlockComponent<HowToGetESimBlock>
{
    public HowToGetESimBlockViewComponent() { }

    protected override IViewComponentResult InvokeComponent(HowToGetESimBlock currentContent)
    {
        HowToGetESimBlockViewModel model = new(currentContent)
        {
        };

        return View(model);
    }
}
