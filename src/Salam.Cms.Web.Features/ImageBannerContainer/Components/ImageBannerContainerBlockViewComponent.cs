namespace Salam.Cms.Web.Features.InternetCardGrid.Components;

using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Salam.Cms.Web.Features.SolutionsSectionsBlock.Models;
using Salam.Cms.Web.Features.SolutionsSectionsBlock.ViewModels;

public sealed class ImageBannerContainerBlockViewComponent : BlockComponent<ImageBannerContainerBlock>
{
    public ImageBannerContainerBlockViewComponent()
    {

    }
    protected override IViewComponentResult InvokeComponent(ImageBannerContainerBlock currentContent)
    {
        ImageBannerContainerBlockViewModel model = new(currentContent)
        {
        };

        return View(model);
    }
}