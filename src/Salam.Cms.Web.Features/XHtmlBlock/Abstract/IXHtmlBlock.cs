namespace Salam.Cms.Web.Features.CallToAction.Abstract;

using EPiServer.Core;
using Salam.Cms.Web.Features.Common.Interfaces;

/// <summary>
/// Used to define a call to action content.
/// </summary>
public interface IXHtmlBlock : ISiteContentBlock
{
    public XhtmlString? CSSBody { get; set; }
    public string? NavigationTitle { get; set; }
    public XhtmlString? HTMLBody { get; set; }
}
