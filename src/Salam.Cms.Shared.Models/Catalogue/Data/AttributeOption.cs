namespace Salam.Cms.Shared.Models.Catalogue.Data;
using System.Text.Json.Serialization;

public class AttributeOption
{
    [JsonPropertyName("label")]
    public string Label { get; set; } = string.Empty;

    [JsonPropertyName("value")]
    public string Value { get; set; } = string.Empty;
}
