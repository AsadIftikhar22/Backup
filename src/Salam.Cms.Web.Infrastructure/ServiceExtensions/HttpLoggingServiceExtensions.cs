namespace Salam.Cms.Web.Infrastructure.ServiceExtensions;

using Microsoft.Extensions.DependencyInjection;

public static class HttpLoggingServiceExtensions
{
    public static IServiceCollection AddHttpLogging(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddHttpLogging((options) =>
        {
        });

        return serviceCollection;
    }
}
