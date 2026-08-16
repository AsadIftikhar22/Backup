namespace Salam.Cms.Web.Features.Embed.Components;

using EPiServer.Core;
using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Salam.Cms.Web.Features.ClientResources.Abstract;
using Salam.Cms.Web.Features.ClientResources.Enums;
using Salam.Cms.Web.Features.ClientResources.Services;
using Salam.Cms.Web.Features.Embed.Models;

public sealed class EmbedBlockViewComponent : AsyncBlockComponent<EmbedBlock>
{
    private readonly IClientResourceRegistrationService _clientResourceRegistrationService;

    public EmbedBlockViewComponent(IClientResourceRegistrationService clientResourceRegistrationService)
    {
        _clientResourceRegistrationService = clientResourceRegistrationService;
    }

    protected override async Task<IViewComponentResult> InvokeComponentAsync(EmbedBlock currentContent)
    {
        var clientResources = currentContent.ClientResources?.FilteredItems.Select(x => x.LoadContent() as IClientResourceInclude);

        if (clientResources != null)
        {
            _clientResourceRegistrationService.RegisterResources(clientResources);

            if (currentContent.RenderLocation != EmbedRenderLocationOption.Inline)
            {
                _clientResourceRegistrationService.RegisterResource(currentContent);
            }
        }

        return await Task.FromResult(View(currentContent));
    }
}