namespace Salam.Cms.Web.Features.CallToAction.Abstract;

using EPiServer.Core;
using EPiServer.SpecializedProperties;
using Salam.Cms.Web.Features.Common.Interfaces;

/// <summary>
/// Used to define a call to action content.
/// </summary>
public interface ICallToAction : ISiteContentBlock
{
    // public CardLayoutOption LayoutOption { get; set; }

    public ContentReference? Media { get; set; }

    public string? BadgeText { get; set; }

    public string? HeadingLineOne { get; set; }

    public string? HeadingLineTwo { get; set; }

    public XhtmlString? MainBody { get; set; }

    public LinkItemCollection? LinkItems { get; set; }
}
