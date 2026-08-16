namespace Salam.Cms.Tests.Extensions;

using System.Collections.ObjectModel;
using System.Reflection;

public static class PropertyInfoExtensions
{
    public static bool IsNullable(this PropertyInfo property) =>
        IsNullableHelper(property.PropertyType, property.DeclaringType, property.CustomAttributes);

    public static bool IsNullable(this FieldInfo field) =>
        IsNullableHelper(field.FieldType, field.DeclaringType, field.CustomAttributes);

    public static bool IsNullable(this ParameterInfo parameter) =>
        IsNullableHelper(parameter.ParameterType, parameter.Member, parameter.CustomAttributes);

    private static bool IsNullableHelper(
        Type memberType,
        MemberInfo declaringType,
        IEnumerable<CustomAttributeData> customAttributes)
    {
        if (memberType.IsValueType)
        {
            return memberType.FullName?.StartsWith("System.Nullable") ?? false;
        }

        const string nullableAttributeName = "System.Runtime.CompilerServices.NullableAttribute";
        const string nullableContextAttributeName = "System.Runtime.CompilerServices.NullableContextAttribute";

        var nullable = customAttributes.FirstOrDefault(x => x.AttributeType.FullName == nullableAttributeName);
        if (nullable != null && nullable.ConstructorArguments.Count == 1)
        {
            var attributeArgument = nullable.ConstructorArguments[0];
            if (attributeArgument.ArgumentType == typeof(byte[]))
            {
                var args = (ReadOnlyCollection<CustomAttributeTypedArgument>)attributeArgument.Value!;
                if (args.Count > 0 && args[0].ArgumentType == typeof(byte))
                {
                    return (byte)args[0].Value! == 2;
                }
            }
            else if (attributeArgument.ArgumentType == typeof(byte))
            {
                return (byte)attributeArgument.Value! == 2;
            }
        }

        for (var type = declaringType; type != null; type = type.DeclaringType)
        {
            var context = type.CustomAttributes
                              .FirstOrDefault(x => x.AttributeType.FullName == nullableContextAttributeName);
            if (context != null &&
                context.ConstructorArguments.Count == 1 &&
                context.ConstructorArguments[0].ArgumentType == typeof(byte))
            {
                return (byte)context.ConstructorArguments[0].Value! == 2;
            }
        }

        // Couldn't find a suitable attribute
        return false;
    }
}