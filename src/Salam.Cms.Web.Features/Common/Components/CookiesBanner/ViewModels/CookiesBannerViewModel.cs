namespace Salam.Cms.Web.Features.Showcase.ViewModels
{
    using Salam.Cms.Web.Features.Showcase.Models;
    using System;

    public class CookiesBannerViewModel
    {
        public CookiesBannerViewModel()
        {
        }

        public CookiesBannerViewModel(CookiesBannerBlock block)
        {
            CookieBannerHeading = block?.CookieBannerHeading;
            CookieBannerDescription = block?.CookieBannerDescription;
            RejectButtonText = block?.RejectButtonText;
            CustomizeButtonText = block?.CustomizeButtonText;
            AcceptButtonText = block?.AcceptButtonText;
            PreferencesHeading = block?.PreferencesHeading;
            EssentialCookiesTitle = block?.EssentialCookiesTitle;
            EssentialCookiesDescription = block?.EssentialCookiesDescription;
            AnalyticsCookiesTitle = block?.AnalyticsCookiesTitle;
            AnalyticsCookiesDescription = block?.AnalyticsCookiesDescription;
            MarketingCookiesTitle = block?.MarketingCookiesTitle;
            MarketingCookiesDescription = block?.MarketingCookiesDescription;
            RejectAllPreferencesText = block?.RejectAllPreferencesText;
            SavePreferencesText = block?.SavePreferencesText;
            AcceptAllPreferencesText = block?.AcceptAllPreferencesText;
            EssentialCookiesSubTitle = block?.EssentialCookiesSubTitle;
        }

        public string CookieBannerHeading { get; set; }
        public string CookieBannerDescription { get; set; }
        public string RejectButtonText { get; set; }
        public string CustomizeButtonText { get; set; }
        public string AcceptButtonText { get; set; }
        public string PreferencesHeading { get; set; }
        public string EssentialCookiesTitle { get; set; }
        public string EssentialCookiesSubTitle { get; set; }
        public string EssentialCookiesDescription { get; set; }
        public string AnalyticsCookiesTitle { get; set; }
        public string AnalyticsCookiesDescription { get; set; }
        public string MarketingCookiesTitle { get; set; }
        public string MarketingCookiesDescription { get; set; }
        public string RejectAllPreferencesText { get; set; }
        public string SavePreferencesText { get; set; }
        public string AcceptAllPreferencesText { get; set; }

        public static CookiesBannerViewModel FromBlock(CookiesBannerBlock block)
        {
            if (block == null) throw new ArgumentNullException(nameof(block));

            return new CookiesBannerViewModel
            {
                CookieBannerHeading = string.IsNullOrWhiteSpace(block.CookieBannerHeading)
                    ? "Cookie Notice"
                    : block.CookieBannerHeading,

                CookieBannerDescription = string.IsNullOrWhiteSpace(block.CookieBannerDescription)
                    ? "We use cookies to enhance your experience. You can accept all, reject non-essential cookies, or customize your preferences."
                    : block.CookieBannerDescription,

                RejectButtonText = string.IsNullOrWhiteSpace(block.RejectButtonText)
                    ? "Reject All"
                    : block.RejectButtonText,

                CustomizeButtonText = string.IsNullOrWhiteSpace(block.CustomizeButtonText)
                    ? "Customize Settings"
                    : block.CustomizeButtonText,

                AcceptButtonText = string.IsNullOrWhiteSpace(block.AcceptButtonText)
                    ? "Accept All"
                    : block.AcceptButtonText,

                PreferencesHeading = string.IsNullOrWhiteSpace(block.PreferencesHeading)
                    ? "Cookie Preferences"
                    : block.PreferencesHeading,

                EssentialCookiesTitle = string.IsNullOrWhiteSpace(block.EssentialCookiesTitle)
                    ? "Essential Cookies"
                    : block.EssentialCookiesTitle,

                EssentialCookiesSubTitle = string.IsNullOrWhiteSpace(block.EssentialCookiesSubTitle)
                    ? "Always Active"
                    : block.EssentialCookiesSubTitle,

                EssentialCookiesDescription = string.IsNullOrWhiteSpace(block.EssentialCookiesDescription)
                    ? "These cookies are necessary for the site to function."
                    : block.EssentialCookiesDescription,

                AnalyticsCookiesTitle = string.IsNullOrWhiteSpace(block.AnalyticsCookiesTitle)
                    ? "Analytics Cookies"
                    : block.AnalyticsCookiesTitle,

                AnalyticsCookiesDescription = string.IsNullOrWhiteSpace(block.AnalyticsCookiesDescription)
                    ? "Help us understand how visitors interact with the site."
                    : block.AnalyticsCookiesDescription,

                MarketingCookiesTitle = string.IsNullOrWhiteSpace(block.MarketingCookiesTitle)
                    ? "Marketing Cookies"
                    : block.MarketingCookiesTitle,

                MarketingCookiesDescription = string.IsNullOrWhiteSpace(block.MarketingCookiesDescription)
                    ? "Used to deliver relevant ads and track effectiveness."
                    : block.MarketingCookiesDescription,

                RejectAllPreferencesText = string.IsNullOrWhiteSpace(block.RejectAllPreferencesText)
                    ? "Reject All"
                    : block.RejectAllPreferencesText,

                SavePreferencesText = string.IsNullOrWhiteSpace(block.SavePreferencesText)
                    ? "Save Preferences"
                    : block.SavePreferencesText,

                AcceptAllPreferencesText = string.IsNullOrWhiteSpace(block.AcceptAllPreferencesText)
                    ? "Accept All"
                    : block.AcceptAllPreferencesText
            };
        }
    }
}
