namespace Salam.CMS.Web.Controller;

using EPiServer.ContentGraph.Helpers;
using EPiServer.Core;
using Google.Api.Gax.ResourceNames;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.RecaptchaEnterprise.V1;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using Salam.Cms.Web.Features.EnquireProduct.Services;
using Salam.Cms.Web.Features.SelectedProductEnquire.Models;
using Salam.CMS.Web.Data;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Text;

[ApiController]
[Route("/b2b-form-api")]
public class B2BFormApiController : Controller
{
    private readonly IWebLayoutSettingsRepo _webLayoutSettingsRepo;
    private readonly string _projectId;
    private readonly string _siteKey;
    private readonly string _credentialFile;
    private readonly IConfiguration _config;
    private readonly IWebHostEnvironment _env;
    private readonly ISessionService _sessionService;
    //private readonly HttpContextAccessor _httpContextAccessor;
    public B2BFormApiController(/*HttpContextAccessor httpContextAccessor,*/
        ISessionService sessionService,
        IWebHostEnvironment env,
        IWebLayoutSettingsRepo webLayoutSettingsRepo,
        IConfiguration configuration)
    {
        //_httpContextAccessor = httpContextAccessor;
        _sessionService = sessionService;
        _webLayoutSettingsRepo = webLayoutSettingsRepo;
        _config = configuration;
        _projectId = _config["GoogleRecaptcha:ProjectId"];
        _siteKey = _config["GoogleRecaptcha:SiteKey"];
        _env = env;
        _credentialFile = GetServiceAccountJsonPath();
        Console.WriteLine($"Project ID is {_projectId} _siteKey is {_siteKey} and" +
            $" _credentialFile path is {_credentialFile}");
    }
    public string GetServiceAccountJsonPath()
    {
        var relativePath = _config["GoogleRecaptcha:ServiceAccountFile"];
        var fullPath = Path.Combine(_env.ContentRootPath, relativePath);
        return fullPath;
    }
    private CultureInfo cultureInfo;

