namespace Salam.Cms.Shared.Models.Common.Enums;

using Salam.Cms.Shared.Models.Extensions;

public enum LayoutOption
{
    MediaThenContent,

    [CssClass("--content-first")]
    ContentThenMedia
}