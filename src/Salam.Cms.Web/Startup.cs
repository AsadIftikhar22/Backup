namespace Salam.Cms.Web;

using EPiServer.DependencyInjection;
using EPiServer.Web;
using EPiServer.Web.Routing;
using Geta.Optimizely.Categories.Configuration;
using Geta.Optimizely.Categories.Find.Infrastructure.Initialization;
using Geta.Optimizely.Categories.Infrastructure.Initialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;
using Salam.Cms.Core.Services.Infrastructure;
using Salam.Cms.Core.Settings;
using Salam.Cms.Core.Settings.Configuration;
using Salam.Cms.Plugin.ApiExplorer;
using Salam.Cms.Plugin.ApiExplorer.Models;
using Salam.Cms.Web.Features.Infrastructure;
using Salam.Cms.Web.Features.RedirectRuleBlock.Models;
using Salam.Cms.Web.Features.SelectedProductEnquire.Services;
using Salam.Cms.Web.Infrastructure.Middlewares;
using Salam.Cms.Web.Infrastructure.Rendering;
using Salam.Cms.Web.Infrastructure.ServiceExtensions;
using Verndale.RedirectManager.Infrastructure.Configuration;

public sealed class Startup
{
    private readonly IWebHostEnvironment _webHostingEnvironment;
    private readonly IConfiguration _configuration;

    public Startup(
        IWebHostEnvironment webHostingEnvironment,
        IConfiguration configuration)
    {
        _webHostingEnvironment = webHostingEnvironment;
        _configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.Configure<CorsSettings>(_configuration.GetSection("Cors"));
        services.Configure<DAMSettings>(_configuration.GetSection("DAM"));
        services.Configure<CatalogueApiSettings>(_configuration.GetSection("CatalogueApiSettings"));
        services.Configure<ImageProxySettings>(_configuration.GetSection("ImageProxy"));
        services.Configure<ImageHandlingSettings>(_configuration.GetSection("ImageHandling"));
        services.Configure<EnvironmentSettings>(_configuration.GetSection("Environment"));
        services.Configure<ContentDeliveryApiSettings>(_configuration.GetSection("ContentDeliveryApi"));

        //services.AddRedirectManager(
        //    addQuickNavigator: true,
        //    enableChangeEvent: true);

        services.Configure<RouteOptions>(options =>
        {
            options.LowercaseUrls = true;
            options.AppendTrailingSlash = true;
        });

        services.AddSendGrid(_configuration);

        services.AddCmsLogins(_webHostingEnvironment, _configuration, useOptiId: true)
                .AddCms()
                .AddCmsCmpPublishing()
                .AddOdpSupport()
                .AddCmsEnvironmentConfiguration(_webHostingEnvironment, _configuration)
                .AddEmbeddedLocalization<Startup>()
                .AddFind()
                .AddHealthChecks();
        // Allow the quick editor to be embedded into CMP via an iframe
        services.AddAntiforgery(options =>
        {
            options.Cookie.SameSite = SameSiteMode.None;
            options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        });

        //EPiServer.Cms.WelcomeIntegration.UI.Components.DAMImageAssetViewComponent;
        // EPiServer.Cms.WelcomeIntegration.UI.Helpers.DAMAssetTagHelper;
        // EPiServer.Cms.WelcomeIntegration.UI.Helpers.HtmlHelpers;
        // EPiServer.Cms.WelcomeIntegration.UI.Helpers.DAMAssetHtmlContentService
        // IDAMAssetMetadataService
        // Optimizely.Cmp.Client.ICmpAssetClient;

        // Configure routing options for strict language routing
        services.Configure<RoutingOptions>(o =>
        {
            o.StrictLanguageRouting = true;
        });

        // Add custom configuration
        services.AddFileUploadLimits()
                .AddRedirectManagerHandler()
                .AddCustomDependencies()
                .AddGetaCategories()
                 .AddGetaNotFoundHandler(_configuration)
                .AddGetaOptimizelySitemapsHandler()
                .AddGetaContentTypeIcons(_webHostingEnvironment)
                .AddRobotsTextHandler()
                .AddHeadlessForms(_configuration)
                .AddSecurityAdmin()
                .AddOptimizelyContentDeliveryApi()
                .AddContentGraph()
                .AddCmsAdvancedReviews()
                .AddHttpLogging()
                .AddContentSecurityPolicyNonce()
                .AddOptimizelyDAM()
                .AddCoreServicesDependencies()
                .AddFeatureComponents();

        services.AddSalamSettings();
        services.AddApiExplorer(options =>
        {
        });

        services.AddScoped<IRedirectRepository, RedirectRepository>();
        services.AddDistributedMemoryCache();
        // Register session services
        services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromMinutes(30); // adjust if needed
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;  // allows session without user consent
        });


        services.AddHttpContextAccessor();
        services.AddScoped<EnquireProductRequestService>();
        services.Configure<RazorViewEngineOptions>(options => options.ViewLocationExpanders.Add(new SiteViewEngineLocationExpander()));
        //services.Configure<ForwardedHeadersOptions>(options =>
        //{
        //    options.ForwardedHeaders =
        //        ForwardedHeaders.XForwardedFor |
        //        ForwardedHeaders.XForwardedProto |
        //        ForwardedHeaders.XForwardedHost;
        //});

        services.Configure<UrlSegmentOptions>(options =>
        {
            options.SupportIriCharacters = true;
            options.ValidCharacters = @"\p{L}0-9\-_~\.\$";
        });

    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        //if (!env.IsDevelopment())
        //{
        //var rewrite = new RewriteOptions();
        //using (StreamReader fileStreamReader = new StreamReader("wwwroot/web.config"))
        //{
        //    rewrite = rewrite.AddIISUrlRewrite(fileStreamReader);
        //}

        //app.UseRewriter(rewrite);
        //}

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/server-error");
        }

        app.UseStatusCodePagesWithReExecute("/error", "?statusCode={0}");
        //app.UseExceptionHandler("/Error/500");
        // Geta Not Found Handler should be as early in the pipeline as possible, but after the error handlers and cookies.
        app.UseGetaNotFoundHandler();

        // Apply custom rewrite rules after Geta Not Found Handler has potentially performed a redirect
        // app.UseCustomRewriteRules();

        // app.UseOptimizelyImageSharp();

        app.UseCachedStaticFiles();
        // app.UseMiddleware<FindTelemetryMiddleware>();

        app.UseRouting();
        //app.UseRedirectManagerHandler();
        //app.UseForwardedHeaders();
        app.UseMiddleware<RedirectMiddleware>();

        app.UseSession();
        app.UseCorsConfiguration();

        app.UseCmsCmpLogins(env);

        //Support for Viewing the content which is going to be published on CMS from the CMP.
        app.UseCmsCmpPublishingPreviewLinks();

        app.UseSecurityAdmin();

        app.UseHttpLogging();

        app.UseGetaCategories();
        app.UseGetaCategoriesFind();
        app.UseGetaContentTypeIcons(env);

        app.UseSalamSettings();
        app.UseApiExplorer(
            mapEndpoints: true,
            null,
            new SwaggerEndpointModel() { Url = "/_forms/v1/docs/openapi.json", Name = "Optimizely Headless Form API V1" });
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapContent();
            endpoints.MapRazorPages();
        });
    }
}

