namespace Salam.Cms.Shared.Models.Blocks;

using EPiServer.Shell;

/// <summary>
/// Used to help Optimizely CMS UI Recognize the <see cref="IContentBlock"/> interface.
/// This allows us to simplify which pages are allowed in main content areas.
/// </summary>
[UIDescriptorRegistration]
public class ContentBlockUIDescriptor : UIDescriptor<IContentBlock>
{
}
