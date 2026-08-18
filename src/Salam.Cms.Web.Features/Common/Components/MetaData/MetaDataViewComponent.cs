namespace Salam.Cms.Web.Features.Common.Components.MetaData;

using EPiServer;
using EPiServer.Core;
using EPiServer.Web.Routing;
using Geta.Optimizely.Categories;
using Geta.Optimizely.Categories.Extensions;
using Geta.Optimizely.Categories.Routing;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Salam.Cms.Core.Settings.Abstract;
using Salam.Cms.Shared.Models.Extensions;
using Salam.Cms.Shared.Models.Helpers;
using Salam.Cms.Web.Features.Common.Models;
using Salam.Cms.Web.Features.Settings.Models;

public class MetaDataViewComponent : ViewComponent
{
    private readonly ISettingsManager _settingsManager;
    private readonly IValueFallbackHandler _fallbackHandler;
    private readonly IPageRouteHelper _pageRouteHelper;
    private readonly ICategoryContentLoader _categoryContentLoader;
    private readonly IContentLoader _contentLoader;
    private readonly IUrlResolver _urlResolver;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public MetaDataViewComponent(
        ISettingsManager settingsManager,
        IValueFallbackHandler fallbackHandler,
        IContentLoader contentLoader,
        IUrlResolver urlResolver,
        ICategoryContentLoader categoryContentLoader,
        IPageRouteHelper pageRouteHelper,
        IHttpContextAccessor httpContextAccessor)
    {
        _settingsManager = settingsManager;
        _fallbackHandler = fallbackHandler;
        _contentLoader = contentLoader;
        _urlResolver = urlResolver;
        _categoryContentLoader = categoryContentLoader;
        _pageRouteHelper = pageRouteHelper;
        _httpContextAccessor = httpContextAccessor;
    }

    public IViewComponentResult Invoke(SitePageData? currentPage, bool showSiteTitle)
    {
        if (currentPage == null)
        {
            return Content(string.Empty);
        }

        var model = BuildModel(currentPage, showSiteTitle);

        return View(model);
    }

