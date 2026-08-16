namespace Salam.Cms.Web.Features.XHtmlBlock.Components;

using EPiServer;
using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Salam.Cms.Web.Features.XHtmlBlock.Models;
using Salam.Cms.Web.Features.XHtmlBlock.ViewModels;

public sealed class XHtmlBlockViewComponent : BlockComponent<XHtmlBlock>
{
    private readonly IContentLoader _contentLoader;

    public XHtmlBlockViewComponent(IContentLoader contentLoader)
    {
        _contentLoader = contentLoader;
    }

    protected override IViewComponentResult InvokeComponent(XHtmlBlock currentContent)
    {
        XHtmlBlockViewModel model = new(currentContent)
        {
        };

        return View(model);
    }
}
