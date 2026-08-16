namespace Salam.Cms.Web.Features.DXPB2BConnectivity.Components;

using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Salam.Cms.Web.Features.DXPB2BConnectivity.Models;
using Salam.Cms.Web.Features.DXPB2BConnectivity.ViewModels;

public sealed class DXPB2BConnectivityBlockViewComponent : BlockComponent<DXPB2BConnectivityBlock>
{
    public DXPB2BConnectivityBlockViewComponent()
    {

    }
    protected override IViewComponentResult InvokeComponent(DXPB2BConnectivityBlock currentContent)
    {
        DXPB2BConnectivityBlockViewModel model = new(currentContent)
        {
        };

        return View(model);
    }
}