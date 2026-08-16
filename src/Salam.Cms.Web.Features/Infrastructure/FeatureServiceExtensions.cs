namespace Salam.Cms.Web.Features.Infrastructure;

using EPiServer.Forms.Core.Internal;
using EPiServer.Web.Internal;
using Microsoft.Extensions.DependencyInjection;
using Salam.Cms.Core.Services.Caching;
using Salam.Cms.Web.API.Services;
using Salam.Cms.Web.Features.ClientResources.Services;
using Salam.Cms.Web.Features.Common.Components.Images.ImageRenderer;
using Salam.Cms.Web.Features.Common.Components.Images.PictureRenderer;
using Salam.Cms.Web.Features.Common.Components.Navigation;
using Salam.Cms.Web.Features.Embed.Abstract;
using Salam.Cms.Web.Features.Embed.ViewModels;
using Salam.Cms.Web.Features.Forms.Services;
using Salam.Cms.Web.Features.NotFound.ViewModels;
using Salam.Cms.Web.Infrastructure.Forms.Services;
using Salam.CMS.Web.Data;

public static class FeatureServiceExtensions
{
    public static IServiceCollection AddFeatureComponents(this IServiceCollection serviceCollection)
    {
        // Page Model Builders
        serviceCollection.AddTransient<INotFoundViewModelBuilder, NotFoundViewModelBuilder>();
        serviceCollection.AddTransient<IEmbedPageViewModelBuilder, EmbedPageViewModelBuilder>();

        // Component Model Builders
        serviceCollection.AddTransient<INavigationViewModelBuilder, NavigationViewModelBuilder>();
        //serviceCollection.AddTransient<IBreadcrumbViewModelBuilder, BreadcrumbViewModelBuilder>();
        //serviceCollection.AddTransient<IFooterMenuViewModelBuilder, FooterMenuViewModelBuilder>();
        serviceCollection.AddTransient<IImageViewModelBuilder, ImageRendererViewModelBuilder>();
        serviceCollection.AddTransient<IPictureRendererViewModelBuilder, PictureRendererViewModelBuilder>();

        // Helpers

        // Services
        serviceCollection.AddTransient<ICachingService, CachingService>();
        serviceCollection.AddScoped<IClientResourceRegistrationService, ClientResourceRegistrationService>();
        //Wrapper for RestClient
        serviceCollection.AddTransient<ProtectionApiWrapper>();
        //Centralized Header and Footer
        serviceCollection.AddTransient<IInlineCssService, InlineCssService>();
        serviceCollection.AddTransient<IWebLayoutSettingsRepo, WebLayoutSettingsImplementation>();
        serviceCollection.AddScoped<FraudComplaintService>();
        //serviceCollection.AddTransient<B2BFormDataSubmissionService>();
        //serviceCollection.AddSingleton<ComplaintFormDataSubmissionService>();
        serviceCollection.AddSingleton<ProtectorChannelFormDataSubmissionService>();
        serviceCollection.AddScoped<DataSubmissionService, ProtectorChannelFormDataSubmissionService>();
        //serviceCollection.AddSingleton<DataSubmissionServiceFactory>();
        serviceCollection.AddTransient<QueryParameterResolver>();
        return serviceCollection;
    }
}