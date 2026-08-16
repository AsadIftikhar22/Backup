namespace Salam.Cms.Web.Features.Common.Components.Images.PictureRenderer;

using EPiServer.Core;
using global::PictureRenderer;
using global::PictureRenderer.Profiles;
using Salam.Cms.Web.Features.Common.Components.Images;

public interface IPictureRendererViewModelBuilder
{
    Task<ImageViewModel?> Build(ContentReference imageReference, PictureProfileBase? pictureProfile = null, PictureAttributes? attributes = null, SvgRenderMode svgRenderMode = SvgRenderMode.ImageSrc);
}
