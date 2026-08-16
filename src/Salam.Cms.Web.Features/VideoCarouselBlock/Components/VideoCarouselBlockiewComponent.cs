namespace Salam.Cms.Web.Features.VideoCarousel.Components;

using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Salam.Cms.Web.Features.VideoCarousel.Models;
using Salam.Cms.Web.Features.VideoCarousel.ViewModels;

public sealed class VideoCarouselBlockViewComponent : BlockComponent<VideoCarouselBlock>
{
    public VideoCarouselBlockViewComponent()
    {

    }
    protected override IViewComponentResult InvokeComponent(VideoCarouselBlock currentContent)
    {
        VideoCarouselBlockViewModel model = new(currentContent)
        {
        };

        return View(model);
    }
}