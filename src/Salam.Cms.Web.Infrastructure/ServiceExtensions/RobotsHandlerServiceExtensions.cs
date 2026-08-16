namespace Salam.Cms.Web.Infrastructure.ServiceExtensions;

using EPiServer.Authorization;
using EPiServer.OptimizelyIdentity;
using Microsoft.Extensions.DependencyInjection;
using Stott.Optimizely.RobotsHandler.Common;
using Stott.Optimizely.RobotsHandler.Configuration;

public static class RobotsHandlerServiceExtensions
{
    public static IServiceCollection AddRobotsTextHandler(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddRobotsHandler(authorizationOptions =>
        {
            authorizationOptions.AddPolicy(RobotsConstants.AuthorizationPolicy, policy =>
            {
                policy.AddAuthenticationSchemes(OptimizelyIdentityDefaults.SchemeName);
                policy.RequireRole(Roles.CmsAdmins, "SecurityAdmins");
            });
        });

        return serviceCollection;
    }
}