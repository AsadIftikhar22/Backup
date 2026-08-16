namespace Salam.Cms.Web.Features.InformationItem.Enums;

using Salam.Cms.Shared.Models.Extensions;

public enum InformationItemListWidthOption
{
    Default,

    [CssClass("information-item-list-block--max-width")]
    ApplyMaxWidth
}
