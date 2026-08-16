namespace Salam.Cms.Shared.Models.Pages;

using EPiServer.Core;

public interface INavigationItem : IContent
{
    public ContentReference? Icon { get; set; }

    public bool VisibleInMenu { get; set; }

    public string MobileName { get; set; }

    public int SortingOrder { get; set; }
    public string NewPageTitle { get; set; }
}
