namespace Salam.Cms.Shared.Models.Validation;

using EPiServer.Core;
using EPiServer.SpecializedProperties;
using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;

[AttributeUsage(AttributeTargets.Property)]
public class MinElementsAttribute : ValidationAttribute
{
    public MinElementsAttribute(int minElementsInList)
    {
        MinCount = minElementsInList;
    }

    public int MinCount { get; set; }

    protected override ValidationResult? IsValid(object? value, ValidationContext? validationContext)
    {
        var propertyName = validationContext?.DisplayName ?? "This Property";

        if (value is ContentArea area && area.Items.Count < MinCount)
        {
            return new ValidationResult($"{propertyName} does not contain enough items. The minimum is {MinCount}.");
        }

        if (value is LinkItemCollection linkItems && linkItems.Count < MinCount)
        {
            return new ValidationResult($"{propertyName} does not contain enough items. The minimum is {MinCount}.");
        }

        if (value is IList list && list.Count < MinCount)
        {
            return new ValidationResult($"{propertyName} does not contain enough items. The minimum is {MinCount}.");
        }

        if (value is null)
        {
            return new ValidationResult($"{propertyName} does not contain enough items. The minimum is {MinCount}.");
        }

        return null;
    }
}
