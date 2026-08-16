namespace Salam.Cms.Web.Infrastructure.ServiceExtensions;

using Ganss.Xss;
using Microsoft.Extensions.DependencyInjection;
using Salam.Cms.Core.Services.Images;
using Salam.Cms.Shared.Models.Common.Components;
using Salam.Cms.Shared.Models.Helpers;
using Salam.Cms.Web.Infrastructure.Sanitization;

public static class DependencyServiceExtensions
{
    public static IServiceCollection AddCustomDependencies(this IServiceCollection serviceCollection)
    {
        // Page Model Builders

        // Component Model Builders

        // Sanitization
        serviceCollection.AddSingleton<IHtmlSanitizer>(ContentSanitizer.Build());

        // Helpers
        serviceCollection.AddTransient<IValueFallbackHandler, ValueFallbackHandler>();
        serviceCollection.AddTransient<ILinkModelConverter, LinkModelConverter>();
        serviceCollection.AddTransient<IImageUtilityService, ImageUtilityService>();
        serviceCollection.AddTransient<IPlaceholderReplacer, PlaceholderReplacer>();
     

        // Services
        serviceCollection.AddTransient<IBlobOperations, BlobOperations>();

        serviceCollection.AddScoped<Salam.Cms.Core.Services.Catalogue.IProductQueryService, Salam.Cms.Core.Services.Catalogue.ProductQueryService>();
        return serviceCollection;
    }
}