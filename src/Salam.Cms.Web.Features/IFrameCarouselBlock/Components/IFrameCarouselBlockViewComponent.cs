namespace Salam.Cms.Web.Features.IFrameCarouselBlock.Components;

using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Salam.Cms.Web.Features.IFrameCarouselBlock.Models;
using Salam.Cms.Web.Features.IFrameCarouselBlock.ViewModels;

public sealed class IFrameCarouselBlockViewComponent : BlockComponent<IFrameCarouselBlock>
{
    protected override IViewComponentResult InvokeComponent(IFrameCarouselBlock currentContent)
    {
        var model = new IFrameCarouselBlockViewModel(currentContent);

        return View(model);
    }
}

