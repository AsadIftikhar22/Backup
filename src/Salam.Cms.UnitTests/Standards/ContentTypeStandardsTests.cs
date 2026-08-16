namespace Salam.Cms.UnitTests.Standards;

using EPiServer;
using EPiServer.Core;
using EPiServer.DataAnnotations;
using EPiServer.Shell.ObjectEditing;
using EPiServer.SpecializedProperties;
using Salam.Cms.Shared.Models.Common.Properties;
using Salam.Cms.Tests.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

[TestFixture]
public class ContentTypeStandardsTests
{
    private readonly Type[] _nullableTypes =
    {
        typeof(ContentReference),
        typeof(ContentArea),
        typeof(IList<ContentReference>),
        typeof(string),
        typeof(LinkItemCollection),
        typeof(Url),
        typeof(XhtmlString)
    };

    private readonly Type[] _nonNullableTypes =
    {
        typeof(bool?),
        typeof(int?),
        typeof(decimal?),
        typeof(double?)
    };

    [Test]
    [TestCaseSource(typeof(ContentTypeStandardsTestCases), nameof(ContentTypeStandardsTestCases.PageDataPropertyTestCases))]
    public void VerifiesPropertiesOnPageDataTypesHaveADisplayAttribute(Type pageDataType, PropertyInfo property)
    {
        if (IsNotScaffolded(property))
        {
            return;
        }

        Assert.Multiple(() =>
        {
            var displayAttribute = property.GetCustomAttributes(typeof(DisplayAttribute), false)
                                           .OfType<DisplayAttribute>()
                                           .FirstOrDefault();

            Assert.That(displayAttribute, Is.Not.Null, $"{pageDataType.Name}.{property.Name} should be decorated with a [Display] attribute.");
            Assert.That(displayAttribute.Order, Is.GreaterThan(0), $"{pageDataType.Name}.{property.Name} should have a defined Order in the [Display] attribute.");
            Assert.That(displayAttribute.Name, Is.Not.Empty, $"{pageDataType.Name}.{property.Name} should have a defined Name in the [Display] attribute.");
            Assert.That(displayAttribute.GroupName, Is.Not.Empty, $"{pageDataType.Name}.{property.Name} should have a defined Group Name in the [Display] attribute.");
        });
    }

    [Test]
    [TestCaseSource(typeof(ContentTypeStandardsTestCases), nameof(ContentTypeStandardsTestCases.PageDataPropertyTestCases))]
    public void VerifiesPropertiesOnPageDataTypesThatShouldBeNullableTypes(Type pageDataType, PropertyInfo property)
    {
        if (ShouldValidateProperty(_nullableTypes, property, pageDataType))
        {
            Assert.That(property.IsNullable(), Is.True, $"{pageDataType.Name}.{property.Name} should be nullable.");
        }
    }

    [Test]
    [TestCaseSource(typeof(ContentTypeStandardsTestCases), nameof(ContentTypeStandardsTestCases.PageDataPropertyTestCases))]
    public void VerifiesPropertiesOnPageDataTypesThatShouldBeNonNullableTypes(Type pageDataType, PropertyInfo property)
    {
        if (ShouldValidateProperty(_nonNullableTypes, property, pageDataType))
        {
            Assert.That(property.IsNullable(), Is.True, $"{pageDataType.Name}.{property.Name} should not be nullable.");
        }
    }

    [Test]
    [TestCaseSource(typeof(ContentTypeStandardsTestCases), nameof(ContentTypeStandardsTestCases.PageDataPropertyTestCases))]
    public void VerifiesEnumPropertiesOnPageDataTypesAreDeclaredProperly(Type pageDataType, PropertyInfo property)
    {
        if (ShouldValidateEnumProperty(property, pageDataType))
        {
            Assert.Multiple(() =>
            {
                Assert.That(
                    EnumHasCorrectBackingType(property),
                    Is.True,
                    $"{pageDataType.Name}.{property.Name} should be decorated with [BackingType(typeof(PropertyNumber))].");

                Assert.That(
                    EnumHasZeroValue(property),
                    Is.True,
                    $"{pageDataType.Name}.{property.Name} should be an enum with a zero value.");

                Assert.That(
                    EnumHasEditorDescriptor(property),
                    Is.True,
                    $"{pageDataType.Name}.{property.Name} should be decorated with [EditorDescriptor(EditorDescriptorType = typeof(EnumEditorDescriptor<TEnum>))].");
            });
        }
    }

