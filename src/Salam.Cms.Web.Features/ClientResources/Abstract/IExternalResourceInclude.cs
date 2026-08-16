namespace Salam.Cms.Web.Features.ClientResources.Abstract;
using Salam.Cms.Web.Features.ClientResources.Enums;

public interface IExternalResourceInclude : IClientResourceInclude, IClientResourceConfiguration
{
    ClientResourceTypeOption ResourceType { get; set; }

    string? ExternalUrl { get; set; }
}
