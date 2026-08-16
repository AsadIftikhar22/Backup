using Microsoft.AspNetCore.Http;

namespace Salam.Cms.Shared.Models;
using Microsoft.AspNetCore.Http;

public class SalamConstants
{
    public static class RoleNames
    {
        public const string AdminTeam = "WebAdmins";

        public const string ContentTeam = "WebEditors";

        public const string DataTeam = "AnalyticsAdmin";

        public const string SeoTeam = "SeoAdmin";

        public const string SecurityAdmins = "SecurityAdmins";
    }

    public static class CatalogueAPIfields
    {
        public const string MetaTitle = "meta_title";
        public const string MetaKeyword = "meta_keyword";
        public const string MetaDescription = "meta_description";
        public const string ValidDays = "valid_days";
        public const string Data_MbS = "data_mbs";
        public const string DataMbS = "data_mbs";
        public const string LocalMinutes = "local_minutes";
        public const string LocalSMS = "local_sms";
        public const string FlexMinutes = "flex_minutes";
        public const string FlexSMS = "flex_sms";
        public const string SocialMediaDataMbS = "social_media_data_mbs";
        public const string ShortDescription = "short_description";
        public const string InternationalMinutes = "international_mins";
        public const string AdditionalDataSim = "additional_data_sim";
        public const string DataRollover = "data_rollover";
        public const string BssPlanID = "bss_plan_id";
        public const string Free5gCpeRouter = "free_5g_cpe_router";
        public const string FreeWifi = "free_wifi";
        public const string UrlKey = "url_key";
        public const string PageLayout = "page_layout";
        public const string GiftMessageAvailable = "gift_message_available";
        public const string Free5gRouter = "free_5g_router";
        public const string OptionsContainer = "options_container";
        public const string Thumbnail = "thumbnail";
        public const string SwatchImage = "swatch_image";
        public const string Color = "color";
        public const string ColorCode = "color_code";
        public const string Capacity = "capacity";
        public const string CategoryIds = "category_ids";
        public const string CorrelatedId = "correlated_id";
        public const string Memory = "memory";
        public const string Image = "image";
        public const string SmallImage = "small_image";
        public const string Eligible_Countries = "eligible_countries";
        public const string DownloadSpeed = "download_speed";
        public const string UploadSpeed = "upload_speed";
        public const string PricingTypes = "pricingtypes";
        public const string DeviceId = "deviceid";
        public const string TaxClassId = "tax_class_id";
        public const string MsrpDisplayActualPriceType = "msrp_display_actual_price_type";
        public const string RequiredOptions = "required_options";
        public const string HasOptions = "has_options";
        public const string Validity = "validity";
        public const string CommitmentPeriod = "commitment_period";
        public const string VanityLandlineNumber = "vanity_landline_number";
        public const string voiceandsms = "voiceandsms";
        public const string ecommerce_partnering_with = "ecommerce_partnering_with";
        public const string fintech_partnering_with = "fintech_partnering_with";
        public const string exclusive = "exclusive";
        public const string free_mifi = "free_mifi";
        public const string MobileFreeMinutes = "mobile_free_minutes";

        public const string OffNetLandlineFreeMinutes = "off_net_landline_free_minutes";
        public const string OneFttrEdgeOnt = "one_fttr_edge_ont";
        public const string OneFttrPrimaryOnt = "one_fttr_primary_ont";
        public const string AttrAdditionalEdgeOntIncluding = "attr_additional_edge_ont_including_";
        public const string SpecialPrice = "special_price";
        public const string SpecialFromDate = "special_from_date";
        public const string InstallationFee = "installation_fee";
        public const string WiFiExtender = "wi_fi_extender";
        //public const string DedicatedCustomerCare = "dedicated_customer_care";
        public const string Name = "name";
        public const string Price = "price";
        public const string ProductSku = "product_sku";
        public const string ProductType = "product_type";
        public const string VisibleFieldsDelimiter = "-";
        public const string VisibleFieldsDelimiterOld = " - ";
        public const string TrialPeriod = "trial_period";
        public const string PlugAndPlay = "plugandplay";
        public const string Unlimited5G = "data_mbs_5g";
        public const string Voice = "voice";
        public const string Plan_Detail_Pdf = "plan_detail_pdf";
        public const string Eligible_Countries_Names = "eligible_countries_names";
        public const string CountryNotes_1 = "country_notes_1";
        public const string CountryNotes_2 = "country_notes_2";
        public const string buy_now_link = "buy_now_link";

        public const string dedicated_customer_care = "dedicated_customer_care";
        public const string one_fttr_primary_ont = "one_fttr_primary_ont";
        public const string attr_additional_edge_ont_including_ = "attr_additional_edge_ont_including_";
        public const string three_fttr_edge_ont = "three_fttr_edge_ont";
        public const string related_products = "related_products";

        public const string free_router = "free_router";
        public const string nextgear_xr1000 = "nextgear_xr1000";
        public const string on_net_landline_free_minutes = "on_net_landline_free_minutes";

        public const string eligible_for = "eligible_for";
        public const string vouchers_monthly_free = "vouchers_monthly_free";
        public const string extra_month_Free = "extra_month_Free";
        public const string LandlineMonthlyFee = "landline_monthly_fee";
        public const string LandlineSetupFee = "landline_setup_fee";

        public const string Promotion = "promotion";

        public const string offer_ends_march_31st = "offer_ends_march_31st";
        public const string contract_period = "contract_period";
        public const string sales_channel = "sales_channel";
        public const string vas = "vas";
        public const string ott = "ott";
        public const string UnlimitedSocialDataOnly = "UnlimitedSocialDataOnly";
        public const string streamLineServices = "viu_streaming";
        #region Visitor Plan
        public const string plan_upgrade = "plan_upgrade";
        public const string plan_downgrade = "plan_downgrade";
        public const string prepaid_bau = "prepaid_bau";
        public const string fair_user_limit = "fair_user_limit";

        #endregion
    }

    public static class AssetLibraryConstants
    {
        public const string AssetLibrary = "Asset Library";
        public const string IconLibrary = "Icon Library";
    }
}
