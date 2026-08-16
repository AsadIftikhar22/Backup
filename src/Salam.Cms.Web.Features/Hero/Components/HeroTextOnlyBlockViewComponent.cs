namespace Salam.Cms.Web.Features.Hero.Components;

using EPiServer.Core;
using EPiServer.Web.Mvc;
using EPiServer.Web.Routing;
using Microsoft.AspNetCore.Mvc;
using Salam.Cms.Web.Features.Hero.Models;
using Salam.Cms.Web.Features.Hero.ViewModels;

public class HeroTextOnlyBlockViewComponent : BlockComponent<HeroTextOnlyBlock>
{
    private readonly IPageRouteHelper _pageRouteHelper;

    public HeroTextOnlyBlockViewComponent(IPageRouteHelper pageRouteHelper)
    {
        _pageRouteHelper = pageRouteHelper;
    }

    protected override IViewComponentResult InvokeComponent(HeroTextOnlyBlock currentContent)
    {
        var model = BuildModel(currentContent);

        return View(model);
    }

    private HeroTextOnlyBlockViewModel BuildModel(HeroTextOnlyBlock heroBlock)
    {
        var currentPage = _pageRouteHelper.Page as PageData;

        var model = new HeroTextOnlyBlockViewModel(heroBlock)
        {
            CurrentPage = currentPage,
        };

        return model;
    }
}
