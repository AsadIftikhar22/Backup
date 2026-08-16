namespace Salam.Cms.Web.Features.Cookies.Controllers
{
    using Microsoft.AspNetCore.Localization;
    using Microsoft.AspNetCore.Mvc;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Text.Json;

    [Route("api/OTPRemedyAPI")]
    public class OTPRemedyAPIController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public OTPRemedyAPIController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        [HttpPost("validate-guest-user")]
        public async Task<IActionResult> ValidateGuestUser([FromForm] string mobile_number)
        {
            string language = HttpContext.Features.Get<IRequestCultureFeature>()?.RequestCulture.Culture.TwoLetterISOLanguageName == "ar" ? "ar" : "en";
            mobile_number = "966" + mobile_number.Substring(1);

            var requestBody = JsonSerializer.Serialize(new { number = mobile_number });
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, _configuration["Proxy:RemedyOtpLoginUrl"]);

            requestMessage.Content = new StringContent(requestBody, Encoding.UTF8);
            requestMessage.Content.Headers.ContentType =
                new MediaTypeHeaderValue("application/json") { CharSet = "utf-8" };
            requestMessage.Headers.Add("Api-Key", _configuration["RemedyOtpKey"]);
            requestMessage.Headers.Add("Accept-Language", language);
            requestMessage.Headers.Add("X-PROTOCOL-VERSION", "v5");

            var client = _httpClientFactory.CreateClient();
            var response = await client.SendAsync(requestMessage);
            var body = await response.Content.ReadAsStringAsync();

            return Content(body, "application/json");
        }

        [HttpPost("verify-guest-user-otp")]
        public async Task<IActionResult> VerifyGuestUserOtp([FromForm] string otp, [FromForm] string reference)
        {
            string language = HttpContext.Features.Get<IRequestCultureFeature>()?.RequestCulture.Culture.TwoLetterISOLanguageName == "ar" ? "ar" : "en";

            var requestBody = JsonSerializer.Serialize(new { otp, reference });
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, _configuration["Proxy:RemedyOtpVerifyUrl"]);
            requestMessage.Content = new StringContent(requestBody, Encoding.UTF8);
            requestMessage.Content.Headers.ContentType =
                new MediaTypeHeaderValue("application/json") { CharSet = "utf-8" };
            requestMessage.Headers.Add("Api-Key", _configuration["RemedyOtpKey"]);
            requestMessage.Headers.Add("Accept-Language", language);
            requestMessage.Headers.Add("X-PROTOCOL-VERSION", "v5");

            var client = _httpClientFactory.CreateClient();
            var response = await client.SendAsync(requestMessage);
            var bodyJson = await response.Content.ReadAsStringAsync();
            var parsed = JsonSerializer.Deserialize<JsonElement>(bodyJson);

            if (parsed.TryGetProperty("error", out _))
            {
                return BadRequest(new { error = "Invalid OTP Code" });
            }

            return Ok(new { status = "success" });
        }

        [HttpPost("business-send-otp")]
        public async Task<IActionResult> BusinessSendOTP([FromForm] string mobile_number)
        {
            string language = HttpContext.Features.Get<IRequestCultureFeature>()?.RequestCulture.Culture.TwoLetterISOLanguageName == "ar" ? "ar" : "en";
            mobile_number = mobile_number.Substring(1);

            var requestBody = JsonSerializer.Serialize(new { number = mobile_number });
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, _configuration["Proxy:RemedyOtpUrl"])
            {
                Content = new StringContent(requestBody, Encoding.UTF8, "application/json")
            };
            requestMessage.Headers.Add("Api-Key", _configuration["RemedyOtpKey"]);
            requestMessage.Headers.Add("Accept-Language", language);
            requestMessage.Headers.Add("X-PROTOCOL-VERSION", "v5");

            var client = _httpClientFactory.CreateClient();
            var response = await client.SendAsync(requestMessage);
            var body = await response.Content.ReadAsStringAsync();

            return Content(body, "application/json");
        }

        [HttpPost("business-validate-otp")]
        public async Task<IActionResult> BusinessValidateOTP([FromForm] string otp, [FromForm] string reference)
        {
            string language = HttpContext.Features.Get<IRequestCultureFeature>()?.RequestCulture.Culture.TwoLetterISOLanguageName == "ar" ? "ar" : "en";

            var requestBody = JsonSerializer.Serialize(new { otp, reference });
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, _configuration["Proxy:RemedyOtpConfirmUrl"])
            {
                Content = new StringContent(requestBody, Encoding.UTF8, "application/json")
            };
            requestMessage.Headers.Add("Api-Key", _configuration["RemedyOtpKey"]);
            requestMessage.Headers.Add("Accept-Language", language);
            requestMessage.Headers.Add("X-PROTOCOL-VERSION", "v5");

            var client = _httpClientFactory.CreateClient();
            var response = await client.SendAsync(requestMessage);
            var bodyJson = await response.Content.ReadAsStringAsync();
            var parsed = JsonSerializer.Deserialize<JsonElement>(bodyJson);

            if (parsed.TryGetProperty("error", out _))
            {
                return BadRequest(new { message = "Invalid OTP Code", result = "error" });
            }
            return Ok(new { message = "Valid OTP Code", result = "success" });
        }
    }
}
