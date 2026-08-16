namespace Salam.Cms.Web.Features.Common.Components.Navigation;

using Salam.Cms.Web.Features.Common.Interfaces;

public interface INavigationViewModelBuilder
{
    NavigationViewModel Build(ISitePageData currentPage, out bool isCacheEnabled);
}
