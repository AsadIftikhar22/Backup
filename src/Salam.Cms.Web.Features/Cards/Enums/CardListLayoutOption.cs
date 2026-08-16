namespace Salam.Cms.Web.Features.Cards.Enums;

using Salam.Cms.Shared.Models.Extensions;

public enum CardListLayoutOption
{
    [CssClass("card-list--row")]
    Row,

    [CssClass("card-list--featured")]
    Featured,

    [CssClass("card-list--flip")]
    AlternatingHorizontalFlip,

    [CssClass("card-list--featured card-list--featured--centered")]
    FeaturedCentered,
}

