namespace Salam.Cms.Web.Infrastructure.ServiceExtensions;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Salam.Cms.Core.Settings.Configuration;

public static class CorsServiceExtensions
{
    public static void UseCorsConfiguration(this IApplicationBuilder app)
    {
        var serviceProvider = app.ApplicationServices;

        var allowedDomains = serviceProvider
            .GetService<IOptions<CorsSettings>>()?
            .Value?
            .AllowedDomains;

        if (allowedDomains == null || !allowedDomains.Any())
            return;

        app.UseCors(policy =>
        {
            policy
                .WithOrigins(allowedDomains)
                .WithExposedContentDeliveryApiHeaders()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials();
        });
    }
}
