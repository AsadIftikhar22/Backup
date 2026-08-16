namespace Salam.Cms.Web.Features.ClientResources.Enums;
using System.ComponentModel.DataAnnotations;

public enum EmbedRenderLocationOption
{
    [Display(Name = "Inline")]
    Inline,

    [Display(Name = "Head")]
    Head,

    [Display(Name = "Body Start")]
    BodyStart,

    [Display(Name = "Body End")]
    BodyEnd
}
