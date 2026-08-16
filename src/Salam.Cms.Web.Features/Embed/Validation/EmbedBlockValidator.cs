namespace Salam.Cms.Web.Features.Embed.Validation;

using EPiServer.Validation;
using HtmlAgilityPack;
using Salam.Cms.Shared.Models.Extensions;
using Salam.Cms.Web.Features.Embed.Models;
using System.Collections.Generic;

public sealed class EmbedBlockValidator : IValidate<EmbedBlock>
{
    public IEnumerable<ValidationError> Validate(EmbedBlock instance)
    {
        var htmlErrors = GetHtmlErrors(instance.EmbedContent);

        if (htmlErrors.IsNullOrEmpty())
        {
            yield break;
        }

        var errors = string.Join(". ", htmlErrors);
        yield return new ValidationError
        {
            ErrorMessage = $"HTML contains errors: {errors}",
            PropertyName = nameof(instance.EmbedContent),
            Severity = ValidationErrorSeverity.Error
        };
    }

    private static IList<string> GetHtmlErrors(string? rawHtml)
    {
        if (string.IsNullOrWhiteSpace(rawHtml))
        {
            return new List<string>(0);
        }

        var htmlDocument = new HtmlDocument
        {
            OptionFixNestedTags = false,
            DisableServerSideCode = true
        };
        htmlDocument.LoadHtml(rawHtml);


        return htmlDocument.ParseErrors
                           .Take(5)
                           .Select(x => $"{x.Reason} on line {x.Line}")
                           .ToList();
    }
}