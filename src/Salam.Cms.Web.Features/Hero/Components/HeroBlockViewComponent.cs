namespace Salam.Cms.Web.Features.Hero.Components;

using EPiServer;
using EPiServer.Web.Mvc;
using EPiServer.Web.Routing;
using Microsoft.AspNetCore.Mvc;
using Salam.Cms.Shared.Models.Common.Components;
using Salam.Cms.Shared.Models.Extensions;
using Salam.Cms.Web.Features.Hero.Models;
using Salam.Cms.Web.Features.Hero.ViewModels;

public sealed class HeroBlockViewComponent : BlockComponent<HeroBlock>
{
    private readonly IContentLoader _contentLoader;
    private readonly ILinkModelConverter _linkModelConverter;
    private readonly IPageRouteHelper _pageRouteHelper;

    public HeroBlockViewComponent(IContentLoader contentLoader,
        ILinkModelConverter linkModelConverter,
        IPageRouteHelper pageRouteHelper)
    {
        _contentLoader = contentLoader;
        _linkModelConverter = linkModelConverter;
        _pageRouteHelper = pageRouteHelper;
    }

    protected override IViewComponentResult InvokeComponent(HeroBlock currentContent)
    {
        var model = BuildModel(currentContent);

        return View(model);
    }

    private HeroBlockViewModel BuildModel(HeroBlock heroBlock)
    {
        var currentPage = _pageRouteHelper.Page;

        var model = new HeroBlockViewModel(heroBlock)
        {
            LinkItems = _linkModelConverter.ConvertToModelCollection(heroBlock.LinkItems),
            LayoutCssClass = heroBlock.Layout.GetCssClass().ToLowerInvariant(),
            CurrentPage = currentPage,
        };

        return model;
    }
}