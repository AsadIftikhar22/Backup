namespace Salam.Cms.Web.Features.CallToAction.Abstract;

using EPiServer.Core;
using Salam.Cms.Web.Features.Common.Interfaces;

/// <summary>
/// Used to define a call to action content.
/// </summary>
public interface IHowToGetESimBlock : ISiteContentBlock
{
    public string? NavigationTitle { get; set; }
    public XhtmlString? MainDescription { get; set; }
    public string? Heading { get; set; }
}
