namespace Salam.Cms.Web.Features.InternetCardGrid.Components;

using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Salam.Cms.Web.Features.SolutionsSectionsBlock.Models;
using Salam.Cms.Web.Features.SolutionsSectionsBlock.ViewModels;

public sealed class SolutionsSections4By4BlockViewComponent : BlockComponent<SolutionsSections4By4Block>
{
    public SolutionsSections4By4BlockViewComponent()
    {

    }
    protected override IViewComponentResult InvokeComponent(SolutionsSections4By4Block currentContent)
    {
        SolutionsSections4By4BlockViewModel model = new(currentContent)
        {
        };

        return View(model);
    }
}