using EPiServer.ContentApi.Core.Configuration;
using EPiServer.ContentApi.Core.Internal;
using EPiServer.ContentApi.Core.Serialization;
using EPiServer.ContentApi.Core.Serialization.Internal;
using EPiServer.Globalization;
using EPiServer.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Salam.Cms.Core.Settings.Configuration;
using Salam.Cms.Core.Settings.Extensions;
using Salam.Cms.Core.Settings.Helpers;
using Salam.Cms.Core.Settings.Infrastructure;
using Salam.Cms.Core.Settings.Models;
using System.Globalization;
using ApiIContextModeResolver = EPiServer.ContentApi.Core.Internal.IContextModeResolver;

namespace Salam.Cms.Core.Settings.Controllers
{
    [ApiController]
    [Route("api/salam/v1.0/settings")]
    [ContentLanguageFilter]
    public class SettingsApiController : Controller
    {
        protected virtual ISettingsService SettingsService { get; }
        protected virtual ISiteDefinitionResolver SiteDefinitionResolver { get; }
        protected virtual ISiteDefinitionRepository SiteDefinitionRepository { get; }
        protected virtual SiteDefinition CurrentSite => SiteDefinitionResolver.GetByHostname(Request.Host.Host, false, out var hostname);
        protected virtual ApiIContextModeResolver ContextModeResolver { get; set; }
        protected virtual ContentApiOptions ContentApiOptions { get; }
        protected virtual ContentConvertingService ContentConvertingService { get; }
        protected virtual ContentDeliveryApiSettings? ContentDeliveryApiSettings { get; }
        protected virtual EnvironmentSettings? EnvironmentSettings { get; }

        public SettingsApiController(
            ISettingsService settingsService,
            ISiteDefinitionResolver siteDefinitionResolver,
            ISiteDefinitionRepository siteDefinitionRepository,
            ContentApiOptions contentApiOptions,
            ApiIContextModeResolver contextModeResolver,
            ContentConvertingService contentConvertingService,
            IOptions<ContentDeliveryApiSettings> contentDeliveryApiSettings,
            IOptions<EnvironmentSettings> environmentSettings
        )
        {
            SettingsService = settingsService;
            SiteDefinitionResolver = siteDefinitionResolver;
            SiteDefinitionRepository = siteDefinitionRepository;
            ContentApiOptions = contentApiOptions;
            ContextModeResolver = contextModeResolver;
            ContentConvertingService = contentConvertingService;
            ContentDeliveryApiSettings = contentDeliveryApiSettings.Value;
            EnvironmentSettings = environmentSettings.Value;
        }

        [HttpGet]
        [Route("default")]
        [ProducesResponseType(typeof(SettingsApiResponse<SettingsGroupsResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SettingsApiResponse<object>), StatusCodes.Status404NotFound)]
        public ActionResult<SettingsApiResponse<SettingsGroupsResponse>> GetSettingsGroups(
            [FromHeader(Name = "Accept-Language")] List<string> languages
        )
        {
            var language = GetLanguage(languages);
            var currentSite = CurrentSite;
            if (currentSite == null)
                return NotFound(new SettingsApiResponse<object> { Error = true, Message = "The current website could not be resolved automatically" });
            return Ok(new SettingsApiResponse<SettingsGroupsResponse> { Data = GetSettingsGroups(currentSite) });
        }

        [HttpGet]
        [Route("{siteId}")]
        [ProducesResponseType(typeof(SettingsApiResponse<SettingsGroupsResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SettingsApiResponse<object>), StatusCodes.Status404NotFound)]
        public ActionResult<SettingsApiResponse<SettingsGroupsResponse>> GetSettingsGroups(
            Guid siteId,
            [FromHeader(Name = "Accept-Language")] List<string> languages
        )
        {
            var language = GetLanguage(languages);
            var currentSite = SiteDefinitionRepository.Get(siteId);
            if (currentSite == null)
                return NotFound(new SettingsApiResponse<object> { Error = true, Message = $"The website with id {siteId.ToString()} could not be found" });
            return Ok(new SettingsApiResponse<SettingsGroupsResponse> { Data = GetSettingsGroups(currentSite) });
        }

        [HttpGet]
        [Route("default/{settingsGroup}")]
        [ProducesResponseType(typeof(SettingsApiResponse<SettingsGroupResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SettingsApiResponse<object>), StatusCodes.Status404NotFound)]
        public IActionResult GetSettings(
            string settingsGroup,
            [FromHeader(Name = "Accept-Language")] List<string> languages
        )
        {
            var language = GetLanguage(languages);
            var currentSite = CurrentSite;
            if (currentSite == null)
                return NotFound(new SettingsApiResponse<object> { Error = true, Message = $"The current website could not be resolved automatically" });

            return GetSettings(currentSite, settingsGroup, language);
        }

