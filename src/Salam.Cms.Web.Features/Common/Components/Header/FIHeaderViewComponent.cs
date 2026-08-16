namespace Salam.Cms.Web.Features.Common.Components.Header;
using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Salam.Cms.Web.Features.Navigation.Models;

public class FIHeaderViewComponent : BlockComponent<NavigationItemCollectionBlock>
{
    protected override IViewComponentResult InvokeComponent(NavigationItemCollectionBlock currentContent)
    {
        string HeaderView = "~/Views/Shared/Components/FIHeader/Default.cshtml";
        return View(HeaderView);
    }

}