namespace Salam.Cms.Web.Features.ConnectivityRemove.Components;

using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Salam.Cms.Web.Features.ConnectivityRemove.Models;
using Salam.Cms.Web.Features.ConnectivityRemove.ViewModels;

public sealed class ConnectivityRemoveBlockViewComponent : BlockComponent<ConnectivityRemoveBlock>
{
    public ConnectivityRemoveBlockViewComponent()
    {

    }
    protected override IViewComponentResult InvokeComponent(ConnectivityRemoveBlock currentContent)
    {
        ConnectivityRemoveBlockViewModel model = new(currentContent)
        {
        };

        return View(model);
    }
}