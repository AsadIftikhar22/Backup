namespace Salam.Cms.Web.Features.Hero.ViewModels;

using Salam.Cms.Web.Features.Common.ViewModels;
using Salam.Cms.Web.Features.Hero.Models;

public sealed class HeroLandingBlockViewModel : BlockViewModel<HeroLandingBlock>
{
    public HeroLandingBlockViewModel(HeroLandingBlock currentBlock) : base(currentBlock)
    {
    }

    public List<string> HeroHeadings { get; set; } = new();
    public List<HeroBlock> HeroBlocks { get; set; } = new();

}
