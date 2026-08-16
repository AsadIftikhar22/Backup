namespace Salam.Cms.Web.Infrastructure.Extensions;
public static class DictionaryExtensions
{
    public static string GetStringValue(this IDictionary<string, object> dict, string key)
    {
        if (dict == null || !dict.TryGetValue(key, out object obj) || obj == null)
            return null;

        if (obj is string str) return str;
        if (obj is System.Text.Json.JsonElement je && je.ValueKind == System.Text.Json.JsonValueKind.String)
            return je.GetString();
        return obj.ToString();
    }
}
