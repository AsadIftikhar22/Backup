using EPiServer.ContentApi.Core.Serialization.Models;

namespace Salam.Cms.Core.Settings.Models
{
    public class ContentDeliveryApiResponse
    {
        public ContentModelReference? ContentLink { get; set; }

        public string? Name { get; set; }

        public string? Url { get; set; }
    }
}