namespace Salam.Cms.Shared.Models.Common.Components;

using EPiServer.SpecializedProperties;
using EPiServer.Web.Routing;
using Salam.Cms.Shared.Models.Extensions;

public sealed class LinkModelConverter : ILinkModelConverter
{
    private readonly IUrlResolver _urlResolver;

    public LinkModelConverter(IUrlResolver urlResolver)
    {
        _urlResolver = urlResolver;
    }

    public LinkModel? ConvertToModel(LinkItem? linkItem)
    {
        if (linkItem.IsValid())
        {
            return new LinkModel
            {
                Text = linkItem.Text ?? string.Empty,
                Title = linkItem.Title ?? string.Empty,
                Target = linkItem.Target ?? string.Empty,
                Url = _urlResolver.ContentUrl(linkItem.Href)
            };
        }

        return default;
    }

    public List<LinkModel> ConvertToModelCollection(LinkItemCollection? linkItemCollection)
    {
        return GetLinkModels(linkItemCollection).ToList();
    }

    private IEnumerable<LinkModel> GetLinkModels(LinkItemCollection? linkItemCollection)
    {
        if (linkItemCollection.IsNullOrEmpty())
        {
            yield break;
        }

        foreach (var linkItem in linkItemCollection)
        {
            yield return new LinkModel
            {
                Text = linkItem.Text ?? string.Empty,
                Title = linkItem.Title ?? string.Empty,
                Target = linkItem.Target ?? string.Empty,
                Url = _urlResolver.ContentUrl(linkItem.Href)
            };
        }
    }
}