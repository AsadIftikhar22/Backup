namespace Salam.Cms.Web.Features.Cookies.Services;

using Microsoft.AspNetCore.Http;
using System;

public class CookieService : ICookieService
{
    private IHttpContextAccessor _httpContextAccessor;

    public CookieService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public virtual string Get(string cookie)
    {
        if (_httpContextAccessor.HttpContext == null)
        {
            return null;
        }

        return _httpContextAccessor.HttpContext.Request.Cookies[cookie];
    }

    public virtual void Set(string cookie, string value, bool sessionCookie = false)
    {
        if (_httpContextAccessor.HttpContext == null)
        {
            return;
        }

        var options = new CookieOptions()
        {
            HttpOnly = true,
            Secure = _httpContextAccessor.HttpContext.Request.IsHttps
        };

        if (!sessionCookie)
        {
            options.Expires = DateTime.Now.AddYears(1);
        }

        _httpContextAccessor.HttpContext.Response.Cookies.Append(cookie, value, options);
    }

    public virtual void Remove(string cookie)
    {
        if (_httpContextAccessor.HttpContext == null)
        {
            return;
        }

        var options = new CookieOptions()
        {
            HttpOnly = true,
            Secure = _httpContextAccessor.HttpContext.Request.IsHttps,
            Expires = DateTime.Now.AddDays(-1),
        };

        _httpContextAccessor.HttpContext.Response.Cookies.Append(cookie, "", options);
    }
    public virtual void RemoveAllCookies()
    {
        if (_httpContextAccessor.HttpContext == null)
        {
            return;
        }

        var context = _httpContextAccessor.HttpContext;

        foreach (var cookie in context.Request.Cookies.Keys)
        {
            var options = new CookieOptions
            {
                HttpOnly = true,
                Secure = context.Request.IsHttps,
                Expires = DateTime.UtcNow.AddDays(+1),
                Path = "/" // VERY IMPORTANT so that deletion works
            };

            context.Response.Cookies.Append(cookie, "", options);
        }
    }

    public void SetCookieConsent(bool analytics, bool marketing, bool SavePreferences)
    {
        if (_httpContextAccessor.HttpContext == null)
            return;

        var options = new CookieOptions
        {
            HttpOnly = false, // MUST be false so JavaScript can read it
            Secure = _httpContextAccessor.HttpContext.Request.IsHttps,
            Expires = DateTime.Now.AddYears(1)
        };

        //If save preferences is false than store other cookies


        _httpContextAccessor.HttpContext.Response.Cookies.Append("Analytics", analytics.ToString().ToLower(), options);
        _httpContextAccessor.HttpContext.Response.Cookies.Append("Marketing", marketing.ToString().ToLower(), options);
        if (SavePreferences && analytics)
            _httpContextAccessor.HttpContext.Response.Cookies.Append("AllowAnalytics", analytics.ToString().ToLower(), options);
        if (SavePreferences && marketing)
            _httpContextAccessor.HttpContext.Response.Cookies.Append("AllowMarketing", marketing.ToString().ToLower(), options);
        _httpContextAccessor.HttpContext.Response.Cookies.Append("CookiePreference", "true", options);

        if (SavePreferences)
            Remove("CookiePreference");
    }
}