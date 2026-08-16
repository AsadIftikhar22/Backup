namespace Salam.Cms.Web.Features.ClientResources.Abstract;

using Salam.Cms.Web.Features.ClientResources.Enums;

public interface IEmbedResourceInclude : IClientResourceInclude
{
    EmbedRenderLocationOption RenderLocation { get; set; }

    string? EmbedContent { get; set; }
}
