using Salam.Cms.Web.Features.B2bSearchPage.Models;
using Salam.Cms.Web.Features.Common.ViewModels;

public class B2bSearchPageViewModel : SitePageViewModel<B2bSearchPage>
{
    public SearchResultsModel SearchResults { get; set; } = new();

    public B2bSearchPageViewModel(B2bSearchPage currentPage)
        : base(currentPage) { }
}
