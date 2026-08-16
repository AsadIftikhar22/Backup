namespace Salam.Cms.Shared.Models.Catalogue.Data;

using System.Text.Json.Serialization;

public class CategoryLink
{
    [JsonPropertyName("position")]
    public int Position { get; set; }

    [JsonPropertyName("category_id")]
    public string CategoryId { get; set; }
}
