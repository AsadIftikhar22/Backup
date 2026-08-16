using EPiServer;
using EPiServer.Core;
using EPiServer.Globalization;
using EPiServer.ServiceLocation;
using Salam.Cms.Core.Settings.Configuration;
using Salam.Cms.Shared.Models.Catalogue.Enums;
using Salam.Cms.Shared.Models.Catalogue.Models;
using Salam.Cms.Web.Features.Common.Models;
using Salam.Cms.Web.Features.IconLinks.Models;
using Salam.Cms.Web.Features.InternetCards.Models;
using Salam.Cms.Web.Features.Navigation.Models;
using Salam.Cms.Web.Features.Navigation.ViewModels;
using Salam.Cms.Web.Features.TabContainer.Models;
using Salam.Cms.Web.Features.TileListBlock.Models;
using System.Globalization;
using static Salam.Cms.Shared.Models.SalamConstants;

namespace Salam.Cms.Web.Features.Common.Helpers;

/// <summary>
/// Helper class to generate URL-friendly anchor IDs.
/// </summary>
public static class ContentHelper
{
    public static IEnumerable<NavigationItemCollectionViewModel> GetNavigationItemCollection(IEnumerable<ContentReference> navigationItemReferences)
    {
        var contentLoader = ServiceLocator.Current.GetInstance<IContentLoader>();
        foreach (var navigationItemReference in navigationItemReferences)
        {
            if (contentLoader.TryGet<NavigationItemCollectionBlock>(navigationItemReference, out var navigationItemBlock))
            {
                yield return new NavigationItemCollectionViewModel(navigationItemBlock)
                {
                    Heading = navigationItemBlock.Heading,
                    Links = navigationItemBlock.Links
                };
            }
        }
    }

    public static IEnumerable<IconLinkItemBlock> GetIconLinks(IEnumerable<ContentReference> references)
    {
        var links = new List<IconLinkItemBlock>();
        var contentLoader = ServiceLocator.Current.GetInstance<IContentLoader>();
        foreach (var reference in references)
        {
            if (contentLoader.TryGet<IconLinkItemBlock>(reference, out var iconLinkBlock))
            {
                links.Add(iconLinkBlock);
            }
        }
        return links;
    }
    /// <summary>
    /// For Arabic Centralized Header and Footer
    /// </summary>
    /// <param name="navigationItemReferences"></param>
    /// <returns></returns>
    public static IEnumerable<NavigationItemCollectionViewModel> GetNavigationItemCollection(IEnumerable<ContentReference> navigationItemReferences, CultureInfo cultureInfo)
    {
        var contentLoader = ServiceLocator.Current.GetInstance<IContentLoader>();
        foreach (var navigationItemReference in navigationItemReferences)
        {
            if (contentLoader.TryGet<NavigationItemCollectionBlock>(navigationItemReference, cultureInfo, out var navigationItemBlock))
            {
                yield return new NavigationItemCollectionViewModel(navigationItemBlock)
                {
                    Heading = navigationItemBlock.Heading,
                    Links = navigationItemBlock.Links
                };
            }
        }
    }
    /// <summary>
    /// For Arabic Centralized Header and Footer
    /// </summary>
    /// <param name="references"></param>
    /// <returns></returns>
    public static IEnumerable<IconLinkItemBlock> GetIconLinks(IEnumerable<ContentReference> references, CultureInfo cultureInfo)
    {
        var links = new List<IconLinkItemBlock>();
        var contentLoader = ServiceLocator.Current.GetInstance<IContentLoader>();
        foreach (var reference in references)
        {
            if (contentLoader.TryGet<IconLinkItemBlock>(reference, cultureInfo, out var iconLinkBlock))
            {
                links.Add(iconLinkBlock);
            }
        }
        return links;
    }

    public static string? GetDefaultFrontendLabel(ProductSku product, string attributeCode)
    {
        if (product.Labels == null || !product.Labels.Any())
            return string.Empty;

        var language = ContentLanguage.PreferredCulture.TwoLetterISOLanguageName;

        return product.Labels.SingleOrDefault(x => x.AttributeCode.Equals(attributeCode, StringComparison.OrdinalIgnoreCase) && x.Language.Equals(language))?.GetLabel();
    }

    public static string FormatSize(int sizeInMB)
    {
        if (sizeInMB >= 1000)
        {
            int _result = sizeInMB % 1024 == 0 ? sizeInMB / 1024 : sizeInMB / 1000;
            // double sizeInGB = sizeInMB / 1024.0;
            return $"{_result.ToString("0.##", CultureInfo.InvariantCulture)} GB";
        }
        else
        {
            return $"{sizeInMB} MB";
        }
    }

