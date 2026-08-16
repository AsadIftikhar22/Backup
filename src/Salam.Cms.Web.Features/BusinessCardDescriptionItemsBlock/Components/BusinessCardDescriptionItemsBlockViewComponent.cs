namespace Salam.Cms.Web.Features.BusinessCardDescriptionItems.Components;

using EPiServer;
using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Salam.Cms.Web.Features.BusinessCardDescriptionItems.Models;
using Salam.Cms.Web.Features.BusinessCardDescriptionItems.ViewModels;

public sealed class BusinessCardDescriptionItemsBlockViewComponent : BlockComponent<BusinessCardDescriptionItemsBlock>
{
    private readonly IContentLoader _contentLoader;

    public BusinessCardDescriptionItemsBlockViewComponent(IContentLoader contentLoader)
    {
        _contentLoader = contentLoader;
    }

    protected override IViewComponentResult InvokeComponent(BusinessCardDescriptionItemsBlock currentContent)
    {
        BusinessCardDescriptionItemsBlockViewModel model = new(currentContent)
        {
        };

        return View(model);
    }
}