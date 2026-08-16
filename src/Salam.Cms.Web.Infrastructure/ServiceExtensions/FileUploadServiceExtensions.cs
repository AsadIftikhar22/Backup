namespace Salam.Cms.Web.Infrastructure.ServiceExtensions;

using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.DependencyInjection;

public static class FileUploadServiceExtensions
{
    public static IServiceCollection AddFileUploadLimits(this IServiceCollection serviceCollection)
    {
        const int oneGigabyte = 1073741824;

        serviceCollection.Configure<FormOptions>(options =>
        {
            options.MultipartBodyLengthLimit = oneGigabyte;
        });

        return serviceCollection;
    }
}