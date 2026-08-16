namespace Salam.Cms.Shared.Models.Validation;

using EPiServer.Core;
using EPiServer.Core.Html.StringParsing;
using System.ComponentModel.DataAnnotations;

[AttributeUsage(AttributeTargets.Property)]
public class NoBlocksInRichTextAttribute : ValidationAttribute
{
    private const string DefaultErrorMessage = "The {0} field must not contain any blocks.";

    public NoBlocksInRichTextAttribute() : base(DefaultErrorMessage)
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

        var hasBlockFragment = richText.Fragments.Any(x => x is ContentFragment);
        var hasPersonalizedBlock = richText.Fragments
                                           .Where(x => x is PersonalizedContentFragment)
                                           .Cast<PersonalizedContentFragment>()
                                           .Any(x => x.Fragments.Any(y => y is ContentFragment));

        return !hasBlockFragment && !hasPersonalizedBlock;
    }
}