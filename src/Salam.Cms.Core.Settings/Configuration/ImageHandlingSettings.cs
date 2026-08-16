namespace Salam.Cms.Core.Settings.Configuration;

public class ImageHandlingSettings
{
    public const string SectionName = "ImageHandling";
    /// <summary>
    /// If true, disables CDN image transformations (for local development)
    /// </summary>
    public bool DisableCdnTransformations { get; set; } = false;
    // Add other image handling settings here as needed
}