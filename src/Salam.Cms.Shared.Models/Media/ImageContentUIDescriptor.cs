namespace Salam.Cms.Shared.Models.Media;

using EPiServer.Shell;

/// <summary>
/// Used to help Optimizely CMS UI Recognize the <see cref="IImageContent"/> interface.
/// This allows us to simplify consumption of multiple image formats.
/// </summary>
[UIDescriptorRegistration]
public sealed class ImageContentUIDescriptor : UIDescriptor<IImageContent>
{
}