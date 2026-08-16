namespace Salam.Cms.Web.Features.OurStoresBlock.Components;

using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Salam.Cms.Web.Features.OurStoresBlock.Models;
using Salam.Cms.Web.Features.OurStoresBlock.ViewModels;

public sealed class OurStoresSectionBlockViewComponent : BlockComponent<OurStoresSectionBlock>
{
    protected override IViewComponentResult InvokeComponent(OurStoresSectionBlock currentContent)
    {
        var model = new OurStoresSectionBlockViewModel(currentContent);
        return View(model);
    }
}
