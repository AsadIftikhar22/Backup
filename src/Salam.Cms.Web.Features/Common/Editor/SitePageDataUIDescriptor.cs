namespace Salam.Cms.Web.Features.Common.Editor;

using EPiServer.Shell;
using Salam.Cms.Web.Features.Common.Interfaces;

/// <summary>
/// Used to help Optimizely CMS UI Recognize the <see cref="ISitePageData"/> interface.
/// This allows us to simplify which pages are allowed in main content areas.
/// </summary>
[UIDescriptorRegistration]
public class SitePageDataUIDescriptor : UIDescriptor<ISitePageData>
{
}
