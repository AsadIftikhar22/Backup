namespace Salam.Cms.Web.Infrastructure.Forms.Services;

using EPiServer;
using EPiServer.Core;
using EPiServer.Find;
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
using Salam.Cms.Web.Features.Forms.ComplaintTabFormContainerBlock;
using Salam.Cms.Web.Features.Forms.ProtectorChannelFormContainerBlock;
using Salam.Cms.Web.Features.Forms.Services;
using Salam.Cms.Web.Features.Forms.Services.Models;
using Salam.Cms.Web.Features.InternetCards.Models;
using Salam.Cms.Web.Features.SelectedProductEnquire.Models;
using Salam.Cms.Web.Features.SelectedProductEnquire.Services;
using Salam.Cms.Web.Features.Settings.Models;
using System.Collections.Generic;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

public class ProtectorChannelFormDataSubmissionService : DataSubmissionService
{
    private readonly IContentRepository _contentRepository;
    private readonly ILogger<ProtectorChannelFormDataSubmissionService> _logger;
    private readonly ISessionService _sessionService;
    private readonly ISettingsManager _settingsManager;
    private readonly UrlResolver _urlResolver;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly EnquireProductRequestService _enquireProductRequestService;
    private readonly IConfiguration _configuration;
    private readonly FraudComplaintService __fraudComplaintService;
    private readonly ProtectionApiWrapper _protectionApiWrapper;
    public ProtectorChannelFormDataSubmissionService(IContentRepository contentRepository,
                                        ILogger<ProtectorChannelFormDataSubmissionService> logger,
                                        ISessionService sessionService,
                                        ISettingsManager settingsManager,
                                        UrlResolver urlResolver,
                                        IHttpContextAccessor httpContextAccessor,
                                        IConfiguration configuration,
                                        EnquireProductRequestService enquireProductRequestService,
                                        FraudComplaintService fraudComplaintService,
                                        ProtectionApiWrapper protectionApiWrapper)
    {

        _contentRepository = contentRepository;
        _logger = logger;
        _sessionService = sessionService;
        _settingsManager = settingsManager;
        _urlResolver = urlResolver;
        _httpContextAccessor = httpContextAccessor;
        _enquireProductRequestService = enquireProductRequestService;
        _configuration = configuration;
        __fraudComplaintService = fraudComplaintService;
        _protectionApiWrapper = protectionApiWrapper;
    }
    public string GetSessionID() => _httpContextAccessor.HttpContext.Session.Id;
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
        string responseBody = string.Empty; bool IsAPIEmailSent = false; bool IsEnquiry = false;
        var obj_FraudComplaintRequest = new ProtectionAddComplaintRequest();
        try
        {
            var containerName = _sessionService.GetObject<string>(GetSessionID() + "_containerName");
            submissionInfo ??= _sessionService.GetObject<SubmissionInfo>(GetSessionID() + "_SubmissionInfo");
            submission ??= _sessionService.GetObject<Submission>(GetSessionID() + "_Submission");

            #region Protector Channel Form Container
            if (formContainer is ProtectorChannelFormContainerBlock || containerName == "ProtectorChannelFormContainerBlockProxy")
            {
                //submissionInfo ??= _sessionService.GetObject<SubmissionInfo>(GetSessionID() + "_SubmissionInfo");
                //submission ??= _sessionService.GetObject<Submission>(GetSessionID() + "_Submission");
                List<int> elementIds = null;
                if (formContainer is ProtectorChannelFormContainerBlock protectorFormContainer &&
                    protectorFormContainer.ElementsArea?.Items != null &&
                    protectorFormContainer.ElementsArea.Items.Any())
                {
                    elementIds = protectorFormContainer.ElementsArea.Items
                    .Select(x => x.ContentLink.ID)
                    .ToList();
                }
                else
                {
                    elementIds = _sessionService.GetObject<List<int>>(GetSessionID() + "_formElementIds");
                }
                var requestData = new Dictionary<string, object>();


                if (!requestData.Any() && httpContext.Request.HasFormContentType)
                {
                    requestData = httpContext.Request.Form
                        .ToDictionary(k => k.Key, v => (object)v.Value.ToString());
                }
                else
                {
                    requestData = submission?.Data.ToDictionary(k => k.Key, v => (object)v.Value);
                }

                var transformedData = new Dictionary<string, object>();
                string mobileNumber = null;
                if (elementIds != null && elementIds.Any())
                {
                    foreach (var submissionKv in requestData)
                    {
                        var fieldIdStr = submissionKv.Key.Replace("__field_", "");
                        if (int.TryParse(fieldIdStr, out int fieldId) && elementIds.Contains(fieldId))
                        {
                            var inputElement = _contentRepository.Get<ElementBlockBase>(new ContentReference(fieldId));
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

                    if (transformedData.TryGetValue("mobilenumber", out var mobileObj))
                        mobileNumber = mobileObj?.ToString() ?? "";

                    if (!submissionInfo.IsLastestStep)
                    {
                        if (submissionInfo.CurrentStepIndex == 1)
                        {

                            _sessionService.Set("mobileNumber", mobileNumber!);
                            var responseOTP = ValidateGuestUser(mobileNumber!).Result;
                            Console.WriteLine($"Protector Channel Service {responseOTP}");
                            var result = JsonSerializer.Deserialize<OtpResponse>(responseOTP.Content);
                            if (result?.Error == "Invalid Mobile Number")
                            {
                                baseResult.IsSuccess = false;
                                baseResult.Message = result?.Error;
                                return baseResult;
                            }
                            else
                            {
                                baseResult.IsSuccess = true;
                                baseResult.IsProgressiveSubmit = true;
                                baseResult.Data = baseResult?.Data ?? submissionInfo;
                                baseResult!.Message = "";
                                baseResult.RedirectUrl = "";
                                _sessionService.Set(GetSessionID() + "otpkey", result.reference);
                                return baseResult;
                            }
                        }

                        if (submissionInfo.CurrentStepIndex == 2)
                        {
                            string otp = string.Empty;
                            var otpreferencenumber = _sessionService.Get(GetSessionID() + "otpkey");
                            if (transformedData.TryGetValue("otp", out var otpObj))
                                otp = otpObj?.ToString() ?? "";
                            var responseOTP = ValidateGuestUserOTP(otp, otpreferencenumber).Result;
                            var result = JsonSerializer.Deserialize<OtpResponse>(responseOTP.Content);
                            if (result?.Error == "Invalid OTP Code")
                            {
                                baseResult.IsSuccess = false;
                                baseResult.Message = result.Error;
                                return baseResult;
                            }
                            else
                            {
                                baseResult.IsSuccess = true;
                                baseResult.IsProgressiveSubmit = true;
                                baseResult.Data = baseResult?.Data ?? submissionInfo;
                                baseResult!.Message = "";
                                baseResult.RedirectUrl = "";
                                return baseResult;
                            }
                        }
                    }
                    Console.WriteLine($"Submission Info Lastest Step is {submissionInfo.IsLastestStep} and " +
                        $"Submission Info Current index is {submissionInfo.CurrentStepIndex}");

                    Console.WriteLine("On Final form clicking");

                    if (submissionInfo.IsLastestStep || submissionInfo.CurrentStepIndex == 3)
                    {
                        Console.WriteLine("On Final form Inside clicking");
                        obj_FraudComplaintRequest = CreateProtectionAddComplaintRequest(transformedData);
                        var FraudComplaintProductAdded = _protectionApiWrapper.AddComplaintAsync(obj_FraudComplaintRequest).Result;

                        baseResult.IsSuccess = FraudComplaintProductAdded.Success;
                        baseResult.Message = FraudComplaintProductAdded.Success ? "" : FraudComplaintProductAdded?.Error?.Message;
                        if (baseResult.IsSuccess)
                        {
                            submissionInfo.IsLastestStep = true;
                        }
                        if (!baseResult.IsSuccess)
                        {
                            submissionInfo.IsLastestStep = false;
                            submissionInfo.CurrentStepIndex = submissionInfo.CurrentStepIndex == 2 ? submissionInfo.CurrentStepIndex + 1 : submissionInfo.CurrentStepIndex;
                            if (_sessionService.GetObject<SubmissionInfo>(GetSessionID() + "_SubmissionInfo") == null)
                                _sessionService.SetObject(GetSessionID() + "_SubmissionInfo", submissionInfo);

                            if (_sessionService.GetObject<Submission>(GetSessionID() + "_Submission") == null)
                                _sessionService.SetObject(GetSessionID() + "_Submission", submission);

                            if (_sessionService.GetObject<string>(GetSessionID() + "_containerName") == null)
                                _sessionService.SetObject(GetSessionID() + "_containerName", formContainer.GetType().Name.ToString());

                            if (_sessionService.GetObject<List<int>>(GetSessionID() + "_formElementIds") == null)
                            {
                                var contentLinkIds = formContainer.ElementsArea.Items
                                    .Select(x => x.ContentLink.ID)
                                    .ToList();

                                _sessionService.SetObject(GetSessionID() + "_formElementIds", contentLinkIds);
                            }
                        }
                        if (baseResult.IsSuccess)
                        {
                            _sessionService.Remove(GetSessionID() + "_SubmissionInfo");
                            _sessionService.Remove(GetSessionID() + "_Submission");
                            _sessionService.Remove(GetSessionID() + "_formElementIds");
                        }
                        return baseResult;
                    }
                }
            }
            #endregion

            #region ComplaintFormContainer
            if (formContainer is ComplaintTabFormContainerBlock || containerName == "")
            {


                List<int> elementIds = null;
                if (formContainer is ComplaintTabFormContainerBlock complaintFormContainer &&
                    complaintFormContainer.ElementsArea?.Items != null &&
                    complaintFormContainer.ElementsArea.Items.Any())
                {
                    elementIds = complaintFormContainer.ElementsArea.Items
                    .Select(x => x.ContentLink.ID)
                    .ToList();
                }
                else
                {
                    elementIds = _sessionService.GetObject<List<int>>(GetSessionID() + "_formElementIds");
                }
                var requestData = new Dictionary<string, object>();
                if (!requestData.Any() && httpContext.Request.HasFormContentType)
                {
                    requestData = httpContext.Request.Form
                        .ToDictionary(k => k.Key, v => (object)v.Value.ToString());
                }
                else
                {
                    requestData = submission?.Data.ToDictionary(k => k.Key, v => (object)v.Value);
                }

                SearchComplaintRequest objSearchComplaintRequest = new SearchComplaintRequest();
                var transformedData = new Dictionary<string, object>();
                string mobileNumber = null; string hdntabfield = null;
                if (elementIds != null && elementIds.Any())
                {
                    foreach (var submissionKv in requestData)
                    {
                        var fieldIdStr = submissionKv.Key.Replace("__field_", "");
                        if (int.TryParse(fieldIdStr, out int fieldId) && elementIds.Contains(fieldId))
                        {
                            var inputElement = _contentRepository.Get<ElementBlockBase>(new ContentReference(fieldId));
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


                    if (!submissionInfo.IsLastestStep)
                    {
                        if (submissionInfo.CurrentStepIndex == 1)
                        {
                            if (transformedData.TryGetValue("hdntabfield", out var hdntabfieldObj))
                                hdntabfield = hdntabfieldObj?.ToString() ?? "";
                            if (hdntabfieldObj?.ToString() == "ticketDetailsTab")
                            {
                                var ComplaintSearch_Request = _protectionApiWrapper?.SearchComplaintTicketAsync(SearchComplaintRequest(transformedData)).Result;
                                baseResult.IsSuccess = ComplaintSearch_Request!.success;
                                baseResult.Message = ComplaintSearch_Request.success ? "" : string.Join(", ", ComplaintSearch_Request?.message!);
                                if (baseResult.IsSuccess)
                                {
                                    submissionInfo.IsLastestStep = false;
                                    baseResult.AdditionalParams = new Dictionary<string, object>
                                {
                                    { "TicketNumber", ComplaintSearch_Request!.data.referenceId },
                                    { "ticketOpenStatus", ComplaintSearch_Request.data.ticketStatus },
                                    { "ticketClosedStatus", ComplaintSearch_Request.data.ticketStatus },
                                    { "ticketMobileNumber", ComplaintSearch_Request.data.ticketStatus },
                                };
                                }
                                return baseResult;
                            }
                            else
                            {
                                if (transformedData.TryGetValue("mobilenumber", out var mobileObj))
                                    mobileNumber = mobileObj?.ToString() ?? "";
                                _sessionService.Set(GetSessionID() + "MobileNumber", mobileNumber!);
                                var responseOTP = ValidateGuestUser(mobileNumber!).Result;
                                var result = JsonSerializer.Deserialize<OtpResponse>(responseOTP.Content);
                                if (result?.Error == "Invalid Mobile Number")
                                {
                                    baseResult.IsSuccess = false;
                                    baseResult.Message = result?.Error;
                                    return baseResult;
                                }
                                else
                                {
                                    _sessionService.Set(GetSessionID() + "otpkey", result.reference);
                                    baseResult.IsSuccess = true;
                                    baseResult.IsProgressiveSubmit = true;
                                    baseResult.Data = baseResult?.Data ?? submissionInfo;
                                    baseResult!.Message = "";
                                    baseResult.RedirectUrl = "";
                                    return baseResult;
                                }
                            }
                        }
                        if (submissionInfo.CurrentStepIndex == 2)
                        {
                            string otp = string.Empty;
                            var otpreferencenumber = _sessionService.Get(GetSessionID() + "otpkey");
                            if (transformedData.TryGetValue("otp", out var otpObj))
                                otp = otpObj?.ToString() ?? "";
                            var responseOTP = ValidateGuestUserOTP(otp, otpreferencenumber).Result;
                            var result = JsonSerializer.Deserialize<OtpResponse>(responseOTP.Content);
                            if (result?.Error == "Invalid OTP Code")
                            {
                                baseResult.IsSuccess = false;
                                baseResult.Message = result.Error;
                                return baseResult;
                            }
                            else
                            {
                                baseResult.IsSuccess = true;
                                baseResult.IsProgressiveSubmit = true;
                                baseResult.Data = baseResult?.Data ?? submissionInfo;
                                baseResult!.Message = "";
                                baseResult.RedirectUrl = "";
                            }
                            return baseResult;
                        }
                    }

                    if (submissionInfo.IsLastestStep)
                    {
                        _logger.LogInformation(JsonSerializer.Serialize(transformedData));
                        ComplaintChannelRequest objComplaintChannelRequest = CreateComplaintAddComplaintRequest(transformedData);
                        var ComplaintAdded_Request = _protectionApiWrapper?.CreateComplaintAsync(objComplaintChannelRequest)?.Result;
                        Console.WriteLine($"ComplaintAdded_Request is {ComplaintAdded_Request}");
                        baseResult.IsSuccess = ComplaintAdded_Request!.Success;
                        baseResult.Message = ComplaintAdded_Request.Success ? "" : ComplaintAdded_Request?.Error?.Message;
                        if (baseResult.IsSuccess)
                        {
                            submissionInfo.IsLastestStep = false;
                        }
                        if (!baseResult.IsSuccess)
                        {
                            submissionInfo.IsLastestStep = false;
                            submissionInfo.CurrentStepIndex = 1;
                            if (_sessionService.GetObject<SubmissionInfo>(GetSessionID() + "_SubmissionInfo") == null)
                                _sessionService.SetObject(GetSessionID() + "_SubmissionInfo", submissionInfo);

                            if (_sessionService.GetObject<Submission>(GetSessionID() + "_Submission") == null)
                                _sessionService.SetObject(GetSessionID() + "_Submission", submission);

                            if (_sessionService.GetObject<string>(GetSessionID() + "_containerName") == null)
                                _sessionService.SetObject(GetSessionID() + "_containerName", formContainer.GetType().Name.ToString());

                            if (_sessionService.GetObject<List<int>>(GetSessionID() + "_formElementIds") == null)
                            {
                                var contentLinkIds = formContainer.ElementsArea.Items
                                    .Select(x => x.ContentLink.ID)
                                    .ToList();
                                _sessionService.SetObject(GetSessionID() + "_formElementIds", contentLinkIds);
                            }
                        }
                        if (baseResult.IsSuccess)
                        {
                            _sessionService.Remove(GetSessionID() + "_SubmissionInfo");
                            _sessionService.Remove(GetSessionID() + "_Submission");
                            _sessionService.Remove(GetSessionID() + "_formElementIds");
                            return baseResult;
                        }
                    }
                }
            }
            #endregion

            #region B2B Form Container Service
            if (isSuccess && formContainer is B2BFormContainerBlock b2bFormContainer)
            {
                var requestData = submission.Data;
                var updatedData = new Dictionary<string, object>(submission.Data);
                var transformedData = new Dictionary<string, object>();
                IsEnquiry = b2bFormContainer?.FormType == "Template2" ? true : false;
                transformedData.Add("language", submissionInfo.FormLanguage);
                transformedData.Add("FormType", b2bFormContainer?.FormType);
                transformedData = GetSelectedProductsFromSession(transformedData);
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
                            {

                                transformedData.Add(TemplateFieldMapping, submissionKv.Value);
                                //if(TemplateFieldMapping == "product_headings" || TemplateFieldMapping == "product_labels")
                                //{
                                //    List<EnquireProductRequest> obj_enquireProductRequests = new();
                                //    if (transformedData.TryGetValue("lst_EnquireProductRequest", out var value) && value is List<EnquireProductRequest> list)
                                //    {
                                //        obj_enquireProductRequests = list;
                                //        if(TemplateFieldMapping == "product_headings")
                                //            updatedData[@submissionKv.Key] = string.Join(", ", obj_enquireProductRequests.Select(x => x.heading));
                                //        if (TemplateFieldMapping == "product_labels")
                                //            updatedData[@submissionKv.Key] = string.Join(", ", obj_enquireProductRequests.SelectMany(x => x.labels));
                                //    }

                                //}
                            }
                            else
                            {
                                transformedData.Add(inputElement.Content.Name, submissionKv.Value);
                            }
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

                var emailMappingServiceURL = b2bFormContainer.EmailMappingURL;
                (IsAPIEmailSent, responseBody) = PostData(emailMappingServiceURL, transformedData).Result;
                baseResult.Message = IsAPIEmailSent ?
                    b2bFormContainer?.FormSuccessMessageCstm ?? responseBody
                    : responseBody;
                baseResult.IsSuccess = IsAPIEmailSent;
            }


            #endregion
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Build Return Result For SubmitAction: {ex.Message} and Stacktrace is {ex.StackTrace}");
            _logger.LogError($"Build Return Result For SubmitAction: {ex.Message} and Stacktrace is {ex.StackTrace}");
            submissionInfo.IsLastestStep = false;
            if (!baseResult.IsSuccess)
            {
                _sessionService.SetObject(GetSessionID() + "_SubmissionInfo", submissionInfo);
                _sessionService.SetObject(GetSessionID() + "_Submission", submission);
                _sessionService.SetObject(GetSessionID() + "_formElementsArea", formContainer.ElementsArea.Items);
            }
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
            //var currentLanguage = transformedData["language"].ToString();
            var culture = ContentLanguage.PreferredCulture;
            List<EnquireProductRequest> list_EnquireProductRequest = new();
            lst_EnquireProductRequest.ForEach(x =>
            {
                EnquireProductRequest obj_EnquireProductRequest = new();
                //IEnumerable<B2BSitePageData> ancestors = _contentRepository.GetAncestors(new ContentReference(x.PageContentLink!.Value))
                //.Select(a => _contentRepository.Get<IContent>(a.ContentLink, CultureInfo.GetCultureInfo(currentLanguage!)))
                //.OfType<B2BSitePageData>();
                //B2BSitePageData result = (B2BSitePageData)null;

                if (_contentRepository.TryGet(new ContentReference(x.blockId), culture,out InternetCardsBlock cardItems))
                {
                    obj_EnquireProductRequest.heading = cardItems?.Heading;
                    obj_EnquireProductRequest.description = cardItems?.Description?.ToString();
                    obj_EnquireProductRequest.labels = (string[]?)cardItems?.Labels;
                    obj_EnquireProductRequest.blockId = x.blockId;
                }
                //var cardItems = _contentRepository.Get<InternetCardsBlock>(new ContentReference(x.blockId), currentLanguage);
                //if (currentLanguage!.Equals("en", StringComparison.OrdinalIgnoreCase) &&
                //                    !String.IsNullOrEmpty(x.enTabName))
                //{
                //    obj_EnquireProductRequest.heading = x.enTabName;
                //}
                //else if (currentLanguage.Equals("ar", StringComparison.OrdinalIgnoreCase) &&
                //!String.IsNullOrEmpty(x.arTabName))
                //{
                //    obj_EnquireProductRequest.heading = x.arTabName;
                //}
                //if (String.IsNullOrEmpty(obj_EnquireProductRequest.heading))
                //{
                //    result = _contentRepository.Get<B2BSitePageData>(new ContentReference(x.PageContentLink.Value), CultureInfo.GetCultureInfo(currentLanguage));
                //    obj_EnquireProductRequest.heading = result?.ProductName! ?? result?.Name;
                //}

                //obj_EnquireProductRequest.labels = result?.Labels?.Any() == true
                //                    ? result.Labels.ToArray()
                //                    : ancestors?.Select(x => !string.IsNullOrEmpty(x.ProductName) ? x.ProductName : x.Name).ToArray()
                //                    ?? Array.Empty<string>();
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

    /// <summary>
    /// Search Complaint Channel Request
    /// </summary>
    /// <returns></returns>
    public SearchComplaintRequest SearchComplaintRequest(Dictionary<string, object> transformedData)
    {
        var objSearchComplaintRequest = new SearchComplaintRequest();

        try
        {
            if (transformedData.TryGetValue("ticketnumber", out var ticketnumber))
                objSearchComplaintRequest.number = ticketnumber?.ToString() ?? "";
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in SearchComplaintRequest: {ex.Message}\n{ex.StackTrace}");
            _logger.LogError($"Error in SearchComplaintRequest: {ex.Message}\n{ex.StackTrace}");
        }

        return objSearchComplaintRequest;
    }
    /// <summary>
    /// Create Protector Channel Request
    /// </summary>
    /// <returns></returns>
    public ComplaintChannelRequest CreateComplaintAddComplaintRequest(Dictionary<string, object> transformedData)
    {
        var objComplaintChannelRequest = new ComplaintChannelRequest();

        try
        {
            string mobilenumber = "";
            if (transformedData.TryGetValue("mobilenumber", out var mobile))
            {
                var number = mobile?.ToString()?.Trim() ?? "";
                if (number.StartsWith("05"))
                {
                    number = number.Substring(1);
                    mobilenumber = "966" + number;
                }
            }

            if (transformedData.TryGetValue("salam_mobile_number", out var SalamMobileNumObj))
            {
                var number = SalamMobileNumObj?.ToString()?.Trim() ?? "";
                if (number.StartsWith("05"))
                {
                    number = number.Substring(1);
                    objComplaintChannelRequest.number = "966" + number;
                }
                else
                {
                    objComplaintChannelRequest.number = number;
                }
            }

            if (transformedData.TryGetValue("description", out var description))
                objComplaintChannelRequest.description = description?.ToString() ?? "";

            if (transformedData.TryGetValue("tier2", out var hdntier2fieldObj))
                objComplaintChannelRequest.tier2 = hdntier2fieldObj?.ToString() ?? "";

            if (transformedData.TryGetValue("tier3", out var hdntier3fieldObj))
                objComplaintChannelRequest.tier3 = hdntier3fieldObj?.ToString() ?? "";

            if (transformedData.TryGetValue("tier1", out var hdntier1fieldObj))
                objComplaintChannelRequest.tier1 = hdntier1fieldObj?.ToString() ?? "";

            objComplaintChannelRequest.summary =
            $"{objComplaintChannelRequest.tier1} : {objComplaintChannelRequest.tier2} - {mobilenumber} {DateTime.Now:yyyy-MM-dd}";
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in CreateProtectionAddComplaintRequest: {ex.Message}\n{ex.StackTrace}");
            _logger.LogError($"Error in CreateProtectionAddComplaintRequest: {ex.Message}\n{ex.StackTrace}");
        }

        return objComplaintChannelRequest;
    }
    /// <summary>
    /// Create Protector Channel Request
    /// </summary>
    /// <returns></returns>
    public ProtectionAddComplaintRequest CreateProtectionAddComplaintRequest(Dictionary<string, object> p_submission_data)
    {
        var request = new ProtectionAddComplaintRequest();

        try
        {
            if (p_submission_data.TryGetValue("mobilenumber", out var mobile))
            {
                var number = mobile?.ToString()?.Trim() ?? "";
                if (number.StartsWith("05"))
                {
                    number = number.Substring(1);
                    request.reporterNumber = "966" + number;
                }
                else
                {
                    request.reporterNumber = number;
                }
            }

            if (p_submission_data.TryGetValue("MobileHdnField", out var reportedIdentity))
            {
                var number = reportedIdentity?.ToString()?.Trim() ?? "";
                if (number.StartsWith("05"))
                {
                    number = number.Substring(1);
                    request.reportedIdentity = "966" + number;
                }
                else
                {
                    request.reportedIdentity = number;
                }
            }

            if (p_submission_data.TryGetValue("SubCategoryHdnField", out var typeOfComplaint))
                request.typeOfComplaint = typeOfComplaint?.ToString() ?? "";

            if (p_submission_data.TryGetValue("FeedbackHdnField", out var message))
                request.message = message?.ToString() ?? "";

            //if (p_submission_data.TryGetValue("operatorTcnHdnField", out var operatorTcn))
            //    request.operatorTcn = operatorTcn?.ToString() ?? "";

            if (p_submission_data.TryGetValue("ModalRatingHdnField", out var rating))
                request.serviceRating = rating?.ToString() ?? "";

            if (p_submission_data.TryGetValue("ratingHdnDetails", out var feedback))
                request.serviceFeedback = feedback?.ToString() ?? "";

            request.operatorTcn = _configuration["FraudApi:operatorTcn"]!;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in CreateProtectionAddComplaintRequest: {ex.Message}\n{ex.StackTrace}");
        }
        return request;
    }


    public class OtpResponse
    {
        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("code")]
        public int Code { get; set; }

        [JsonPropertyName("error")]
        public string Error { get; set; }

        [JsonPropertyName("source")]
        public string Source { get; set; }

        public string reference { get; set; }
        public string received_On { get; set; }
    }
    /// <summary>
    /// Re Send OTP Method
    /// </summary>
    /// <returns></returns>
    public async Task<ResultStatus> ReSendOTP()
    {
        ResultStatus obj_ResultStatus = new ResultStatus();
        try
        {
            var mobilenumber = _sessionService.Get("mobileNumber");
            var responseOTP = await ValidateGuestUser(mobilenumber);
            Console.WriteLine($"ReSendOTP method responseOTP is {responseOTP}");
            if (responseOTP.Success)
            {
                Console.WriteLine("ReSendOTP method Response is Success from ReSendOTP");
                var result = JsonSerializer.Deserialize<OtpResponse>(responseOTP.Content);
                if (result != null && !String.IsNullOrEmpty(result.reference))
                {
                    _sessionService.Remove(GetSessionID() + "otpkey");
                    _sessionService.Set(GetSessionID() + "otpkey", result.reference);
                }
                obj_ResultStatus.ResponseStatus = responseOTP.Success ? 200 : -112;
            }
            else
            {
                obj_ResultStatus.ResponseMessage = responseOTP.Content;
                obj_ResultStatus.ResponseStatus = -112;
                Console.WriteLine($"ReSendOTP method Resend OTP Failed for {mobilenumber}");
            }
        }
        catch (Exception ex)
        {
            obj_ResultStatus.ResponseMessage = ex.Message + ex.StackTrace;
            obj_ResultStatus.ResponseStatus = -112;
            Console.WriteLine($"Error in ReSendOTP method: {ex.Message}\n{ex.StackTrace}");
            _logger.LogError($"Error in ReSendOTP method: {ex.Message}\n{ex.StackTrace}");
        }
        return obj_ResultStatus;
    }
    #region Remedy Form Steps
    /// <summary>
    /// Validate Guest USER Mobile Number
    /// </summary>
    /// <param name="mobile_number"></param>
    /// <returns></returns>
    public async Task<(bool Success, string Content)> ValidateGuestUser(
    string mobile_number)
    {
        try
        {
            string GuestUserAPI = _configuration["OTPRemedyAPI:ValidateGuestUserAPI"]!;
            var client = new RestClient(GuestUserAPI);
            var request = new RestRequest("", Method.Post);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("mobile_number", mobile_number);
            RestResponse response = await client.ExecuteAsync(request);
            Console.WriteLine($"ValidateGuestUser API Response for {GuestUserAPI}: {response.StatusCode} - {response.Content}");
            var result = JsonSerializer.Deserialize<OtpResponse>(response.Content);
            Console.WriteLine($"ValidateGuestUser Validate Guest User is {result}");
            _logger.LogInformation($"API Response for {GuestUserAPI}: {response.StatusCode} - {response.Content}");
            bool success = result?.Status != "Error";
            return (Success: success, Content: response.Content!);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in ValidateGuestUser: {ex.Message}\n{ex.StackTrace}");
            _logger.LogError($"Error in ValidateGuestUser: {ex.Message}\n{ex.StackTrace}");
            return (Success: false, Content: ex.Message);
        }
    }
    /// <summary>
    /// Validate Guest User OTP
    /// </summary>
    /// <param name="mobile_number"></param>
    /// <returns></returns>
    public async Task<(bool Success, string Content)> ValidateGuestUserOTP(
    string otp, string referencenumber)
    {
        try
        {
            string ValidateGuestUserOTPAPIURL = _configuration["OTPRemedyAPI:ValidateGuestUserOTPAPI"]!;
            var client = new RestClient(ValidateGuestUserOTPAPIURL!);
            var request = new RestRequest("", Method.Post);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("otp", otp);
            request.AddParameter("reference", referencenumber);
            RestResponse response = await client.ExecuteAsync(request);
            Console.WriteLine($"API Response for {ValidateGuestUserOTPAPIURL}: {response.StatusCode} - {response.Content}");
            _logger.LogInformation($"API Response for {ValidateGuestUserOTPAPIURL}: {response.StatusCode} - {response.Content}");
            var result = JsonSerializer.Deserialize<OtpResponse>(response?.Content);
            Console.WriteLine($"Result is {result.Status} and {result.Source}");
            bool success = result?.Status != "Error";
            return (Success: success, Content: response.Content!);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in ValidateGuestUserOTP: {ex.Message}\n{ex.StackTrace}");
            _logger.LogError($"Error in ValidateGuestUserOTP: {ex.Message}\n{ex.StackTrace}");
            return (Success: false, Content: ex.Message);
        }
    }
    #endregion
}
