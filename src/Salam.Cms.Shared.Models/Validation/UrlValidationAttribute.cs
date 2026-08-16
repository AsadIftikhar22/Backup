namespace Salam.Cms.Shared.Models.Validation;

using System.ComponentModel.DataAnnotations;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public sealed class UrlValidationAttribute : ValidationAttribute
{
    private const string DefaultErrorMessage = "The {0} field must not be valid url beginning with http:// or https://.";

    public UrlValidationAttribute() : base(DefaultErrorMessage)
    {
    }

    public override bool IsValid(object? value)
    {
        var valueAsString = value as string;
        if (string.IsNullOrEmpty(valueAsString))
        {
            return true;
        }

        return Uri.IsWellFormedUriString(valueAsString, UriKind.Absolute)
            && (valueAsString.StartsWith("http://", StringComparison.OrdinalIgnoreCase)
             || valueAsString.StartsWith("https://", StringComparison.OrdinalIgnoreCase));
    }
}