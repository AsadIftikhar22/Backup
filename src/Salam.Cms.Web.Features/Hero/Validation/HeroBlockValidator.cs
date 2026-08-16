namespace Salam.Cms.Web.Features.Hero.Validation;

using EPiServer.Find.Helpers.Text;
using EPiServer.Validation;
using Salam.Cms.Shared.Models.Extensions;
using Salam.Cms.Web.Features.Hero.Models;

public sealed class HeroBlockValidator : IValidate<HeroBlock>
{
    public IEnumerable<ValidationError> Validate(HeroBlock instance)
    {
        if (string.IsNullOrWhiteSpace(instance.Heading) && !instance.Description.IsNullOrEmpty())
        {
            yield return new ValidationError
            {
                ErrorMessage = "Title must have a value if Description has a value. Alternatively, remove the Description value.",
                PropertyName = nameof(instance.Heading),
                Severity = ValidationErrorSeverity.Error
            };
        }

        if (instance.LinkItems != null && !instance.LinkItems.Any())
        {
            yield return new ValidationError
            {
                ErrorMessage = "Please consider adding links for the call to action buttons otherwise they won't be visible in the hero block.",
                PropertyName = nameof(instance.LinkItems),
                Severity = ValidationErrorSeverity.Warning
            };
        }
    }
}