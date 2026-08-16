namespace Salam.Cms.Web.Features.NotFound.ViewModels;

using EPiServer;
using EPiServer.Core;
using Salam.Cms.Core.Settings.Abstract;
using Salam.Cms.Web.Features.Common.ViewModels;
using Salam.Cms.Web.Features.NotFound.Models;
using Salam.Cms.Web.Features.Settings.Models;

public class NotFoundViewModelBuilder : SitePageViewModelBuilder<NotFoundPage, NotFoundViewModel>, INotFoundViewModelBuilder
{
    private readonly IContentLoader _contentLoader;
    private readonly IContentRepository _contentRepository;
    private readonly ISettingsManager _settingsManager;

    private int _statusCode;

    public NotFoundViewModelBuilder(IContentLoader contentLoader, IContentRepository contentRepository, ISettingsManager settingsManager)
    {
        _contentLoader = contentLoader;
        _contentRepository = contentRepository;
        _settingsManager = settingsManager;
    }

    public override NotFoundViewModel Build()
    {
        //Get NotfoundPage content reference
        var webLayoutSettings = _settingsManager.GetSettings<WebLayoutSettings>();

        NotFoundPage? notFoundPage = null;

        // Attempt to load the configured page
        _contentLoader.TryGet<NotFoundPage>(webLayoutSettings.NotFoundPage, out notFoundPage);

        // If loading failed or the result was null, create a default page
        if (notFoundPage == null)
        {
            // Try to find a NotFoundPage by its type
            var notFoundPageReference = _contentLoader
                .GetDescendents(ContentReference.StartPage)
                .Select(x => _contentLoader.Get<IContent>(x))
                .OfType<NotFoundPage>()
                .FirstOrDefault();

            if (notFoundPageReference != null)
            {
                // Load the existing NotFoundPage instance
                notFoundPage = _contentLoader.Get<NotFoundPage>(notFoundPageReference.ContentLink);
            }
            else
            {
                // Create a new NotFoundPage instance
                notFoundPage = _contentRepository.GetDefault<NotFoundPage>(ContentReference.StartPage);
                notFoundPage.Name = "Error";
                notFoundPage.Heading = "Oops, we can't find the page";

                var cr = _contentRepository.Save(notFoundPage, EPiServer.DataAccess.SaveAction.Publish, EPiServer.Security.AccessLevel.NoAccess);

                notFoundPage = _contentRepository.Get<NotFoundPage>(cr); // make sure it's loaded
            }
        }

        // Now notFoundPage is guaranteed to be non-null (assuming GetDefault and Save work)
        Model = new NotFoundViewModel(notFoundPage);
        Model.StatusCode = _statusCode;

        return Model;
    }

    public INotFoundViewModelBuilder WithStatusCode(int statusCode)
    {
        _statusCode = statusCode;

        return this;
    }
}

