namespace Salam.Cms.Web.Features.Accordion.Validation;

using EPiServer.Validation;
using Salam.Cms.Shared.Models.Extensions;
using Salam.Cms.Web.Features.Accordion.Models;
using System.Collections.Generic;

public sealed class B2BAccordionBlockValidator : IValidate<B2BAccordionBlock>
{
    public IEnumerable<ValidationError> Validate(B2BAccordionBlock instance)
    {
        if (instance.Items.IsNullOrEmpty())
        {
            yield return new ValidationError
            {
                ErrorMessage = "Please consider adding at least one accordion item for the accordion block to be rendered correctly.",
                PropertyName = nameof(instance.Items),
                Severity = ValidationErrorSeverity.Warning
            };
        }

    }
}