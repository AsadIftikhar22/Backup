namespace Salam.Cms.Web.Features.Landing.ViewModels;

using Salam.Cms.Web.Features.Common.ViewModels;
using Salam.Cms.Web.Features.Landing.Models;

public class LandingPageViewModel : SitePageViewModel<LandingPage>
{
    public LandingPageViewModel(LandingPage currentPage) : base(currentPage)
    {
    }
}