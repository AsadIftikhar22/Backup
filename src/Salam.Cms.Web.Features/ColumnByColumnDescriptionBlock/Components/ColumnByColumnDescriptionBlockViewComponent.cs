namespace Salam.Cms.Web.Features.ColumnByColumnDescriptionBlock.Components;

using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Salam.Cms.Web.Features.ColumnByColumnDescriptionBlock.ViewModels;
using Salam.Cms.Web.Features.ColumnByColumnDescriptionBlock.Models;

public sealed class ColumnByColumnDescriptionBlockViewComponent : BlockComponent<ColumnByColumnDescriptionBlock>
{
    public ColumnByColumnDescriptionBlockViewComponent()
    {

    }
    protected override IViewComponentResult InvokeComponent(ColumnByColumnDescriptionBlock currentContent)
    {
        ColumnByColumnDescriptionBlockViewModel model = new(currentContent)
        {
        };

        return View(model);
    }
}