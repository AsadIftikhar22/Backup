namespace Salam.Cms.Shared.Models.Media;

using EPiServer.Core;
using EPiServer.DataAnnotations;
using EPiServer.Framework.Blobs;
using EPiServer.Framework.DataAnnotations;
using Salam.Cms.Shared.Models.Common;
using System.ComponentModel.DataAnnotations;

[ContentType(
    DisplayName = "Vector Image Content",
    GUID = "e113f322-c6fe-4567-9b5f-5cc5206873fd",
    Description = "Images can be uploaded as SVG formats only",
    GroupName = GroupNames.Content)]
[MediaDescriptor(ExtensionString = "svg")]
public class VectorImageContent : ImageData, IImageContent
{
    [Display(
        Name = "Alternative Text",
        Description = "Description of the image to be rendered in the image's alt property.",
        GroupName = GroupNames.Content,
        Order = 10)]
    public virtual string? AltText { get; set; }

    public string? ImageFocalPoint => null;

    /// <summary>
    /// Gets the generated thumbnail for this media.
    /// </summary>
    public override Blob Thumbnail
    {
        get { return BinaryData; }
    }
}