namespace Salam.Cms.Web.Infrastructure.ServiceExtensions;

using EPiServer.Authorization;
using EPiServer.OptimizelyIdentity;
using Geta.Optimizely.Sitemaps;
using Geta.Optimizely.Sitemaps.Utils;
using Microsoft.Extensions.DependencyInjection;
using Salam.Cms.Web.Infrastructure.Services;

public static class GetaOptimizelySitemapsServiceExtension
{
    public static IServiceCollection AddGetaOptimizelySitemapsHandler(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSitemaps(x =>
            {
                x.EnableLanguageDropDownInAdmin = false;
                x.EnableRealtimeCaching = true;
                x.EnableRealtimeSitemap = false;
            },
            policy =>
            {
                policy.AddAuthenticationSchemes(OptimizelyIdentityDefaults.SchemeName);
                policy.RequireRole(Roles.CmsAdmins, "SecurityAdmins");
            });

        serviceCollection.AddTransient<IContentFilter, SiteContentFilter>();

        return serviceCollection;
    }
}