    public static string[] SplitExtensionForVisibleFields(string visibleField)
        => visibleField.Contains(CatalogueAPIfields.VisibleFieldsDelimiterOld)
            ? visibleField.Split(CatalogueAPIfields.VisibleFieldsDelimiterOld)
            : visibleField.Split(CatalogueAPIfields.VisibleFieldsDelimiter);

    /// <summary>
    /// Returns the handoff URL based on the handoff option and language.
    /// </summary>
    /// <param name="option">The handoff option.</param>
    /// <param name="language">The language code (e.g., "en").</param>
    /// <param name="settings">The catalogue API settings.</param>
    /// <returns>The handoff URL or an empty string if none.</returns>
    public static string GetHandoffUrl(HandoffOption option, string language, CatalogueApiSettings settings)
    {
        switch (option)
        {
            case HandoffOption.Plan:
                return string.Format(settings.PlanHandoffBaseUrl, language);
            case HandoffOption.Device:
                return string.Format(settings.DeviceHandoffBaseUrl, language);
            case HandoffOption.Fiber:
                return string.Format(settings.FiberHandoffBaseUrl, language);
            case HandoffOption.FiveG:
                return string.Format(settings.FiveGHandoffBaseUrl, language);
            case HandoffOption.None:
            default:
                return string.Empty;
        }
    }

