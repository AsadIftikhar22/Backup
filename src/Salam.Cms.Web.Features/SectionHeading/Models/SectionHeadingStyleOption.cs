namespace Salam.Cms.Web.Features.SectionHeading.Models;
using Salam.Cms.Shared.Models.Extensions;

public enum SectionHeadingStyleOption
{
    Default,

    [CssClass("section-heading--transparent")]
    Outline,

    [CssClass("section-heading--faded")]
    Faded
}