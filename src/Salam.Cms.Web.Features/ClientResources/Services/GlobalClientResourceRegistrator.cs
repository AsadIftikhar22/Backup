using Salam.Cms.Web.Features.Settings.Models;

namespace Salam.Cms.Web.Features.ClientResources.Services;

using EPiServer.Core;
using EPiServer.Framework.Web.Resources;
using Salam.Cms.Core.Settings.Abstract;
using Salam.Cms.Web.Features.ClientResources.Abstract;
using ClientResourceSettings = ClientResourceSettings;

[ClientResourceRegistrator]
public class GlobalClientResourceRegistrator : IClientResourceRegistrator
{
    private readonly IClientResourceRegistrationService _clientResourceRegistrationService;
    private readonly ISettingsManager _settingsManager;

    public GlobalClientResourceRegistrator(IClientResourceRegistrationService clientResourceRegistrationService,
        ISettingsManager settingsManager)
    {
        _clientResourceRegistrationService = clientResourceRegistrationService;
        _settingsManager = settingsManager;
    }

    public void RegisterResources(IRequiredClientResourceList requiredResources)
    {
        var clientResourceSettings = _settingsManager.GetSettings<ClientResourceSettings>();

        var clientResources = clientResourceSettings?.ClientResources?.FilteredItems.Select(x => x.LoadContent() as IClientResourceInclude);

        if (clientResources == null)
        {
            return;
        }

        _clientResourceRegistrationService.RegisterResources(clientResources);
    }
}
