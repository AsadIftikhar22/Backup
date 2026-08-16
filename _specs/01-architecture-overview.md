# Architecture Overview

## Introduction

This document outlines the high-level architecture for our composable Optimizely CMS implementation. The architecture is designed to support both coupled (traditional MVC) and decoupled (headless) approaches from the beginning, allowing for flexibility as project requirements evolve.

## Core Architectural Principles

1. **Separation of Concerns**: Clear boundaries between different layers and components of the system.
2. **Domain-Driven Design**: Models and business logic organized around business domains.
3. **Modularity**: Isolated components that can be developed, tested, and deployed independently.
4. **Extensibility**: Support for third-party vendors to contribute models and features without affecting core functionality.
5. **Headless-First**: Design with APIs and content delivery in mind from the beginning.
6. **Forward Compatibility**: Enable smooth evolution of content models without breaking existing content.

## High-Level Architecture

The solution follows a multi-repository approach with several key components:

```
┌─────────────────────────────┐     ┌──────────────────────┐
│  Optimizely CMS Backend     │     │  Next.js Frontend    │
│  (Core platform & models)   │     │  (Headless consumer) │
└───────────┬─────────────────┘     └──────────┬───────────┘
            │                                  │
            ▼                                  ▼
┌─────────────────────────────┐     ┌──────────────────────┐
│  Vendor Extensions          │     │  Additional Frontend │
│  (Models & functionality)   │     │  Applications        │
└─────────────────────────────┘     └──────────────────────┘
```

### Key Components

1. **Optimizely CMS Backend**: Contains the core CMS platform, infrastructure components, and base content models. This is the foundation of the system.

2. **Vendor Extensions**: Separate packages containing vendor-specific models and functionality. These integrate with the core CMS but are independently developed and maintained.

3. **Next.js Frontend**: Decoupled frontend application consuming content via Optimizely's Content Delivery API and/or Graph API.

4. **Additional Frontend Applications**: Optional additional consumers of content (mobile apps, other websites, etc.).

## Communication Patterns

- **Content Delivery**: Content is delivered to frontends via the Optimizely Content Delivery API or Optimizely Graph (GraphQL).
- **Preview Capabilities**: Both coupled and decoupled frontends support content preview via Optimizely's preview mechanisms.
- **Event-Based Communication**: When appropriate, components communicate via events to maintain loose coupling.

## Deployment Model

The solution follows a separate deployment model:

- The CMS Backend is deployed to Optimizely DXP or a similar hosting environment.
- The Next.js Frontend is deployed separately (e.g., Vercel, Netlify, Azure Static Web Apps).
- Vendor Extensions are packaged as NuGet packages and incorporated into the CMS Backend deployment.

This separation allows for independent deployment lifecycles and scaling strategies for each component.

## Evolutionary Architecture

The architecture is designed to evolve over time:

1. Projects can start with either a coupled or decoupled approach.
2. Content models can be gradually refined and extended without breaking existing content.
3. New frontend channels can be added without requiring changes to the content model.
4. Third-party vendors can contribute models and functionality without affecting the core system.

## ASP.NET Core Specifics

The architecture fully embraces ASP.NET Core, leveraging its modern features and patterns:

### Dependency Injection

The solution uses ASP.NET Core's built-in dependency injection container without third-party DI frameworks:

```csharp
// In Program.cs or Startup.cs
public void ConfigureServices(IServiceCollection services)
{
    // Register services with appropriate lifetimes
    services.AddTransient<ISearchService, SearchService>();
    services.AddScoped<IContentLoader, DefaultContentLoader>();
    services.AddSingleton<ISettingsService, SettingsService>();
    
    // Add Optimizely specific services
    services.AddOptimizely(options => 
    {
        // Optimizely-specific options
    });
}
```

Key principles:
- Use constructor injection exclusively for dependencies
- Match service lifetime to use case (transient, scoped, singleton)
- Avoid the service locator pattern and static access to services
- Use `IOptions<T>` pattern for configuration access

### Middleware Pipeline

The solution leverages the ASP.NET Core middleware pipeline for request processing:

```csharp
// In Program.cs or Startup.cs
public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    if (env.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }
    else
    {
        app.UseExceptionHandler("/error");
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();
    app.UseRouting();

    app.UseAuthentication();
    app.UseAuthorization();

    // Optimizely specific middleware
    app.UseOptimizelyAlloy();

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapContent();
        endpoints.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
    });
}
```

### Configuration

The solution uses the standard ASP.NET Core configuration system:

