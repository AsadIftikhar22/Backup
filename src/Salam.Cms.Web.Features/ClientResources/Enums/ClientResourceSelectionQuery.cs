namespace Salam.Cms.Web.Features.ClientResources.Enums;

using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.ServiceLocation;
using EPiServer.Shell.ObjectEditing;
using Salam.Cms.Web.Features.ClientResources.Media;

[ServiceConfiguration(typeof(ISelectionQuery))]
public class ClientResourceSelectionQuery : ISelectionQuery
{
    private readonly IContentTypeRepository _contentTypeRepository;
    private readonly IContentModelUsage _contentModelUsage;
    private readonly IReadOnlyList<SelectItem> _items;

    public ClientResourceSelectionQuery(IContentTypeRepository contentTypeRepository, IContentModelUsage contentModelUsage)
    {
        _contentTypeRepository = contentTypeRepository;
        _contentModelUsage = contentModelUsage;
        _items = GetClientResources();
    }

    public ISelectItem? GetItemByValue(string value)
        => _items.FirstOrDefault(country => country.Value.Equals(value));

    public IEnumerable<ISelectItem> GetItems(string query)
        => _items.Where(country => country.Text.Contains(query, StringComparison.OrdinalIgnoreCase));

    private List<SelectItem> GetClientResources()
    {
        var contentType = _contentTypeRepository.Load<JavaScriptContent>();
        var allScriptContent = _contentModelUsage.ListContentOfContentType(contentType);

        return new();
    }
}
