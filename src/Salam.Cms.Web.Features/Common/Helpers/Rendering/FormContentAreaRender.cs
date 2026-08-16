namespace Salam.Cms.Web.Features.Common.Helpers.Rendering;

using EPiServer;
using EPiServer.Core;
using EPiServer.ServiceLocation;
using EPiServer.Web.Mvc.Html;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

[ServiceConfiguration(ServiceType = typeof(FormContentAreaRender), Lifecycle = ServiceInstanceScope.Singleton)]
public class FormContentAreaRender : ContentAreaRenderer
{
    private IContent _currentContent;

    public string GetColumnWidth(HtmlHelper html, ContentAreaItem item)
    {
        var tag = GetContentAreaItemTemplateTag(html, item);
        return tag;
    }

    /// Get css of a content area item
    public string GetItemCssClass(HtmlHelper html, ContentAreaItem areaItem)
    {
        var tag = GetContentAreaItemTemplateTag(html, areaItem);
        var baseClasses = base.GetContentAreaItemCssClass(html, areaItem);
        return $"block {GetTypeSpecificCssClasses(areaItem)} {tag} {baseClasses}";
    }


    private string GetTypeSpecificCssClasses(ContentAreaItem contentAreaItem)
    {
        var content = GetCurrentContent(contentAreaItem);
        var cssClass = content?.GetOriginalType().Name.ToLowerInvariant() ?? string.Empty;
        var customClassContent = content as ICustomCssInContentArea;
        if (customClassContent != null && !string.IsNullOrWhiteSpace(customClassContent.ContentAreaCssClass))
        {
            cssClass += $" {customClassContent.ContentAreaCssClass}";
        }
        return cssClass;
    }

    private IContent GetCurrentContent(ContentAreaItem contentAreaItem)
    {
        if (_currentContent == null || !_currentContent.ContentLink.CompareToIgnoreWorkID(contentAreaItem.ContentLink))
        {
            _currentContent = contentAreaItem?.GetContent();
        }
        return _currentContent;
    }
}