namespace Salam.Cms.Shared.Models.Common.Properties;

using EPiServer.Find;
using EPiServer.ServiceLocation;
using EPiServer.Shell.ObjectEditing;
using Salam.Cms.Shared.Models.Catalogue.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using static Salam.Cms.Shared.Models.SalamConstants;

[ServiceConfiguration(typeof(ISelectionQuery))]
public class ProductVisibleFieldsSelectionQuery : ISelectionQuery
{
    private readonly List<SelectItem> _items = new List<SelectItem>();

    public ProductVisibleFieldsSelectionQuery()
    {
        var postpaid = GetClassPropertyNames<PrepaidPostpaid>()
            .Select(name => new SelectItem { Text = name, Value = name });
        var device = GetClassPropertyNames<Device>()
            .Select(name => new SelectItem { Text = name, Value = name });
        var visitor = GetClassPropertyNames<Visitor>()
            .Select(name => new SelectItem { Text = name, Value = name });
        var fiber = GetClassPropertyNames<Fiber>()
            .Select(name => new SelectItem { Text = name, Value = name });
        var addOn = GetClassPropertyNames<AddOn>()
            .Select(name => new SelectItem { Text = name, Value = name });
        var dataPlan = GetClassPropertyNames<DataPlan>()
            .Select(name => new SelectItem { Text = name, Value = name });
        var fiveG = GetClassPropertyNames<FiveG>()
            .Select(name => new SelectItem { Text = name, Value = name });

        _items = postpaid
            .Union(device)
            .Union(visitor)
            .Union(fiber)
            .Union(addOn)
            .Union(dataPlan)
            .Union(fiveG)
            .OrderBy(name => name.Text)
            .ToList();
    }
    public ISelectItem? GetItemByValue(string value)
        => _items.FirstOrDefault(i => i.Value != null && i.Value.ToString().Equals(value, StringComparison.OrdinalIgnoreCase));

    public IEnumerable<ISelectItem> GetItems(string query)
        => _items.Where(i => i.Text.StartsWith(query, StringComparison.OrdinalIgnoreCase));
    public static List<string> GetClassPropertyNames<T>()
    {
        var type = typeof(T);
        var className = type.Name;

        return type
            .GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Select(prop => $"{className}{CatalogueAPIfields.VisibleFieldsDelimiter}{prop.Name}")
            .ToList();
    }

    private List<SelectItem> GetPropertyItemsWithValues<T, TStaticFields>()
    {
        var classType = typeof(T);
        var staticFieldsType = typeof(TStaticFields);

        var staticFieldMap = staticFieldsType
            .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
            .Where(f => f.IsLiteral && !f.IsInitOnly && f.FieldType == typeof(string))
            .ToDictionary(f => f.Name, f => (string)f.GetRawConstantValue());

        return classType
            .GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(prop => staticFieldMap.ContainsKey(prop.Name))
            .Select(prop => new SelectItem
            {
                Text = $"{classType.Name} - {prop.Name}",
                Value = staticFieldMap[prop.Name]
            })
            .ToList();
    }

    public static List<SelectItem> GetPropertyItemsWithValues<T>(Type staticFieldsType)
    {
        var classType = typeof(T);

        var staticFieldMap = staticFieldsType
            .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
            .Where(f => f.IsLiteral && !f.IsInitOnly && f.FieldType == typeof(string))
            .ToDictionary(f => f.Name, f => (string)f.GetRawConstantValue());

        return classType
            .GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(prop => staticFieldMap.ContainsKey(prop.Name))
            .Select(prop => new SelectItem
            {
                Text = $"{classType.Name} - {prop.Name}",
                Value = staticFieldMap[prop.Name]
            })
            .ToList();
    }
}
