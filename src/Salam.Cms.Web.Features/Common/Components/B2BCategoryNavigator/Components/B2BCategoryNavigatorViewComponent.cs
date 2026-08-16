namespace Salam.Cms.Web.Features.Common.Components.B2BCategoryNavigator.Components;

using EPiServer;
using EPiServer.Core;
using EPiServer.Web.Mvc;
using EPiServer.Web.Routing;
using Microsoft.AspNetCore.Mvc;
using Salam.Cms.Web.Features.Catalogue.ViewModels;
using Salam.Cms.Web.Features.Common.Components.B2BCategoryNavigator.ViewModels;
using Salam.Cms.Web.Features.Common.Components.PageNavigator.ViewModels;
using Salam.Cms.Web.Features.Common.Helpers;
using Salam.Cms.Web.Features.Common.Interfaces;
using Salam.Cms.Web.Features.InternetCardGrid.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public sealed class B2BCategoryNavigatorViewComponent : AsyncPartialContentComponent<IPageNavigatorEnabled>
{
    readonly IContentLoader _contentLoader;

    public B2BCategoryNavigatorViewComponent(IContentLoader contentLoader, IPageRouteHelper pageRouteHelper)
    {
        _contentLoader = contentLoader;
    }

    protected override Task<IViewComponentResult> InvokeComponentAsync(IPageNavigatorEnabled currentBlock)
    {
        //// Use pattern matching for a slightly cleaner check
        if (currentBlock is not IPageNavigatorEnabled { EnableCategoryNavigator: true } pageNavigatorEnabled)
        {
            return Task.FromResult<IViewComponentResult>(Content(string.Empty));
        }

        var mainContent = pageNavigatorEnabled.MainContent;

        if (mainContent?.FilteredItems?.Any() != true)
        {
            return Task.FromResult<IViewComponentResult>(Content(string.Empty));
        }
        if (mainContent?.FilteredItems?.Any() != true)
        {
            return Task.FromResult<IViewComponentResult>(Content(string.Empty));
        }

        var contentItems = _contentLoader.GetItems(
            mainContent?.FilteredItems.Select(x => x.ContentLink),
            new LoaderOptions { LanguageLoaderOption.FallbackWithMaster() }
        );

        var orderedContent = new List<IContent>();

        foreach (var item in contentItems)
        {
            //orderedContent.Add(item);

            if (item is InternetCardGridBlock grid &&
                grid.TabContainer?.FilteredItems?.Any() == true)
            {
                orderedContent.AddRange(
                    grid.TabContainer.FilteredItems
                        .Select(x => _contentLoader.Get<IContent>(
                            x.ContentLink,
                            new LoaderOptions { LanguageLoaderOption.FallbackWithMaster() }))
                );
            }
        }
        // Extract navigation items with valid titles and anchor IDs
        var navigatorItems = GetValidNavigatorItems(orderedContent).ToList();

        if (!navigatorItems.Any())
        {
            return Task.FromResult<IViewComponentResult>(Content(string.Empty));
        }

        var model = new B2BCategoryNavigatorViewModel
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
    private static IEnumerable<B2BCategoryNavigatorViewModel.B2BCategoryNavigatorItemViewModel> GetValidNavigatorItems(
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
            .Select(x => new B2BCategoryNavigatorViewModel.B2BCategoryNavigatorItemViewModel(x.Title!, x.AnchorId!));
    }
}