    private MetaDataViewModel BuildModel(SitePageData sitePageData, bool showSiteTitle)
    {
        var description = _fallbackHandler.GetBest(
            sitePageData.MetaDescription,
            sitePageData.SocialSharingDescription);

        string[]? categorySegments = null;

        if (_httpContextAccessor.HttpContext?.Request?.RouteValues != null)
        {
            object? routeValue;
            if (_httpContextAccessor.HttpContext.Request.RouteValues.TryGetValue(CategoryRoutingConstants.CurrentCategories, out routeValue) && routeValue != null)
            {
                categorySegments = routeValue as string[];
            }
        }

        var categories = categorySegments?.Select(x => _categoryContentLoader.GetFirstBySegment<CategoryData>(x)).ToList();

        var relativeCategoryUrl = categories?.Any() == true
            ? _urlResolver.GetCategoryRoutedUrl(sitePageData.ContentLink, categories.FirstOrDefault()?.ContentLink)
            : null;

        var canonicalLink = _fallbackHandler.GetBest(
            sitePageData.AlternateCanonicalLink,
            sitePageData.ContentLink);
        //Code Added by Asif for Canonical urls on 04/08/2026 Start 

        var webLayoutSettings = _settingsManager.GetSettings<WebLayoutSettings>();
        string canonicalUrl;
        // Preserve existing page level override
        if (!ContentReference.IsNullOrEmpty(sitePageData.AlternateCanonicalLink))
        {
            canonicalUrl = Url.AbsoluteUrl(sitePageData.AlternateCanonicalLink);
        }
        else
        {
            var relativeUrl = _urlResolver.GetUrl(sitePageData.ContentLink);

            if (string.IsNullOrWhiteSpace(relativeUrl))
            {
                canonicalUrl = Url.AbsoluteUrl(sitePageData.ContentLink);
            }
            else
            {
                relativeUrl = relativeUrl.TrimStart('/');

                var baseUrl = webLayoutSettings.CanonicalBaseUrl;
                if (!string.IsNullOrWhiteSpace(baseUrl))
                {
                    var culture = sitePageData.Language?.Name;

                    if (!string.IsNullOrWhiteSpace(culture) &&
                        relativeUrl.StartsWith($"{culture}/", StringComparison.OrdinalIgnoreCase))
                    {
                        relativeUrl = relativeUrl[(culture.Length + 1)..];
                    }

                    canonicalUrl = $"{baseUrl.TrimEnd('/')}/{relativeUrl}";
                }
                else
                {
                    // Existing fallback
                    canonicalUrl = Url.AbsoluteUrl(sitePageData.ContentLink);
                }
            }
        }
        //Code Added by Asif for Canonical urls on 04/08/2026 Start 
        var usingAlternateCanonicalLink = !ContentReference.IsNullOrEmpty(sitePageData.AlternateCanonicalLink);
        //Code Added by Asif for Hreflang urls Start
        var hrefLangRelativeUrl = _urlResolver.GetUrl(sitePageData.ContentLink)?.TrimStart('/') ?? string.Empty;
        var hrefLangCulture = sitePageData.Language?.Name;
        if (!string.IsNullOrWhiteSpace(hrefLangCulture) &&
            hrefLangRelativeUrl.StartsWith($"{hrefLangCulture}/", StringComparison.OrdinalIgnoreCase))
        {
            hrefLangRelativeUrl = hrefLangRelativeUrl[(hrefLangCulture.Length + 1)..];
        }

        var enHrefLangBase = webLayoutSettings.HreflangInitialUrl;
        var arHrefLangBase = webLayoutSettings.HreflangInitialUrlAr;

        var hrefLangEnUrl = !string.IsNullOrWhiteSpace(enHrefLangBase)
            ? $"{enHrefLangBase.TrimEnd('/')}/{hrefLangRelativeUrl}"
            : string.Empty;
        var hrefLangArUrl = !string.IsNullOrWhiteSpace(arHrefLangBase)
            ? $"{arHrefLangBase.TrimEnd('/')}/{hrefLangRelativeUrl}"
            : string.Empty;
        //Code Added by Asif for Hreflang urls End

        var action = GetAction(_httpContextAccessor.HttpContext);

        return new MetaDataViewModel
        {
            Title = GetPageTitle(sitePageData, showSiteTitle),
            Description = description,
            Robots = sitePageData.MetaRobots,
            HasCategoryRouting = sitePageData is ICategoryRoutableContent,
            RelativeCategoryUrl = relativeCategoryUrl,
            ContentLink = sitePageData.ContentLink,
            Action = action,
            CanonicalLink = canonicalLink,
            CanonicalUrl = canonicalUrl,
            HrefLangEnUrl = hrefLangEnUrl,
            HrefLangArUrl = hrefLangArUrl,
            UsingAlternativeCanonicalLink = usingAlternateCanonicalLink,
            RenderAlternativeLinks = sitePageData.RenderAlternativeLinks,
            Categories = GetCategories(sitePageData).ToList(),
            PublishedDateTime = GetFormattedDate(sitePageData.StartPublish),
            ModifiedDateTime = GetFormattedDate(sitePageData.Changed),
            ExpirationDateTime = GetFormattedDate(sitePageData.StopPublish)
        };
    }

    private static string? GetAction(HttpContext requestContext)
    {
        return requestContext.GetRouteValue(RoutingConstants.ActionKey) as string;
    }

    private string GetPageTitle(SitePageData sitePageData, bool showSiteTitle)
    {
        var contentTitle = _fallbackHandler.GetBest(
            sitePageData.MetaTitle,
            sitePageData.ShortPageName,
            sitePageData.Heading,
            sitePageData.Name);

        var webLayoutSettings = _settingsManager.GetSettings<WebLayoutSettings>();

        if (string.IsNullOrWhiteSpace(webLayoutSettings?.SiteName) || !showSiteTitle)
        {
            return contentTitle;
        }

        return webLayoutSettings.PageTitleOrder switch
        {
            PageTitleOrder.PageTitleThenSiteName => $"{contentTitle} | {webLayoutSettings.SiteName}",
            PageTitleOrder.SiteNameThenPageTitle => $"{webLayoutSettings.SiteName} | {contentTitle}",
            _ => contentTitle,
        };
    }

    private IEnumerable<string> GetCategories(SitePageData sitePageData)
    {
        if (sitePageData.Category == null || sitePageData.Category.IsEmpty)
        {
            yield break;
        }

        // TODO: Return categories
        //foreach (var contentReference in sitePageData.Category)
        //{
        //    if (_contentLoader.TryGet<ContentCategory>(contentReference, out var category) &&
        //        !string.IsNullOrWhiteSpace(category.Name))
        //    {
        //        yield return category.Name;
        //    }
        //}

        yield break;
    }

    private static HtmlString? GetFormattedDate(DateTime? dateTime)
    {
        // Returns a datetime string in a format for use by Content Recommendations
        return dateTime.HasValue ? new HtmlString(dateTime.Value.ToString("o")) : null;
    }
}