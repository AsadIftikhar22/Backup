namespace Salam.Cms.Web.Infrastructure.ServiceExtensions;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Salam.Cms.Core.Settings.Configuration;

public static class OptimizelyDAMServiceExtensions
{
    public static IServiceCollection AddOptimizelyDAM(this IServiceCollection serviceCollection)
    {
        var credentials = serviceCollection
            .BuildServiceProvider()
            .GetService<IOptions<DAMSettings>>()?
            .Value;

        serviceCollection.AddDAMUi(
            x => x.Enabled = true,
            x =>
            {
                x.ClientId = credentials?.ClientId;
                x.ClientSecret = credentials?.ClientSecret;
            });

        return serviceCollection;
    }
}
