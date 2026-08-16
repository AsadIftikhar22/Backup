namespace Salam.Cms.Shared.Models.Media;

using EPiServer.Core;
using EPiServer.DataAnnotations;
using EPiServer.Framework.DataAnnotations;
using EPiServer.Web;
using Salam.Cms.Shared.Models.Common;
using System.ComponentModel.DataAnnotations;

[ContentType(
    DisplayName = "Video Content",
    GUID = "85468104-E26F-47E5-A317-FC9B83D3CBA6",
    Description = "Videos can be uploaded in MP4, M4V, MPV, WEBM and OGV formats only.",
    GroupName = GroupNames.Content)]
[MediaDescriptor(ExtensionString = "mp4,m4v,mpv,webM,ogv")]
public class VideoContent : VideoData
{
    [UIHint(UIHint.Image)]
    [Display(
        Name = "Preview Image",
        Description = "An optional image to display as a preview or thumbnail for this video",
        GroupName = GroupNames.Content,
        Order = 10)]
    public virtual ContentReference? PreviewImage { get; set; }

    [Display(
        Name = "Transcript",
        Description = "A transcript of the audio track.",
        GroupName = GroupNames.Content,
        Order = 20)]
    public virtual XhtmlString? Transcript { get; set; }
}