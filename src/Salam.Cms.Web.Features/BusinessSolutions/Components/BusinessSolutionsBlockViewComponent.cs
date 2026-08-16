namespace Salam.Cms.Web.Features.Accordion.Components;

using EPiServer;
using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Salam.Cms.Web.Features.Accordion.Models;
using Salam.Cms.Web.Features.Accordion.ViewModels;

public sealed class BusinessSolutionsBlockViewComponent : BlockComponent<BusinessSolutionsBlock>
{
    private readonly IContentLoader _contentLoader;

    public BusinessSolutionsBlockViewComponent(IContentLoader contentLoader)
    {
        _contentLoader = contentLoader;
    }

    protected override IViewComponentResult InvokeComponent(BusinessSolutionsBlock currentContent)
    {
        BusinessSolutionsBlockViewModel model = new(currentContent)
        {
        };

        return View(model);
    }
}