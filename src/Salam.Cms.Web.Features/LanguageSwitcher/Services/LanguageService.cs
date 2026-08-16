namespace Salam.Cms.Web.Features.LanguageSwitcher.Services;

using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.Globalization;
using EPiServer.ServiceLocation;
using Salam.Cms.Web.Features.Cookies.Services;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

[ServiceConfiguration]
public class LanguageService : IUpdateCurrentLanguage
{
    private const string LanguageCookie = "Language";
    private readonly ILanguageBranchRepository _languageBranchRepository;
    private readonly ICookieService _cookieService;
    private readonly IUpdateCurrentLanguage _defaultUpdateCurrentLanguage;

    public LanguageService(
        ILanguageBranchRepository languageBranchRepository,
        ICookieService cookieService,
        IUpdateCurrentLanguage defaultUpdateCurrentLanguage)
    {
        _languageBranchRepository = languageBranchRepository;
        _cookieService = cookieService;
        _defaultUpdateCurrentLanguage = defaultUpdateCurrentLanguage;
    }

    public void SetRoutedContent(IContent currentContent, string languageId)
    {
        var chosenLanguage = languageId;
        var cookieLanguage = _cookieService.Get(LanguageCookie);

        if (string.IsNullOrEmpty(chosenLanguage))
        {
            if (cookieLanguage != null)
            {
                chosenLanguage = cookieLanguage;
            }
        }

        _defaultUpdateCurrentLanguage?.SetRoutedContent(currentContent, chosenLanguage);

        if (cookieLanguage == null || cookieLanguage != chosenLanguage)
        {
            _cookieService.Set(LanguageCookie, chosenLanguage);
        }
    }

    public virtual IEnumerable<CultureInfo> GetAvailableLanguages()
    {
        foreach (var language in _languageBranchRepository.ListEnabled())
        {
            var cultureInfo = CultureInfo.GetCultureInfo(language.LanguageID);
            yield return cultureInfo;
        }
    }

    public virtual CultureInfo GetCurrentLanguage()
    {
        return TryGetLanguage(_cookieService.Get(LanguageCookie), out var cultureInfo)
            ? cultureInfo
            : CultureInfo.CurrentUICulture; // TODO: verify if this is correct approach. Might be better to hard code to `en` or `ar`
    }

    private bool TryGetLanguage(string language, out CultureInfo cultureInfo)
    {
        cultureInfo = null;

        if (language == null)
        {
            return false;
        }

        try
        {
            var culture = CultureInfo.GetCultureInfo(language);
            cultureInfo = GetAvailableLanguages().FirstOrDefault(c => c.Name == culture.Name);
            return cultureInfo != null;
        }
        catch (CultureNotFoundException)
        {
            return false;
        }
    }
}
