namespace Salam.Cms.Web.Features.Cards.Enums;

using Salam.Cms.Shared.Models.Extensions;

public enum CardListStyleOption
{
    [CssClass("card-list__featured-image")]
    FeaturedImage,

    [CssClass("card-list__icon")]
    Icon,

    [CssClass("card-list--centered")]
    Centered,

    [CssClass("card-list__badge")]
    Badge
}
