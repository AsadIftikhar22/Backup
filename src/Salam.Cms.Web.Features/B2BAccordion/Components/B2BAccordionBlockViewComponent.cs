namespace Salam.Cms.Web.Features.B2BAccordion.Components;

using EPiServer;
using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Salam.Cms.Web.Features.Accordion.Models;
using Salam.Cms.Web.Features.Accordion.ViewModels;

public sealed class B2BAccordionBlockViewComponent : BlockComponent<B2BAccordionBlock>
{
    private readonly IContentLoader _contentLoader;

    public B2BAccordionBlockViewComponent(IContentLoader contentLoader)
    {
        _contentLoader = contentLoader;
    }

    protected override IViewComponentResult InvokeComponent(B2BAccordionBlock currentContent)
    {
        B2BAccordionBlockViewModel model = new(currentContent)
        {
        };

        return View(model);
    }
}