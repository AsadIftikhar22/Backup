namespace Salam.Cms.Web.Features.Common.Components.Images;

using EPiServer.Core;
using global::PictureRenderer;
using global::PictureRenderer.Profiles;
using Microsoft.AspNetCore.Html;
using Salam.Cms.Shared.Models.Media;
using System.Collections.Generic;

public class ImageViewModel
{
    public required ContentReference ImageReference { get; set; }

    // For CMS images; null for DAM images
    public IImageContent? ImageContent { get; set; }

    // For DAM images; null for CMS images
    public string? DamImageUrl { get; set; }

    // Alt text resolved from DAM metadata, CMS property, or overridden by tag helper
    public string? AltText { get; set; }

    public bool IsVectorImage { get; set; } = false;

    public HtmlString? RawVectorImageContent { get; set; }

    public SvgRenderMode SvgRenderMode { get; set; }

    public PictureProfileBase? PictureProfile { get; set; }

    // Controls native lazy loading (default: BrowserNative)
    public LazyLoading LazyLoading { get; set; } = LazyLoading.Browser;

    // Controls fetch priority (default: Auto)
    public FetchPriority ImgFetchPriority { get; set; } = FetchPriority.Auto;

    // Controls image decoding (default: Auto)
    public ImageDecoding ImgDecoding { get; set; } = ImageDecoding.Auto;

    // Additional attributes for <img> tag
    public Dictionary<string, string>? ImgAdditionalAttributes { get; set; }
}
