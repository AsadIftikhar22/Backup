namespace Salam.Cms.Web.Features.Hero.Validation;
using EPiServer.Validation;
using Salam.Cms.Shared.Models.Extensions;
using Salam.Cms.Web.Features.Hero.Models;
using System.Collections.Generic;

public sealed class HeroLandingBlockValidator : IValidate<HeroLandingBlock>
{
    public IEnumerable<ValidationError> Validate(HeroLandingBlock instance)
    {
        if (instance.Items.IsNullOrEmpty())
        {
            yield return new ValidationError
            {
                ErrorMessage = "Please consider adding one or more hero landing item blocks for the hero landing block to be rendered correctly.",
                PropertyName = nameof(instance.Items),
                Severity = ValidationErrorSeverity.Warning
            };
        }
    }
}
