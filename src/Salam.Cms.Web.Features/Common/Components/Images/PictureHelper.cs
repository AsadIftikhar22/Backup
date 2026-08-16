using EPiServer.Core;
using EPiServer.Web.Routing;
using global::PictureRenderer;
using global::PictureRenderer.Profiles;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Salam.Cms.Core.Settings.Configuration;
using Salam.Cms.Web.Features.Common.Components.Images.PictureRenderer;

namespace Salam.Cms.Web.Features.Common.Components.Images;

/// <summary>
/// Provides extension methods for IHtmlHelper specific to image rendering.
/// </summary>
public static class PictureHelper
{
    /// <summary>
    /// Enhanced Html.Picture() method that is DAM-aware and supports local development settings.
    /// Maintains backward compatibility with existing usage.
    /// </summary>
    public static async Task<IHtmlContent> Picture(
        this IHtmlHelper htmlHelper,
        ContentReference imageReference,
        PictureProfileBase pictureProfile,
        PictureAttributes? attributes = null,
        SvgRenderMode svgRenderMode = SvgRenderMode.ImageSrc)
    {
        if (imageReference == null || imageReference.ID == 0)
        {
            return HtmlString.Empty;
        }

        var serviceProvider = htmlHelper.ViewContext.HttpContext.RequestServices;
        var viewModelBuilder = serviceProvider.GetRequiredService<IPictureRendererViewModelBuilder>();
        var imageHandlingSettings = serviceProvider.GetRequiredService<IOptions<ImageHandlingSettings>>();

        // Create a copy of the profile to avoid modifying the static instance
        var profileToUse = CloneProfile(pictureProfile);

        // Apply local development settings
        if (imageHandlingSettings.Value.DisableCdnTransformations && profileToUse is CloudflareProfile cfProfile)
        {
            cfProfile.IsDisabled = true;
            // Ensure safe defaults
            if (cfProfile.SrcSetWidths == null || !cfProfile.SrcSetWidths.Any())
            {
                cfProfile.SrcSetWidths = new[] { 1920 };
            }
            if (cfProfile.Sizes == null || !cfProfile.Sizes.Any())
            {
                cfProfile.Sizes = new[] { "100vw" };
            }
        }

        // Build the view model using our DAM-aware builder
        var viewModel = await viewModelBuilder.Build(imageReference, profileToUse, attributes, svgRenderMode);

        if (viewModel == null)
        {
            return HtmlString.Empty;
        }

        // Handle inline SVGs
        if (viewModel.IsVectorImage && viewModel.SvgRenderMode == SvgRenderMode.Inline && viewModel.RawVectorImageContent != null)
        {
            return viewModel.RawVectorImageContent;
        }

        // Get the image path (DAM or CMS)
        var imagePath = viewModel.DamImageUrl;
        if (string.IsNullOrEmpty(imagePath) && viewModel.ImageReference != null && viewModel.ImageReference.ID != 0)
        {
            var urlResolver = serviceProvider.GetRequiredService<IUrlResolver>();
            imagePath = urlResolver.GetUrl(viewModel.ImageReference);
        }

        if (string.IsNullOrEmpty(imagePath))
        {
            return HtmlString.Empty;
        }

        // Create final attributes with resolved alt text
        var finalAttributes = attributes ?? new PictureAttributes();
        if (string.IsNullOrEmpty(finalAttributes.ImgAlt))
        {
            finalAttributes.ImgAlt = viewModel.AltText;
        }

        // Use Picture.Render from PictureRenderer library
        var renderedHtml = global::PictureRenderer.Picture.Render(imagePath, profileToUse, finalAttributes);
        return new HtmlString(renderedHtml);
    }

    /// <summary>
    /// Overload without PictureAttributes for simpler usage
    /// </summary>
    public static async Task<IHtmlContent> Picture(
        this IHtmlHelper htmlHelper,
        ContentReference imageReference,
        PictureProfileBase pictureProfile)
    {
        return await htmlHelper.Picture(imageReference, pictureProfile, null, SvgRenderMode.ImageSrc);
    }

    /// <summary>
    /// Clone a PictureProfileBase to avoid modifying static instances
    /// </summary>
    private static PictureProfileBase CloneProfile(PictureProfileBase original)
    {
        return original switch
        {
            CloudflareProfile cf => new CloudflareProfile
            {
                SrcSetWidths = cf.SrcSetWidths?.ToArray(),
                Sizes = cf.Sizes?.ToArray(),
                Quality = cf.Quality,
                AspectRatio = cf.AspectRatio,
                MultiImageMediaConditions = cf.MultiImageMediaConditions?.ToArray(),
                IsDisabled = cf.IsDisabled
            },
            _ => original // For other profile types, return as-is
        };
    }
}