namespace Salam.Cms.Shared.Models.Common.Components;

using EPiServer.SpecializedProperties;

public interface ILinkModelConverter
{
    LinkModel? ConvertToModel(LinkItem? linkItem);

    List<LinkModel> ConvertToModelCollection(LinkItemCollection? linkItemCollection);
}