namespace Salam.Cms.Shared.Models.Catalogue.Data;

using EPiServer.DataAnnotations;
using EPiServer.Find;
using EPiServer.Find.Api;
using System.Collections.Generic;
using System.Text.Json.Serialization;

public class AttributeDefinition
{
    [Id]
    [JsonPropertyName("attribute_id")]
    public int AttributeId { get; set; }

    [JsonPropertyName("attribute_code")]
    public string AttributeCode { get; set; } = string.Empty;

    [JsonPropertyName("default_frontend_label")]
    public string DefaultFrontendLabel { get; set; } = string.Empty;

    [JsonPropertyName("frontend_labels")]
    public List<FrontEndLabel> FrontEndLabels { get; set; }

    [JsonPropertyName("options")]
    public List<AttributeOption>? Options { get; set; }

    [LanguageRouting]
    public LanguageRouting LanguageRouting { get; set; }

    [Searchable]
    public string Language { get; set; }
}
