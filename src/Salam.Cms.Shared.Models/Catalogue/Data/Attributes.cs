namespace Salam.Cms.Shared.Models.Catalogue.Data;
using System.Collections.Generic;
using System.Text.Json.Serialization;

public class Attributes
{
    [JsonPropertyName("items")]
    public List<AttributeDefinition> Items { get; set; }
}
