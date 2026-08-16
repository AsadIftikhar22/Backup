using global::PictureRenderer.Profiles;

namespace Salam.Cms.Web.Features.Common.Components.Images;

public static class PictureProfiles
{
    #region Hero

    // Hero Landing Block
    public static readonly CloudflareProfile HeroLanding = new()
    {
        SrcSetWidths = new[] { 340, 376, 524, 540 },
        Sizes = new[]
        {
        "(min-width: 1366px) 540px",
        "(min-width: 767px) 100vw",
        "50vw"
    },
        Quality = 80
    };

    // Hero Block
    public static readonly CloudflareProfile HeroBlock = new()
    {
        SrcSetWidths = new[] { 338, 523, 987 }, // Mobile first: smallest to largest
        Sizes = new[]
        {
            "(min-width: 1023px) 100vw",
            "50vw"
        },
        Quality = 80
    };

    #endregion Hero

    #region Cards

    // Card Block - Row
    public static readonly CloudflareProfile CardRow = new()
    {
        SrcSetWidths = new[] { 342, 991, 418, 425 }, // Mobile first: smallest to largest
        Sizes = new[] {
            "(min-width: 1439px) 100vw",
            "(min-width: 1365px) 100vw",
            "(min-width: 1023px) 100vw",
            "50vw"
        },
        Quality = 80
    };

    // Card Block - Featured Large
    public static readonly CloudflareProfile CardFeaturedLarge = new()
    {
        SrcSetWidths = new[] { 342, 555, 991 }, // Mobile first, deduplicated
        Sizes = new[] {
            "(min-width: 1023px) 100vw",
            "50vw"
        },
        Quality = 80
    };

    // Card Block - Featured
    public static readonly CloudflareProfile CardFeatured = new()
    {
        SrcSetWidths = new[] { 342, 443, 991 }, // Mobile first, deduplicated
        Sizes = new[] {
            "(min-width: 1365px) 100vw", // Same for 1439px and 1365px
            "(min-width: 1023px) 100vw",
            "50vw"
        },
        Quality = 80
    };

    // Card Block - Alternating
    public static readonly CloudflareProfile CardAlternating = new()
    {
        SrcSetWidths = new[] { 342, 425, 991 }, // Mobile first, deduplicated
        Sizes = new[] {
            "(min-width: 1365px) 100vw", // Same for 1439px and 1365px
            "(min-width: 1023px) 100vw",
            "50vw"
        },
        Quality = 80
    };

    // Card Block - Default
    // Note: CSV shows no specific dimensions, using same pattern as alternating
    public static readonly CloudflareProfile CardDefault = new()
    {
        SrcSetWidths = new[] { 342, 425, 991 }, // Mobile first, deduplicated
        Sizes = new[] {
            "(min-width: 1365px) 100vw", // Assumed same as alternating
            "(min-width: 1023px) 100vw",
            "50vw"
        },
        Quality = 80
    };

    #endregion Cards

    #region CTA

    // CTA Block
    public static readonly CloudflareProfile CTA = new()
    {
        SrcSetWidths = new[] { 340, 376, 741, 752, 984 }, // match static widths in srcset
        Sizes = new[]
        {
        "(max-width: 767px) 100vw",
        "(min-width:768px) 50vw",
        "741px"
    },
        Quality = 80
    };

    // Carousel Cards
    public static readonly CloudflareProfile CarouselCards = new()
    {
        SrcSetWidths = new[] { 340, 376, 524, 540 },
        Sizes = new[]
        {
        "(min-width: 1366px) 540px",
        "(min-width: 767px) 100vw",
        "50vw"
    },
        Quality = 80
    };

    public static readonly CloudflareProfile UserGuideBanner = new()
    {
        SrcSetWidths = new[] {1000,900},
        Sizes = new[]
      {
        "(min-width: 1366px) 1000",
        "(min-width: 1290) 900",
        "(min-width: 767px) 100vw",
        "50vw"
    },
        Quality = 80
    };
    #endregion CTA
}