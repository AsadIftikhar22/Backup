namespace Salam.Cms.Web.Features.InternetCardGrid.Components;

using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Salam.Cms.Web.Features.SolutionsSectionsBlock.Models;
using Salam.Cms.Web.Features.SolutionsSectionsBlock.ViewModels;

public sealed class SolutionsSections3By3BlockViewComponent : BlockComponent<DXPB2BSolutionsSections3By3Block>
{
    public SolutionsSections3By3BlockViewComponent()
    {

    }
    protected override IViewComponentResult InvokeComponent(DXPB2BSolutionsSections3By3Block currentContent)
    {
        SolutionsSections3By3BlockViewModel model = new(currentContent)
        {
        };

        return View(model);
    }
}