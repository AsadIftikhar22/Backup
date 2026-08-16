using Microsoft.Extensions.DependencyInjection;
using Salam.Cms.Core.Settings.Infrastructure;

namespace Salam.Cms.Core.Settings
{
    public static class CmsSettingsInjectionExtensions
    {
        public static IServiceCollection AddSalamSettings(this IServiceCollection services)
        {
            services.AddSingleton<ISettingsService, SettingsService>();
            return services;
        }
    }
}

namespace Microsoft.AspNetCore.Builder
{
    public static class CmsSettingsBuilderExtensions
    {
        public static IApplicationBuilder UseSalamSettings(this IApplicationBuilder builder)
        {
            var service = builder.ApplicationServices.GetService<ISettingsService>();
            if (service != null)
                service.InitializeSettings();
            else
                throw new Exception("The settings service was not registered, please add the ISettingsService before using it.");
            return builder;
        }
    }
}