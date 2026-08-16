namespace Salam.Cms.Core.Services.Images;

using Salam.Cms.Shared.Models.Images;
using System.Threading.Tasks;

/// <summary>
/// Interface for image proxy service to fetch and cache external images
/// </summary>
public interface IProxyImageService
{
    /// <summary>
    /// Fetches an image from an external URL, validating and caching it
    /// </summary>
    /// <param name="url">URL of the image to fetch</param>
    /// <returns>Result containing the image data and content type</returns>
    Task<ProxyImageResult> FetchAsync(string url);
}
