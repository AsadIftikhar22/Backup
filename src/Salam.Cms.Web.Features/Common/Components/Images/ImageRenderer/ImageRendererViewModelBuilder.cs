namespace Salam.Cms.Web.Features.Common.Components.Images.ImageRenderer;

using EPiServer;
using EPiServer.Core;
using Salam.Cms.Core.Services.Images;
using Salam.Cms.Shared.Models.Media;
using Salam.Cms.Web.Features.Common.Components.Images;

public class ImageRendererViewModelBuilder : IImageViewModelBuilder
{
    private readonly IContentLoader _contentLoader;
    private readonly IImageUtilityService _imageUtilityService;

    public ImageRendererViewModelBuilder(
        IContentLoader contentLoader,
        IImageUtilityService imageUtilityService
    )
    {
        _contentLoader = contentLoader;
        _imageUtilityService = imageUtilityService;
    }

    public async Task<ImageViewModel?> Build(ContentReference imageReference, SvgRenderMode svgRenderMode = SvgRenderMode.Inline)
    {
        if (!_contentLoader.TryGet<IImageContent>(imageReference, out var imageData))
        {
            return default;
        }

        var model = new ImageViewModel
        {
            ImageReference = imageReference,
            ImageContent = imageData,
            SvgRenderMode = svgRenderMode
        };

        if (imageData is VectorImageContent vectorImageContent)
        {
            var svgContent = await _imageUtilityService.ConvertImageToRawContentAsync(vectorImageContent);

            model.RawVectorImageContent = svgContent;
            model.IsVectorImage = true;
        }

        return model;
    }
}
