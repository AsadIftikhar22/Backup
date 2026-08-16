namespace Salam.Cms.Web.Features.DXPB2BTable.Components;

using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Salam.Cms.Web.Features.DXPB2BTable.Models;
using Salam.Cms.Web.Features.DXPB2BTable.ViewModels;

public sealed class DXPB2BTableBlockViewComponent : BlockComponent<B2BTableGenericBlock>
{
    public DXPB2BTableBlockViewComponent()
    {

    }
    protected override IViewComponentResult InvokeComponent(B2BTableGenericBlock currentContent)
    {
        DXPB2BTableBlockViewModel model = new(currentContent)
        {
        };

        return View(model);
    }
}