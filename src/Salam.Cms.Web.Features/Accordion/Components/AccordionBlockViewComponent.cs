namespace Salam.Cms.Web.Features.Accordion.Components;

using EPiServer;
using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Salam.Cms.Web.Features.Accordion.Models;
using Salam.Cms.Web.Features.Accordion.ViewModels;

public sealed class AccordionBlockViewComponent : BlockComponent<AccordionBlock>
{
    private readonly IContentLoader _contentLoader;

    public AccordionBlockViewComponent(IContentLoader contentLoader)
    {
        _contentLoader = contentLoader;
    }

    protected override IViewComponentResult InvokeComponent(AccordionBlock currentContent)
    {
        AccordionBlockViewModel model = new(currentContent)
        {
        };

        return View(model);
    }
}