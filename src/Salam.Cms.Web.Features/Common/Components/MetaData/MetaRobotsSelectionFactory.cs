namespace Salam.Cms.Web.Features.Common.Components.MetaData;

using EPiServer.Shell.ObjectEditing;

public class MetaRobotsSelectionFactory : ISelectionFactory
{
    public IEnumerable<ISelectItem> GetSelections(ExtendedMetadata metadata)
    {
        return new List<SelectItem>
        {
            new() { Text = "index, follow", Value = "index, follow" },
            new() { Text = "noindex, follow", Value = "noindex" },
            new() { Text = "index, nofollow", Value = "nofollow" },
            new() { Text = "noindex, nofollow", Value = "noindex, nofollow" }
        };
    }
}