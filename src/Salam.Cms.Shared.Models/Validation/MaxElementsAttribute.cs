namespace Salam.Cms.Shared.Models.Validation;

using EPiServer.Core;
using EPiServer.SpecializedProperties;
using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;

[AttributeUsage(AttributeTargets.Property)]
public class MaxElementsAttribute : ValidationAttribute
{
    public MaxElementsAttribute(int maxElementsInList)
    {
        MaxCount = maxElementsInList;
    }

    public int MaxCount { get; set; }

    protected override ValidationResult? IsValid(object? value, ValidationContext? validationContext)
    {
        var propertyName = validationContext?.DisplayName ?? "This Property";

        if (value is ContentArea area && area.Items.Count > MaxCount)
        {
            return new ValidationResult($"{propertyName} contains too many items. The maximum is {MaxCount}.");
        }

        if (value is LinkItemCollection linkItems && linkItems.Count > MaxCount)
        {
            return new ValidationResult($"{propertyName} contains too many items. The maximum is {MaxCount}.");
        }

        if (value is IList list && list.Count > MaxCount)
        {
            return new ValidationResult($"{propertyName} contains too many items. The maximum is {MaxCount}.");
        }

        return null;
    }
}
