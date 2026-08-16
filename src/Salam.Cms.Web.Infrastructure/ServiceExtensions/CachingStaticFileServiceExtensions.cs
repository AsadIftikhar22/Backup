namespace Salam.Cms.Web.Infrastructure.ServiceExtensions;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.StaticFiles;
using System;
using System.Net.Http.Headers;

public static class CachingStaticFileServiceExtensions
{
    public static IApplicationBuilder UseCachedStaticFiles(this IApplicationBuilder app)
    {
        var cacheHeaderValue = new CacheControlHeaderValue
        {
            Public = true,
            MaxAge = TimeSpan.FromDays(365)
        }.ToString();

        app.UseStaticFiles(new StaticFileOptions
        {
            ContentTypeProvider = new FileExtensionContentTypeProvider
            {
                Mappings ={
                    [".txt"] = "text/plain; charset=utf-8"
                }
            },
            OnPrepareResponse = ctx =>
            {
                ctx.Context.Response.Headers.CacheControl = cacheHeaderValue;
            }
        });

        return app;
    }
}