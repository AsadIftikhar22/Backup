namespace Salam.Cms.Web.Features.Cards.Enums;

using Salam.Cms.Shared.Models.Extensions;

public enum CardStyleOption
{
    [CssClass("card-block__featured-image")]
    FeaturedImage,

    [CssClass("card-block__icon")]
    Icon,

    [CssClass("card-block--centered")]
    Centered,

    [CssClass("card-block__badge")]
    Badge
}