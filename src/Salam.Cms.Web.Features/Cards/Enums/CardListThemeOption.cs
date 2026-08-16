namespace Salam.Cms.Web.Features.Cards.Enums;

using Salam.Cms.Shared.Models.Extensions;

public enum CardListThemeOption
{
    Default,

    [CssClass("card-list--light-only")]
    LightOnly,

    [CssClass("card-list--dark-only")]
    DarkOnly,

    [CssClass("card-list--light-first")]
    AlternatingLightFirst,

    [CssClass("card-list--dark-first")]
    AlternatingDarkFirst
}
