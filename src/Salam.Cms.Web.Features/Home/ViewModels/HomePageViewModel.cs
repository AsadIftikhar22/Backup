namespace Salam.Cms.Web.Features.Home.ViewModels;

using Salam.Cms.Web.Features.Common.ViewModels;
using Salam.Cms.Web.Features.Home.Models;

public class HomePageViewModel : SitePageViewModel<HomePage>
{
    public HomePageViewModel(HomePage currentPage) : base(currentPage)
    {
    }
}
