namespace Salam.Cms.Core.Settings.Configuration;

/// <summary>
/// Configuration options for the image proxy service
/// </summary>
public class ImageProxySettings
{
    /// <summary>
    /// List of allowed host domains that can be proxied
    /// </summary>
    public IList<string> AllowedHosts { get; init; } = new List<string>();

    /// <summary>
    /// Duration in seconds for caching proxied images (default 24 hours)
    /// </summary>
    public int CacheDurationSeconds { get; init; } = 86400; // 24 hours
}