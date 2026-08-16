namespace Salam.Cms.Web.Features.TabsFormBlock.Components;

using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Salam.Cms.Web.Features.TabsFormBlock.Models;
using Salam.Cms.Web.Features.TabsFormBlock.ViewModels;
public sealed class TabsFormBlockViewComponent : BlockComponent<TabsFormBlock>
{
    public TabsFormBlockViewComponent()
    {

    }
    protected override IViewComponentResult InvokeComponent(TabsFormBlock currentContent)
    {
        TabsFormBlockViewModel model = new(currentContent)
        {
        };

        return View(model);
    }
}