using EPiServer.Forms.Core.Internal;
using Microsoft.Extensions.DependencyInjection;
using Salam.Cms.Web.Infrastructure.Forms.Services;

public class DataSubmissionServiceFactory
{
    private readonly IServiceProvider _provider;

    public DataSubmissionServiceFactory(IServiceProvider provider)
    {
        _provider = provider;
    }

    public DataSubmissionService Get(string channel)
    {
        return channel switch
        {
            "B2B" => _provider.GetRequiredService<B2BFormDataSubmissionService>(),
            "Protector" => _provider.GetRequiredService<ProtectorChannelFormDataSubmissionService>(),
            "Complaint" => _provider.GetRequiredService<ComplaintFormDataSubmissionService>(),
            _ => throw new ArgumentException("Invalid channel")
        };
    }
}
