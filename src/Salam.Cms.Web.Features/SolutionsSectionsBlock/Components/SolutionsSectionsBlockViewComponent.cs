namespace Salam.Cms.Web.Features.InternetCardGrid.Components;

using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Salam.Cms.Web.Features.SolutionsSectionsBlock.Models;
using Salam.Cms.Web.Features.SolutionsSectionsBlock.ViewModels;

public sealed class SolutionsSectionsBlockViewComponent : BlockComponent<DXPB2BSolutionsSectionsBlock>
{
    public SolutionsSectionsBlockViewComponent()
    {

    }
    protected override IViewComponentResult InvokeComponent(DXPB2BSolutionsSectionsBlock currentContent)
    {
        SolutionsSectionsBlockViewModel model = new(currentContent)
        {
        };

        return View(model);
    }
}