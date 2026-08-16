namespace Salam.Cms.Web.Features.RedirectRuleBlock.Components;

using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Salam.Cms.Web.Features.RedirectRuleBlock.Models;
using Salam.Cms.Web.Features.RedirectRuleBlock.ViewModels;

public sealed class RedirectRuleBlockViewComponent : BlockComponent<RedirecttRuleBlock>
{
    protected override IViewComponentResult InvokeComponent(RedirecttRuleBlock currentContent)
    {
        var model = new RedirectRuleBlockViewModel(currentContent);
        return View(model);
    }
}
