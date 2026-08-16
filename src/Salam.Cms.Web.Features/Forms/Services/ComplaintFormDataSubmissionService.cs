namespace Salam.Cms.Web.Infrastructure.Forms.Services;

using EPiServer;
using EPiServer.Core;
using EPiServer.Forms.Core;
using EPiServer.Forms.Core.Internal;
using EPiServer.Forms.Core.Models;
using EPiServer.Forms.Core.Models.Internal;
using EPiServer.Forms.Implementation.Elements;
using EPiServer.Web.Routing;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RestSharp;
using Salam.Cms.Core.Settings.Abstract;
using Salam.Cms.Web.Features.EnquireProduct.Services;
using Salam.Cms.Web.Features.Forms.B2BRecaptchaBlock;
using Salam.Cms.Web.Features.Forms.ComplaintTabFormContainerBlock;
using Salam.Cms.Web.Features.Forms.Services;
using Salam.Cms.Web.Features.Forms.Services.Models;
using Salam.Cms.Web.Features.SelectedProductEnquire.Services;
using System;
using System.Text.Json;
using static Salam.Cms.Web.Infrastructure.Forms.Services.ProtectorChannelFormDataSubmissionService;

public class ComplaintFormDataSubmissionService : DataSubmissionService
{
    private readonly IContentRepository _contentRepository;
    private readonly ILogger<ComplaintFormDataSubmissionService> _logger;
    private readonly ISessionService _sessionService;
    private readonly ISettingsManager _settingsManager;
    private readonly UrlResolver _urlResolver;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly EnquireProductRequestService _enquireProductRequestService;
    private readonly IConfiguration _configuration;
    private readonly ProtectionApiWrapper _protectionAPIWrapper;

