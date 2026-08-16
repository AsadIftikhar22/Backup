namespace Salam.Cms.Web.Features.NotFound.ViewModels;

using Salam.Cms.Web.Features.Common.ViewModels;
using Salam.Cms.Web.Features.NotFound.Models;

public class NotFoundViewModel : SitePageViewModel<NotFoundPage>
{
    public NotFoundViewModel(NotFoundPage currentPage) : base(currentPage)
    {
    }

    public int StatusCode { get; set; }
}
