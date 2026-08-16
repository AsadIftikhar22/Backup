namespace Salam.Cms.Shared.Models.Media;

using EPiServer.Core;
using EPiServer.DataAnnotations;
using EPiServer.Framework.DataAnnotations;
using Salam.Cms.Shared.Models.Common;
using System.ComponentModel.DataAnnotations;

[ContentType(
    DisplayName = "Audio Content",
    GUID = "EA8FC3F2-AA60-429B-AE67-4177381FA621",
    Description = "Audio files can be uploaded in MP3, M4A, OGA or WAV format only.",
    GroupName = GroupNames.Content)]
[MediaDescriptor(ExtensionString = "mp3,m4a,oga,wav")]
public class AudioContent : MediaData
{
    [Display(
        Name = "Transcript",
        Description = "A transcript of the audio track.",
        GroupName = GroupNames.Content,
        Order = 10)]
    public virtual XhtmlString? Transcript { get; set; }
}