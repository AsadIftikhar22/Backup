namespace Salam.Cms.Web.Features.VideoCarouselCard.Components;

using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Salam.Cms.Web.Features.VideoCarouselCard.Models;
using Salam.Cms.Web.Features.VideoCarouselCard.ViewModels;

public sealed class VideoCarouselCardBlockViewComponent : BlockComponent<VideoCarouselCardBlock>
{
    public VideoCarouselCardBlockViewComponent()
    {

    }
    protected override IViewComponentResult InvokeComponent(VideoCarouselCardBlock currentContent)
    {
        VideoCarouselCardBlockViewModel model = new(currentContent)
        {
        };

        return View(model);
    }
}