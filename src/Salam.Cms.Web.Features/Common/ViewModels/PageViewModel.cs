namespace Salam.Cms.Web.Features.Common.ViewModels;
using EPiServer.Core;
using System.Diagnostics.CodeAnalysis;

public abstract class PageViewModel<TContent> : IPageViewModel<TContent>
    where TContent : PageData
{
    protected PageViewModel(TContent? currentPage)
    {
        CurrentPage = currentPage;
        //IsInEditMode = ContextModeResolver.Service.CurrentMode == ContextMode.Edit;
    }

    [NotNull]
    public TContent? CurrentPage { get; internal set; }

    public string ThemeCssClass { get; set; }

    //public bool IsInEditMode { get; private set; }

    //public Injected<IContextModeResolver> ContextModeResolver { get; set; }
}
