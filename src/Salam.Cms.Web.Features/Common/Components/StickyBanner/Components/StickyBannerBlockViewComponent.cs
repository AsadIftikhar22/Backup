namespace Salam.Cms.Web.Features.Common.Components.StickyBanner.Components;
using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Salam.Cms.Web.Features.Common.Components.StickyBanner.Models;
using Salam.Cms.Web.Features.Common.Components.StickyBanner.ViewModels;

public sealed class StickyBannerBlockViewComponent : BlockComponent<StickyBannerBlock>
{
    public StickyBannerBlockViewComponent() { }

    protected override IViewComponentResult InvokeComponent(StickyBannerBlock currentContent)
    {
        StickyBannerBlockViewModel model = new(currentContent)
        {
        };

        return View(model);
    }
}
