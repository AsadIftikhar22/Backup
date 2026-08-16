# Salam CMS 12

This repository contains a build based on Optimizely CMS 12.32.2, implementing a feature-based architecture.

## Dependencies

### Optimizely Packages

| Package | Usage |
|---------|-------|
| EPiServer.CMS | Core Optimizely CMS functionality |
| EPiServer.CMS.AspNetCore.* | ASP.NET Core integration components |
| EPiServer.CMS.TinyMce | Rich text editor functionality |
| EPiServer.CMS.UI.Core | CMS UI components |
| EPiServer.Find.Cms | Content indexing and search functionality |
| EPiServer.Forms | Base forms functionality |
| EPiServer.ContentDeliveryApi.Cms | Provides readonly content consumption APIs |
| EPiServer.CloudPlatform.Cms | Cloud platform integration |
| EPiServer.Azure | Azure integration |
| EPiServer.Hosting | Hosting infrastructure |
| EPiServer.OptimizelyIdentity | Identity management |
| Optimizely.Cms.Forms.Service | Forms service functionality |
| Optimizely.ContentGraph.Cms | Content Graph integration |
| Optimizely.Cms.Cmp.Publishing | Publishing integration |

### Third-Party Addons

| Package | Usage |
|---------|-------|
| Geta.NotFoundHandler.Optimizely | Provides 301/302 redirect management functionality |
| Geta.Optimizely.Categories(.Find) | Provides extensions and UI for improved Category functionality |
| Geta.Optimizely.ContentTypeIcons | Provides customisable UI Icons and tiles |
| Geta.Optimizely.Sitemaps | Provides XML Sitemap functionality |
| PictureRenderer.Optimizely | Provides responsive image handling |
| Stott.Optimizely.RobotsHandler | Provides customizable robots.txt management |
| Stott.Security.Optimizely | Provides CSP and Security Header management |
| Advanced.CMS.AdvancedReviews | Provides content review functionality |
| OpenIddict.Server | Provides OpenID Connect server functionality |

### Development Tools

| Package | Usage |
|---------|-------|
| Serilog | Logging framework |
| Swashbuckle.AspNetCore | API documentation |
| Flurl | HTTP client |
| HtmlAgilityPack | HTML parsing |
| HtmlSanitizer | HTML sanitization |
| Humanizer.Core | String manipulation |
| ImagePointEditor | Image editing |
| System.Linq.Async | Async LINQ support |

## Project Structure

| Project Name | Responsibilities |
|--------------|------------------|
| Salam.Cms.Web | Main web application (MVC) - hosts the Optimizely CMS UI and content delivery |
| Salam.Cms.Web.Infrastructure | Web-specific infrastructure components including routing configurations, middleware, and CMS extensions |
| Salam.Cms.Web.Features | Feature-based organization of web components following domain-driven design principles |
| Salam.Cms.Shared.Models | Common models used across all implementations and content types consumed by multiple projects |
| Salam.Cms.Core.Settings | Configuration models and settings for CMS and application configuration |
| Salam.Cms.Core.Services | Core services implementation handling business logic isolated from presentation concerns |
| Salam.Cms.Api.Extensions | API extensions and utilities for headless content delivery |
| Salam.Cms.UnitTests | Unit tests focused on testing individual components in isolation |
| Salam.Cms.IntegrationTests | Integration tests for testing component interactions |
| Salam.Cms.Tests | Common test utilities shared across test projects |

## Development Guidelines

### Nullable Reference Types
The solution uses .NET 8.0 nullable reference types pattern:
```csharp
public class Example
{
    public string? NullableString { get; set; }
    public string NonNullableString { get; set; } = string.Empty;
    public int NonNullableInt { get; set; }
}
```

### Page Models
All page models inherit from `SitePageViewModel<TContent>` and implement `ISitePageViewModel<out TContent>`:
```csharp
public class HomePageViewModel : SitePageViewModel<HomePage>, ISitePageViewModel<HomePage>
{
    public HomePageViewModel(HomePage currentPage) : base(currentPage) { }
}
```

If there is no custom data loading required to serve the page, then the controller action can be as simple as:

```csharp
public class HomePageController : PageControllerBase<HomePage>
{
    public IActionResult Index(HomePage currentPage)
    {
        var model = new HomePageViewModel(currentPage);
        return View(model);
    }
}
```

### Page Model Builders

If your page requires additional data in the view model, use a view model builder that inherits from `SitePageViewModelBuilder<TContent, TModel>`:

```csharp
public class SearchPageViewModelBuilder : SitePageViewModelBuilder<SearchPage, SearchPageViewModel>, ISearchPageViewModelBuilder
{
    private readonly ISearchService _searchService;
    private string _searchText = string.Empty;
    private int _skip;
    private int _take;

    public SearchPageViewModelBuilder(ISearchService searchService)
    {
        _searchService = searchService;
    }

    public ISearchPageViewModelBuilder WithSearchCriteria(string searchText, int skip, int take)
    {
        _searchText = searchText;
        _skip = skip;
        _take = take;
        return this;
    }

    public override SearchPageViewModel Build()
    {
        var skip = _skip < 0 ? 0 : _skip;
        var take = _take <= 0 ? 10 : _take;
        Model.SearchResult = _searchService.Search(_searchText, skip, take);
        return Model;
    }
}
```

### Asynchronous Controllers and Model Builders

For services that can execute asynchronously, ensure your controllers and model builders support asynchronous execution:

```csharp
public class HomePageController : PageControllerBase<HomePage>
{
    private readonly IAsyncHomePageViewModelBuilder _asyncViewModelBuilder;

    public HomePageController(IAsyncHomePageViewModelBuilder asyncViewModelBuilder)
    {
        _asyncViewModelBuilder = asyncViewModelBuilder;
    }

    public async Task<IActionResult> IndexAsync(HomePage currentPage)
    {
        var model = await _asyncViewModelBuilder.WithContent(currentPage).BuildAsync();
        return View(model);
    }
}
```

Your model builder should inherit from `AsyncSitePageViewModelBuilder<TContent, TModel>` and implement `IAsyncSitePageViewModelBuilder<TContent, TModel>`.

### Common Components

Common components like Headers and Footers are implemented as `ViewComponent`s:

```csharp
public class HeaderMenuViewComponent : ViewComponent
{
    private readonly IHeaderViewModelBuilder _viewModelBuilder;

    public HeaderMenuViewComponent(IHeaderViewModelBuilder viewModelBuilder)
    {
        _viewModelBuilder = viewModelBuilder;
    }

    public IViewComponentResult Invoke(ISitePageData sitePage)
    {
        var model = _viewModelBuilder.WithCurrentPage(sitePage).Build();
        return View(model);
    }
}
```

Usage in page templates:
```html
<header>
    @await Component.InvokeAsync(typeof(HeaderMenuViewComponent), new { sitePage = Model.CurrentPage })
</header>
```

## Configuration

### DXP Configuration
- Configuration in `appsettings.json`
- Sensitive settings stored in Azure App Service
- Development settings in `appsettings.Development.json`

### Local Development

#### Database
The database connection string should be named "EPiServerDB":

```json
{
    "ConnectionStrings": {
        "EPiServerDB": "Server=server-name;Database=database-name;..."
    }
}
```

#### Blob Storage
For local development, configure blob storage to use a network share instead of the default App_Data folder:

```json
{
    "EPiServer": {
        "Cms": {
            "BlobProvidersOptions": {
                "DefaultProvider": "fileShare",
                "Providers": {
                    "fileShare": "EPiServer.Framework.Blobs.FileBlobProvider, EPiServer.Framework"
                }
            },
            "FileBlobProvider": {
                "Path": "\\\\server-name\\file-store-folder\\App_Data\\"
            }
        }
    }
}
```

#### Optimizely Search & Navigation
Optimizely provide indexes for all of the Optimizely DXP instances; however these indexes are not to be used on internal development systems.  Developers can request temporary indexes for use with development environments.  Turn around of these is near instant and is requested by visiting the [Optimizely Find](https://find.episerver.com/) developer demos site.  It should be noted that these indexes are limited in size (approx 30,000 documents with a maximum of two languages for stemming) and expire after 30 days.

Configure Optimizely Find for development environments:

```json
{
    "EPiServer": {
        "Find": {
            "ServiceUrl": "https://find.episerver.com/...",
            "DefaultIndex": "your-index-name"
        }
    }
}
```

## Architecture

For detailed architecture information, see the following documents:

- [Architecture Overview](.specs/01-architecture-overview.md) - High-level architecture and core principles
- [Project Structure](.specs/02-project-structure.md) - Repository and project organization 
- [Content Modeling](.specs/03-content-modeling.md) - Domain-driven content modeling approach
- [Headless Integration](.specs/04-headless-integration.md) - Headless delivery and Next.js integration
- [Vendor Extensions](.specs/05-vendor-extensions.md) - Third-party vendor contribution guidelines
- [Governance](.specs/06-governance.md) - Content model governance processes
- [Testing Strategy](.specs/07-testing-strategy.md) - Comprehensive testing approach
- [Development Workflow](.specs/08-development-workflow.md) - Development tools and practices
