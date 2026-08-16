namespace Salam.Cms.Web.Infrastructure.Forms.Services;

using EPiServer;
using EPiServer.Core;
using EPiServer.Core.Internal;
using EPiServer.Forms.Core;
using EPiServer.Forms.Core.Internal;
using EPiServer.Forms.Core.Models;
using EPiServer.Forms.Core.Models.Internal;
using EPiServer.Forms.Implementation.Elements;
using EPiServer.Globalization;
using EPiServer.Web.Routing;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RestSharp;
using Salam.Cms.Core.Settings.Abstract;
using Salam.Cms.Web.Features.Common.Models;
using Salam.Cms.Web.Features.EnquireProduct.Services;
using Salam.Cms.Web.Features.Forms.B2BFormContainerBlock;
using Salam.Cms.Web.Features.Forms.B2BRecaptchaBlock;
using Salam.Cms.Web.Features.SelectedProductEnquire.Models;
using Salam.Cms.Web.Features.SelectedProductEnquire.Services;
using Salam.Cms.Web.Features.Settings.Models;
using System.Globalization;

public class B2BFormDataSubmissionService : DataSubmissionService
{
    private readonly IContentRepository _contentRepository;
    private readonly ILogger<B2BFormDataSubmissionService> _logger;
    private readonly ISessionService _sessionService;
    private readonly ISettingsManager _settingsManager;
    private readonly UrlResolver _urlResolver;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly EnquireProductRequestService _enquireProductRequestService;
    private readonly IConfiguration _configuration;

    public B2BFormDataSubmissionService(IContentRepository contentRepository,
                                        ILogger<B2BFormDataSubmissionService> logger,
                                        ISessionService sessionService,
                                        ISettingsManager settingsManager,
                                        UrlResolver urlResolver,
                                        IHttpContextAccessor httpContextAccessor,
                                        IConfiguration configuration,
                                        EnquireProductRequestService enquireProductRequestService)
    {

        _contentRepository = contentRepository;
        _logger = logger;
        _sessionService = sessionService;
        _settingsManager = settingsManager;
        _urlResolver = urlResolver;
        _httpContextAccessor = httpContextAccessor;
        _enquireProductRequestService = enquireProductRequestService;
        _configuration = configuration;
    }

    protected override SubmitActionResult BuildReturnResultForSubmitAction(
    bool isJavaScriptSupport,
    bool isSuccess,
    string message,
    HttpContext httpContext,
    FormContainerBlock formContainer = null,
    Dictionary<string, object> additionalParams = null,
    SubmissionInfo submissionInfo = null,
    Submission submission = null,
    bool isProgressiveSubmit = false,
    string redirectUrl = "")
    {
        var baseResult = base.BuildReturnResultForSubmitAction(
            isJavaScriptSupport,
            isSuccess,
            message,
            httpContext,
            formContainer,
            additionalParams,
            submissionInfo,
            submission,
            isProgressiveSubmit,
            redirectUrl);
        bool IsAPIEmailSent = false;
        string responseBody = string.Empty;
        bool IsEnquiry = false;
        var culture = System.Globalization.CultureInfo.CurrentCulture;
        var langCode = culture.TwoLetterISOLanguageName;
        try
        {
            if (isSuccess && formContainer is B2BFormContainerBlock b2bFormContainer)
            {
                var requestData = submission.Data;
                var transformedData = new Dictionary<string, object>();
                IsEnquiry = b2bFormContainer?.FormType == "Template2" ? true : false;
                foreach (var submissionKv in requestData)
                {
                    var currentField = b2bFormContainer.ElementsArea.Items.SingleOrDefault(x =>
                        x.ContentLink.ID.ToString() == submissionKv.Key.Replace("__field_", ""));

                    if (currentField != null)
                    {
                        var inputElement = _contentRepository.Get<ElementBlockBase>(currentField.ContentGuid);
                        if (inputElement != null && inputElement is not B2BRecaptchaBlock)
                        {
                            var blockContentLink = (dynamic)inputElement;
                            string TemplateFieldMapping = (string)blockContentLink.GetType().GetProperty("FieldMapping").GetValue(blockContentLink, null);
                            if (TemplateFieldMapping != null)
                                transformedData.Add(TemplateFieldMapping, submissionKv.Value);
                            else
                                transformedData.Add(inputElement.Content.Name, submissionKv.Value);
                        }
                    }
                }

                foreach (var contentAreaItem in b2bFormContainer.ElementsArea.FilteredItems)
                {
                    var block = _contentRepository.Get<IContent>(contentAreaItem.ContentLink);
                    if (block is B2BRecaptchaBlock)
                    {
                        var blockContentLink = (dynamic)block;
                        string recaptchaToken = (string)blockContentLink.GetType().GetProperty("recaptchaToken").GetValue(blockContentLink, null);
                        if (recaptchaToken != null)
                            transformedData.Add("recaptchaToken", recaptchaToken);

                        var lastfieldValue = requestData.Where(x => x.Key.Contains("__field_"))
                                            .Select(x => x.Value as string).LastOrDefault();

                        if (lastfieldValue != null)
                            transformedData.Add("clienttoken", lastfieldValue);
                    }
                }
                transformedData.Add("language", submissionInfo.FormLanguage);
                transformedData.Add("FormType", b2bFormContainer?.FormType);
                transformedData = GetSelectedProductsFromSession(transformedData);
                var emailMappingServiceURL = b2bFormContainer.EmailMappingURL;
                (IsAPIEmailSent, responseBody) = PostData(emailMappingServiceURL, transformedData).Result;
                if (IsAPIEmailSent)
                    baseResult.Message = b2bFormContainer?.FormSuccessMessageCstm ?? responseBody;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"Build Return Result For SubmitAction: {ex.Message} and Stacktrace is {ex.StackTrace}");
            throw;
        }
        if (!IsAPIEmailSent)
        {
            baseResult.IsSuccess = false;
            baseResult.Message = responseBody;
        }
        if (IsAPIEmailSent && IsEnquiry)
        {
            _enquireProductRequestService.RemoveAllProductsForSpecificUser(_httpContextAccessor.HttpContext?.Session.Id);
        }

        return baseResult;
    }

