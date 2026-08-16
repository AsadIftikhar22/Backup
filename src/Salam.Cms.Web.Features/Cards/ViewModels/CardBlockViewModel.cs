namespace Salam.Cms.Web.Features.Cards.ViewModels;

using PictureRenderer.Profiles;
using Salam.Cms.Shared.Models.Common.Enums;
using Salam.Cms.Web.Features.Cards.Models;
using Salam.Cms.Web.Features.Common.ViewModels;

public sealed class CardBlockViewModel : BlockViewModel<CardBlock>
{
    public CardBlockViewModel(CardBlock currentBlock) : base(currentBlock)
    {
    }

    public string? Theme { get; set; }
    public string? Style { get; set; }
    public string? Layout { get; set; }
    public StyleOption ButtonStyle { get; set; }
    public CloudflareProfile? ImageProfile { get; set; }
}
