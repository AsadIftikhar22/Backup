using EPiServer.Core;
using EPiServer.Web.Routing;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Salam.Cms.Web.Features.Common.Extensions;
using Salam.Cms.Web.Features.LanguageSwitcher.Services;
using System.Globalization;

namespace Salam.Cms.Web.Features.LanguageSwitcher.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LanguageController : ControllerBase
    {
        private readonly LanguageService _languageService;
        private readonly UrlResolver _urlResolver;
        private readonly IContentRouteHelper _contentRouteHelper;

        public LanguageController(LanguageService languageService, UrlResolver urlResolver, IContentRouteHelper contentRouteHelper)

        {
            _languageService = languageService;
            _urlResolver = urlResolver;
            _contentRouteHelper = contentRouteHelper;
        }

        [HttpPost]
        [Route("Set")]
        [ValidateAntiForgeryToken]
        public ActionResult Set([FromForm] string language, ContentReference contentLink)
        {
            // validate that language is a valid culture
            if (!IsValidCulture(language))
            {
                return BadRequest("Invalid language specified.");
            }

            _languageService.SetRoutedContent(_contentRouteHelper.Content, language);

            var returnUrl = _urlResolver.GetUrl(Request, contentLink, language);
            return new ContentResult
            {
                Content = JsonConvert.SerializeObject(new { returnUrl }),
                ContentType = "application/json",
            };
        }

        private static bool IsValidCulture(string cultureCode)
        {
            if (string.IsNullOrWhiteSpace(cultureCode))
                return false;

            try
            {
                // This will validate both "en" and "en-US"
                CultureInfo culture = CultureInfo.GetCultureInfo(cultureCode);
                return true;
            }
            catch (CultureNotFoundException)
            {
                return false;
            }
        }
    }
}
