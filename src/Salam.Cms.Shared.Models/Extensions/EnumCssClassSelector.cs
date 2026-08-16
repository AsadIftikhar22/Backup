namespace Salam.Cms.Shared.Models.Extensions;

using System;
using System.Linq;
using System.Reflection;

public static class CssClassExtension
{
    public static string GetCssClass(this Enum input)
    {
        var enumFieldInfo = input.GetType().GetTypeInfo().GetDeclaredField(input.ToString());

        var cssClassAttribute = enumFieldInfo?.CustomAttributes.SingleOrDefault(x => x.AttributeType == typeof(CssClassAttribute));

        var cssClass = cssClassAttribute?.ConstructorArguments?.SingleOrDefault().Value?.ToString();

        return cssClass ?? string.Empty;
    }
}