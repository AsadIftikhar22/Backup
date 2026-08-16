namespace Salam.Cms.Shared.Models.Common.Enums;

using Salam.Cms.Shared.Models.Extensions;

public enum ThemeOption
{
    Default,

    [CssClass("--light")]
    Light,

    [CssClass("--dark")]
    Dark
}