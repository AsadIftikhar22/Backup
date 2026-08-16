namespace Salam.Cms.Web.Features.Infrastructure;

using EPiServer.DataAbstraction;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.Globalization;
using EPiServer.ServiceLocation;
using EPiServer.Web.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Salam.Cms.Web.Features.Common.Error.Rendering;
using Salam.Cms.Web.Features.Cookies.Services;
using Salam.Cms.Web.Features.EnquireProduct.Services;
using Salam.Cms.Web.Features.LanguageSwitcher.Services;

[InitializableModule]
[ModuleDependency(typeof(EPiServer.Web.InitializationModule))]
public class Initialization : IConfigurableModule
{
    public void ConfigureContainer(ServiceConfigurationContext context)
    {
        // Register ICookieService
        context.Services.AddTransient<ICookieService, CookieService>();
        context.Services.AddScoped<ISessionService, SessionService>();
        context.Services.AddTransient<SearchService>();
        context.ConfigurationComplete += (o, e) =>
        {
            e.Services.Intercept<IUpdateCurrentLanguage>(
            (locator, defaultImplementation) =>
                new LanguageService(
                    locator.GetInstance<ILanguageBranchRepository>(),
                    locator.GetInstance<ICookieService>(),
                    defaultImplementation));

            context.Services.AddTransient<IContentRenderer, ErrorHandlingContentRenderer>();
        };
    }

    public void Initialize(InitializationEngine context)
    {
        // This method is called after ConfigureContainer
    }

    public void Uninitialize(InitializationEngine context)
    {
        // This method is called when the module is uninitialized
    }
}
