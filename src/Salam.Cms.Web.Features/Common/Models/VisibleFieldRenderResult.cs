namespace Salam.Cms.Web.Features.Common.Models;

/// <summary>
/// Represents the result of determining if a product field should be rendered.
/// </summary>
public sealed class VisibleFieldRenderResult
{
    /// <summary>
    /// Gets a value indicating whether the field should be rendered.
    /// </summary>
    public bool ShouldRender { get; }

    /// <summary>
    /// Gets the template modifier to be applied, if any.
    /// </summary>
    public string TemplateModifier { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="VisibleFieldRenderResult"/> class.
    /// </summary>
    /// <param name="shouldRender">Whether the field should be rendered.</param>
    /// <param name="templateModifier">The template modifier to apply (if any).</param>
    public VisibleFieldRenderResult(bool shouldRender, string templateModifier = "")
    {
        ShouldRender = shouldRender;
        TemplateModifier = templateModifier;
    }
}