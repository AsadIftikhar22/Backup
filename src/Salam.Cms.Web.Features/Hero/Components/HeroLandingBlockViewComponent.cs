namespace Salam.Cms.Web.Features.Hero.Components;

using EPiServer;
using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Salam.Cms.Web.Features.Hero.Models;
using Salam.Cms.Web.Features.Hero.ViewModels;

public sealed class HeroLandingBlockViewComponent : BlockComponent<HeroLandingBlock>
{
    private readonly IContentLoader _contentLoader;

    public HeroLandingBlockViewComponent(IContentLoader contentLoader)
    {
        _contentLoader = contentLoader;
    }

    protected override IViewComponentResult InvokeComponent(HeroLandingBlock currentContent)
    {
        var model = new HeroLandingBlockViewModel(currentContent)
        {
            HeroHeadings = currentContent.Items?.FilteredItems
                .Select(item => _contentLoader.Get<HeroBlock>(item.ContentLink).BadgeText)
                .ToList() ?? new List<string>(),

            HeroBlocks = currentContent.Items?.FilteredItems
                .Select(item => _contentLoader.Get<HeroBlock>(item.ContentLink))
                .ToList() ?? new List<HeroBlock>()

        };

        return View(model);
    }
}

