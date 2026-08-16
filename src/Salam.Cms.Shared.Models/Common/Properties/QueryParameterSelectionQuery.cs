namespace Salam.Cms.Shared.Models.Common.Properties;
using EPiServer.Find;
using EPiServer.Find.Framework;
using EPiServer.ServiceLocation;
using EPiServer.Shell.ObjectEditing;
using Salam.Cms.Shared.Models.Catalogue.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using static Salam.Cms.Shared.Models.SalamConstants;

[ServiceConfiguration(typeof(ISelectionQuery))]
public class QueryParameterSelectionQuery : ISelectionQuery
{
    private readonly List<SelectItem> _items = new List<SelectItem>();

    public QueryParameterSelectionQuery()
    {
        var client = SearchClient.Instance;

        var results = client.Search<QueryParameter>()
                            .Take(20)
                            .GetResult();

        if (results.Any())
        {
            _items = results
                .Select(hit => new SelectItem
                {
                    Text = hit.Key,
                    Value = hit.Key
                })
                .ToList();
        }
        else
        {
            _items.Add(new SelectItem() { Text = "Name", Value = CatalogueAPIfields.Name });
            _items.Add(new SelectItem() { Text = "Valid days", Value = CatalogueAPIfields.ValidDays });
            _items.Add(new SelectItem() { Text = "Price", Value = CatalogueAPIfields.Price });
            _items.Add(new SelectItem() { Text = "Product Sku", Value = CatalogueAPIfields.ProductSku });
            _items.Add(new SelectItem() { Text = "Product type", Value = CatalogueAPIfields.ProductType });
        }
    }
    public ISelectItem? GetItemByValue(string value)
        => _items.FirstOrDefault(i => i.Value.Equals(value));

    public IEnumerable<ISelectItem> GetItems(string query)
        => _items.Where(i => i.Text.StartsWith(query, StringComparison.OrdinalIgnoreCase));

}
