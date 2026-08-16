namespace Salam.Cms.Web.Features.InternetCardGrid.Components;

using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Salam.Cms.Web.Features.SolutionsSectionsBlock.Models;
using Salam.Cms.Web.Features.SolutionsSectionsBlock.ViewModels;

public sealed class UserGuideBannerBlockViewComponent : BlockComponent<UserGuideBannerBlock>
{
    public UserGuideBannerBlockViewComponent()
    {

    }
    protected override IViewComponentResult InvokeComponent(UserGuideBannerBlock currentContent)
    {
        UserGuideBannerBlockViewModel model = new(currentContent)
        {
        };

        return View(model);
    }
}