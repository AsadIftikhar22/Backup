namespace Salam.Cms.Web.Features.Common.Components.Footer;
using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Salam.Cms.Web.Features.Navigation.Models;

public sealed class FIFooterViewComponent : BlockComponent<NavigationItemCollectionBlock>
{
    protected override IViewComponentResult InvokeComponent(NavigationItemCollectionBlock currentContent)
    {
        string FooterView = "~/Views/Shared/Components/FIFooter/Default.cshtml";
        return View(FooterView);
    }

}