        [HttpGet]
        [Route("{siteId}/{settingsGroup}")]
        [ProducesResponseType(typeof(SettingsApiResponse<SettingsGroupResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SettingsApiResponse<object>), StatusCodes.Status404NotFound)]
        public IActionResult GetSettings(
            Guid siteId,
            string settingsGroup,
            [FromHeader(Name = "Accept-Language")] List<string> languages
        )
        {
            var language = GetLanguage(languages);
            var currentSite = SiteDefinitionRepository.Get(siteId);
            if (currentSite == null)
                return NotFound(new SettingsApiResponse<object> { Error = true, Message = $"The website with id {siteId.ToString()} could not be found" });

            return GetSettings(currentSite, settingsGroup, language);
        }

        /// TODO
        //[HttpGet]
        //[Route("category")]
        //[ProducesResponseType(typeof(SettingsApiResponse<List<CategoryContainerResponse>>), StatusCodes.Status200OK)]
        //[ProducesResponseType(typeof(SettingsApiResponse<object>), StatusCodes.Status404NotFound)]
        //public IActionResult GetCategoryDetailed(
        //    int pageId,
        //    string propertyName,
        //    [FromHeader(Name = "Accept-Language")] List<string> languages
        //)
        //{
        //    var language = GetLanguage(languages);
        //    var nestedCategoryList = SettingsService.GetPropertyNestedCategories(pageId, propertyName, language);

        //    if (!nestedCategoryList.Any())
        //    {
        //        return NotFound(new SettingsApiResponse<object> { Error = true, Message = $"There are no categories for the given page and property." });
        //    }

        //    return Ok(new SettingsApiResponse<object> { Error = false, Data = nestedCategoryList });
        //}

        [NonAction]
        protected virtual IActionResult GetSettings(SiteDefinition site, string settingsGroup, CultureInfo? language)
        {
            var dict = SettingsService.GetSiteSettings(site.Id);
            if (dict == null)
                return NotFound(new SettingsApiResponse<object> { Error = true, Message = $"There are no settings for this website" });
            var hits = dict.Keys.Count(key => string.Equals(key.Name, settingsGroup, StringComparison.OrdinalIgnoreCase));
            if (hits == 0)
                return NotFound(new SettingsApiResponse<object> { Error = true, Message = $"There is no settings group with the name {settingsGroup} found" });
            if (hits > 1)
                return NotFound(new SettingsApiResponse<object> { Error = true, Message = $"There are multiple settings groups with the name {settingsGroup} found" });

            var group = dict.First(kvp => string.Equals(kvp.Key.Name, settingsGroup, StringComparison.OrdinalIgnoreCase)).Value;

            // Leaveing this here as an example of how to apply custom options to the content api as we might need to update the options in the future
            // var overrideContentApiOptions = ContentApiOptions;
            // overrideContentApiOptions.FlattenPropertyModel = true;
            var context = new ConverterContext(
                group.ContentLink, language ?? ContentLanguage.PreferredCulture,
                ContentApiOptions, // overrideContentApiOptions
                ContextMode.Default,
                string.Empty,
                "*",
                true);
            if (ContentConvertingService.TryConvert(group, context, out var contentModel))
            {
                if (contentModel == null)
                    return BadRequest("Empty Content Model");
                var responseData = new SettingsGroupResponse(site, settingsGroup, contentModel);
                var response = new SettingsApiResponse<SettingsGroupResponse> { Data = responseData };
                //TODO: implement actual fix for incorrect URLs returned for content reference properties
                ContentDeliveryApiHelper.ProcessContentProperties(response.Data.Settings.Properties, $"{EnvironmentSettings?.Domain?.TrimEnd('/')}/{ContentDeliveryApiSettings?.RouteSegment?.TrimEnd('/')}");
                var headers = new Dictionary<string, string>();
                return new SettingsApiResult<SettingsApiResponse<SettingsGroupResponse>>(response, 200, headers);
            }
            return BadRequest("Content Model could not be converted");
        }

        [NonAction]
        protected virtual SettingsGroupsResponse GetSettingsGroups(SiteDefinition siteDefinition)
        {
            var dict = SettingsService.GetSiteSettings(siteDefinition.Id);
            var settingGroups = dict is not null ? dict.Select(keyValuePair => keyValuePair.Key.Name) : Array.Empty<string>();
            return new SettingsGroupsResponse(siteDefinition, settingGroups);
        }

        [NonAction]
        protected virtual CultureInfo GetLanguage(List<string> languages)
        {
            var language = languages is null || !languages.Any() ? null : languages.Where(x => x != "*").FirstOrDefault();
            var culture = language is not null ? CultureInfo.GetCultureInfo(language) : ContentLanguage.PreferredCulture;
            return culture;
        }
    }
}