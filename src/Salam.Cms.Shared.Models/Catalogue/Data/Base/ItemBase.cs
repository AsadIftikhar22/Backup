namespace Salam.Cms.Shared.Models.Catalogue.Data.Base;

using EPiServer.DataAnnotations;
using EPiServer.Find;
using EPiServer.Find.Api;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

public class ItemBase
{
    [Id]
    public int Id { get; set; }

    [JsonProperty("sku")]
    [JsonPropertyName("sku")]
    public string Sku { get; set; } = string.Empty;

    [JsonProperty("name")]
    public string Name { get; set; } = string.Empty;

    [JsonProperty("price")]
    public decimal Price { get; set; }

    [LanguageRouting]
    public LanguageRouting LanguageRouting { get; set; }
    [Searchable]
    public string Language { get; set; }
    [JsonProperty("Banner")]
    [JsonPropertyName("Banner")]
    public string Banner { get; set; }

    [JsonProperty("BuyNowURL")]
    [JsonPropertyName("BuyNowURL")]
    public string BuyNowURL { get; set; }
}

