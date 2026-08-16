namespace Salam.Cms.Web.Features.B2BInfrastructureCardBlock.Components;

using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Salam.Cms.Web.Features.B2BInfrastructureCardBlock.Models;
using Salam.Cms.Web.Features.B2BInfrastructureCardBlock.ViewModels;


public sealed class B2BInfrastructureCardBlockViewComponent : BlockComponent<B2BInfrastructureCardBlock>
{
    public B2BInfrastructureCardBlockViewComponent()
    {

    }
    protected override IViewComponentResult InvokeComponent(B2BInfrastructureCardBlock currentContent)
    {
        B2BInfrastructureCardBlockViewModel model = new(currentContent)
        {
        };

        return View(model);
    }
}