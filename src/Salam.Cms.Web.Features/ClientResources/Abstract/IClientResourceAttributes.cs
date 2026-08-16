namespace Salam.Cms.Web.Features.ClientResources.Abstract;

using Salam.Cms.Web.Features.ClientResources.Common;
using Salam.Cms.Web.Features.ClientResources.Enums;
using System.Collections.Generic;

public interface IClientResourceConfiguration
{
    ClientResourceRenderLocationOption RenderLocation { get; set; }

    bool IsMinified { get; set; }

    string? SubResourceIntegrity { get; set; }

    IList<ClientResourceAttributeConfiguration>? Attributes { get; set; }
}
