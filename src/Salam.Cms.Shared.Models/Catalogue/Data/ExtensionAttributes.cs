namespace Salam.Cms.Shared.Models.Catalogue.Data;

using Salam.Cms.Shared.Models.Catalogue.Models;
using System.Collections.Generic;
using System.Text.Json.Serialization;

public class ExtensionAttributes
{
    [JsonPropertyName("website_ids")]
    public List<int> WebsiteIds { get; set; }

    [JsonPropertyName("category_links")]
    public List<CategoryLink> CategoryLinks { get; set; }

    [JsonPropertyName("related_products")]
    public List<ProductSku> RelatedProducts { get; set; }

    [JsonPropertyName("qty")]
    public int Quantity { get; set; }
}