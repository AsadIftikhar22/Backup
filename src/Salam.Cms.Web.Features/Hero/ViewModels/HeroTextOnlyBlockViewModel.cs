namespace Salam.Cms.Web.Features.Hero.ViewModels;

using EPiServer.Core;
using Salam.Cms.Web.Features.Common.ViewModels;
using Salam.Cms.Web.Features.Hero.Models;

public sealed class HeroTextOnlyBlockViewModel : BlockViewModel<HeroTextOnlyBlock>
{
    public HeroTextOnlyBlockViewModel(HeroTextOnlyBlock currentBlock) : base(currentBlock)
    {
    }
    public PageData? CurrentPage { get; set; }
}