    [Test]
    [TestCaseSource(typeof(ContentTypeStandardsTestCases), nameof(ContentTypeStandardsTestCases.PageDataPropertyTestCases))]
    public void VerifiesPropertiesOnPageDataTypesAreNotBlockDataProperties(Type pageDataType, PropertyInfo property)
    {
        // Act
        var isBlockProperty = property.PropertyType.IsAssignableTo(typeof(BlockData));

        // Assert
        // While Optimizely CMS allows you to add blocks as properties on content types
        // to allow for the reuse of properties; It is not generally recommended to
        // follow this practice as it means property names and descriptions have to be 
        // very generic, which makes those titles and descriptions unhelpful for the CMS Editor.
        // Instead consider adding properly named properties to the content type,
        // If there are multiple usages of the same block property on one content type, 
        // then instead consider the use of a content area.
        Assert.That(isBlockProperty, Is.False, $"{pageDataType.Name}.{property.Name} should not be a Block property.");
    }

    [Test]
    [TestCaseSource(typeof(ContentTypeStandardsTestCases), nameof(ContentTypeStandardsTestCases.BlockDataPropertyTestCases))]
    public void VerifiesPropertiesOnBlockDataTypesHaveADisplayAttribute(Type blockDataType, PropertyInfo property)
    {
        if (IsNotScaffolded(property))
        {
            return;
        }

        Assert.Multiple(() =>
        {
            var displayAttribute = property.GetCustomAttributes(typeof(DisplayAttribute), false)
                                           .OfType<DisplayAttribute>()
                                           .FirstOrDefault();

            Assert.That(displayAttribute, Is.Not.Null, $"{blockDataType.Name}.{property.Name} should be decorated with a [Display] attribute.");
            Assert.That(displayAttribute.Order, Is.Not.EqualTo(0), $"{blockDataType.Name}.{property.Name} should have a defined Order in the [Display] attribute.");
            Assert.That(displayAttribute.Name, Is.Not.Empty, $"{blockDataType.Name}.{property.Name} should have a defined Name in the [Display] attribute.");
            Assert.That(displayAttribute.GroupName, Is.Not.Empty, $"{blockDataType.Name}.{property.Name} should have a defined Group Name in the [Display] attribute.");
        });
    }

    [Test]
    [TestCaseSource(typeof(ContentTypeStandardsTestCases), nameof(ContentTypeStandardsTestCases.BlockDataPropertyTestCases))]
    public void VerifiesPropertiesOnBlockDataTypesThatShouldBeNullableTypes(Type blockDataType, PropertyInfo property)
    {
        if (ShouldValidateProperty(_nullableTypes, property, blockDataType))
        {
            Assert.That(property.IsNullable(), Is.True, $"{blockDataType.Name}.{property.Name} should be nullable.");
        }
    }

    [Test]
    [TestCaseSource(typeof(ContentTypeStandardsTestCases), nameof(ContentTypeStandardsTestCases.BlockDataPropertyTestCases))]
    public void VerifiesPropertiesOnBlockDataTypesThatShouldBeNonNullableTypes(Type blockDataType, PropertyInfo property)
    {
        if (ShouldValidateProperty(_nonNullableTypes, property, blockDataType))
        {
            Assert.That(property.IsNullable(), Is.True, $"{blockDataType.Name}.{property.Name} should not be nullable.");
        }
    }

