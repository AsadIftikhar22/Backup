namespace Salam.Cms.Shared.Models.Catalogue.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

public class CustomAttributeValueConverter : JsonConverter
{
    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(object);
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        JToken token = JToken.Load(reader);

        if (token.Type == JTokenType.Array)
        {
            return token.ToObject<List<string>>();
        }

        if (token.Type == JTokenType.Integer)
        {
            return token.ToObject<int>();
        }

        if (token.Type == JTokenType.Float)
        {
            return token.ToObject<double>();
        }

        if (token.Type == JTokenType.Boolean)
        {
            return token.ToObject<bool>();
        }

        if (token.Type == JTokenType.String)
        {
            return token.ToString();
        }

        // if this is JSON object, return as JObject
        return token;
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        JToken token = JToken.FromObject(value);
        token.WriteTo(writer);
    }
}
