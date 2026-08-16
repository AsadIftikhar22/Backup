namespace Salam.Cms.Web.Infrastructure.Services;

using EPiServer.Core;
using EPiServer.Framework.Web;
using EPiServer.Security;
using EPiServer.Web;
using Geta.Optimizely.Sitemaps;
using Geta.Optimizely.Sitemaps.Entities;
using Geta.Optimizely.Sitemaps.SpecializedProperties;
using Geta.Optimizely.Sitemaps.Utils;
using Microsoft.Extensions.Logging;
using System;
using System.Reflection;
using System.Security.Principal;

public class SiteContentFilter : ContentFilter
{
    readonly TemplateResolver _templateResolver;
    readonly ILogger<ContentFilter> _logger;

    public SiteContentFilter(TemplateResolver templateResolver, ILogger<ContentFilter> logger)
        : base(templateResolver, logger)
    {
        _templateResolver = templateResolver;
        _logger = logger;
    }

    public override bool ShouldExcludeContent(IContent content)
    {
        if (content == null)
            return true;

        if (!IsAccessibleToEveryone(content))
            return true;

        if (content.IsDeleted)
            return true;

        if (!IsSitemapPropertyEnabled(content))
            return true;

        if (!IsVisibleOnSite(content))
            return true;

        if (content.ContentLink.CompareToIgnoreWorkID(ContentReference.WasteBasket))
            return true;

        if (content is BlockData || content is MediaData)
            return true;

        if (content is PageData page && IsLink(page))
            return true;

        return false;
    }

    public override bool ShouldExcludeContent(CurrentLanguageContent languageContentInfo, SiteDefinition siteSettings, SitemapData sitemapData)
    {
        return ShouldExcludeContent(languageContentInfo.Content);
    }

    private bool IsVisibleOnSite(IContent content)
    {
        return _templateResolver.HasTemplate(content, TemplateTypeCategories.Page);
    }

    private static bool IsLink(PageData page)
    {
        if (page.LinkType != PageShortcutType.External && page.LinkType != PageShortcutType.Shortcut)
        {
            return page.LinkType == PageShortcutType.Inactive;
        }

        return true;
    }

    private static bool IsSitemapPropertyEnabled(IContentData content)
    {
        PropertySEOSitemaps propertySEOSitemaps = content.Property["SEOSitemaps"] as PropertySEOSitemaps;
        if (propertySEOSitemaps == null)
        {
            if (!(content is PageData pageData))
            {
                return true;
            }

            PropertyInfo property = pageData.GetType().GetProperty("SEOSitemaps");
            if (property?.GetValue(pageData) is PropertySEOSitemaps)
            {
                return ((PropertySEOSitemaps)property.GetValue(pageData)).Enabled;
            }
        }

        if (propertySEOSitemaps != null && !propertySEOSitemaps.Enabled)
        {
            return false;
        }

        return true;
    }

    private bool IsAccessibleToEveryone(IContent content)
    {
        try
        {
            if (content is ISecurable securable)
            {
                GenericPrincipal principal = new GenericPrincipal(new GenericIdentity("visitor"), new string[1] { "Everyone" });
                return securable.GetSecurityDescriptor().HasAccess(principal, AccessLevel.Read);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError("Error on content parent " + content.ContentLink.ID + Environment.NewLine + ex);
        }

        return false;
    }
}
