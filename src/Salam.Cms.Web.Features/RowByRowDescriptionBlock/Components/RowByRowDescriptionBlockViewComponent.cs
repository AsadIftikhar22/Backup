namespace Salam.Cms.Web.Features.RowByRowDescriptionBlock.Components;

using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Salam.Cms.Web.Features.RowByRowDescriptionBlock.ViewModels;
using Salam.Cms.Web.Features.RowByRowDescriptionBlock.Models;

public sealed class RowByRowDescriptionBlockViewComponent : BlockComponent<RowByRowDescriptionBlock>
{
    public RowByRowDescriptionBlockViewComponent()
    {

    }
    protected override IViewComponentResult InvokeComponent(RowByRowDescriptionBlock currentContent)
    {
        RowByRowDescriptionBlockViewModel model = new(currentContent)
        {
        };

        return View(model);
    }
}