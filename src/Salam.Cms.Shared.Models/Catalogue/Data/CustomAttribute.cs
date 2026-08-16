namespace Salam.Cms.Shared.Models.Catalogue.Data;
using Newtonsoft.Json;
using Salam.Cms.Shared.Models.Catalogue.Enums;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

public class CustomAttribute
{
    [JsonPropertyName("attribute_code")]
    public string AttributeCode { get; set; } = string.Empty;

    // Value can be string or List<string>
    //[Newtonsoft.Json.JsonConverter(typeof(CustomAttributeValueConverter))]
    [JsonPropertyName("value")]
    public JsonElement Value { get; set; }
    public string GetStringValue()
    {
        if (Value.ValueKind == JsonValueKind.String)
        {
            return Value.GetString();
        }
        else if (Value.ValueKind == JsonValueKind.Number)
        {
            return Value.GetRawText();
        }
        else if (Value.ValueKind == JsonValueKind.True || Value.ValueKind == JsonValueKind.False)
        {
            return Value.GetBoolean().ToString();
        }
        return string.Empty;
    }


    public List<string> GetStringListValue()
    {
        if (Value.ValueKind == JsonValueKind.Array)
        {
            return Value.EnumerateArray().Select(e => e.GetString()).ToList();
        }
        else if (Value.ValueKind == JsonValueKind.String)
        {
            return new List<string> { Value.GetString() };
        }
        return new List<string>();
    }

    public int? GetIntValue()
    {
        if (Value.ValueKind == JsonValueKind.Number && Value.TryGetInt32(out int result))
            return result;

        if (Value.ValueKind == JsonValueKind.String && int.TryParse(Value.GetString(), out result))
            return result;

        return null;
    }

    public List<int> GetIntListValue()
    {
        if (Value.ValueKind == JsonValueKind.Array)
        {
            return Value.EnumerateArray().Select(e => e.GetInt32()).ToList();
        }

        else if (Value.ValueKind == JsonValueKind.String)
        {
            return new List<int> { Value.GetInt32() };
        }

        return null;
    }


    public double? GetDoubleValue()
    {
        if (Value.ValueKind == JsonValueKind.Number && Value.TryGetDouble(out double result))
            return result;

        if (Value.ValueKind == JsonValueKind.String && double.TryParse(Value.GetString(), out result))
            return result;

        return null;
    }

    public decimal? GetDecimalValue()
    {
        if (Value.ValueKind == JsonValueKind.Number && Value.TryGetDecimal(out decimal result))
            return result;

        if (Value.ValueKind == JsonValueKind.String && decimal.TryParse(Value.GetString(), out result))
            return result;

        return null;
    }

    public bool? GetBoolValue()
    {
        if (Value.ValueKind == JsonValueKind.True) return true;
        if (Value.ValueKind == JsonValueKind.False) return false;

        if (Value.ValueKind == JsonValueKind.String)
        {
            var str = Value.GetString()?.ToLower();
            if (bool.TryParse(str, out bool parsed)) return parsed;
            if (str == "1") return true;
            if (str == "0") return false;
        }

        return null;
    }


    public Uri? GetImageUrl(string baseUrl = "https://domain.com")
    {
        if (AttributeCode == "image" && Value.ValueKind == JsonValueKind.String)
        {
            var path = Value.GetString()?.Trim();
            var firstPart = "pub/media/catalog/product";
            if (!string.IsNullOrWhiteSpace(path))
            {
                return new Uri(new Uri(baseUrl), firstPart + path);
            }
        }
        return null;
    }


    public List<PricingType>? GetPricingTypes()
    {
        var jsonString = GetStringValue();
        if (string.IsNullOrEmpty(jsonString) || jsonString.Equals("null", StringComparison.OrdinalIgnoreCase))
        {
            return new List<PricingType>();
        }

        try
        {
            var result = JsonConvert.DeserializeObject<List<PricingType>>(jsonString);
            return result ?? new List<PricingType>();
        }
        catch
        {
            return new List<PricingType>();
        }
    }

    public List<AddOn>? GetAddOns()
    {
        var jsonString = GetStringValue();
        if (string.IsNullOrEmpty(jsonString) || jsonString.Equals("null", StringComparison.OrdinalIgnoreCase))
        {
            return new List<AddOn>();
        }

        try
        {
            var result = JsonConvert.DeserializeObject<List<AddOn>>(jsonString);
            return result ?? new List<AddOn>();
        }
        catch
        {
            return new List<AddOn>();
        }
    }

}
