namespace Salam.Cms.Web.Features.Navigation.Components;

using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Salam.Cms.Web.Features.Navigation.Models;
using Salam.Cms.Web.Features.Navigation.ViewModels;

public sealed class NavigationItemCollectionBlockViewComponent : BlockComponent<NavigationItemCollectionBlock>
{
    protected override IViewComponentResult InvokeComponent(NavigationItemCollectionBlock currentContent)
    {
        NavigationItemCollectionViewModel model = new(currentContent)
        {
        };

        return View(model);
    }

}