namespace Salam.Cms.Core.Services.Infrastructure;

using Microsoft.Extensions.DependencyInjection;
using Salam.Cms.Core.Services.Catalogue;
using Salam.Cms.Core.Services.Images;
using Salam.Cms.Core.Settings.Abstract;
using Salam.Cms.Core.Settings.Services;
using Salam.Cms.Web.Infrastructure.Services;
using System.Net;

public static class CoreServiceExtensions
{
    public static IServiceCollection AddCoreServicesDependencies(this IServiceCollection services)
    {
        services.AddSingleton<ICatalogueApiService, CatalogueApiService>();
        services.AddTransient<ISettingsManager, SettingsManager>();

        // Add image proxy services
        services.AddHttpClient("image-proxy")
                .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
                {
                    AllowAutoRedirect = true,
                    AutomaticDecompression = DecompressionMethods.All,
                });
        services.AddSingleton<IProxyImageService, ProxyImageService>();

        return services;
    }
}
