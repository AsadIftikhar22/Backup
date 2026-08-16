namespace Salam.Cms.Web.Features.Common.Interfaces;

using EPiServer.Core;

/// <summary>
/// Used to define a standard content block to be allowed in main content areas.
/// This combined with <see cref="SiteContentBlockUIDescriptor"/> simplify these declarations.
/// </summary>
public interface ISiteContentBlock : IContentData
{
}