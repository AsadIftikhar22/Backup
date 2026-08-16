namespace Salam.Cms.Web.Features.Navigation.ViewModels
{
    using EPiServer.SpecializedProperties;
    using Salam.Cms.Web.Features.Common.ViewModels;
    using Salam.Cms.Web.Features.Navigation.Models;

    public class NavigationItemCollectionViewModel : BlockViewModel<NavigationItemCollectionBlock>
    {
        public NavigationItemCollectionViewModel(NavigationItemCollectionBlock? currentBlock) : base(currentBlock)
        {
        }

        public string? Heading { get; set; }
        public LinkItemCollection? Links { get; set; }
    }
}
