namespace Salam.Cms.Web.Features.SLABusinessPDFBlock.Components;

using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Salam.Cms.Web.Features.SLABusinessPDFBlock.Models;
using Salam.Cms.Web.Features.SLABusinessPDFBlock.ViewModels;

public sealed class SLABusinessPDFBlockViewComponent : BlockComponent<SLABusinessPDFBlock>
{
    public SLABusinessPDFBlockViewComponent()
    {

    }
    protected override IViewComponentResult InvokeComponent(SLABusinessPDFBlock currentContent)
    {
        SLABusinessPDFBlockViewModel model = new(currentContent)
        {
        };

        return View(model);
    }
}