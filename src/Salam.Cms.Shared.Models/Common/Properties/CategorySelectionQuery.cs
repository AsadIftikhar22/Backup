namespace Salam.Cms.Shared.Models.Common.Properties;

using EPiServer.Find;
using EPiServer.Find.Framework;
using EPiServer.ServiceLocation;
using EPiServer.Shell.ObjectEditing;
using Salam.Cms.Shared.Models.Catalogue.Data;
using System;
using System.Collections.Generic;
using System.Linq;

[ServiceConfiguration(typeof(ISelectionQuery))]
public class CategorySelectionQuery : ISelectionQuery
{
    private readonly List<SelectItem> _items = new List<SelectItem>();

    public CategorySelectionQuery()
    {
        var client = SearchClient.Instance;

        var results = client.Search<Category>()
                            .Take(1000)
                            .GetResult();

        if (results.Any())
        {
            _items = results
            .Select(hit => new SelectItem
            {
                Text = hit.Name,
                Value = hit.Id
            })
            .ToList();
        }
    }
    public ISelectItem? GetItemByValue(string value)
        => _items.FirstOrDefault(i => i.Value != null && i.Value.ToString().Equals(value, StringComparison.OrdinalIgnoreCase));

    public IEnumerable<ISelectItem> GetItems(string query)
        => _items.Where(i => i.Text.Contains(query, StringComparison.OrdinalIgnoreCase));
}
