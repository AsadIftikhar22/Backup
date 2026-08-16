namespace Salam.Cms.Web.Infrastructure.ServiceExtensions;

using EPiServer.Authorization;
using EPiServer.OptimizelyIdentity;
using Geta.NotFoundHandler.Infrastructure.Configuration;
using Geta.NotFoundHandler.Infrastructure.Initialization;
using Geta.NotFoundHandler.Optimizely.Infrastructure.Configuration;
using Geta.NotFoundHandler.Optimizely.Infrastructure.Initialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Salam.Cms.Web.Infrastructure;

public static class GetaNotFoundHandlerServiceExtensions
{
    public static IServiceCollection AddGetaNotFoundHandler(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString(ConfigConstants.OptimizelyDbConnectionStringName);
        serviceCollection.AddNotFoundHandler(o =>
            {
                o.UseSqlServer(connectionString);
                o.BufferSize = 100;
                o.ThreshHold = 10;
                o.HandlerMode = FileNotFoundMode.On;
                o.IgnoredResourceExtensions = new[] { "jpg", "gif", "png", "css", "js", "ico", "swf", "woff", "woff2", "webp" };
                o.Logging = LoggerMode.On;
                o.LogWithHostname = true;
            },
            policy =>
            {
                policy.AddAuthenticationSchemes(OptimizelyIdentityDefaults.SchemeName);
                policy.RequireRole(Roles.CmsAdmins, "SecurityAdmins");
            });

        serviceCollection.AddOptimizelyNotFoundHandler(o =>
        {
            // We don't want automatic redirects enabling as this results in a
            // new redirect rule every time any content moves, including images.
            // Deleting of content also ends up with a redirect rule pointing at the Recycle Bin
            o.AutomaticRedirectsEnabled = false;
        });

        return serviceCollection;
    }

    public static IApplicationBuilder UseGetaNotFoundHandler(this IApplicationBuilder app)
    {
        app.UseNotFoundHandler();
        app.UseOptimizelyNotFoundHandler();

        return app;
    }
}
