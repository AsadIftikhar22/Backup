namespace Salam.Cms.Web.Features.BusinessCardDescription.Components;

using EPiServer;
using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Salam.Cms.Web.Features.BusinessCardDescription.Models;
using Salam.Cms.Web.Features.BusinessCardDescription.ViewModels;

public sealed class BusinessCardDescriptionBlockViewComponent : BlockComponent<BusinessCardDescriptionBlock>
{
    private readonly IContentLoader _contentLoader;

    public BusinessCardDescriptionBlockViewComponent(IContentLoader contentLoader)
    {
        _contentLoader = contentLoader;
    }

    protected override IViewComponentResult InvokeComponent(BusinessCardDescriptionBlock currentContent)
    {
        BusinessCardDescriptionBlockViewModel model = new(currentContent)
        {
        };

        return View(model);
    }
}