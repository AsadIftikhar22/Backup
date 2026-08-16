namespace Salam.Cms.Shared.Models.Validation;

using Salam.Cms.Shared.Models.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

public class UniqueKeyAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is IList<KeyValue> keyValueList)
        {
            var duplicateKeys = keyValueList
                .GroupBy(kv => kv.Key)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key)
                .ToList();

            if (duplicateKeys.Any())
                return new ValidationResult($"Duplicate keys found: {string.Join(", ", duplicateKeys)}");
        }

        return ValidationResult.Success;
    }
}