    [Test]
    [TestCaseSource(typeof(ContentTypeStandardsTestCases), nameof(ContentTypeStandardsTestCases.BlockDataPropertyTestCases))]
    public void VerifiesEnumPropertiesOnBlockDataTypesAreDeclaredProperly(Type blockDataType, PropertyInfo property)
    {
        if (ShouldValidateEnumProperty(property, blockDataType))
        {
            Assert.Multiple(() =>
            {
                Assert.That(
                    EnumHasCorrectBackingType(property),
                    Is.True,
                    $"{blockDataType.Name}.{property.Name} should be decorated with [BackingType(typeof(PropertyNumber))].");

                Assert.That(
                    EnumHasZeroValue(property),
                    Is.True,
                    $"{blockDataType.Name}.{property.Name} should be an enum with a zero value.");

                Assert.That(
                    EnumHasEditorDescriptor(property),
                    Is.True,
                    $"{blockDataType.Name}.{property.Name} should be decorated with [EditorDescriptor(EditorDescriptorType = typeof(EnumEditorDescriptor<TEnum>))] or [SelectOne(SelectionFactoryType = typeof(SectionThemeBasicSelectionFactory))].");
            });
        }
    }

    [Test]
    [TestCaseSource(typeof(ContentTypeStandardsTestCases), nameof(ContentTypeStandardsTestCases.BlockDataPropertyTestCases))]
    public void VerifiesPropertiesOnBlockDataTypesAreNotBlockDataProperties(Type blockDataType, PropertyInfo property)
    {
        // Act
        var isBlockProperty = property.PropertyType.IsAssignableTo(typeof(BlockData));

        // Assert
        // While Optimizely CMS allows you to add blocks as properties on content types
        // to allow for the reuse of properties; It is not generally recommended to
        // follow this practice as it means property names and descriptions have to be 
        // very generic, which makes those titles and descriptions unhelpful for the CMS Editor.
        // Instead consider adding properly named properties to the content type,
        // If there are multiple usages of the same block property on one content type, 
        // then instead consider the use of a content area.
        Assert.That(isBlockProperty, Is.False, $"{blockDataType.Name}.{property.Name} should not be a Block property.");
    }

    private static bool ShouldValidateProperty(Type[] allowedPropertyTypes, PropertyInfo property, Type expectedParentType)
    {
        return property.DeclaringType == expectedParentType && allowedPropertyTypes.Contains(property.PropertyType);
    }

    private static bool ShouldValidateEnumProperty(PropertyInfo property, Type expectedParentType)
    {
        return property.DeclaringType == expectedParentType && property.PropertyType.IsEnum;
    }

    private static bool EnumHasZeroValue(PropertyInfo property)
    {
        if (!property.PropertyType.IsEnum)
        {
            return false;
        }

        var enumValues = property.PropertyType.GetEnumValues();
        foreach (var enumValue in enumValues)
        {
            if ((int)enumValue == 0)
            {
                return true;
            }
        }

        return false;
    }

    private static bool EnumHasCorrectBackingType(PropertyInfo property)
    {
        var hasBackingType = property.GetCustomAttributes(typeof(BackingTypeAttribute), false)
                                     .OfType<BackingTypeAttribute>()
                                     .Any(x => x.BackingType == typeof(PropertyNumber));

        return hasBackingType || IsNotScaffolded(property);
    }

    private static bool EnumHasEditorDescriptor(PropertyInfo property)
    {
        var hasBackingType = property.GetCustomAttributes(typeof(EditorDescriptorAttribute), false)
                                     .OfType<EditorDescriptorAttribute>()
                                     .Any(x => x.EditorDescriptorType.GetGenericTypeDefinition() == typeof(EnumEditorDescriptor<>));

        var hasSelectOneAttribute = property.GetCustomAttributes(typeof(SelectOneAttribute), false).Any();

        return hasBackingType || hasSelectOneAttribute || IsNotScaffolded(property);
    }

    private static bool IsNotScaffolded(PropertyInfo property)
    {
        return property.GetCustomAttributes(typeof(ScaffoldColumnAttribute), false)
                       .OfType<ScaffoldColumnAttribute>()
                       .Any(x => x.Scaffold == false);
    }
}
