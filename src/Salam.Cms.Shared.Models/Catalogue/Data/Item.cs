namespace Salam.Cms.Shared.Models.Catalogue.Data;

using Salam.Cms.Shared.Models.Catalogue.Data.Base;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

public class Item : ItemBase
{
    [JsonPropertyName("custom_attributes")]
    public List<CustomAttribute> CustomAttributes { get; set; } = new List<CustomAttribute>();

    [JsonPropertyName("extension_attributes")]
    public ExtensionAttributes? ExtensionAttributes { get; set; }

    public CustomAttribute? GetAttribute(string attributeCode) =>
        CustomAttributes?.FirstOrDefault(a => a.AttributeCode == attributeCode);
}

