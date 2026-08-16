namespace Salam.Cms.Web.Infrastructure.ServiceExtensions;

using Geta.Optimizely.ContentTypeIcons.Infrastructure.Configuration;
using Geta.Optimizely.ContentTypeIcons.Infrastructure.Initialization;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public static class GetaContentTypeIconsServiceExtensions
{
    public static IServiceCollection AddGetaContentTypeIcons(
        this IServiceCollection serviceCollection,
        IWebHostEnvironment webHostEnvironment)
    {
        if (webHostEnvironment.IsDevelopment() && Environment.OSVersion.Platform == PlatformID.MacOSX)
        {
            return serviceCollection;
        }

        serviceCollection.AddContentTypeIcons(options =>
        {
            options.EnableTreeIcons = true;
            options.ForegroundColor = "#ffffff";
            options.BackgroundColor = "#212529";
            options.FontSize = 40;

            if (webHostEnvironment.IsDevelopment())
            {
                options.CachePath = "[appDataPath]\\thumb_cache\\";
                options.CustomFontPath = "[appDataPath]\\fonts\\";
            }
        });

        return serviceCollection;
    }

    public static IApplicationBuilder UseGetaContentTypeIcons(
        this IApplicationBuilder app,
        IWebHostEnvironment webHostEnvironment)
    {
        if (webHostEnvironment.IsDevelopment() && Environment.OSVersion.Platform == PlatformID.MacOSX)
        {
            return app;
        }

        app.UseContentTypeIcons();

        return app;
    }
}
