namespace Salam.Cms.Shared.Models.Helpers;

using Castle.Core.Internal;
using EPiServer.Core;

public class ValueFallbackHandler : IValueFallbackHandler
{
    public string GetBest(params string?[] possibleValues)
    {
        return possibleValues.Find(x => !string.IsNullOrWhiteSpace(x)) ?? string.Empty;
    }

    public ContentReference GetBest(params ContentReference?[] possibleValues)
    {
        return possibleValues.Find(x => !ContentReference.IsNullOrEmpty(x)) ?? ContentReference.EmptyReference;
    }
}