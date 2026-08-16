namespace Salam.Cms.Web.Features.Common.Interfaces;

using EPiServer.Core;

/// <summary>
/// Interface for content types that include SEO metadata.
/// This can be implemented by any content type across any channel that needs SEO capabilities.
/// </summary>
public interface ISeoMetaData
{
    /// <summary>
    /// Gets the meta title to be used for SEO purposes.
    /// This will typically be used in the HTML title tag.
    /// </summary>
    string? MetaTitle { get; }

    /// <summary>
    /// Gets the meta description to be used for SEO purposes.
    /// This will typically be used in the meta description tag.
    /// </summary>
    string? MetaDescription { get; }

    /// <summary>
    /// Gets the Open Graph title. Falls back to MetaTitle if not set.
    /// Used specifically for social media sharing previews.
    /// </summary>
    string? SocialSharingTitle { get; }

    /// <summary>
    /// Gets the Open Graph description. Falls back to MetaDescription if not set.
    /// Used specifically for social media sharing previews.
    /// </summary>
    string? SocialSharingDescription { get; }

    /// <summary>
    /// Gets the Social Sharing Image for the page.
    /// Client specific implementation.
    /// </summary>
    ContentReference? SocialSharingImage { get; }

    /// <summary>
    /// Gets the alt text for the Social Sharing Image.
    /// Client specific implementation.
    /// </summary>
    string? SocialSharingImageAltText { get; }

    /// <summary>
    /// Gets the twitter account for the author of the page.
    /// Client specific implementation.
    /// </summary>
    string? TwitterCardCreator { get; }

    /// <summary>
    /// Gets the canonical URL reference for this content.
    /// If specified, this will be used as the canonical URL instead of the content's own URL.
    /// </summary>
    ContentReference? AlternateCanonicalLink { get; }

    /// <summary>
    /// Gets the indicator stating whether to include the page and it's children in the HTML Sitemap.
    /// Client specific implementation.
    /// </summary>
    bool IncludeInHtmlSitemap { get; }

    /// <summary>
    /// Gets the indicator stating whether to exclude the page in search results.
    /// Client specific implementation.
    /// </summary>
    bool ExcludeFromSearchResults { get; }

    /// <summary>
    /// Gets the indicators for 'noindex' and 'nofollow' robots meta tags to prevent indexing and link following.
    /// </summary>
    string? MetaRobots { get; set; }

    /// <summary>
    /// Gets the indicator stating whether to render alt ref lands for the page.
    /// Client specific implementation.
    /// </summary>
    bool RenderAlternativeLinks { get; }
}