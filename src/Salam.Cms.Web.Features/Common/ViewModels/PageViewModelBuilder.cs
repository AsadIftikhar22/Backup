namespace Salam.Cms.Web.Features.Common.ViewModels;

using EPiServer.Core;

public abstract class PageViewModelBuilder<TContent, TModel> : IPageViewModelBuilder<TContent, TModel>
    where TContent : PageData
    where TModel : IPageViewModel<TContent>
{
    protected TModel Model;

    protected PageViewModelBuilder()
    {
        Model = (TModel)Activator.CreateInstance(typeof(TModel), default(TContent))!;
    }

    public IPageViewModelBuilder<TContent, TModel> WithContent(TContent? content)
    {
        Model = (TModel)Activator.CreateInstance(typeof(TModel), content)!;
        return this;
    }


    public abstract TModel Build();
}
