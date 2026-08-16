namespace Salam.Cms.Web.Features.InformationItem.Components;
using EPiServer;
using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Salam.Cms.Web.Features.InformationItem.Models;
using Salam.Cms.Web.Features.InformationItem.ViewModels;

public class InformationItemListBlockViewComponent : BlockComponent<InformationItemListBlock>
{
    private readonly IContentLoader _contentLoader;
    public InformationItemListBlockViewComponent(IContentLoader contentLoader)
    {
        _contentLoader = contentLoader;
    }
    protected override IViewComponentResult InvokeComponent(InformationItemListBlock currentContent)
    {
        InformationItemListBlockViewModel model = new(currentContent)
        {
        };

        return View(model);
    }
}