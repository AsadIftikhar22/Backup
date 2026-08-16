namespace Salam.Cms.Web.Infrastructure.ServiceExtensions;

using EPiServer.OptimizelyIdentity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

public static class CmpServiceExtensions
{
    public static IServiceCollection AddCmpSupport(
        this IServiceCollection services)
    {
        services.AddAntiforgery(options =>
        {
            options.Cookie.SameSite = SameSiteMode.None;
            options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        });

        services.Configure<CookieAuthenticationOptions>(OptimizelyIdentityDefaults.CookieSchemeName, options =>
        {
            options.Cookie.SameSite = SameSiteMode.None;
            options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        });

        return services;
    }
}