namespace Salam.Cms.Shared.Models.Catalogue.Data;
using System.Text.Json.Serialization;

public class FrontEndLabel
{
    [JsonPropertyName("store_id")]
    public int StoreId { get; set; }

    [JsonPropertyName("label")]
    public string Label { get; set; }
}