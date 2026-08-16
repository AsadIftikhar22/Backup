namespace Salam.Cms.Web.Features.InternetCardGrid.Components;

using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Salam.Cms.Web.Features.AccordInternetCardGridion.ViewModels;
using Salam.Cms.Web.Features.InternetCardGrid.Models;

public sealed class InternetCardGridBlockViewComponent : BlockComponent<InternetCardGridBlock>
{
    public InternetCardGridBlockViewComponent() { }

    protected override IViewComponentResult InvokeComponent(InternetCardGridBlock currentContent)
    {
        InternetCardGridBlockViewModel model = new(currentContent)
        {
        };

        return View(model);
    }
}
