namespace Salam.Cms.Web.Features.TabContainer.Components;

using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Salam.Cms.Web.Features.TabContainer.Models;
using Salam.Cms.Web.Features.TabContainer.ViewModels;

public sealed class TabContainerBlockViewComponent : BlockComponent<TabContainerBlock>
{
    public TabContainerBlockViewComponent()
    {

    }
    protected override IViewComponentResult InvokeComponent(TabContainerBlock currentContent)
    {
        TabContainerBlockViewModel model = new(currentContent)
        {
        };

        return View(model);
    }
}