namespace Salam.Cms.Web.Features.IconLinks.Components;

using EPiServer;
using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Salam.Cms.Web.Features.IconLinks.Models;
using Salam.Cms.Web.Features.IconLinks.ViewModels;

public sealed class IconLinkItemListBlockViewComponent : BlockComponent<IconLinkItemListBlock>
{
    private readonly IContentLoader _contentLoader;

    public IconLinkItemListBlockViewComponent(IContentLoader contentLoader)
    {
        _contentLoader = contentLoader;
    }

    protected override IViewComponentResult InvokeComponent(IconLinkItemListBlock currentContent)
    {
        IconLinkItemListBlockViewModel model = new(currentContent)
        {
        };

        return View(model);
    }
}
