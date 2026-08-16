namespace Salam.Cms.Shared.Models.Extensions;

using Newtonsoft.Json;
using System.Collections;

public static class JsonSerializationExtensions
{
    public static string ToJson(this IList list)
    {
        return JsonConvert.SerializeObject(list);
    }

    public static string ToJson<T>(this T objectToConvert)
    {
        return JsonConvert.SerializeObject(objectToConvert);
    }
}