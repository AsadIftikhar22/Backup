namespace Salam.Cms.Web.Features.TileListBlock.Components;

using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Salam.Cms.Web.Features.TileListBlock.Models;
using Salam.Cms.Web.Features.TileListBlock.ViewModels;

public sealed class TileListBlockViewComponent : BlockComponent<TileListBlock>
{
    public TileListBlockViewComponent()
    {

    }
    protected override IViewComponentResult InvokeComponent(TileListBlock currentContent)
    {
        TileListBlockViewModel model = new(currentContent)
        {
        };

        return View(model);
    }
}