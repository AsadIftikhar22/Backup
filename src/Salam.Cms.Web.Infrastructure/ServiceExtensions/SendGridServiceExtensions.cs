namespace Salam.Cms.Web.Infrastructure.ServiceExtensions;

using EPiServer.Framework;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class SendGridServiceExtensions
{
    public static IServiceCollection AddSendGrid(this IServiceCollection services, IConfiguration configuration)
    {
        var sendGridSettings = configuration.GetSection("EPiServer:Cms:Smtp").Get<SmtpOptions>();

        services.Configure<SmtpOptions>(x =>
        {
            x.DeliveryMethod = sendGridSettings.DeliveryMethod;
            x.Network = new Network
            {
                UserName = sendGridSettings.Network.UserName,
                Password = sendGridSettings.Network.Password,
                UseSsl = false,
                Port = 587,
                Host = sendGridSettings.Network.Host
            };
        });

        return services;
    }
}
