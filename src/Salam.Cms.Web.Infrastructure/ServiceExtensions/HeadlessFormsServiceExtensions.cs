namespace Salam.Cms.Web.Infrastructure.ServiceExtensions
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Microsoft.Extensions.Options;
    using OpenIddict.Server;
    using Optimizely.Cms.Forms;
    using Optimizely.Cms.Forms.DependencyInjection;

    public static class HeadlessFormsServiceExtensions
    {
        public static IServiceCollection AddHeadlessForms(this IServiceCollection services, IConfiguration configuration)
        {
            services.TryAddEnumerable(ServiceDescriptor.Singleton<IPostConfigureOptions<OptimizelyFormsServiceOptions>, HeadlessFormServiceOptionsPostConfigure>());

            services.AddOptimizelyFormsService(options =>
            {
                options.EnableOpenApiDocumentation = true;

                options.FormCorsPolicy = new FormCorsPolicy
                {
                    AllowOrigins = ["*"],
                    AllowCredentials = true
                };

                //options.OpenIDConnectClients.Add(new()
                //{
                //    Authority = environmentSettings.Domain.TrimEnd('/')
                //});
            });

            return services;
        }

        public class HeadlessFormServiceOptionsPostConfigure : IPostConfigureOptions<OptimizelyFormsServiceOptions>
        {
            readonly OpenIddictServerOptions _options;

            public HeadlessFormServiceOptionsPostConfigure(IOptions<OpenIddictServerOptions> options)
            {
                _options = options.Value;
            }

            public void PostConfigure(string name, OptimizelyFormsServiceOptions options)
            {
                foreach (var client in options.OpenIDConnectClients)
                {
                    foreach (var key in _options.EncryptionCredentials.Select(c => c.Key))
                        client.EncryptionKeys.Add(key);

                    foreach (var key in _options.SigningCredentials.Select(c => c.Key))
                        client.SigningKeys.Add(key);
                }
            }
        }
    }
}