```csharp
// In Program.cs
public static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .ConfigureAppConfiguration((hostingContext, config) =>
        {
            var env = hostingContext.HostingEnvironment;
            
            // Base configuration from appsettings.json
            config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            
            // Environment-specific configuration
            config.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);
            
            // Secrets for development
            if (env.IsDevelopment())
            {
                config.AddUserSecrets<Program>();
            }
            
            // Environment variables (crucial for containerized deployments)
            config.AddEnvironmentVariables();
            
            // Command-line arguments override all
            config.AddCommandLine(args);
        })
        .ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.UseStartup<Startup>();
        });
```

Configuration is accessed through the `IOptions<T>` pattern:

```csharp
public class SearchSettings
{
    public string ApiKey { get; set; }
    public int ResultsPerPage { get; set; } = 10;
}

// In a service
public class SearchService : ISearchService
{
    private readonly SearchSettings _settings;
    
    public SearchService(IOptions<SearchSettings> options)
    {
        _settings = options.Value;
    }
}

// In Startup.cs
services.Configure<SearchSettings>(Configuration.GetSection("Search"));
```

### Background Services

For long-running tasks, the solution leverages ASP.NET Core's background service capabilities:

```csharp
public class ContentIndexingService : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            // Perform indexing work
            await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
        }
    }
}

// In Startup.cs
services.AddHostedService<ContentIndexingService>();
```

### Minimal APIs

For simple endpoints, especially those related to headless delivery, the solution can leverage ASP.NET Core minimal APIs:

```csharp
// In Program.cs
app.MapGet("/api/healthcheck", () => Results.Ok(new { status = "healthy" }));

app.MapGet("/api/settings/{key}", async (string key, ISettingsService settings) =>
    await settings.GetSettingAsync(key) is var setting
        ? Results.Ok(setting)
        : Results.NotFound());
```

### Tag Helpers

For MVC views, the solution leverages ASP.NET Core tag helpers to simplify markup:

```csharp
[HtmlTargetElement("inline-content-svg")]
public class InlineContentSvgTagHelper : TagHelper
{
    private readonly IContentLoader _contentLoader;
    private readonly ILogger<InlineContentSvgTagHelper> _logger;

    public InlineContentSvgTagHelper(
        IContentLoader contentLoader,
        ILogger<InlineContentSvgTagHelper> logger)
    {
        _contentLoader = contentLoader;
        _logger = logger;
    }

    /// <summary>
    /// A filepath to a SVG on disk such as /assets/icon.svg
    /// </summary>
    [HtmlAttributeName("src")]
    public ContentReference? ContentSource { get; set; }

    /// <summary>
    /// A css class to be applied to the svg element
    /// </summary>
    [HtmlAttributeName("class")]
    public string? CssClass { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        if (ContentSource.IsNullOrEmpty())
        {
            // Nothing to render, so don't render anything
            output.SuppressOutput();
            return;
        }

        var cleanContent = GetFileContents(ContentSource);
        if (string.IsNullOrWhiteSpace(cleanContent))
        {
            // Nothing to render, so don't render anything
            output.SuppressOutput();
            return;
        }

        SetOutput(output, cleanContent);
    }

    private string GetFileContents(ContentReference imageReference)
    {
        try
        {
            // SVG fileContents to render to DOM
            if (_contentLoader.TryGet<VectorImageContent>(imageReference, out var vectorImage))
            {
                var xmlDoc = new XmlDocument();
                xmlDoc.Load(vectorImage.BinaryData.OpenRead());

                var fileContents = xmlDoc.InnerXml;

                // Sanitize SVG
                var cleanedFileContents = Regex.Replace(fileContents,
                    @"<script.*?script>",
                    @"",
                    RegexOptions.IgnoreCase | RegexOptions.Singleline,
                    TimeSpan.FromMilliseconds(100));

                cleanedFileContents = Regex.Replace(cleanedFileContents,
                    @"javascript:",
                    @"syntax:error:",
                    RegexOptions.IgnoreCase | RegexOptions.Singleline,
                    TimeSpan.FromMilliseconds(100));

                if (!string.IsNullOrWhiteSpace(CssClass))
                {
                    cleanedFileContents = cleanedFileContents.Replace("<svg", $"<svg class=\"{CssClass}\"");
                }

                return cleanedFileContents;
            }
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Failed to retrieve svg content with id of {ImageReference}", imageReference);
        }

        return string.Empty;
    }

    private static void SetOutput(TagHelperOutput output, string? content)
    {
        output.Attributes.RemoveAll("src");
        output.Attributes.RemoveAll("cache");

        output.TagName = null;
        output.Content.SetHtmlContent(content);
    }
}
```

The next sections provide more detailed specifications for each component of the architecture. 