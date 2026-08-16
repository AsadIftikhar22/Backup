namespace Salam.Cms.Shared.Models.Helpers;

using EPiServer.Core;

public interface IValueFallbackHandler
{
    string GetBest(params string?[] possibleValues);

    ContentReference GetBest(params ContentReference?[] possibleValues);
}