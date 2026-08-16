namespace Salam.Cms.Shared.Models.Validation;

using System.ComponentModel.DataAnnotations;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class NotNullOrEmptyAttribute : ValidationAttribute
{
    private const string DefaultErrorMessage = "The {0} field must not be empty";

    public NotNullOrEmptyAttribute() : base(DefaultErrorMessage)
    {
    }

    public override bool IsValid(object? value)
    {
        if (value is null)
        {
            return false;
        }

        return value switch
        {
            Guid guid => guid != Guid.Empty,
            string text => !string.IsNullOrWhiteSpace(text),
            _ => true
        };
    }
}