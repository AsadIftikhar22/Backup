namespace Salam.Cms.Shared.Models.Catalogue.Data;

using EPiServer.DataAnnotations;
using EPiServer.Find;
using EPiServer.Find.Api;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Serialization;

public class AddOn
{
    [Id]
    public string Id { get; set; }

    //[Id]
    [JsonPropertyName("record_id")]
    [JsonProperty("record_id")]
    public string RecordId { get; set; } = string.Empty;

    [JsonPropertyName("Data")]
    [JsonProperty("Data")]
    public string Data { get; set; } = string.Empty;

    [JsonPropertyName("Calls Minutes")]
    [JsonProperty("Calls Minutes")]
    public string CallsMinutes { get; set; } = string.Empty;

    [JsonPropertyName("International Minutes")]
    [JsonProperty("International Minutes")]
    public string InternationalMinutes { get; set; } = string.Empty;

    [JsonPropertyName("Validity")]
    [JsonProperty("Validity")]
    public string Validity { get; set; } = string.Empty;

    // Keep both, otherwise price doesn't come through?
    [JsonPropertyName("Price (with VAT)")]
    [JsonProperty("Price (with VAT)")]
    public string Price { get; set; }

    [JsonPropertyName("Banner")]
    [JsonProperty("Banner")]
    public string Banner { get; set; }

    //[JsonPropertyName("Call Only")]
    //[JsonProperty("Call Only")]
    //public string CallOnly { get; set; }

    //[JsonPropertyName("Unlimited data only")]
    //[JsonProperty("Unlimited data only")]
    //public string DataOnly { get; set; }

    //[JsonPropertyName("Unlimited Data Only")]
    //[JsonProperty("Unlimited Data Only")]
    //public string UnlimitedDataOnly { get; set; }

    [JsonPropertyName("initialize")]
    [JsonProperty("initialize")]
    public int Initialize { get; set; }

    public List<int> CategoryIds { get; set; } = new List<int>();

    public string Sku { get; set; }

    public string Name { get; set; }

    public int ProductId { get; set; }

    [LanguageRouting]
    public LanguageRouting LanguageRouting { get; set; }

    [Searchable]
    public string Language { get; set; }

    private static string ComputeHash(string input)
    {
        using var sha256 = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(input);
        var hash = sha256.ComputeHash(bytes);
        return Convert.ToHexString(hash).ToLowerInvariant();
    }
}