    public static VisibleFieldRenderResult ShouldRenderVisibleField(string visibleField, ProductSku? product)
    {
        var templateModifier = "";
        var shouldRender = false;

        switch (visibleField)
        {
            case "Data":
                shouldRender = !string.IsNullOrEmpty(product?.Data);
                break;
            case "CallsMinutes":
                shouldRender = !string.IsNullOrEmpty(product?.CallsMinutes);
                break;
            case "SpecialPrice":
                shouldRender = true; // This one always renders based on the partial
                break;
            case "LocalMinutes":
                shouldRender = !string.IsNullOrEmpty(product?.LocalMinutes);
                break;
            case "InternationalMinutes":
                shouldRender = !string.IsNullOrEmpty(product?.InternationalMinutes);
                templateModifier = "Featured";
                break;
            case "LocalSMS":
                shouldRender = !string.IsNullOrEmpty(product?.LocalSMS);
                break;
            case "DownloadSpeed":
                shouldRender = !string.IsNullOrEmpty(product?.DownloadSpeed);
                break;
            case "UploadSpeed":
                shouldRender = !string.IsNullOrEmpty(product?.UploadSpeed);
                break;
            case "FreeWifi":
                shouldRender = product?.FreeWifi == true;
                break;
            case "Free5gRouter":
                shouldRender = product?.Free5gRouter == true;
                break;
            case "Free5gCpeRouter":
                shouldRender = product?.Free5gCpeRouter == true;
                break;
            case "Validity":
                shouldRender = !string.IsNullOrEmpty(product?.Validity);
                break;
            case "DataRollover":
                shouldRender = product?.DataRollover != null && product?.DataRollover > 0;
                break;
            case "FlexMinutes":
                shouldRender = !string.IsNullOrEmpty(product?.FlexMinutes);
                break;
            case "FlexSMS":
                shouldRender = !string.IsNullOrEmpty(product?.FlexSMS);
                break;
            case "SocialMediaDataMbS":
                shouldRender = !string.IsNullOrEmpty(product?.SocialMediaDataMbS);
                break;
            case "DataMbS":
                shouldRender = !string.IsNullOrEmpty(product?.Data_MbS);
                break;
            case "WiFiExtender":
                shouldRender = product?.WiFiExtender == true;
                break;
            case "GiftMessageAvailable":
                shouldRender = product?.GiftMessageAvailable != null && product?.GiftMessageAvailable > 0;
                break;
            case "DedicatedCustomerCare":
                shouldRender = !string.IsNullOrEmpty(product?.DedicatedCustomerCare);
                break;
            case "VanityLandlineNumber":
                shouldRender = !string.IsNullOrEmpty(product?.VanityLandlineNumber);
                break;
            case "LandlineSetupFee":
                shouldRender = !string.IsNullOrEmpty(product?.LandlineSetupFee);
                break;
            case "LandlineMonthlyFee":
                shouldRender = !string.IsNullOrEmpty(product?.LandlineMonthlyFee);
                break;
            case "MobileFreeMinutes":
                shouldRender = product?.MobileFreeMinutes != null && product?.MobileFreeMinutes > 0;
                break;
            case "OffNetLandlineFreeMinutes":
                shouldRender = product?.OffNetLandlineFreeMinutes != null && product?.OffNetLandlineFreeMinutes > 0;
                break;
            case "InstallationFee":
                shouldRender = !string.IsNullOrEmpty(product?.InstallationFee);
                break;
            case "OneFttrEdgeOnt":
                shouldRender = !string.IsNullOrEmpty(product?.OneFttrEdgeOnt);
                break;
            case "OneFttrPrimaryOnt":
                shouldRender = !string.IsNullOrEmpty(product?.OneFttrPrimaryOnt);
                break;
            case "AttrAdditionalEdgeOntIncluding":
                shouldRender = !string.IsNullOrEmpty(product?.AttrAdditionalEdgeOntIncluding);
                break;
            case "SpecialFromDate":
                shouldRender = !string.IsNullOrEmpty(product?.SpecialFromDate);
                break;
            case "ValidDays":
                shouldRender = product?.ValidDays != null && product?.ValidDays > 0;
                break;
            case "CountriesAllowedPerPlan":
                shouldRender = product?.CountriesAllowedPerPlan != null && product?.CountriesAllowedPerPlan > 0;
                break;
            case "AdditionalDataSim":
                shouldRender = product?.AdditionalDataSim == true;
                break;
            case "Memory":
                shouldRender = product?.Memory != null && product?.Memory > 0;
                break;
            case "Capacity":
                shouldRender = product?.Capacity != null && product?.Capacity > 0;
                break;
            case "ColorId":
                shouldRender = product?.ColorId != null && product?.ColorId > 0;
                break;
            case "ColorCode":
                shouldRender = !string.IsNullOrEmpty(product?.ColorCode);
                break;
            case "DeviceId":
                shouldRender = !string.IsNullOrEmpty(product?.DeviceId);
                break;
            case "DeviceImage":
                shouldRender = product?.DeviceImage != null;
                break;
            case "DeviceCategoryId":
                shouldRender = product?.DeviceCategoryId != null && product?.DeviceCategoryId > 0;
                break;
            case "BssPlanId":
                shouldRender = !string.IsNullOrEmpty(product?.BssPlanId);
                break;
            case "CorrelatedId":
                shouldRender = !string.IsNullOrEmpty(product?.CorrelatedId);
                break;
            case "MsrpDisplayActualPriceType":
                shouldRender = product?.MsrpDisplayActualPriceType == true;
                break;
            case "PricingType":
                shouldRender = product?.PricingType?.Any() == true;
                break;
            case "Name":
            case "Price":
            case "Sku":
            case "RecordId":
            case "Id":
            case "Initialize":
                shouldRender = true;
                break;
            case "ShortDescription":
                shouldRender = !string.IsNullOrEmpty(product?.ShortDescription);
                break;
            case "MetaTitle":
                shouldRender = !string.IsNullOrEmpty(product?.MetaTitle);
                break;
            case "MetaDescription":
                shouldRender = !string.IsNullOrEmpty(product?.MetaDescription);
                break;
            case "MetaKeywords":
                shouldRender = !string.IsNullOrEmpty(product?.MetaKeyword);
                break;
            case "UrlKey":
                shouldRender = !string.IsNullOrEmpty(product?.UrlKey);
                break;
            case "TaxClassId":
                shouldRender = product?.TaxClassId != null && product?.TaxClassId > 0;
                break;
            case "OptionsContainer":
                shouldRender = !string.IsNullOrEmpty(product?.OptionsContainer);
                break;
            case "CategoryIds":
                shouldRender = product?.CategoryIds.Any() == true;
                break;
            case "TrialPeriod":
                shouldRender = !string.IsNullOrEmpty(product?.TrialPeriod);
                break;
            case "plugandplay":
                shouldRender = shouldRender = product?.PlugAndPlay == true;
                break;
            case "data_mbs_5g":
                shouldRender = !string.IsNullOrEmpty(product?.Unlimited5G);
                break;
            case "data_mbs":
                shouldRender = !string.IsNullOrEmpty(product?.DataMbS);
                break;
            case "voice":
                shouldRender = product?.Voice != null && product?.Voice > 0;
                break;
            case "plan_detail_pdf":
                shouldRender = !string.IsNullOrEmpty(product?.Plan_Detail_Pdf);
                break;
            case "eligible_countries":
               shouldRender = product?.Eligible_Countries != null && product?.Eligible_Countries > 0;
                break;
            case "eligible_countries_names":
                shouldRender = !string.IsNullOrEmpty(product?.Eligible_Countries_Names);
                break;
            case "country_notes_2":
                shouldRender = !string.IsNullOrEmpty(product?.country_notes_2);
                break;
            case "country_notes_1":
                shouldRender = !string.IsNullOrEmpty(product?.country_notes_1);
                break;
            case "buy_now_link":
                shouldRender = !string.IsNullOrEmpty(product?.buy_now_link);
                break;
            case "dedicated_customer_care":
                shouldRender = !string.IsNullOrEmpty(product?.dedicated_customer_care);
                break;
            case "one_fttr_primary_ont":
                shouldRender = !string.IsNullOrEmpty(product?.one_fttr_primary_ont);
                break;
            case "attr_additional_edge_ont_including_":
                shouldRender = !string.IsNullOrEmpty(product?.attr_additional_edge_ont_including_);
                break;
            case "three_fttr_edge_ont":
                shouldRender = !string.IsNullOrEmpty(product?.three_fttr_edge_ont);
                break;
            case "related_products":
                shouldRender = product.RelatedProductIDs.Any() == true;
                break;
            case "nextgear_xr1000":
                shouldRender = !string.IsNullOrEmpty(product?.nextgear_xr1000);
                break;
            case "free_router":
                shouldRender = product?.free_router == true;
                break;
            case "on_net_landline_free_minutes":
                shouldRender = !string.IsNullOrEmpty(product?.on_net_landline_free_minutes);
                break;
            case "promotion":
                shouldRender = !string.IsNullOrEmpty(product?.Promotion);
                break;

            case "fair_user_limit":
                shouldRender = product?.fair_user_limit == true;
                break;

            case "plan_upgrade":
                shouldRender = product?.plan_upgrade == true;
                break;

            case "plan_downgrade":
                shouldRender = product?.plan_downgrade == true;
                break;

            case "prepaid_bau":
                shouldRender = product?.prepaid_bau == true;
                break;

            case "contract_period":
                shouldRender = !string.IsNullOrEmpty(product?.contract_period);
                break;

            case "sales_channel":
                shouldRender = !string.IsNullOrEmpty(product?.sales_channel);
                break;

            case "vas":
                shouldRender = !string.IsNullOrEmpty(product?.vas);
                break;

            case "ott":
                shouldRender = !string.IsNullOrEmpty(product?.ott);
                break;

            case "offer_ends_march_31st":
                shouldRender = product?.offer_ends_march_31st == true;
                break;

            case "viu_streaming":
                shouldRender = !string.IsNullOrEmpty(product?.streamLineServices);
                break;
            //For Freelance Postpaid
            case "voiceandsms":
                shouldRender = !string.IsNullOrEmpty(product?.voiceandsms);
                break;
            case "ecommerce_partnering_with":
                shouldRender = !string.IsNullOrEmpty(product?.ecommerce_partnering_with);
                break;
            case "fintech_partnering_with":
                shouldRender = !string.IsNullOrEmpty(product?.fintech_partnering_with);
                break;
            case "exclusive":
                shouldRender = product?.exclusive == true;
                break;
            case "free_mifi":
                shouldRender = product?.free_mifi == true;
                break;
            //End 
            default:
                shouldRender = true;
                break;
        }

        return new VisibleFieldRenderResult(shouldRender, templateModifier);
    }


