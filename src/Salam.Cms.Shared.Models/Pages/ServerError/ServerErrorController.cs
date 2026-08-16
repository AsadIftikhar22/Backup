namespace Salam.Cms.Shared.Models.Pages.ServerError;

using Microsoft.AspNetCore.Mvc;
using System.Net;

/// <summary>
/// Provides a near static error page using no business logic or data access
/// Ideally we would use a static html file, but we need a controller in order
/// to return a status code that is not a 200 code.
/// </summary>
public sealed class ServerErrorController : Controller
{
    [HttpGet("/server-error")]
    public IActionResult ServerError()
    {
        Response.StatusCode = (int)HttpStatusCode.ServiceUnavailable;

        return View();
    }
}