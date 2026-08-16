namespace Salam.Cms.Shared.Models.Pages;

using EPiServer.Shell;

/// <summary>
/// Used to help Optimizely CMS UI Recognize the <see cref="ISharedPageData"/> interface.
/// This allows us to simplify which pages are allowed in main content areas.
/// </summary>
[UIDescriptorRegistration]
public class SharedPageDataUIDescriptor : UIDescriptor<ISharedPageData>
{
}
