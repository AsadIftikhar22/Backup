namespace Salam.Cms.Web.Controllers;

using Microsoft.AspNetCore.Mvc;
using Salam.Cms.Core.Services.Images;
using System.Threading.Tasks;

/// <summary>
/// Controller to proxy external HTTP images through HTTPS
/// </summary>
[ApiController]
[Route("image-proxy")]
public sealed class ImageProxyController : ControllerBase
{
    private readonly IProxyImageService _proxyImageService;

    /// <summary>
    /// Initializes a new instance of the <see cref="ImageProxyController"/>
    /// </summary>
    public ImageProxyController(IProxyImageService proxyImageService)
    {
        _proxyImageService = proxyImageService;
    }

    /// <summary>
    /// Fetches and serves an image from an external URL
    /// </summary>
    /// <param name="url">URL of the image to fetch</param>
    /// <returns>Image content or 404 if not found/invalid</returns>
    [HttpGet]
    [ResponseCache(Duration = 86400, Location = ResponseCacheLocation.Client)]
    public async Task<IActionResult> Get([FromQuery] string url)
    {
        var result = await _proxyImageService.FetchAsync(url);

        if (!result.Success)
        {
            return NotFound();
        }

        return File(result.Bytes, result.ContentType);
    }
}