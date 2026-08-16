namespace Salam.Cms.Web.Features.Common.Components.MetaData;

using EPiServer.Core;

using Microsoft.AspNetCore.Html;

public class MetaDataViewModel
{
    public string? Title { get; set; }

    public string? Description { get; set; }

    public string? RelativeCategoryUrl { get; set; }

    public string? Robots { get; set; }

    public string? Action { get; set; }

    public ContentReference? ContentLink { get; set; }

    public ContentReference? CanonicalLink { get; set; }

    public bool UsingAlternativeCanonicalLink { get; set; }

    public bool RenderAlternativeLinks { get; set; }

    public List<string>? Categories { get; set; }

    public HtmlString? PublishedDateTime { get; set; }

    public HtmlString? ModifiedDateTime { get; set; }

    public HtmlString? ExpirationDateTime { get; set; }

    public bool HasCategoryRouting { get; set; } = false;
    public string CanonicalUrl { get; set; } = string.Empty;
}