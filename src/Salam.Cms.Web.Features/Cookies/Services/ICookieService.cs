namespace Salam.Cms.Web.Features.Cookies.Services;
public interface ICookieService
{
    string Get(string cookie);

    void Set(string cookie, string value, bool sessionCookie = false);

    void Remove(string cookie);

    void SetCookieConsent(bool analytics, bool marketing, bool SavePreferences);

    void RemoveAllCookies();
}
