namespace Salam.Cms.Core.Services.Caching;

public static class CacheKeys
{
    // Master keys for grouping related cache entries
    public static class MasterKeys
    {
        public const string SiteContent = "MASTER_SITE_CONTENT";
        public const string ProductCatalogue = "MASTER_PRODUCT_CATALOGUE";
        public const string ProductCategoryCatalogue = "MASTER_CATEGORY_CATALOGUE";
        public const string Navigation = "MASTER_NAVIGATION";
        public const string Media = "MASTER_MEDIA";
        public const string DamMetadata = "MASTER_DAMMETADATA";
        public const string InlineCss = "MASTER_INLINECSS";
    }

    // Individual cache keys
    public static string Navigation => "SITE_NAVIGATION";
    public static string B2B_Navigation => "B2B_SITE_NAVIGATION";
    public static string WholeSale_Navigation => "WholeSale_Navigation";

    public static string Footer => "SITE_FOOTER";
    public static string CookiesBanner => "Cookies_Banner";
    public static string B2B_Footer => "B2B_SITE_FOOTER";
    public static string SiteMap => "SITE_MAP";
    public static string BreadCrumbs => "SITE_BREADCRUMBS";
    public static string SvgContent => "SITE_SVGCONTENT";
    public static string DamMetadata => "SITE_DAMMETADATA";
    public static string InlineCss => "SITE_INLINECSS";

    // ---------- Catalogue / ProductSku ----------
    // Prefix used for all cached product dictionaries
    public static string ProductSkuPrefix => "ProductSku:";
    public static string LabelsPrefix => "Labels:";
    public static string CategoriesPrefix => "Categories:";

    public static IEnumerable<string> GetAllKeys()
    {
        yield return Navigation;
        yield return Footer;
        yield return SiteMap;
        yield return BreadCrumbs;
        yield return SvgContent;
        yield return ProductSkuPrefix;
        yield return LabelsPrefix;
        yield return CategoriesPrefix;
    }

    /// <summary>
    /// Builds a cache key for a set of product sku IDs and language.
    /// The ID list is sorted then hashed so even long lists create short keys.
    /// </summary>
    public static string CreateProductSkusCacheKey(IEnumerable<int> ids, string language)
    {
        var ordered = ids.OrderBy(i => i).ToArray();
        var raw = string.Join(',', ordered);
        using var sha1 = System.Security.Cryptography.SHA1.Create();
        var hash = BitConverter.ToString(sha1.ComputeHash(System.Text.Encoding.UTF8.GetBytes(raw))).Replace("-", "");
        return $"{ProductSkuPrefix}{language}:{hash}";
    }
}