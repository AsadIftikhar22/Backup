namespace Salam.Cms.Web.Infrastructure.Sanitization;

using Ganss.Xss;

internal static class ContentSanitizer
{
    internal static HtmlSanitizer Build()
    {
        var sanitizer = new HtmlSanitizer();

        AllowSvgTags(sanitizer);
        AllowImageUtilityServiceTags(sanitizer);

        return sanitizer;
    }

    static void AllowSvgTags(HtmlSanitizer sanitizer)
    {
        sanitizer.AllowedTags.Add("svg");
        sanitizer.AllowedTags.Add("circle");
        sanitizer.AllowedTags.Add("rect");
        sanitizer.AllowedTags.Add("path");
        sanitizer.AllowedTags.Add("text");
        sanitizer.AllowedTags.Add("g");

        sanitizer.AllowedAttributes.Add("width");
        sanitizer.AllowedAttributes.Add("height");
        sanitizer.AllowedAttributes.Add("viewBox");
        sanitizer.AllowedAttributes.Add("fill");
        sanitizer.AllowedAttributes.Add("d");
        sanitizer.AllowedAttributes.Add("stroke");
        sanitizer.AllowedAttributes.Add("x");
        sanitizer.AllowedAttributes.Add("y");
        sanitizer.AllowedAttributes.Add("font-size");
        sanitizer.AllowedAttributes.Add("text-anchor");
    }

    static void AllowImageUtilityServiceTags(HtmlSanitizer sanitizer)
    {
        string[] allowedAttributes = new string[]
        {
            "xmlns", "id", "x1", "y1", "x2", "y2", "gradientUnits",
            "stop-color", "offset", "width", "height", "clip-path"
        };

        string[] allowedTags = new string[]
        {
            "defs", "linearGradient", "stop", "clipPath", "rect"
        };

        foreach (var attribute in allowedAttributes)
        {
            if (!sanitizer.AllowedAttributes.Contains(attribute))
            {
                sanitizer.AllowedAttributes.Add(attribute);
            }
        }

        foreach (var tag in allowedTags)
        {
            if (!sanitizer.AllowedTags.Contains(tag))
            {
                sanitizer.AllowedTags.Add(tag);
            }
        }
    }
}
