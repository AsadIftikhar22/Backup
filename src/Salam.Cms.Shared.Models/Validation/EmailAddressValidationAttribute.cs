namespace Salam.Cms.Shared.Models.Validation;

using System.ComponentModel.DataAnnotations;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public sealed class EmailAddressValidationAttribute : ValidationAttribute
{
    public EmailAddressValidationAttribute()
    {
        ErrorMessage = "The {0} field must be a valid email address.";
    }

    /// <summary>
    /// This has the same validation as <see cref="EmailAddressAttribute"/>
    /// with the exception that empty strings no longer cause an error
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public override bool IsValid(object? value)
    {
        var valueAsString = value as string;
        if (string.IsNullOrEmpty(valueAsString))
        {
            return true;
        }

        // only return true if there is only 1 '@' character
        // and it is neither the first nor the last character
        var index = valueAsString.IndexOf('@');

        return index > 0 &&
               index != valueAsString.Length - 1 &&
               index == valueAsString.LastIndexOf('@');
    }
}