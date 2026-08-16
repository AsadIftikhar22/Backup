namespace Salam.Cms.Shared.Models.Catalogue.Extensions;

using EPiServer.Find;
using Salam.Cms.Shared.Models.Catalogue.Data;
using Salam.Cms.Shared.Models.Catalogue.Models;

public static class SearchExtensions
{
    public static ITypeSearch<ProductSku> FilterByName(this ITypeSearch<ProductSku> query, string name)
        => query.Filter(x => x.Name.MatchCaseInsensitive(name));

    public static ITypeSearch<ProductSku> FilterByPrice(this ITypeSearch<ProductSku> query, decimal minValue, decimal maxValue)
        => query.Filter(x => x.Price.InRange(minValue, maxValue));

    public static ITypeSearch<ProductSku> FilterBySku(this ITypeSearch<ProductSku> query, string sku)
        => query.Filter(x => x.Sku.MatchCaseInsensitive(sku));

    public static ITypeSearch<ProductSku> FilterByType(this ITypeSearch<ProductSku> query, string type)
        => query.Filter(x => x.ProductType.MatchCaseInsensitive(type));

    [Obsolete("Don't use this method, it returns inconsistent results.", true)]
    public static ITypeSearch<ProductSku> FilterByCategories(this ITypeSearch<ProductSku> query, List<int> categoryIds)
    => query.Filter(x => x.CategoryIds.In(categoryIds));

    [Obsolete("Don't use this method, it returns inconsistent results.", true)]
    public static ITypeSearch<ProductSku> FilterByCategory(this ITypeSearch<ProductSku> query, int categoryId)
    => query.Filter(x => x.CategoryIds.Match(categoryId));

    //Category
    public static ITypeSearch<Category> FilterByCategoryId(this ITypeSearch<Category> query, int categoryId)
    => query.Filter(x => x.Id.Match(categoryId));
}