    public ComplaintFormDataSubmissionService(IContentRepository contentRepository,
                                        ILogger<ComplaintFormDataSubmissionService> logger,
                                        ISessionService sessionService,
                                        ISettingsManager settingsManager,
                                        UrlResolver urlResolver,
                                        IHttpContextAccessor httpContextAccessor,
                                        IConfiguration configuration,
                                        EnquireProductRequestService enquireProductRequestService,
                                        ProtectionApiWrapper protectionAPIWrapper)
    {

        _contentRepository = contentRepository;
        _logger = logger;
        _sessionService = sessionService;
        _settingsManager = settingsManager;
        _urlResolver = urlResolver;
        _httpContextAccessor = httpContextAccessor;
        _enquireProductRequestService = enquireProductRequestService;
        _configuration = configuration;
        _protectionAPIWrapper = protectionAPIWrapper;
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
        string responseBody = string.Empty;
        try
        {
            submissionInfo ??= _sessionService.GetObject<SubmissionInfo>(GetSessionID() + "_SubmissionInfo");
            submission ??= _sessionService.GetObject<Submission>(GetSessionID() + "_Submission");

            List<int> elementIds = null;
            if (formContainer is ComplaintTabFormContainerBlock b2bFormContainer &&
                b2bFormContainer.ElementsArea?.Items != null &&
                b2bFormContainer.ElementsArea.Items.Any()){
                elementIds = b2bFormContainer.ElementsArea.Items
                .Select(x => x.ContentLink.ID)
                .ToList();
            }
            else{
                elementIds = _sessionService.GetObject<List<int>>(GetSessionID() + "_formElementIds");
            }
            var requestData = new Dictionary<string, object>();
            if (!requestData.Any() && httpContext.Request.HasFormContentType){
                requestData = httpContext.Request.Form
                    .ToDictionary(k => k.Key, v => (object)v.Value.ToString());
            }
            else{
                requestData = submission?.Data.ToDictionary(k => k.Key, v => (object)v.Value);
            }

            SearchComplaintRequest objSearchComplaintRequest = new SearchComplaintRequest();
            var transformedData = new Dictionary<string, object>();
            string mobileNumber = null; string hdntabfield = null;
            if (elementIds != null && elementIds.Any()){
                foreach (var submissionKv in requestData){
                    var fieldIdStr = submissionKv.Key.Replace("__field_", "");
                    if (int.TryParse(fieldIdStr, out int fieldId) && elementIds.Contains(fieldId)){
                        var inputElement = _contentRepository.Get<ElementBlockBase>(new ContentReference(fieldId));
                        if (inputElement != null && inputElement is not B2BRecaptchaBlock){
                            var blockContentLink = (dynamic)inputElement;
                            string TemplateFieldMapping = (string)blockContentLink.GetType().GetProperty("FieldMapping").GetValue(blockContentLink, null);
                            if (TemplateFieldMapping != null)
                                transformedData.Add(TemplateFieldMapping, submissionKv.Value);
                            else
                                transformedData.Add(inputElement.Content.Name, submissionKv.Value);
                        }
                    }
                }


                if (!submissionInfo.IsLastestStep){
                    if (submissionInfo.CurrentStepIndex == 1){
                        if (transformedData.TryGetValue("hdntabfield", out var hdntabfieldObj))
                            hdntabfield = hdntabfieldObj?.ToString() ?? "";
                        if (hdntabfieldObj?.ToString() == "ticketDetailsTab"){
                            var ComplaintSearch_Request = _protectionAPIWrapper?.SearchComplaintTicketAsync(SearchComplaintRequest(transformedData)).Result;
                            baseResult.IsSuccess = ComplaintSearch_Request!.success;
                            baseResult.Message = ComplaintSearch_Request.success ? "" : string.Join(", ", ComplaintSearch_Request?.message!);
                            if (baseResult.IsSuccess){
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
                        else{
                            if (transformedData.TryGetValue("mobilenumber", out var mobileObj))
                                mobileNumber = mobileObj?.ToString() ?? "";
                            _sessionService.Set(GetSessionID() + "MobileNumber", mobileNumber!);
                            var responseOTP = ValidateGuestUser(mobileNumber!).Result;
                            var result = JsonSerializer.Deserialize<OtpResponse>(responseOTP.Content);
                            if (result?.Error == "Invalid Mobile Number"){
                                baseResult.IsSuccess = false;
                                baseResult.Message = result?.Error;
                                return baseResult;
                            }
                            else{
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
                    if (submissionInfo.CurrentStepIndex == 2){
                        string otp = string.Empty;
                        var otpreferencenumber = _sessionService.Get(GetSessionID() + "otpkey");
                        if (transformedData.TryGetValue("otp", out var otpObj))
                            otp = otpObj?.ToString() ?? "";
                        var responseOTP = ValidateGuestUserOTP(otp, otpreferencenumber).Result;
                        var result = JsonSerializer.Deserialize<OtpResponse>(responseOTP.Content);
                        if (result?.Error == "Invalid OTP Code"){
                            baseResult.IsSuccess = false;
                            baseResult.Message = result.Error;
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

                if (submissionInfo.IsLastestStep){
                    _logger.LogInformation(JsonSerializer.Serialize(transformedData));
                    ComplaintChannelRequest objComplaintChannelRequest = CreateComplaintAddComplaintRequest(transformedData);
                    var ComplaintAdded_Request = _protectionAPIWrapper?.CreateComplaintAsync(objComplaintChannelRequest)?.Result;

                    baseResult.IsSuccess = ComplaintAdded_Request!.Success;
                    baseResult.Message = ComplaintAdded_Request.Success ? "" : ComplaintAdded_Request?.Error?.Message;
                    if (baseResult.IsSuccess){
                        submissionInfo.IsLastestStep = false;
                    }
                    if (!baseResult.IsSuccess){
                        submissionInfo.IsLastestStep = false;
                        submissionInfo.CurrentStepIndex = 1;
                        if (_sessionService.GetObject<SubmissionInfo>(GetSessionID() + "_SubmissionInfo") == null)
                            _sessionService.SetObject(GetSessionID() + "_SubmissionInfo", submissionInfo);

                        if (_sessionService.GetObject<Submission>(GetSessionID() + "_Submission") == null)
                            _sessionService.SetObject(GetSessionID() + "_Submission", submission);

                        if (_sessionService.GetObject<List<int>>(GetSessionID() + "_formElementIds") == null){
                            var contentLinkIds = formContainer.ElementsArea.Items
                                .Select(x => x.ContentLink.ID)
                                .ToList();
                            _sessionService.SetObject(GetSessionID() + "_formElementIds", contentLinkIds);
                        }
                    }
                    if (baseResult.IsSuccess){
                        _sessionService.Remove(GetSessionID() + "_SubmissionInfo");
                        _sessionService.Remove(GetSessionID() + "_Submission");
                        _sessionService.Remove(GetSessionID() + "_formElementIds");
                        return baseResult;
                    }
                }
            }
        }
        catch (Exception ex) {
            Console.WriteLine($"Build Return Result For SubmitAction: {ex.Message} and Stacktrace is {ex.StackTrace}");
            _logger.LogError($"Build Return Result For SubmitAction: {ex.Message} and Stacktrace is {ex.StackTrace}");
            submissionInfo.IsLastestStep = false;
            submissionInfo.CurrentStepIndex = 1;
            if (!baseResult.IsSuccess)
            {
                _sessionService.SetObject(GetSessionID() + "_SubmissionInfo", submissionInfo);
                _sessionService.SetObject(GetSessionID() + "_Submission", submission);
                _sessionService.SetObject(GetSessionID() + "_formElementsArea", formContainer.ElementsArea.Items);
            }
        }
        return baseResult;
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
            Console.WriteLine($"API Response for {GuestUserAPI}: {response.StatusCode} - {response.Content}");
            _logger.LogInformation($"API Response for {GuestUserAPI}: {response.StatusCode} - {response.Content}");

            bool success = response.IsSuccessStatusCode
                           && !string.IsNullOrEmpty(response.Content);
            Console.WriteLine("Success is", success);
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

            bool success = response.IsSuccessStatusCode
                           && !string.IsNullOrEmpty(response.Content);
            Console.WriteLine("Success is", success);
            return (Success: success, Content: response.Content!);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in ValidateGuestUserOTP: {ex.Message}\n{ex.StackTrace}");
            _logger.LogError($"Error in ValidateGuestUserOTP: {ex.Message}\n{ex.StackTrace}");
            return (Success: false, Content: ex.Message);
        }
    }

    public async Task<(bool Success, string Content)> BusinessSendOTP(string mobile_number)
    {
        try
        {
            string apiUrl = _configuration["OTPRemedyAPI:BusinessSendOTP"];
            var client = new RestClient(apiUrl);
            var request = new RestRequest("", Method.Post);

            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("mobile_number", mobile_number);

            RestResponse response = await client.ExecuteAsync(request);

            _logger.LogInformation($"API Response for {apiUrl}: {response.StatusCode} - {response.Content}");

            bool success = response.IsSuccessStatusCode &&
                           !string.IsNullOrEmpty(response.Content);

            return (Success: success, Content: response.Content);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while calling BusinessSendOTP API");
            return (Success: false, Content: ex.Message);
        }
    }
    /// <summary>
    /// Business Validate OTP
    /// </summary>
    /// <param name="otp"></param>
    /// <param name="reference"></param>
    /// <returns></returns>
    public async Task<(bool Success, string Content)> BusinessValidateOTP(string otp, string reference)
    {
        try
        {
            string apiUrl = _configuration["OTPRemedyAPI:BusinessValidateOTP"];
            var client = new RestClient(apiUrl);
            var request = new RestRequest("", Method.Post);

            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("otp", otp);
            request.AddParameter("reference", reference);

            RestResponse response = await client.ExecuteAsync(request);

            _logger.LogInformation($"API Response for {apiUrl}: {response.StatusCode} - {response.Content}");

            bool success = response.IsSuccessStatusCode &&
                           !string.IsNullOrEmpty(response.Content);

            return (Success: success, Content: response.Content);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while calling BusinessValidateOTP API");
            return (Success: false, Content: ex.Message);
        }
    }
    #endregion
}
