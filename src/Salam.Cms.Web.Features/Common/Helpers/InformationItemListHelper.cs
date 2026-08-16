namespace Salam.Cms.Web.Features.Common.Helpers;

public static class InformationItemListBlockHelper

{
    public static string GetModifierClass(int itemCount)
    {
        return itemCount switch
        {
            2 => "information-item-list-block--two-items",
            3 => "information-item-list-block--three-items",
            4 => "information-item-list-block--four-items",
            5 => "information-item-list-block--five-items",
            _ => string.Empty
        };
    }
}

