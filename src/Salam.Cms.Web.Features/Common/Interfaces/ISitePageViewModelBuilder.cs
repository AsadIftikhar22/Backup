namespace Salam.Cms.Web.Features.Common.Interfaces;
public interface ISitePageViewModelBuilder<in TContent, out TModel>
    where TContent : ISitePageData
    where TModel : ISitePageViewModel<TContent>
{
    /// <summary>
    /// Adds content of <see cref="TContent"/> to be used with the building of the <see cref="TModel"/>.
    /// </summary>
    /// <param name="content"></param>
    /// <returns></returns>
    ISitePageViewModelBuilder<TContent, TModel> WithContent(TContent content);


    /// <summary>
    /// Builds an instance of <see cref="ISitePageViewModelBuilder{TContent,TModel}"/>
    /// That must be implemented within the concrete builder
    /// </summary>
    /// <returns></returns>
    TModel Build();
}
