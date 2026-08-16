namespace Salam.Cms.Web.Features.Common.ViewModels;

using EPiServer.Core;

public interface IPageViewModelBuilder<in TContent, out TModel>
    where TContent : PageData
    where TModel : IPageViewModel<TContent>
{
    /// <summary>
    /// Adds content of <see cref="TContent"/> to be used with the building of the <see cref="TModel"/>.
    /// </summary>
    /// <param name="content"></param>
    /// <returns></returns>
    IPageViewModelBuilder<TContent, TModel> WithContent(TContent content);


    /// <summary>
    /// Builds an instance of <see cref="ISitePageViewModelBuilder{TContent,TModel}"/>
    /// That must be implemented within the concrete builder
    /// </summary>
    /// <returns></returns>
    TModel Build();
}
