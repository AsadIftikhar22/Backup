namespace Salam.Cms.Web.Features.CallToAction.Components;

using EPiServer;
using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Salam.Cms.Web.Features.CallToAction.Models;
using Salam.Cms.Web.Features.CallToAction.ViewModels;

public sealed class CallToActionBlockViewComponent : BlockComponent<CallToActionBlock>
{
    private readonly IContentLoader _contentLoader;

    public CallToActionBlockViewComponent(IContentLoader contentLoader)
    {
        _contentLoader = contentLoader;
    }

    protected override IViewComponentResult InvokeComponent(CallToActionBlock currentContent)
    {
        CallToActionBlockViewModel model = new(currentContent)
        {
        };

        return View(model);
    }
}