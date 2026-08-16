using EPiServer.ContentApi.Core.Serialization;
using EPiServer.ContentApi.Core.Serialization.Models;
using Flurl.Http;
using Salam.Cms.Core.Settings.Models;

namespace Salam.Cms.Core.Settings.Helpers;

public static class ContentDeliveryApiHelper
{
    public static void ProcessContentProperties(IDictionary<string, object> properties, string apiBaseUrl)
    {
        if (!properties.Any())
        {
            return;
        }

        foreach (var key in properties.Keys)
        {
            var value = properties[key];

            if (value is ContentModelReference contentRef)
            {
                ReplaceContentReferenceWithDetails(properties, key, contentRef, apiBaseUrl);
            }
            else if (value is IDictionary<string, object> nestedDictionary)
            {
                ProcessContentProperties(nestedDictionary, apiBaseUrl);
            }
            else if (value is IEnumerable<IContentItem> contentItems)
            {
                foreach (var item in contentItems)
                {
                    if (item is ContentModelReference nestedContentReference)
                    {
                        ReplaceContentReferenceWithDetails(properties, key, nestedContentReference, apiBaseUrl);
                    }
                }
            }
        }
    }

    private static async Task<ContentDeliveryApiResponse> GetContentByIdAsync(int contentId, string apiBaseUrl)
    {
        try
        {
            return await $"{apiBaseUrl}/{contentId}"
                .AllowAnyHttpStatus()
                .WithHeader("Accept", "application/json")
                .GetJsonAsync<ContentDeliveryApiResponse>();
        }
        catch (FlurlHttpException)
        {
            return new ContentDeliveryApiResponse();
        }
    }
    private static ContentDeliveryApiResponse? GetContentById(int contentId, string apiBaseUrl)
    {
        var resultTest = GetContentByIdAsync(contentId, apiBaseUrl).GetAwaiter().GetResult();
        return resultTest;
    }

    private static void ReplaceContentReferenceWithDetails(IDictionary<string, object> properties, string key, ContentModelReference contentReference, string apiBaseUrl)
    {
        var contentDetails = GetContentById(contentReference.Id ?? 0, apiBaseUrl);

        if (contentDetails != null && contentDetails != new ContentDeliveryApiResponse())
        {
            properties[key] = contentDetails;
        }
    }
}
