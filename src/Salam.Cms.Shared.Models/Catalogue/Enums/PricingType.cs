namespace Salam.Cms.Shared.Models.Catalogue.Enums;

using EPiServer.DataAnnotations;
using EPiServer.Find;
using EPiServer.Find.Api;
using Newtonsoft.Json;

public class PricingType
{
    [Id]
    public string Id { get; set; }

    [JsonProperty("record_id")]
    public string RecordId { get; set; } = string.Empty;

    [JsonProperty("FREE TIME")]
    public string FreeTime { get; set; } = string.Empty;

    [JsonProperty("PRICE")]
    public decimal Price { get; set; }

    [JsonProperty("PACKAGE DURATION MONTHS")]
    public string PackageDuration { get; set; } = string.Empty;

    [JsonProperty("initialize")]
    public int Initialize { get; set; }

    public int ProductId { get; set; }

    public List<int> CategoryIds { get; set; } = new List<int>();

    public string Name { get; set; }

    public string Sku { get; set; }

    public string DownloadSpeed { get; set; }

    public string UploadSpeed { get; set; }

    public string? CorrelatedId { get; set; }
    public string? extra_month_Free { get; set; }
    public bool? free_router { get; set; }
    public string? InstallationFee { get; set; }

    [LanguageRouting]
    public LanguageRouting LanguageRouting { get; set; }

    [Searchable]
    public string Language { get; set; }
}
