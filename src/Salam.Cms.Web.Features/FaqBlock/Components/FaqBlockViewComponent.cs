namespace Salam.Cms.Web.Features.FaqBlock.Components;

using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Salam.Cms.Web.Features.FaqBlock.Models;
using Salam.Cms.Web.Features.FaqBlock.ViewModels;

public sealed class FaqBlockViewComponent : BlockComponent<FaqBlock>
{
    protected override IViewComponentResult InvokeComponent(FaqBlock currentContent)
    {
        var model = new FaqBlockViewModel(currentContent);

        return View(model);
    }
}

