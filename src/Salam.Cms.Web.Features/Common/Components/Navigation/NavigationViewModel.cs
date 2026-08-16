using EPiServer.Core;
using EPiServer.SpecializedProperties;
using Salam.Cms.Shared.Models.Pages;
using Salam.Cms.Web.Features.Common.Interfaces;
using Salam.Cms.Web.Features.Common.Models;
using Salam.Cms.Web.Features.IconLinks.Models;
using Salam.Cms.Web.Features.LanguageSwitcher.ViewModels;

namespace Salam.Cms.Web.Features.Common.Components.Navigation
{
    public class NavigationViewModel
    {
        public ISitePageData? CurrentPage { get; set; }

        public ContentReference? Logo { get; set; }
        public ContentReference? WholeSaleLogo { get; set; }

        public ContentReference? LogoSmall { get; set; }

        public ContentReference? WholeSaleLogoSmall { get; set; }

        public List<INavigationItem>? Pages { get; set; }

        public LinkItem CoverageButtonLink { get; set; } = new LinkItem();
        public LinkItem B2BCoverageButtonLink { get; set; } = new LinkItem();
        public LinkItem HelpAndSupportButtonLink { get; set; } = new LinkItem();

        public LinkItem MySalamLink { get; set; } = new LinkItem();
        public LinkItem B2BMySalamLink { get; set; } = new LinkItem();

        public LinkItem SelectedProductCounterr { get; set; }
        public ContentReference? MySalamIcon { get; set; }
        public ContentReference? B2BMySalamIcon { get; set; }

        public List<SitePageData> TopLinks { get; set; } = new List<SitePageData>();

        public LinkItemCollection TopNavigationMenu { get; set; } = new LinkItemCollection();
        public LinkItemCollection B2BTopNavigationMenu { get; set; } = new LinkItemCollection();
        public List<IconLinkItemBlock> FooterSocialLinks { get; set; } = new List<IconLinkItemBlock>();

        public string? CopyrightText { get; set; } = string.Empty;

        public string? CurrentLanguage { get; set; }

        public IEnumerable<LanguageItem> Languages { get; set; } = new List<LanguageItem>();
        public IEnumerable<SalamMenu> SalamMenu { get; set; } = new List<SalamMenu>();


        public string? LanguagesNavItemText { get; set; }
        public string? B2BLanguagesNavItemText { get; set; }
        public string? SalamNavItemText { get; set; }

        public Dictionary<ContentReference, List<INavigationItem>>? ChildPages2ndLevel { get; set; }
        public Dictionary<ContentReference, List<INavigationItem>>? ChildPages3rdLevel { get; set; }

        public LinkItemCollection? FooterLegalLinks { get; set; }
        public IList<LinkItem> SalamNavItems { get; set; }

        public string B2bSearchPlaceHolderTxt { get; set; }
        public string B2bSearchBtnTxt { get; set; }

    }
}
