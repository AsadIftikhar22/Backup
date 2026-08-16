namespace Salam.Cms.Web.Infrastructure.ServiceExtensions;

using EPiServer.ContentApi.Core.Configuration;

using Microsoft.Extensions.DependencyInjection;

public static class ContentDeliveryApiServiceExtensions
{
    public static IServiceCollection AddOptimizelyContentDeliveryApi(this IServiceCollection serviceCollection)
    {
        serviceCollection.ConfigureContentApiOptions(options =>
        {
            options.IncludeInternalContentRoots = true;
            options.IncludeSiteHosts = true;
            options.RichTextFormat = RichTextFormat.Html;
            options.FlattenPropertyModel = true;
        });

        serviceCollection.AddContentDeliveryApi(options =>
        {
            options.SiteDefinitionApiEnabled = true;
            options.DisableScopeValidation = true;
        })
        // .WithFriendlyUrl()
        .WithSiteBasedCors();

        return serviceCollection;
    }
}
