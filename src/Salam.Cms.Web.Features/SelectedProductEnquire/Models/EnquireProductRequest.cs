namespace Salam.Cms.Web.Features.SelectedProductEnquire.Models;

using System.Security.Policy;
using System.Text.Json.Serialization;

public class EnquireProductRequest
{
    [JsonPropertyName("blockId")]
    public int blockId { get; set; }
    public int id { get; set; }
    [JsonPropertyName("enTabName")]
    public string? enTabName { get; set; }
    [JsonPropertyName("arTabName")]
    public string? arTabName { get; set; }
    [JsonPropertyName("heading")]
    public string? heading { get; set; }
    [JsonPropertyName("description")]
    public string? description { get; set; }
    [JsonPropertyName("labels")]
    public string[]? labels { get; set; }
    [JsonPropertyName("PageContentLink")]
    public int? PageContentLink { get; set; }
    [JsonPropertyName("redirectchildpageURL")]
    public string? redirectchildpageURL { get; set; }
    [JsonPropertyName("language")]
    public string? language { get; set; }
}

public class ResultStatus
{
    public string ResponseMessage { get; set; }
    public int ResponseStatus { get; set; }
    public string RedirectSelectedProductPageURL { get; set; }

}
