namespace Salam.Cms.Web.Features.TelecomItRightsBlock.Components;

using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Salam.Cms.Web.Features.TelecomItRightsBlock.Models;
using Salam.Cms.Web.Features.TelecomItRightsBlock.ViewModels;

public sealed class TelecomItRightsSectionBlockViewComponent : BlockComponent<TelecomItRightsSectionBlock>
{
    protected override IViewComponentResult InvokeComponent(TelecomItRightsSectionBlock currentContent)
    {
        var model = new TelecomItRightsSectionBlockViewModel(currentContent);
        return View(model);
    }
}
