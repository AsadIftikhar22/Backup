namespace Salam.Cms.Web.Infrastructure.ServiceExtensions;

using EPiServer.Authorization;
using EPiServer.OptimizelyIdentity;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Salam.Cms.Shared.Models;
using Stott.Security.Optimizely.Common;
using Stott.Security.Optimizely.Features.Configuration;

public static class SecurityServiceExtensions
{
    public static IServiceCollection AddSecurityAdmin(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddStottSecurity(
            cspSetupOptions =>
            {
                cspSetupOptions.ConnectionStringName = ConfigConstants.OptimizelyDbConnectionStringName;
            },
            authorizationOptions =>
            {
                authorizationOptions.AddPolicy(CspConstants.AuthorizationPolicy, policy =>
                {
                    policy.AddAuthenticationSchemes(OptimizelyIdentityDefaults.SchemeName);
                    policy.RequireRole(Roles.CmsAdmins, SalamConstants.RoleNames.DataTeam);
                });
            });

        return serviceCollection;
    }

    public static IApplicationBuilder UseSecurityAdmin(this IApplicationBuilder app)
    {
        app.UseStottSecurity();

        return app;
    }
}