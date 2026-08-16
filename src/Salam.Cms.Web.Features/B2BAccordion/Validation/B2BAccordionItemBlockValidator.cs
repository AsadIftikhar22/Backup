namespace Salam.Cms.Web.Features.Accordion.Validation;
using EPiServer.Validation;
using Salam.Cms.Shared.Models.Extensions;
using Salam.Cms.Web.Features.Accordion.Models;
using System.Collections.Generic;

public sealed class B2BAccordionItemBlockValidator : IValidate<B2BAccordionItemBlock>
{
    public IEnumerable<ValidationError> Validate(B2BAccordionItemBlock instance)
    {
        if (string.IsNullOrEmpty(instance.Heading))
        {
            yield return new ValidationError
            {
                ErrorMessage = "Please consider adding a heading to display on the accordion item for the accordion item block to be rendered correctly.",
                PropertyName = nameof(instance.Heading),
                Severity = ValidationErrorSeverity.Warning
            };
        }

        if (instance.Links.IsNullOrEmpty())
        {
            yield return new ValidationError
            {
                ErrorMessage = "Please consider populating this field for the accordion item to be rendered correctly.",
                PropertyName = nameof(instance.Links),
                Severity = ValidationErrorSeverity.Warning
            };
        }
    }
}