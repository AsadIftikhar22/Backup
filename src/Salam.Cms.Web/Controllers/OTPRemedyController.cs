namespace Salam.Cms.Web.Features.Cookies.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Salam.Cms.Web.Features.SelectedProductEnquire.Models;
    using Salam.Cms.Web.Features.SelectedProductEnquire.Services;
    using Salam.Cms.Web.Infrastructure.Forms.Services;

    [Route("api/OTPRemedy")]
    public class OTPRemedyController : Controller
    {
        private readonly ProtectorChannelFormDataSubmissionService _ProtectorChannelFormDataSubmissionService;
        public OTPRemedyController(ProtectorChannelFormDataSubmissionService ProtectorChannelFormDataSubmissionService)
        {
            _ProtectorChannelFormDataSubmissionService = ProtectorChannelFormDataSubmissionService;
        }
        [HttpGet("test")]
        public IActionResult Test()
        {
            return Ok("API Working");
        }

        [HttpPost("ReSendOTP")]
        public async Task<ResultStatus> ReSendOTP()
        {
            ResultStatus obj_ResultStatus = new ResultStatus();
            try
            {
                var responseOTP = await _ProtectorChannelFormDataSubmissionService.ReSendOTP();
                obj_ResultStatus.ResponseMessage = responseOTP.ResponseMessage;
                obj_ResultStatus.ResponseStatus = responseOTP.ResponseStatus;
                Console.WriteLine($"API in ReSendOTP: {obj_ResultStatus.ResponseMessage}\n{obj_ResultStatus.ResponseStatus}");
            }
            catch (Exception ex)
            {
                obj_ResultStatus.ResponseMessage = ex.Message + ex.StackTrace;
                obj_ResultStatus.ResponseStatus = (int)ResponseStatus.Error;
                Console.WriteLine($"Error in ReSendOTP API: {ex.Message}\n{ex.StackTrace}");
            }
            return obj_ResultStatus;
        }
    }
}
