namespace Salam.Cms.Web.Features.Common.Components.PageNavigator.Components;

using EPiServer;
using EPiServer.Core;
using EPiServer.Web.Mvc;
using EPiServer.Web.Routing;
using Microsoft.AspNetCore.Mvc;
using Salam.Cms.Web.Features.Catalogue.ViewModels;
using Salam.Cms.Web.Features.Common.Components.PageNavigator.ViewModels;
using Salam.Cms.Web.Features.Common.Helpers;
using Salam.Cms.Web.Features.Common.Interfaces;
using Salam.Cms.Web.Features.InternetCardGrid.Models;
using Salam.Cms.Web.Features.TabContainer.Models;
using SixLabors.Fonts.Tables.AdvancedTypographic;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public sealed class PageNavigatorViewComponent : AsyncPartialContentComponent<IPageNavigatorEnabled>
{
    readonly IContentLoader _contentLoader;

    public PageNavigatorViewComponent(IContentLoader contentLoader, IPageRouteHelper pageRouteHelper)
    {
        _contentLoader = contentLoader;
    }

    protected override Task<IViewComponentResult> InvokeComponentAsync(IPageNavigatorEnabled currentPage)
    {
        // Use pattern matching for a slightly cleaner check
        if (currentPage is not IPageNavigatorEnabled { EnablePageNavigator: true } pageNavigatorEnabled)
        {
            return Task.FromResult<IViewComponentResult>(Content(string.Empty));
        }

        // Check if we're in a product detail page with proxy content
        // Not the best solution, but it works for now
        var viewModel = ViewContext.ViewData.Model as ProductDetailPageViewModel;
        var mainContent = viewModel?.ProxyMainContent ?? pageNavigatorEnabled.MainContent;

        if (mainContent?.FilteredItems?.Any() != true)
        {
            return Task.FromResult<IViewComponentResult>(Content(string.Empty));
        }

        var contentItems = _contentLoader.GetItems(
            mainContent.FilteredItems.Select(x => x.ContentLink),
            new LoaderOptions { LanguageLoaderOption.FallbackWithMaster() }
        );

        //var orderedContent = new List<IContent>();

        //foreach (var item in contentItems)
        //{
        //    orderedContent.Add(item);

        //    if (item is InternetCardGridBlock grid &&
        //        grid.TabContainer?.FilteredItems?.Any() == true)
        //    {
        //        orderedContent.AddRange(
        //            grid.TabContainer.FilteredItems
        //                .Select(x => _contentLoader.Get<IContent>(
        //                    x.ContentLink,
        //                    new LoaderOptions { LanguageLoaderOption.FallbackWithMaster() }))
        //        );
        //    }
        //}

        // Extract navigation items with valid titles and anchor IDs
        var navigatorItems = GetValidNavigatorItems(contentItems).ToList();

        if (!navigatorItems.Any())
        {
            return Task.FromResult<IViewComponentResult>(Content(string.Empty));
        }

        var model = new PageNavigatorViewModel
        {
            Items = navigatorItems
        };

        return Task.FromResult<IViewComponentResult>(View(model));
    }

    /// <summary>
    /// Filters content items to valid navigator items with proper titles and anchor IDs.
    /// </summary>
    /// <param name="contentItems">The content items to filter</param>
    /// <returns>A sequence of valid PageNavigatorItemViewModels</returns>
    private static IEnumerable<PageNavigatorViewModel.PageNavigatorItemViewModel> GetValidNavigatorItems(
        IEnumerable<IContent> contentItems)
    {
        return contentItems
            .OfType<IPageNavigatorData>()
            .Select(item => new
            {
                Item = item,
                Title = item.NavigationTitle,
                AnchorId = !string.IsNullOrWhiteSpace(item.NavigationTitle)
                    ? AnchorIdHelper.Generate(item.NavigationTitle)
                    : null
            })
            .Where(x => !string.IsNullOrWhiteSpace(x.Title) && !string.IsNullOrEmpty(x.AnchorId))
            .Select(x => new PageNavigatorViewModel.PageNavigatorItemViewModel(x.Title!, x.AnchorId!));
    }
}