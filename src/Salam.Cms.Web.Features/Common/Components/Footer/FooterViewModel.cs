using EPiServer.Core;
using EPiServer.SpecializedProperties;
using Salam.Cms.Web.Features.IconLinks.Models;
using Salam.Cms.Web.Features.Navigation.ViewModels;

namespace Salam.Cms.Web.Features.Common.Components.Footer
{
    public class FooterViewModel
    {
        public ContentReference? Logo { get; set; }
        public List<NavigationItemCollectionViewModel> NavigationItems { get; set; }
        public LinkItemCollection? FooterLegalLinks { get; set; }
        public List<IconLinkItemBlock> FooterSocialLinks { get; set; }
        public string CopyrightText { get; set; }
        public XhtmlString b2bFooterHtml { get; set; }
        public XhtmlString WholesaleFooterHTML { get; set; }
    }
}
