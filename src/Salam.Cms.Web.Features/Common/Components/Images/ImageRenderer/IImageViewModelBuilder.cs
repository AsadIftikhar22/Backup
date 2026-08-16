namespace Salam.Cms.Web.Features.Common.Components.Images.ImageRenderer;

using EPiServer.Core;
using Salam.Cms.Web.Features.Common.Components.Images;

public interface IImageViewModelBuilder
{
    Task<ImageViewModel?> Build(ContentReference imageReference, SvgRenderMode svgRenderMode = SvgRenderMode.Inline);
}
