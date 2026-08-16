namespace Salam.Cms.Web.Infrastructure.Middlewares;

using EPiServer.Web;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Hosting;
using Salam.Cms.Core.Settings.Abstract;
using Salam.Cms.Web.Features.Cards.Components;
using Salam.Cms.Web.Features.RedirectRuleBlock.Models;
using Salam.Cms.Web.Features.Settings.Models;
using System.Linq;
using System.Threading.Tasks;

public class RedirectMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IRedirectRepository _repo;
    private readonly ISettingsManager _settingsManager;
    private readonly IWebHostEnvironment _env;
    public RedirectMiddleware(RequestDelegate next,
                              IRedirectRepository repo,
                              ISettingsManager settingsManager,
                              IWebHostEnvironment env)
    {
        _next = next;
        _repo = repo;
        _settingsManager = settingsManager;
        _env = env;
    }

    public async Task Invoke(HttpContext context)
    {
        var host = context.Request.Host.Host?.ToLowerInvariant() ?? "";
        var path = context.Request.Path.Value?.ToLowerInvariant();
        var pathBase = context.Request.PathBase.Value?.ToLowerInvariant() ?? "";
        var dxpHeader = context.Request.Headers["X-DXP"].ToString()?.ToLowerInvariant() ?? "";
        bool isDxpRequest = dxpHeader.Contains("dxp") || host.Contains("dxp"); // new change here
        Console.WriteLine($"Is DXP REQUEST {isDxpRequest}");
        Console.WriteLine($"Request Path is {path}");
        Console.WriteLine($"X-DXP Header: {dxpHeader}");
        Console.WriteLine($"Reverse Proxy Host is {context.Request.Host}");
        Console.WriteLine($"Reverse Proxy Scheme is {context.Request.Headers["X-Original-Upstream"].ToString()}");
        Console.WriteLine($"Request Before Display URL is {context.Request}");

        bool RedirectRuleDxpSlug = _settingsManager.GetSettings<WebLayoutSettings>().RedirectRuleDxpSlug;

        var requestUrl = context.Request.GetDisplayUrl()
          ?.TrimEnd('/')
          .ToLowerInvariant();

        var RedirectShouldApplyURL = _settingsManager.GetSettings<WebLayoutSettings>().RedirectShouldApplyURL;
        var RedirectShouldNotApplyURL = _settingsManager.GetSettings<WebLayoutSettings>().RedirectShouldNotApplyURL;


        if (RedirectRuleDxpSlug)
        {
            var redirect = _repo.GetAllDXPRepositoriesSlug()?
                    .FirstOrDefault(x =>
                      x.SourceUrl?.TrimEnd('/').ToLowerInvariant() == requestUrl);
            if (redirect != null)
            {
                foreach (var item in RedirectShouldNotApplyURL)
                {
                    if (requestUrl.StartsWith(item) && !String.IsNullOrWhiteSpace(redirect.TargetUrl))
                    {
                        await _next(context);
                        return;

                    }
                }

                foreach(var item in RedirectShouldApplyURL)
                {
                    bool StartWithInteg = requestUrl.StartsWith(item);
                    bool isTargetRedirectURL = !String.IsNullOrWhiteSpace(redirect.TargetUrl);
                    if (StartWithInteg && isTargetRedirectURL)

                    {
                        context.Response.Redirect(redirect.TargetUrl, true);
                        return;

                    }
                }
            }
        }
        else
        {
            var redirect = _repo.GetAll()?
     .FirstOrDefault(x =>
         x.SourceUrl?.TrimEnd('/').ToLowerInvariant() == requestUrl);
            if (redirect != null)
            {

                if (redirect.IsPermanent)
                {
                    context.Response.Redirect(redirect.TargetUrl, true);
                    await _next(context);
                    return;
                }
                if (
                    !string.IsNullOrEmpty(path) && path.StartsWith("/dxp/")
                           || pathBase.StartsWith("/dxp")
                           || dxpHeader.Contains("dxp")
                           && !redirect.IsPermanent

                )
                {
                    //Console.WriteLine("Skipping middleware for /dxp");
                    await _next(context);
                    return;
                }
                if (isDxpRequest)
                {
                    context.Response.Redirect(redirect.TargetUrl, true);
                    await _next(context);
                    return;
                }
                if (!redirect.IsPermanent)
                {
                    context.Response.Redirect(redirect.TargetUrl, true);
                    await _next(context);
                    return;
                }
            }
        }
        await _next(context);
    }
}