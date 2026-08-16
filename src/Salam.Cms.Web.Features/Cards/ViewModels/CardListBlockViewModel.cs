namespace Salam.Cms.Web.Features.Cards.ViewModels;

using Salam.Cms.Web.Features.Cards.Enums;
using Salam.Cms.Web.Features.Cards.Models;
using Salam.Cms.Web.Features.Common.ViewModels;

public sealed class CardListBlockViewModel : BlockViewModel<CardListBlock>
{
    public CardListBlockViewModel(CardListBlock currentBlock) : base(currentBlock)
    {
    }

    public string CssClasses { get; set; } = string.Empty;
    public IEnumerable<CardBlockViewModel>? CardBlocks { get; set; }
    public CardListStylePresetOption StylePreset => CurrentBlock.StylePreset;

}
