using Salam.Cms.Web.Features.Common.Interfaces;
using Salam.Cms.Web.Features.Common.Models;
using System.Diagnostics.CodeAnalysis;

namespace Salam.Cms.Web.Features.Common.ViewModels;

public abstract class SitePageViewModel<TContent> : ISitePageViewModel<TContent>
    where TContent : ISitePageData
{
    protected SitePageViewModel(TContent? currentPage)
    {
        CurrentPage = currentPage;
    }

    [NotNull]
    public TContent? CurrentPage { get; internal set; }
}

public static class SitePageViewModel
{
    /// <summary>
    /// Returns a PageViewModel of type <typeparam name="T"/>.
    /// </summary>
    /// <remarks>
    /// Convenience method for creating PageViewModels without having to specify the type as methods can use type inference while constructors cannot.
    /// </remarks>
    public static SitePageViewModel<T> Create<T>(T page)
        where T : SitePageData
    {
        // Create a concrete implementation of SitePageViewModel<T>
        return new ConcreteSitePageViewModel<T>(page);
    }
}

// Example concrete implementation
public class ConcreteSitePageViewModel<T> : SitePageViewModel<T>
    where T : SitePageData
{
    public ConcreteSitePageViewModel(T? currentPage) : base(currentPage) { }
}