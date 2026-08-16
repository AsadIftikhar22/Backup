namespace Salam.Cms.Shared.Models.Catalogue.Data;
using EPiServer.DataAnnotations;
using EPiServer.Find;
using EPiServer.Find.Api;
using System.Collections.Generic;
using System.Text.Json.Serialization;

public class Category
{
    [Id]
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("parent_id")]
    public int ParentId { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("is_active")]
    public bool IsActive { get; set; }

    [JsonPropertyName("position")]
    public int Position { get; set; }

    [JsonPropertyName("level")]
    public int Level { get; set; }

    [JsonPropertyName("product_count")]
    public int ProductCount { get; set; }

    [JsonPropertyName("children_data")]
    public List<Category> ChildrenData { get; set; } = new List<Category>();
    [LanguageRouting]
    public LanguageRouting LanguageRouting { get; set; }
    [Searchable]
    public string Language { get; set; }
}
