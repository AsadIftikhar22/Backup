namespace Salam.Cms.Shared.Models.Media;

using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Framework.DataAnnotations;
using ImagePointEditor;
using Salam.Cms.Shared.Models.Common;
using System.ComponentModel.DataAnnotations;

[ContentType(
    DisplayName = "Image Content",
    GUID = "31e8200f-aec9-4463-a31d-053aec3f03a4",
    Description = "Images can be uploaded as JPG, JPE, JPEG, PNG or GIF formats only",
    GroupName = GroupNames.Content)]
[MediaDescriptor(ExtensionString = "jpg,jpeg,jpe,png,gif")]
public class ImageContent : ImageData, IImageContent
{
    [Display(
        Name = "Alternative Text",
        Description = "Description of the image to be rendered in the image's alt property.",
        GroupName = GroupNames.Content,
        Order = 10)]
    public virtual string? AltText { get; set; }

    [UIHint(ImagePoint.UIHint)]
    [Display(
        Name = "Focal Point",
        Description = "Defines the central point for an image when it is being resized and cropped for use within a block.",
        GroupName = GroupNames.Content,
        Order = 20)]
    public virtual string? ImageFocalPoint { get; set; }

    public override void SetDefaultValues(ContentType contentType)
    {
        base.SetDefaultValues(contentType);

        ImageFocalPoint = "0.5|0.5";
    }
}