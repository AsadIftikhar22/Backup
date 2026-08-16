namespace Salam.Cms.Web.Features.ClientResources.Abstract;

using EPiServer.Core;

public interface IClientResourceInclude : IContentData
{
    bool IsLoadedInEditMode { get; set; }
}
