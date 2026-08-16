namespace Salam.Cms.Web.Features.Common.Components.Navigation;

using EPiServer.Core;
using Salam.Cms.Web.Features.Common.Interfaces;

/// <summary>
/// Extension methods for HTML helpers to assist with navigation item management.
/// </summary>
public static class HtmlHelperExtensions
{
    /// <summary>
    /// Checks if the current page is the active navigation item.
    /// </summary>
    /// <typeparam name="TModel">The type of the model (not directly used but required for extension method context).</typeparam>
    /// <param name="helper">The HTML helper instance.</param>
    /// <param name="currentPage">The current page's content reference. Can be null.</param>
    /// <returns></returns>
    public static bool IsNavigationItemActive(this ISitePageData helper, ContentReference? currentPage)
    {
        if (currentPage == null)
        {
            return false;
        }

        if (helper.ContentLink.CompareToIgnoreWorkID(currentPage))
        {
            return true;
        }

        return false;
    }
}