namespace Salam.Cms.Web.Infrastructure.TagHelpers;

using EPiServer;
using EPiServer.Core;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Logging;
using Salam.Cms.Shared.Models.Extensions;
using Salam.Cms.Shared.Models.Media;
using System;
using System.Text.RegularExpressions;
using System.Xml;

[HtmlTargetElement("inline-content-svg")]
public class InlineContentSvgTagHelper : TagHelper
{
    private readonly IContentLoader _contentLoader;

    private readonly ILogger<InlineContentSvgTagHelper> _logger;

    public InlineContentSvgTagHelper(
        IContentLoader contentLoader,
        ILogger<InlineContentSvgTagHelper> logger)
    {
        _contentLoader = contentLoader;
        _logger = logger;
    }

    /// <summary>
    /// A filepath to a SVG on disk such as /assets/icon.svg
    /// </summary>
    [HtmlAttributeName("src")]
    public ContentReference? ContentSource { get; set; }

    /// <summary>
    /// A css class to be applied to the svg element
    /// </summary>
    [HtmlAttributeName("class")]
    public string? CssClass { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        if (ContentSource.IsNullOrEmpty())
        {
            // Nothing to render, so don't render anything
            output.SuppressOutput();
            return;
        }

        var cleanContent = GetFileContents(ContentSource);
        if (string.IsNullOrWhiteSpace(cleanContent))
        {
            // Nothing to render, so don't render anything
            output.SuppressOutput();
            return;
        }

        SetOutput(output, cleanContent);
    }

    private string GetFileContents(ContentReference imageReference)
    {
        try
        {
            // SVG fileContents to render to DOM
            if (_contentLoader.TryGet<VectorImageContent>(imageReference, out var vectorImage))
            {
                var xmlDoc = new XmlDocument();
                xmlDoc.Load(vectorImage.BinaryData.OpenRead());

                var fileContents = xmlDoc.InnerXml;

                // Sanitize SVG
                // https://stackoverflow.com/questions/65247336/is-there-anyway-to-sanitize-svg-file-in-c-any-libraries-anything/65375485#65375485
                var cleanedFileContents = Regex.Replace(fileContents,
                    @"<script.*?script>",
                    @"",
                    RegexOptions.IgnoreCase | RegexOptions.Singleline,
                    TimeSpan.FromMilliseconds(100));

                cleanedFileContents = Regex.Replace(cleanedFileContents,
                    @"javascript:",
                    @"syntax:error:",
                    RegexOptions.IgnoreCase | RegexOptions.Singleline,
                    TimeSpan.FromMilliseconds(100));

                if (!string.IsNullOrWhiteSpace(CssClass))
                {
                    cleanedFileContents = cleanedFileContents.Replace("<svg", $"<svg class=\"{CssClass}\"");
                }

                return cleanedFileContents;
            }
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Failed to retrieve svg content with id of {ImageReference}", imageReference);
        }

        return string.Empty;
    }

    private static void SetOutput(TagHelperOutput output, string? content)
    {
        output.Attributes.RemoveAll("src");
        output.Attributes.RemoveAll("cache");

        output.TagName = null;
        output.Content.SetHtmlContent(content);
    }
}