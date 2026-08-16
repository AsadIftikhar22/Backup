// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Onca Online" file="TagManagerViewComponent.cs">
// Copyright (c) Onca Online.  All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Salam.Cms.Web.Features.Common.Components.TagManager;

using EPiServer;
using EPiServer.Cms.Shell;
using EPiServer.Core;
using EPiServer.Web;
using EPiServer.Web.Routing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Salam.Cms.Core.Settings.Abstract;
using Salam.Cms.Web.Features.Common.Interfaces;
using Salam.Cms.Web.Features.Common.Models;
using Salam.Cms.Web.Features.Settings.Models;
using System.Linq;
using System.Text;

public sealed class TagManagerViewComponent : ViewComponent
{
    private readonly IContentLoader _contentLoader;

    private readonly ISettingsManager _settingsManager;

    private readonly IUrlResolver _urlResolver;

    private readonly ISiteDefinitionResolver _siteDefinitionResolver;

    public TagManagerViewComponent(
        IContentLoader contentLoader,
        ISettingsManager settingsManager,
        IUrlResolver urlResolver,
        ISiteDefinitionResolver siteDefinitionResolver)
    {
        _contentLoader = contentLoader;
        _settingsManager = settingsManager;
        _urlResolver = urlResolver;
        _siteDefinitionResolver = siteDefinitionResolver;
    }

    public IViewComponentResult Invoke(ISitePageData? sitePage)
    {
        var webLayoutSettings = _settingsManager.GetSettings<WebLayoutSettings>();

        if (sitePage is not SitePageData concretePage ||
            string.IsNullOrWhiteSpace(webLayoutSettings.TagManagerKey))
        {
            return Content(string.Empty);
        }

        var model = BuildDataLayer(HttpContext, concretePage, webLayoutSettings);

        return View(model);
    }

    private TagManagerViewModel BuildDataLayer(HttpContext httpContext, SitePageData sitePageData, WebLayoutSettings webLayoutSettings)
    {
        return new TagManagerViewModel
        {
            TagManagerKey = webLayoutSettings.TagManagerKey,
            DataLayer = new DataLayer
            {
                // Site host name without the protocol: www.domain.com
                HostName = webLayoutSettings.HostName,

                // Page ancestors id array: [1,2,3]
                PageAncestors = string.Join(",", _contentLoader.GetAncestors(sitePageData.ContentLink).Select(x => x.ContentLink.ID)),

                // Page classification: 'Home', 'Article', 'Listing'
                PageClassification = sitePageData.PageTypeName,

                // The page content type ID
                PageContentTypeId = sitePageData.ContentTypeID.ToString(),

                // The page language in ISO format: 'en' / 'en-gb'
                PageLanguage = sitePageData.LanguageBranch(),

                // The page parent content ID
                PageParentId = sitePageData.ParentLink.ID.ToString(),

                // Site name as used in the page title
                SiteName = webLayoutSettings.SiteName,

                // When the page was request in Unix time.
                RequestedTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(),

                // The title of the page
                PageTitle = sitePageData.Name,

                // The user's group / permission level: 'WebAdmin', 'WebEditor', 'User' or 'none' if none applies / if the user is logged out
                UserAccessLevel = GetUserAccessLevel(sitePageData),

                // User state: 'Logged In' / 'Logged Out'
                UserLoggedInState = httpContext.User.Identity?.IsAuthenticated is true ? "Logged In" : "Logged Out",

                // The full absolute URI for the request
                Uri = $"{webLayoutSettings.AbsoluteURL}{Request.Path}{Request.QueryString}",

                // The relative url
                RelativeUrl = _urlResolver.GetUrl(sitePageData.ContentLink),

                // The absolute url of the page
                AbsoluteUrl = $"{webLayoutSettings.AbsoluteURL + "/"}"
            }
        };
    }

    private string GetSiteAbsoluteUrl(SitePageData sitePageData)
    {
        var siteDefinition = _siteDefinitionResolver.GetByContent(sitePageData.ContentLink, true);
        if (siteDefinition?.SiteUrl is null)
        {
            return string.Empty;
        }

        var uriBuilder = new UriBuilder(siteDefinition.SiteUrl);

        return uriBuilder.Uri.AbsoluteUri;
    }

    private static string GetUserAccessLevel(SitePageData sitePageData)
    {
        try
        {
            return sitePageData.QueryAccess().ToString();
        }
        catch (Exception)
        {
            return "Unknown";
        }
    }
}