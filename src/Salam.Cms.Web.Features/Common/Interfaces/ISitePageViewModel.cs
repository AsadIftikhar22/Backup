namespace Salam.Cms.Web.Features.Common.Interfaces;
public interface ISitePageViewModel<out TContent>
    where TContent : ISitePageData
{
    TContent? CurrentPage { get; }
}
