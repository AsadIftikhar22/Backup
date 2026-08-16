using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Html;
using Microsoft.Extensions.Logging;
using Salam.Cms.Core.Services.Caching;
using Salam.Cms.Web.Features.ClientResources.Services;
using System.Globalization;

public class InlineCssService : IInlineCssService
{
    private readonly IWebHostEnvironment _env;
    private readonly ICachingService _cachingService;
    private readonly ILogger<InlineCssService> _logger;

    public InlineCssService(
        IWebHostEnvironment env,
        ICachingService cachingService,
        ILogger<InlineCssService> logger)
    {
        _env = env;
        _cachingService = cachingService;
        _logger = logger;
    }

    public HtmlString LoadInlineCss(string pattern)
    {
        var cacheKey = $"{CacheKeys.InlineCss}_{pattern}_{CultureInfo.CurrentUICulture.Name}";

        var cachedContent = _cachingService.Get<HtmlString>(cacheKey);

        if (cachedContent != null)
            return cachedContent;

        _logger.LogInformation($"Web Root Path: {_env.WebRootPath}");

        var cssDir = Path.Combine(_env.WebRootPath, "static", "assets", "css");

        if (!Directory.Exists(cssDir))
        {
            _logger.LogError("CSS directory not found: {CssDir}", cssDir);
            return new HtmlString(string.Empty);
        }

        var file = Directory.EnumerateFiles(cssDir, pattern, SearchOption.AllDirectories).SingleOrDefault();

        if (file == null)
        {
            _logger.LogWarning("CSS file not found for pattern: {Pattern} in directory: {CssDir}", pattern, cssDir);
            return new HtmlString(string.Empty);
        }

        var content = File.ReadAllText(file);
        var htmlString = new HtmlString(content);

        _cachingService.Add(htmlString, cacheKey, CacheKeys.MasterKeys.InlineCss);

        return htmlString;

    }
}
