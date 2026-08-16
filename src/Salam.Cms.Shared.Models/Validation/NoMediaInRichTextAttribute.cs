namespace Salam.Cms.Shared.Models.Validation;

using EPiServer;
using EPiServer.Core;
using EPiServer.Core.Html.StringParsing;
using EPiServer.Web;
using EPiServer.Web.Routing;
using System.ComponentModel.DataAnnotations;

[AttributeUsage(
    AttributeTargets.Property)]
public sealed class NoMediaInRichTextAttribute : ValidationAttribute
{
    private const string DefaultMessage = "The {0} field must not contain any media items.";

    public NoMediaInRichTextAttribute() : base(DefaultMessage)
    {
    }

    public override bool IsValid(object? value)
    {
        if (value is null)
        {
            return true;
        }

        return value switch
        {
            XhtmlString richText => IsValidRichText(richText),
            _ => true
        };
    }

    private static bool IsValidRichText(XhtmlString? richText)
    {
        if (richText?.Fragments == null || richText.IsEmpty)
        {
            return true;
        }

        var hasImgTag = richText.Fragments
                                .Where(x => x is StaticFragment)
                                .Any(x => x.InternalFormat.ToLowerInvariant().Contains("<img"));

        var hasOtherMediaTag = richText.Fragments
                                       .Where(x => x is UrlFragment)
                                       .Any(x => UrlResolver.Current.GetUrl(new UrlBuilder(x.InternalFormat), ContextMode.Default).ToLowerInvariant().Contains('.'));

        return !hasImgTag && !hasOtherMediaTag;
    }
}