    /// <summary>
    /// For Internet Card Blocks
    /// </summary>
    /// <param name="references"></param>
    /// <returns></returns>
    public static IEnumerable<InternetCardsBlock> GetInternetCardsGrid(IEnumerable<ContentReference> references, CultureInfo cultureInfo)
    {
        var links = new List<InternetCardsBlock>();
        var contentLoader = ServiceLocator.Current.GetInstance<IContentLoader>();
        foreach (var reference in references)
        {
            if (contentLoader.TryGet<InternetCardsBlock>(reference, cultureInfo, out var internetCardBlock))
            {
                links.Add(internetCardBlock);
            }
        }
        return links;
    }

    /// <summary>
    /// For Internet Card Tab Container
    /// </summary>
    /// <param name="references"></param>
    /// <returns></returns>
    public static IEnumerable<TabContainerBlock> GetTabWrappers(IEnumerable<ContentReference> references, CultureInfo cultureInfo)
    {
        var links = new List<TabContainerBlock>();
        var contentLoader = ServiceLocator.Current.GetInstance<IContentLoader>();
        foreach (var reference in references)
        {
            if (contentLoader.TryGet<TabContainerBlock>(reference, cultureInfo, out var internetCardBlock))
            {
                links.Add(internetCardBlock);
            }
        }
        return links;
    }

    /// <summary>
    /// For Tile List Item Block
    /// </summary>
    /// <param name="references"></param>
    /// <returns></returns>
    public static IEnumerable<TileListItemBlock> GetTileListItems(IEnumerable<ContentReference> references, CultureInfo cultureInfo)
    {
        var links = new List<TileListItemBlock>();
        var contentLoader = ServiceLocator.Current.GetInstance<IContentLoader>();
        foreach (var reference in references)
        {
            if (contentLoader.TryGet<TileListItemBlock>(reference, cultureInfo, out var tilelistitems))
            {
                links.Add(tilelistitems);
            }
        }
        return links;
    }
}
