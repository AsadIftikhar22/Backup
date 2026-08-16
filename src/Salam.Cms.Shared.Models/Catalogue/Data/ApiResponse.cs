namespace Salam.Cms.Shared.Models.Catalogue.Data;

using System.Collections.Generic;
using System.Text.Json.Serialization;

public class ApiResponse
{
    [JsonPropertyName("items")]
    public List<Item> Items { get; set; }

    [JsonPropertyName("total_count")]
    public int TotalCount { get; set; }
    public Category Categories { get; set; }

    public Attributes Attributes { get; set; }
}