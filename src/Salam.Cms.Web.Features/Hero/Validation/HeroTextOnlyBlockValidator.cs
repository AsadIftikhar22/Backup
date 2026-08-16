namespace Salam.Cms.Web.Features.Hero.Validation;

using EPiServer.Validation;
using Salam.Cms.Shared.Models.Extensions;
using Salam.Cms.Web.Features.Hero.Models;

public sealed class HeroTextOnlyBlockValidator : IValidate<HeroTextOnlyBlock>
{
    public IEnumerable<ValidationError> Validate(HeroTextOnlyBlock instance)
    {
        if (string.IsNullOrWhiteSpace(instance.Heading) && instance.Description.IsNullOrEmpty())
        {
            yield return new ValidationError
            {
                ErrorMessage = "Please consider adding at least one 'heading' or 'description' property for the block to be rendered correctly.",
                PropertyName = nameof(instance.Heading),
                Severity = ValidationErrorSeverity.Warning
            };
        }
    }
}
