using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Salam.Cms.Web.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Add your service registrations here
            // Example: services.AddScoped<IMyService, MyService>();

            return services;
        }
    }
}