    /// <summary>
    /// Salam Send Email API
    /// </summary>
    /// <param name="p_ComplaintRequestDto"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    [HttpPost]
    public async Task<IActionResult> SalamSendEmail([FromBody] Dictionary<string, object> p_ComplaintRequestDto)
    {
        try
        {
            p_ComplaintRequestDto.ForEach(x =>
            {
                Console.WriteLine($"The dictionary items key is {x.Key} " +
                $"value is {x.Value} ");
            });

            string? language = null;
            string? recaptchaToken = null;
            string? formType = null;
            string? email = null;
            string? clienttoken = null;
            string companyName = null;

            if (p_ComplaintRequestDto.TryGetValue("language", out var langObj))
                language = langObj?.ToString() ?? "";

            if (p_ComplaintRequestDto.TryGetValue("recaptchaToken", out var recaptchaTokenObj))
                recaptchaToken = recaptchaTokenObj?.ToString() ?? "";

            if (p_ComplaintRequestDto.TryGetValue("clienttoken", out var clienttokenObj))
                clienttoken = clienttokenObj?.ToString() ?? "";

            if (p_ComplaintRequestDto.TryGetValue("companyName", out var companyNameObj))
                companyName = companyNameObj?.ToString() ?? "";

            if (p_ComplaintRequestDto.TryGetValue("FormType", out var formTypeObj))
                formType = formTypeObj?.ToString() ?? "";

            if (p_ComplaintRequestDto.TryGetValue("email", out var emailObj))
                email = emailObj?.ToString() ?? "";

            if (!String.IsNullOrEmpty(recaptchaToken))
            {
                Console.WriteLine("Before Validating Re-captcha token");
                var recaptchaScore = ValidateReCAPTCHA(clienttoken,
                                                      "LOGIN");
                Console.WriteLine("Recpatcha score is " + recaptchaScore);
                if (recaptchaScore < 0.5m)
                {
                    return StatusCode(499, new
                    {
                        errors = new[]
                    { "reCAPTCHA verification failed. Please try again." }
                    });
                }
            }

            p_ComplaintRequestDto.Add("ip", GetFormSubmissionIPAddress());
            var result = GetEmailBody(cultureInfo, formType);
            var Body = UpdateHtmlBody(result.EmailBody, p_ComplaintRequestDto);
            Console.WriteLine("BaseURL is" + result.APIbaseURL);
            if (string.IsNullOrEmpty(result.APIbaseURL))
            {
                throw new ArgumentException("Base URL cannot be null or empty.");
            }
            var client = new RestClient(result.APIbaseURL);
            var request = new RestRequest("", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            var emailPayload = new
            {
                json = new
                {
                    to = new[] { !String.IsNullOrWhiteSpace(result.ToEmail) ?
                         result.ToEmail :  email},
                    from = result.FromEmail,
                    cc = new string[] { },
                    subject = result.Emailsubject.Replace("{companyName}", companyName),
                    content = Body,
                    files = new string[] { }
                }
            };
            Console.WriteLine("Payload is: " + JsonConvert.SerializeObject(emailPayload));
            var req_body = JsonConvert.SerializeObject(emailPayload);
            request.AddStringBody(req_body, DataFormat.Json);
            RestResponse response = await client.ExecuteAsync(request);
            Console.WriteLine("Response is" + response.IsSuccessful);
            if (response.IsSuccessful)
            {
                return Ok(response.Content);
            }
            else
            {
                return NotFound(response.Content);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception Message: {ex.Message}");
            Console.WriteLine($"StackTrace: {ex.StackTrace}");
            //throw new ArgumentException($"Exception message: {ex.Message} and StackTrace: {ex.StackTrace}");
            return NotFound();
        }
    }

    [HttpGet]
    [Route("removeselectedproducts")]
    public IActionResult RemoveSelectedProducts()
    {
        try
        {
            string SessionKey =HttpContext.Session.Id;
            _sessionService.Remove(SessionKey);
            return Ok(new { success = true });
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Exception message is {ex.Message} and Stacktrace is {ex.StackTrace}");
            return StatusCode(500, new { success = false, message = ex.Message });
        }
    }
    /// <summary>
    /// GetEmailBody
    /// </summary>
    /// <param name="cultureInfo"></param>
    /// <param name="RequestForm"></param>
    /// <returns></returns>
    private EmailBodyResponse GetEmailBody(CultureInfo cultureInfo, string RequestForm)
    {
        return _webLayoutSettingsRepo.GetFormEmailBody(cultureInfo, RequestForm);
    }
    public string testing()
    {
        return "!23";
    }
    public XhtmlString UpdateHtmlBody(XhtmlString emailbody, Dictionary<string, object> dto)
    {
        if (emailbody == null)
        {
            return new XhtmlString();
        }
        // Get the raw HTML string from XhtmlString
        string html = emailbody.ToInternalString();
        List<EnquireProductRequest> obj_enquireProductRequests = new();
        try
        {
            foreach (var kv in dto)
            {
                if (kv.Key == null) continue;
                string placeholder = $"{{{{{kv.Key}}}}}";
                string rawValue = kv.Value?.ToString() ?? "";
                string encodedValue = WebUtility.HtmlEncode(rawValue);
                html = html.Replace(placeholder, encodedValue);
            }
            html = html.Replace($"{{{{DateNow}}}}", DateTime.Now.Date.ToString("yyyy-MM-dd"));

            if (dto.TryGetValue("lst_EnquireProductRequest", out var lst_EnquireProductRequestObj) && lst_EnquireProductRequestObj != null)
                obj_enquireProductRequests = JsonConvert.DeserializeObject<List<EnquireProductRequest>>(lst_EnquireProductRequestObj?.ToString());

            var sb = new StringBuilder();
            sb.AppendLine("<table style=\"width: 100%; border-collapse: collapse;\">\r\n<tbody>\r\n<tr>\r\n<th style=\"background: #c5e0b4; padding: 10px; border: 1px solid #aaa;\" colspan=\"4\">Product Name</th>\r\n</tr>");
            foreach (var pr in obj_enquireProductRequests)
            {
                string productName = WebUtility.HtmlEncode(pr.heading ?? "");
                string categories = pr.labels != null ? WebUtility.HtmlEncode(string.Join(", ", pr.labels)) : "";

                sb.AppendLine("<tr>");
                sb.AppendLine($"    <td style=\"padding: 8px; background: #c5e0b4; border: 1px solid #aaa;\">Product Name</td>");
                sb.AppendLine($"    <td style=\"padding: 8px; border: 1px solid #aaa;\">{productName}</td>");
                sb.AppendLine($"    <td style=\"padding: 8px; background: #c5e0b4;  border: 1px solid #aaa;\">Product Category</td>");
                sb.AppendLine($"    <td style=\"padding: 8px; border: 1px solid #aaa;\">{categories}</td>");
                sb.AppendLine("</tr>");
            }

            sb.AppendLine("</tbody></table>");
            html = html.Replace("{{Product_Rows}}", sb.ToString());
            html = html.Replace("{{Request_Date}}", DateTime.Now.ToString("dd/MM/yyy"));
            return new XhtmlString(html);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error occurred while updating email body: " + ex.Message);
            return new XhtmlString();
        }
    }



    private string GetFormSubmissionIPAddress()
    {
        string ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
        if (Request.Headers.ContainsKey("X-Forwarded-For"))
        {
            ipAddress = Request.Headers["X-Forwarded-For"].FirstOrDefault();
        }
        return ipAddress;
    }

    #region Validate Re Captcha
    /// <summary>
    /// ValidateRecaptcha
    /// </summary>
    /// <param name="token"></param>
    /// <param name="recaptchaAction"></param>
    /// <returns></returns>
    [Obsolete]
    public decimal ValidateReCAPTCHA(string token, string recaptchaAction)
    {
        try
        {
            var credential = GoogleCredential.FromFile(_credentialFile)
                    .CreateScoped(RecaptchaEnterpriseServiceClient.DefaultScopes);
            var clientBuilder = new RecaptchaEnterpriseServiceClientBuilder { Credential = credential };
            var client = clientBuilder.Build();
            var projectName = new ProjectName(_projectId);
            Console.WriteLine($"Project Name is {projectName}");
            Console.WriteLine($"Site Key is {_siteKey}");
            Console.WriteLine($"Recaptcha Token is {recaptchaAction}");
            Console.WriteLine($"Token is {token}");
            var request = new CreateAssessmentRequest()
            {
                ParentAsProjectName = projectName,
                Assessment = new Assessment()
                {
                    Event = new Event()
                    {
                        SiteKey = _siteKey,
                        Token = token,
                        ExpectedAction = recaptchaAction
                    },
                }
            };
            var response = client.CreateAssessment(request);
            Console.WriteLine($"Response from Validation is", JsonConvert.SerializeObject(response));
            if (!response.TokenProperties.Valid)
            {
                Console.WriteLine("Invalid reCAPTCHA token: " + response.TokenProperties.InvalidReason);
                return 0;
            }
            if (response.TokenProperties.Action != recaptchaAction)
            {
                Console.WriteLine("Action mismatch in reCAPTCHA token.");
                return 0;
            }
            return (decimal)response.RiskAnalysis.Score;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception message is {ex.Message} and stacktrace is {ex.StackTrace}");
            return 0;
         }
    }
    #endregion
}

