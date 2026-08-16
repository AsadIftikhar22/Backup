namespace Salam.Cms.Web.Infrastructure.TagHelpers;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

[HtmlTargetElement("inline-svg")]
public class InlineSvgTagHelper : TagHelper
{
    private readonly IWebHostEnvironment _webHostEnvironment;

    public InlineSvgTagHelper(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }

    /// <summary>
    /// A filepath to a SVG on disk such as /assets/icon.svg
    /// </summary>
    [HtmlAttributeName("src")]
    public string? FileSource { get; set; }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        if (string.IsNullOrWhiteSpace(FileSource))
        {
            // Nothing to render, so don't render anything
            output.SuppressOutput();
            return;
        }

        var cleanContent = await GetFileContents();
        if (string.IsNullOrWhiteSpace(cleanContent))
        {
            // Nothing to render, so don't render anything
            output.SuppressOutput();
            return;
        }

        SetOutput(output, cleanContent);
    }

    private async Task<string?> GetFileContents()
    {
        // SVG fileContents to render to DOM
        var fileContents = string.Empty;

        if (!string.IsNullOrWhiteSpace(FileSource))
        {
            // Check string src filepath ends with .svg
            if (!FileSource.EndsWith(".svg", StringComparison.InvariantCultureIgnoreCase))
            {
                return null;
            }

            // Get file from wwwRoot using a path such as
            // /assets/logo/my-logo.svg as opposed to wwwRoot/assets/logo/my-logo.svg
            // Can we or should we support ~ in paths at root?
            var webRoot = _webHostEnvironment.WebRootFileProvider;
            var file = webRoot.GetFileInfo(FileSource);

            // Ensure file exists in wwwroot path
            if (!file.Exists)
            {
                return null;
            }

            using var reader = new StreamReader(file.CreateReadStream());
            fileContents = await reader.ReadToEndAsync();
        }

        // Sanitize SVG (Is there anything in Umbraco to reuse)
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

        return cleanedFileContents;
    }

    private static void SetOutput(TagHelperOutput output, string? content)
    {
        output.Attributes.RemoveAll("src");
        output.Attributes.RemoveAll("cache");

        output.TagName = null;
        output.Content.SetHtmlContent(content);
    }
}