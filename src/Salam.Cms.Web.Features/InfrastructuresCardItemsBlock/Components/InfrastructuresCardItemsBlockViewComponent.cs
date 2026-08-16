namespace Salam.Cms.Web.Features.InfrastructuresCardItems.Components;

using EPiServer;
using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Salam.Cms.Web.Features.InfrastructuresCardItems.Models;
using Salam.Cms.Web.Features.InfrastructuresCardItems.ViewModels;

public sealed class InfrastructuresCardItemsBlockViewComponent : BlockComponent<InfrastructuresCardItemsBlock>
{
    private readonly IContentLoader _contentLoader;

    public InfrastructuresCardItemsBlockViewComponent(IContentLoader contentLoader)
    {
        _contentLoader = contentLoader;
    }

    protected override IViewComponentResult InvokeComponent(InfrastructuresCardItemsBlock currentContent)
    {
        InfrastructuresCardItemsBlockViewModel model = new(currentContent)
        {
        };

        return View(model);
    }
}