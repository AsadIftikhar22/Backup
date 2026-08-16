namespace Salam.Cms.Web.Features.Common.ViewModels;

using EPiServer.Core;

public interface IPageViewModel<out TContent>
    where TContent : PageData
{
    TContent? CurrentPage { get; }
}
