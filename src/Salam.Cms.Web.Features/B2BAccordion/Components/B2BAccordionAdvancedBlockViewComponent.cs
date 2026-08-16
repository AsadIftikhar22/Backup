namespace Salam.Cms.Web.Features.Accordion.Components;

using EPiServer;
using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Salam.Cms.Web.Features.Accordion.Models;
using Salam.Cms.Web.Features.Accordion.ViewModels;

public sealed class B2BAccordionAdvancedBlockViewComponent : BlockComponent<B2BAccordionAdvancedBlock>
{
    private readonly IContentLoader _contentLoader;

    public B2BAccordionAdvancedBlockViewComponent(IContentLoader contentLoader)
    {
        _contentLoader = contentLoader;
    }

    protected override IViewComponentResult InvokeComponent(B2BAccordionAdvancedBlock currentContent)
    {
        B2BAccordionAdvancedBlockViewModel model = new(currentContent)
        {
        };

        return View(model);
    }
}