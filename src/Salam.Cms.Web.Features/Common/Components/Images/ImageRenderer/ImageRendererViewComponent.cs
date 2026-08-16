namespace Salam.Cms.Web.Features.Common.Components.Images.ImageRenderer;

using EPiServer.Core;
using Microsoft.AspNetCore.Mvc;
using Salam.Cms.Web.Features.Common.Components.Images;

/// <summary>
/// Render inline <svg> tags by default or <img> tag if specified
/// </summary>
public sealed class ImageRendererViewComponent : ViewComponent
{
    private readonly IImageViewModelBuilder _imageViewModelBuilder;

    public ImageRendererViewComponent(IImageViewModelBuilder imageViewModelBuilder)
    {
        _imageViewModelBuilder = imageViewModelBuilder;
    }

    public async Task<IViewComponentResult> InvokeAsync(ContentReference imageReference, SvgRenderMode svgRenderMode = SvgRenderMode.Inline)
    {
        ImageViewModel? model = await _imageViewModelBuilder.Build(imageReference, svgRenderMode);

        return View(model);
    }
}
