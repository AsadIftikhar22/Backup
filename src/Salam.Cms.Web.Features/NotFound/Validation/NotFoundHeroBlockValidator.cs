namespace Salam.Cms.Web.Features.NotFound.Validation;

using EPiServer.Validation;
using Salam.Cms.Shared.Models.Extensions;
using Salam.Cms.Web.Features.NotFound.Models;
using System.Collections.Generic;

public sealed class NotFoundHeroBlockValidator : IValidate<NotFoundHeroBlock>
{
    public IEnumerable<ValidationError> Validate(NotFoundHeroBlock instance)
    {
        if (string.IsNullOrEmpty(instance.Title))
        {
            yield return new ValidationError
            {
                ErrorMessage = "Please consider adding a title for the not found hero block to be rendered correctly.",
                PropertyName = nameof(instance.Title),
                Severity = ValidationErrorSeverity.Warning
            };
        }

        if (instance.Image.IsNullOrEmpty())
        {
            yield return new ValidationError
            {
                ErrorMessage = "Please consider adding an image for the not found hero block to be rendered correctly.",
                PropertyName = nameof(instance.Image),
                Severity = ValidationErrorSeverity.Warning
            };
        }
    }
}