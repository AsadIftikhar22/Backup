namespace Salam.Cms.Web.Infrastructure.ServiceExtensions;

using EPiServer.Cms.Shell;
using EPiServer.Cms.Shell.UI;
using EPiServer.Cms.UI.AspNetIdentity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

// This class sets up standard Optimizely logins based on Microsoft Identities in the development environment.
// For Integration -> Prod, this configures the use of Azure AD as per the following article:
// https://docs.developers.optimizely.com/content-cloud/v12.0.0-content-cloud/docs/integrate-azure-ad-using-openid-connect
public static class CmsLoginServiceExtensions
{
    // Optimizely documentation suggests "azure-cookie".
    private const string AuthenticationScheme = CookieAuthenticationDefaults.AuthenticationScheme;

    // Optimizely documentation suggests "azure"
    //private const string ChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;

    /// <summary>
    /// Sets up authentication based on one of the following schemes:
    /// Optimizely CMS Identities built on Microsoft Identities for the development environment.
    /// Azure AD using Open ID connect for all other environments.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="environment"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection AddCmsLogins(
        this IServiceCollection services,
        IWebHostEnvironment environment,
        IConfiguration configuration,
        bool useOptiId)
    {
        if (useOptiId)
        {
            services.AddOptimizelyIdentity(useAsDefault: true);
        }
        else
        {
            services.AddCmsAspNetIdentity<ApplicationUser>()
                    .AddAdminUserRegistration(x => x.Behavior = RegisterAdminUserBehaviors.Enabled | RegisterAdminUserBehaviors.SingleUserOnly);
        }

        return services;
    }

    public static IApplicationBuilder UseCmsCmpLogins(this IApplicationBuilder app, IWebHostEnvironment environment)
    {
        app.UseAuthentication();
        app.UseCmsCmpPublishingPreviewLinks();
        app.UseAuthorization();

        return app;
    }
}