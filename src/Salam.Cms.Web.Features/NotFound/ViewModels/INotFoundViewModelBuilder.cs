namespace Salam.Cms.Web.Features.NotFound.ViewModels;
using Salam.Cms.Web.Features.Common.Interfaces;
using Salam.Cms.Web.Features.NotFound.Models;

public interface INotFoundViewModelBuilder : ISitePageViewModelBuilder<NotFoundPage, NotFoundViewModel>
{
    INotFoundViewModelBuilder WithStatusCode(int statusCode);
}
