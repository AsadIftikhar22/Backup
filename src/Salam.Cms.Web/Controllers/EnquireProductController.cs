namespace Salam.Cms.Web.Features.Cookies.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Salam.Cms.Web.Features.SelectedProductEnquire.Models;
    using Salam.Cms.Web.Features.SelectedProductEnquire.Services;

    [Route("api/EnquireProduct")]
    public class EnquireProductController : Controller
    {
        private readonly EnquireProductRequestService _enquireProductRequestService;
        public EnquireProductController(EnquireProductRequestService enquireProductRequestService)
        {
            _enquireProductRequestService = enquireProductRequestService;
        }
        public string GetSessionID() => HttpContext.Session.Id;

        [HttpPost("AddProduct")]
        public ResultStatus AddProduct([FromBody] EnquireProductRequest request_EnquireProductRequest)
        {
            return _enquireProductRequestService.SaveSelectedProductInSession(request_EnquireProductRequest);
        }
        [HttpPost("RemoveProduct")]
        public ResultStatus RemoveProduct(int blockId)
        {
            return _enquireProductRequestService.RemoveProductFromSession(blockId);
        }
    }
}
