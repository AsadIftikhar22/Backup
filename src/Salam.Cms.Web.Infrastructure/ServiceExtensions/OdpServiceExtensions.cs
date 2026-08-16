namespace Salam.Cms.Web.Infrastructure.ServiceExtensions;

using Microsoft.Extensions.DependencyInjection;
using UNRVLD.ODP.VisitorGroups.Initilization;

public static class OdpServiceExtensions
{
    public static IServiceCollection AddOdpSupport(
        this IServiceCollection services)
    {
        services.AddODPVisitorGroups();

        return services;
    }
}