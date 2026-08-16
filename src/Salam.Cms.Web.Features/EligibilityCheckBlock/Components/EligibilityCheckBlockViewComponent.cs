namespace Salam.Cms.Web.Features.EligibilityCheckBlock.Components;
using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Salam.Cms.Web.Features.EligibilityCheckBlock.ViewModels;
using Salam.Cms.Web.Features.Eligibility;

public sealed class EligibilityCheckBlockViewComponent : BlockComponent<EligibilityCheckBlock>
{
    protected override IViewComponentResult InvokeComponent(EligibilityCheckBlock currentContent)
    {
        EligibilityCheckBlockViewModel model = new(currentContent)
        {
        };

        return View(model);
    }
}