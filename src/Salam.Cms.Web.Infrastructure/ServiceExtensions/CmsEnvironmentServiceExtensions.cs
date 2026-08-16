namespace Salam.Cms.Web.Infrastructure.ServiceExtensions;

using EPiServer.Scheduler;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Optimizely.Cms.DependencyInjection;
using Optimizely.Cms.Preview1;
using Salam.Cms.Core.Settings.Configuration;
using Salam.Cms.Web.Infrastructure;

public static class CmsEnvironmentServiceExtensions
{
    public static IServiceCollection AddCmsEnvironmentConfiguration(
        this IServiceCollection serviceCollection,
        IWebHostEnvironment environment,
        IConfiguration configuration)
    {
        if (environment.IsDevelopment())
        {
            // Set Shared App Data folder for development environments.
            AppDomain.CurrentDomain.SetData("DataDirectory", Path.Combine(environment.ContentRootPath, "App_Data"));

            // Disable scheduled jobs in development
            serviceCollection.Configure<SchedulerOptions>(options => options.Enabled = false);

            // Add the blob provider for use with local development.
            var blobConfig = configuration.GetSection(nameof(BlobSettings)).Get<BlobSettings>();
            if (blobConfig != null)
            {
                serviceCollection.AddAzureBlobProvider(o =>
                {
                    o.ConnectionString = blobConfig?.ConnectionString;
                    o.ContainerName = blobConfig?.ContainerName;
                });
            }

            serviceCollection.Configure<CmsServiceOptions>(o =>
            {
                o.AddDevelopmentSigningCredentials();
            });
        }
        else
        {
            // Add Azure Configuration for DXP Environments.
            // This sets up all configuration for the differing environmental components
            // e.g. Blob Storage, Event Management, GeoLocation etc.
            serviceCollection.AddCmsCloudPlatformSupport(configuration);

            serviceCollection.AddAzureBlobProvider(o =>
            {
                o.ConnectionString = configuration.GetConnectionString(ConfigConstants.OptimizelyBlobsConnectionStringName);
                o.ContainerName = configuration.GetValue<string>("EPiServer:Cms:BlobProviders:AzureBlobProvider:ContainerName");
            });
        }

        return serviceCollection;
    }
}