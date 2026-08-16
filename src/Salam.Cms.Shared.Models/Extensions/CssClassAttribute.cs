namespace Salam.Cms.Shared.Models.Extensions;

using System;

[AttributeUsage(AttributeTargets.Field)]
public sealed class CssClassAttribute : Attribute
{
    public CssClassAttribute(string cssClass)
    {
        CssClass = cssClass;
    }

    public string CssClass { get; }
}
