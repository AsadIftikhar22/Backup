namespace Salam.Cms.Web.Features.Common.Components.Images.PictureRenderer;

using EPiServer.Core;
using global::PictureRenderer;
using global::PictureRenderer.Profiles;
using Microsoft.AspNetCore.Mvc;
using Salam.Cms.Shared.Models.Extensions;
using Salam.Cms.Web.Features.Common.Components.Images;

/// <summary>
/// Render <picture> tags by default with option of rendering inline <svg> tags if specified
/// </summary>
public sealed class PictureRendererViewComponent : ViewComponent
{
    private readonly IPictureRendererViewModelBuilder _imageViewModelBuilder;

    public PictureRendererViewComponent(IPictureRendererViewModelBuilder imageViewModelBuilder)
    {
        _imageViewModelBuilder = imageViewModelBuilder;
    }

    public async Task<IViewComponentResult> InvokeAsync(ContentReference imageReference, PictureProfileBase? pictureProfile = null, PictureAttributes? attributes = null, SvgRenderMode svgRenderMode = SvgRenderMode.ImageSrc)
    {
        if (imageReference.IsNullOrEmpty())
        {
            return Content(string.Empty);
        }

        ImageViewModel? model = await _imageViewModelBuilder.Build(imageReference, pictureProfile, attributes, svgRenderMode);

        return View(model);
    }
}
