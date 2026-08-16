namespace Salam.Cms.Web.Features.Hero.ViewModels;

using EPiServer.Core;
using Salam.Cms.Shared.Models.Common.Components;
using Salam.Cms.Web.Features.Common.ViewModels;
using Salam.Cms.Web.Features.Hero.Models;

public sealed class HeroBlockViewModel : BlockViewModel<HeroBlock>
{
    public HeroBlockViewModel(HeroBlock currentBlock) : base(currentBlock)
    {
    }

    public List<LinkModel> LinkItems { get; set; } = new();

    public string? LayoutCssClass { get; set; }

    public PageData? CurrentPage { get; set; }

}