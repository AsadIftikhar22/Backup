namespace Salam.Cms.Web.Features.Cookies.Controllers
{
    using global::Salam.Cms.Web.Features.Cookies.Services;
    using Microsoft.AspNetCore.Mvc;

    [Route("cookie")]
    public class CookieController : Controller
    {
        private readonly ICookieService _cookieService;

        public CookieController(ICookieService cookieService)
        {
            _cookieService = cookieService;
        }

        [HttpPost("save")]
        public IActionResult Save([FromBody] CookiePreferences prefs)
        {
            _cookieService.SetCookieConsent(prefs.Analytics, prefs.Marketing, prefs.IsSave);
            return Ok();
        }

        public class CookiePreferences
        {
            public bool Analytics { get; set; }
            public bool Marketing { get; set; }
            public bool IsSave { get; set; }
        }
    }
}
