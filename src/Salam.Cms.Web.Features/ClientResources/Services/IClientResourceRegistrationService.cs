namespace Salam.Cms.Web.Features.ClientResources.Services;

using Salam.Cms.Web.Features.ClientResources.Abstract;
using System.Collections.Generic;

public interface IClientResourceRegistrationService
{
    void RegisterScript(IScriptResourceInclude scriptResource);

    void RegisterScripts(IEnumerable<IScriptResourceInclude?>? scriptResources);

    void RegisterStyle(IStyleResourceInclude styleResource);

    void RegisterStyles(IEnumerable<IStyleResourceInclude?>? styleResources);

    void RegisterEmbed(IEmbedResourceInclude embedResource);

    void RegisterEmbeds(IEnumerable<IEmbedResourceInclude?>? embedResources);

    void RegisterExternalResource(IExternalResourceInclude externalResource);

    void RegisterExternalResources(IEnumerable<IExternalResourceInclude?>? externalResources);

    void RegisterResource(IClientResourceInclude clientResource);

    void RegisterResources(IEnumerable<IClientResourceInclude?>? clientResources);
}
