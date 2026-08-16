namespace Salam.Cms.Core.Settings.Services;

using EPiServer;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using Salam.Cms.Core.Settings.Abstract;
using System.Globalization;
using System.Linq;

public class SettingsManager : ISettingsManager
{
    private readonly IContentLoader _contentLoader;
    private readonly IContentTypeRepository _contentTypeRepository;
    private readonly IContentModelUsage _contentModelUsage;

    public SettingsManager(IContentLoader contentLoader, IContentTypeRepository contentTypeRepository, IContentModelUsage contentModelUsage)
    {
        _contentLoader = contentLoader;
        _contentTypeRepository = contentTypeRepository;
        _contentModelUsage = contentModelUsage;
    }

    public T GetSettings<T>(CultureInfo culture = null)
       where T : IContentData
    {
        var settingsType = _contentTypeRepository.Load<T>();

        var contentReference = _contentModelUsage
            .ListContentOfContentType(settingsType)
            .Select(x => x.ContentLink.ToReferenceWithoutVersion())
            .MaxBy(x => x.WorkID);

        if (ContentReference.IsNullOrEmpty(contentReference))
            return default;

        return _contentLoader.Get<T>(contentReference, culture);
    }
}