    public async Task<(bool Success, string Content)> PostData(string url, Dictionary<string, object> transformedData)
    {
        try
        {
            var client = new RestClient(url);
            var request = new RestRequest("", Method.Post);
            request.AddJsonBody(transformedData);
            RestResponse response = await client.ExecuteAsync(request);
            _logger.LogInformation($"API Email Sending Response: {response.Content}");
            return (Success: response.IsSuccessStatusCode, Content: response.Content!);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error posting data to API: {ex.Message} and stacktrace is {ex.StackTrace}");
        }
        return (Success: false, Content: "API Call not successfull");
    }
    private Dictionary<string, object> GetSelectedProductsFromSession(Dictionary<string, object> transformedData)
    {
        try
        {
            var webLayoutSettings = _settingsManager.GetSettings<WebLayoutSettings>();
            string SessionKey = _httpContextAccessor.HttpContext?.Session.Id!;
            List<EnquireProductRequest> lst_EnquireProductRequest = _sessionService.GetObject<List<EnquireProductRequest>>(SessionKey) ?? new List<EnquireProductRequest>();
            var currentLanguage = transformedData["language"].ToString();
            List<EnquireProductRequest> list_EnquireProductRequest = new();
            lst_EnquireProductRequest.ForEach(x =>
            {
                EnquireProductRequest obj_EnquireProductRequest = new();
                IEnumerable<B2BSitePageData> ancestors = _contentRepository.GetAncestors(new ContentReference(x.PageContentLink!.Value))
                .Select(a => _contentRepository.Get<IContent>(a.ContentLink, CultureInfo.GetCultureInfo(currentLanguage!)))
                .OfType<B2BSitePageData>();
                B2BSitePageData result = (B2BSitePageData)null;
             
                if (currentLanguage!.Equals("en", StringComparison.OrdinalIgnoreCase) &&
                                    !String.IsNullOrEmpty(x.enTabName))
                {
                    obj_EnquireProductRequest.heading = x.enTabName;
                }
                else if (currentLanguage.Equals("ar", StringComparison.OrdinalIgnoreCase) &&
                !String.IsNullOrEmpty(x.arTabName))
                {
                    obj_EnquireProductRequest.heading = x.arTabName;
                }
                if (String.IsNullOrEmpty(obj_EnquireProductRequest.heading))
                {
                    result = _contentRepository.Get<B2BSitePageData>(new ContentReference(x.PageContentLink.Value), CultureInfo.GetCultureInfo(currentLanguage));
                    obj_EnquireProductRequest.heading = result?.ProductName! ?? result?.Name;
                }

                obj_EnquireProductRequest.labels = result?.Labels?.Any() == true
                                    ? result.Labels.ToArray()
                                    : ancestors?.Select(x => !string.IsNullOrEmpty(x.ProductName) ? x.ProductName : x.Name).ToArray()
                                    ?? Array.Empty<string>();
                list_EnquireProductRequest.Add(obj_EnquireProductRequest);
            });

            transformedData.Add("lst_EnquireProductRequest", list_EnquireProductRequest);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error posting data to API: {ex.Message} and stacktrace is {ex.StackTrace}");
        }
        return transformedData;
    }
}
