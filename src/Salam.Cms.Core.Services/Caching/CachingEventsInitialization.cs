namespace Salam.Cms.Core.Services.Caching;

using EPiServer;
using EPiServer.Core;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;
using Microsoft.Extensions.Logging;
using Salam.Cms.Shared.Models.Media;
using System;

[InitializableModule]
[ModuleDependency(typeof(EPiServer.Web.InitializationModule))]
public sealed class CachingEventsInitialization : IInitializableModule
{
    private ICachingService? _cachingService;

    public void Initialize(InitializationEngine context)
    {
        _cachingService = context.Locate.Advanced.GetInstance<ICachingService>();

        var events = context.Locate.ContentEvents();
        events.PublishedContent += ClearCache;
        events.MovedContent += ClearCache;
        events.ScheduledContent += ClearCache;
        events.DeletedContent += ClearCache;
    }

    public void Uninitialize(InitializationEngine context)
    {
        var events = context.Locate.ContentEvents();
        events.PublishedContent -= ClearCache;
        events.MovedContent -= ClearCache;
        events.ScheduledContent -= ClearCache;
        events.DeletedContent -= ClearCache;
    }

    public void ClearCache(object? sender, ContentEventArgs eventArgs)
    {
        try
        {
            if (eventArgs.Content is VectorImageContent)
            {
                // Clear media-related cache using master key
                _cachingService?.RemoveByMasterKey(CacheKeys.MasterKeys.Media);
            }
            else
            {
                // Clear all site content cache using master key
                _cachingService?.RemoveByMasterKey(CacheKeys.MasterKeys.SiteContent);
                _cachingService?.RemoveByMasterKey(CacheKeys.MasterKeys.Navigation);
            }
        }
        catch (Exception exception)
        {
            var logger = ServiceLocator.Current.GetInstance<ILogger<CachingEventsInitialization>>();
            logger.LogError(exception, "Error encountered when trying clear the cache keys.");
        }
    }
}
