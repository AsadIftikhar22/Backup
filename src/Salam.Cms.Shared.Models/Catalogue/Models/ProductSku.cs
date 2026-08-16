namespace Salam.Cms.Shared.Models.Catalogue.Models;

using EPiServer.Find;
using EPiServer.Find.Api;
using Newtonsoft.Json;
using Salam.Cms.Shared.Models.Catalogue.Data;
using Salam.Cms.Shared.Models.Catalogue.Enums;
using System.Globalization;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Serialization;
using static Salam.Cms.Shared.Models.SalamConstants;

public sealed class ProductSku : Item
{
    public string ProductType { get; set; } = string.Empty;
    public Uri? ProductImage { get; set; }
    public List<int> CategoryIds { get; set; } = new List<int>();
    public List<int> RelatedProductIDs { get; set; } = new List<int>();
    public int? ValidDays { get; set; }
    public string? DataMbS { get; set; }
    public string Data_MbS { get; set; }
    public string? LocalMinutes { get; set; }
    public string? LocalSMS { get; set; }
    public string? SocialMediaDataMbS { get; set; }
    public string? InternationalMinutes { get; set; }
    public bool? AdditionalDataSim { get; set; }
    public int? DataRollover { get; set; }
    public string? BssPlanId { get; set; }
    public string? CorrelatedId { get; set; }
    public int? CategoryId { get; set; }
    public string? ShortDescription { get; set; }
    public string? DataText { get; set; }
    public string? CallLabel { get; set; }
    public string? CallAmountText { get; set; }
    public string BuyButtonRedirection { get; set; }

    #region Device
    public int? ColorId { get; set; }

    // color_code in format "rgb(0-255,0-255,0-255)"
    public string? ColorCode { get; set; }
    public int? Memory { get; set; }
    public int? Capacity { get; set; }
    public string? DeviceId { get; set; }
    public Uri? DeviceImage { get; set; }
    public int? DeviceCategoryId { get; set; }
    public string? DeviceIdEncoded { get; set; }
    #endregion

    #region Fiber
    public string? DownloadSpeed { get; set; }
    public string? UploadSpeed { get; set; }
    public string? voiceandsms { get; set; }
    public string? ecommerce_partnering_with { get; set; }
    public string? fintech_partnering_with { get; set; }
    public bool? exclusive { get; set; }
    public bool? free_mifi { get; set; }
    public Uri? Image { get; set; }
    public Uri? SmallImage { get; set; }
    public Uri? Thumbnail { get; set; }
    public Uri? SwatchImage { get; set; }
    public bool? Free5gCpeRouter { get; set; }
    public bool? FreeWifi { get; set; }
    public bool? Free5gRouter { get; set; }
    public bool? MsrpDisplayActualPriceType { get; set; }
    public bool? RequiredOptions { get; set; }
    public bool? HasOptions { get; set; }
    public string? UrlKey { get; set; }
    public string? PageLayout { get; set; }
    public int? GiftMessageAvailable { get; set; }
    public string? MetaTitle { get; set; }
    public string? MetaKeyword { get; set; }
    public string? MetaDescription { get; set; }
    public string? OptionsContainer { get; set; }
    public string? Validity { get; set; }
    public string? SpanWhereValidityIsNull { get; set; }
    public string? VanityLandlineNumber { get; set; }
    public string? LandlineSetupFee { get; set; }
    public string? LandlineMonthlyFee { get; set; }
    public int? MobileFreeMinutes { get; set; }
    public int? OffNetLandlineFreeMinutes { get; set; }
    public string? OneFttrEdgeOnt { get; set; }
    public string? OneFttrPrimaryOnt { get; set; }
    public string? AttrAdditionalEdgeOntIncluding { get; set; }
    public decimal? SpecialPrice { get; set; }
    public string? SpecialFromDate { get; set; }
    public string InstallationFee { get; set; }
    public string Plan_Detail_Pdf { get; set; }
    public bool? WiFiExtender { get; set; }
    public string? DedicatedCustomerCare { get; set; }
    public int? TaxClassId { get; set; }
    public IEnumerable<PricingType>? PricingType { get; set; }

    public string TrialPeriod { get; set; }
    public bool? PlugAndPlay { get; set; }
    public string Unlimited5G { get; set; }
    public int? Eligible_Countries { get; set; }
    public string Eligible_Countries_Names { get; set; }
    public string buy_now_link { get; set; }
    public string dedicated_customer_care { get; set; }
    public string one_fttr_primary_ont { get; set; }
    public string attr_additional_edge_ont_including_ { get; set; }
    public string three_fttr_edge_ont { get; set; }
    public string nextgear_xr1000 { get; set; }
    public bool? free_router { get; set; }
    public string on_net_landline_free_minutes { get; set; }

    public string eligible_for { get; set; }
    public string vouchers_monthly_free { get; set; }
    public string extra_month_Free { get; set; }
    public string CommitmentPeriod { get; set; }
    public string Promotion { get; set; }

    public string streamLineServices { get; set; }
    #endregion

    #region Visitor
    public string? FlexMinutes { get; set; }
    public string? FlexSMS { get; set; }
    public int? CountriesAllowedPerPlan { get; set; }
    public int? Voice { get; set; }
    #endregion

    #region AddOn

    public string Data { get; set; } = string.Empty;
    public string Banner { get; set; }
    public string CallsMinutes { get; set; } = string.Empty;
    public string PriceString { get; set; }

    public bool? IsAddOn { get; set; }

    public string? RecordId { get; set; }

    public int? Initialize { get; set; }

    public string BuyNowURL { get; set; }

    #endregion

    #region Fiber - Pricing Type

    public string FreeTime { get; set; }

    public string PackageDuration { get; set; } = string.Empty;

    public int ParentProductId { get; set; }

    #endregion

    #region
    public string country_notes_1 { get; set; }
    public string country_notes_2 { get; set; }
    #endregion

    #region Post-Index Enrichment Data

    public List<FrontEndLabelInfo> Labels { get; set; }

    #endregion

    #region Visitor Plan
    public bool? plan_upgrade { get; set; }
    public bool? plan_downgrade { get; set; }
    public bool? prepaid_bau { get; set; }
    public bool? fair_user_limit { get; set; }
    public string contract_period { get; set; }
    public string sales_channel { get; set; }
    public string vas { get; set; }
    public string ott { get; set; }
    public bool? offer_ends_march_31st { get; set; }
    #endregion
    public static int HashToInt(string input)
    {
        using var sha256 = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(input);
        var hash = sha256.ComputeHash(bytes);

        return BitConverter.ToInt32(hash, 0);
    }

    public static int HashToPositiveInt(string input)
    {
        using var sha256 = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(input);
        var hash = sha256.ComputeHash(bytes);
        var longValue = BitConverter.ToUInt64(hash, 0);

        return (int)(longValue % int.MaxValue);
    }

    public ProductSku() { }

    public ProductSku(PricingType pricingType, string typeName, string languageCode)
    {
        Id = HashToInt($"{pricingType.ProductId}_{pricingType.RecordId}");
        LanguageRouting = new LanguageRouting(languageCode);
        Language = languageCode;
        RecordId = pricingType.RecordId;
        FreeTime = pricingType.FreeTime;
        Price = pricingType.Price;
        PackageDuration = pricingType.PackageDuration;
        ProductType = "Fiber";
        CategoryIds = pricingType?.CategoryIds;
        ParentProductId = pricingType.ProductId;
        Name = pricingType.Name;
        Sku = pricingType.Sku;
        DownloadSpeed = pricingType.DownloadSpeed;
        UploadSpeed = pricingType.UploadSpeed;
        CorrelatedId = pricingType.CorrelatedId;
        extra_month_Free = pricingType?.extra_month_Free;
        free_router = pricingType.free_router;
        InstallationFee = pricingType?.InstallationFee;
    }

    public ProductSku(AddOn addOn, string typeName, string languageCode)
    {
        Id = HashToInt($"{addOn.ProductId}_{addOn.RecordId}");
        LanguageRouting = new LanguageRouting(languageCode);
        Language = languageCode;
        Data = addOn.Data;
        InternationalMinutes = addOn.InternationalMinutes;
        CallsMinutes = addOn.CallsMinutes;
        Validity = addOn.Validity;
        PriceString = addOn.Price;
        Banner = addOn.Banner;
        //UnlimitedDataOnly = addOn.UnlimitedDataOnly;
        //CallOnly = addOn.CallOnly;
        //DataOnly = addOn.DataOnly;
        //Using InvariantCulture, parsing still fails even if you construct CultureInfo using language code
        if (!string.IsNullOrEmpty(PriceString))
            Price = decimal.Parse(PriceString.Split(' ')[0], CultureInfo.InvariantCulture);

        CategoryIds = addOn?.CategoryIds;
        RecordId = addOn.RecordId;
        Name = addOn.Name;
        Initialize = addOn.Initialize;
        IsAddOn = true;
        ProductType = "AddOn";
        Sku = addOn.Sku;
        ParentProductId = addOn.ProductId;
    }

    public ProductSku(Item item, string typeName, string baseUrl, string languageCode)
    {
        LanguageRouting = new LanguageRouting(languageCode);
        Language = languageCode;
        Id = item.Id;
        Name = item.Name;
        Sku = item.Sku;
        Price = item.Price;
        ProductType = typeName;
        ProductImage = item.GetAttribute(CatalogueAPIfields.Image)?.GetImageUrl(baseUrl);
        CategoryIds = item.ExtensionAttributes?.CategoryLinks?
            .Select(link =>
            {
                if (int.TryParse(link.CategoryId, out var id)) return (int?)id;
                // silently skip unparseable category ids; logging happens upstream during transformation service
                return null;
            })
            .Where(id => id.HasValue)
            .Select(id => id.Value)
            .ToList();
        CustomAttributes = item.CustomAttributes;

        #region PrepaidPostpaid
        ValidDays = item
            .GetAttribute(CatalogueAPIfields.ValidDays)?
            .GetIntValue();
        DataMbS = item
            .GetAttribute(CatalogueAPIfields.DataMbS)?
            .GetStringValue();
        LocalMinutes = item
            .GetAttribute(CatalogueAPIfields.LocalMinutes)?
            .GetStringValue();
        LocalSMS = item
            .GetAttribute(CatalogueAPIfields.LocalSMS)?
            .GetStringValue();
        SocialMediaDataMbS = item
            .GetAttribute(CatalogueAPIfields.SocialMediaDataMbS)?
            .GetStringValue();
        InternationalMinutes = item
            .GetAttribute(CatalogueAPIfields.InternationalMinutes)?
            .GetStringValue();
        AdditionalDataSim = item
            .GetAttribute(CatalogueAPIfields.AdditionalDataSim)?
            .GetBoolValue();
        DataRollover = item
            .GetAttribute(CatalogueAPIfields.DataRollover)?
            .GetIntValue();
        BssPlanId = item
            .GetAttribute(CatalogueAPIfields.BssPlanID)?
            .GetStringValue();
        CorrelatedId = item
            .GetAttribute(CatalogueAPIfields.CorrelatedId)?
            .GetStringValue();
        ShortDescription = item
            .GetAttribute(CatalogueAPIfields.ShortDescription)?
            .GetStringValue();
        #endregion

        #region Device
        ColorId = item
            .GetAttribute(CatalogueAPIfields.Color)?
            .GetIntValue();
        ColorCode = item
            .GetAttribute(CatalogueAPIfields.ColorCode)?
            .GetStringValue();
        Memory = item
            .GetAttribute(CatalogueAPIfields.Memory)?
            .GetIntValue();
        Capacity = item
            .GetAttribute(CatalogueAPIfields.Capacity)?
            .GetIntValue();
        DeviceId = item
            .GetAttribute(CatalogueAPIfields.DeviceId)?
            .GetStringValue();
        DeviceImage = item
            .GetAttribute(CatalogueAPIfields.Image)?
            .GetImageUrl(baseUrl);
        string? deviceCategoryStr = item
            .GetAttribute(CatalogueAPIfields.CategoryIds)?
            .GetStringListValue()
            .First();
        if (!string.IsNullOrEmpty(deviceCategoryStr))
        {
            DeviceCategoryId = int.Parse(deviceCategoryStr);
        }

        // Base64 encode the deviceId
        if (!string.IsNullOrEmpty(DeviceId))
        {
            DeviceIdEncoded = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes($"Product:{DeviceId}"));
        }
        #endregion

        #region Fiber
        DownloadSpeed = item
            .GetAttribute(CatalogueAPIfields.DownloadSpeed)?
            .GetStringValue();
        UploadSpeed = item
            .GetAttribute(CatalogueAPIfields.UploadSpeed)?
            .GetStringValue();
        PricingType = item
            .GetAttribute(CatalogueAPIfields.PricingTypes)?
            .GetPricingTypes();
        ShortDescription = item
            .GetAttribute(CatalogueAPIfields.ShortDescription)?
            .GetStringValue();
        Image = item
            .GetAttribute(CatalogueAPIfields.Image)?
            .GetImageUrl();
        SmallImage = item
            .GetAttribute(CatalogueAPIfields.SmallImage)?
            .GetImageUrl();
        Free5gCpeRouter = item
            .GetAttribute(CatalogueAPIfields.Free5gCpeRouter)?
            .GetBoolValue();
        FreeWifi = item
            .GetAttribute(CatalogueAPIfields.FreeWifi)?
            .GetBoolValue();
        UrlKey = item
            .GetAttribute(CatalogueAPIfields.UrlKey)?
            .GetStringValue();
        PageLayout = item
            .GetAttribute(CatalogueAPIfields.PageLayout)?
            .GetStringValue();
        GiftMessageAvailable = item
            .GetAttribute(CatalogueAPIfields.GiftMessageAvailable)?
            .GetIntValue();
        Free5gRouter = item
            .GetAttribute(CatalogueAPIfields.Free5gRouter)?
            .GetBoolValue();
        MetaTitle = item
            .GetAttribute(CatalogueAPIfields.MetaTitle)?
            .GetStringValue();
        MetaKeyword = item
            .GetAttribute(CatalogueAPIfields.MetaKeyword)?
            .GetStringValue();
        MetaDescription = item
            .GetAttribute(CatalogueAPIfields.MetaDescription)?
            .GetStringValue();
        OptionsContainer = item
            .GetAttribute(CatalogueAPIfields.OptionsContainer)?
            .GetStringValue();
        Thumbnail = item
            .GetAttribute(CatalogueAPIfields.Thumbnail)?
            .GetImageUrl();
        SwatchImage = item
            .GetAttribute(CatalogueAPIfields.SwatchImage)?
            .GetImageUrl();
        TaxClassId = item
            .GetAttribute(CatalogueAPIfields.TaxClassId)?
            .GetIntValue();
        MsrpDisplayActualPriceType = item
            .GetAttribute(CatalogueAPIfields.MsrpDisplayActualPriceType)?
            .GetBoolValue();
        AdditionalDataSim = item
            .GetAttribute(CatalogueAPIfields.AdditionalDataSim)?
            .GetBoolValue();
        RequiredOptions = item
            .GetAttribute(CatalogueAPIfields.RequiredOptions)?
            .GetBoolValue();
        HasOptions = item
            .GetAttribute(CatalogueAPIfields.HasOptions)?
            .GetBoolValue();
        Validity = item
            .GetAttribute(CatalogueAPIfields.Validity)?
            .GetStringValue();
        VanityLandlineNumber = item
            .GetAttribute(CatalogueAPIfields.VanityLandlineNumber)?
            .GetStringValue();
        LandlineSetupFee = item
            .GetAttribute(CatalogueAPIfields.LandlineSetupFee)?
            .GetStringValue();
        LandlineMonthlyFee = item
            .GetAttribute(CatalogueAPIfields.LandlineMonthlyFee)?
            .GetStringValue();
        MobileFreeMinutes = item
            .GetAttribute(CatalogueAPIfields.MobileFreeMinutes)?
            .GetIntValue();
        OffNetLandlineFreeMinutes = item
            .GetAttribute(CatalogueAPIfields.OffNetLandlineFreeMinutes)?
            .GetIntValue();
        OneFttrEdgeOnt = item
            .GetAttribute(CatalogueAPIfields.OneFttrEdgeOnt)?
            .GetStringValue();
        OneFttrPrimaryOnt = item
            .GetAttribute(CatalogueAPIfields.OneFttrPrimaryOnt)?
            .GetStringValue();
        AttrAdditionalEdgeOntIncluding = item
            .GetAttribute(CatalogueAPIfields.AttrAdditionalEdgeOntIncluding)?
            .GetStringValue();
        SpecialPrice = item
            .GetAttribute(CatalogueAPIfields.SpecialPrice)?
            .GetDecimalValue();
        SpecialFromDate = item
            .GetAttribute(CatalogueAPIfields.SpecialFromDate)?
            .GetStringValue();
        InstallationFee = item
            .GetAttribute(CatalogueAPIfields.InstallationFee)?
            .GetStringValue();
        WiFiExtender = item
            .GetAttribute(CatalogueAPIfields.WiFiExtender)?
            .GetBoolValue();

        CorrelatedId = item
            .GetAttribute(CatalogueAPIfields.CorrelatedId)?
            .GetStringValue();
        voiceandsms = item
                            .GetAttribute(CatalogueAPIfields.voiceandsms)?
                            .GetStringValue();
        ecommerce_partnering_with = item
                                    .GetAttribute(CatalogueAPIfields.ecommerce_partnering_with)?
                                    .GetStringValue();
        fintech_partnering_with = item
                                .GetAttribute(CatalogueAPIfields.fintech_partnering_with)?
                                .GetStringValue();
        exclusive = item
                   .GetAttribute(CatalogueAPIfields.exclusive)?
                   .GetBoolValue();

        free_mifi = item
           .GetAttribute(CatalogueAPIfields.free_mifi)?
           .GetBoolValue();
        #endregion

        #region Visitor
        FlexMinutes = item
                .GetAttribute(CatalogueAPIfields.FlexMinutes)?
                .GetStringValue();
        FlexSMS = item
            .GetAttribute(CatalogueAPIfields.FlexSMS)?
            .GetStringValue();
        Eligible_Countries = item
            .GetAttribute(CatalogueAPIfields.Eligible_Countries)?
            .GetIntValue();
        Voice = item.GetAttribute(CatalogueAPIfields.Voice)?.GetIntValue();
        #endregion

        #region 5G
        UploadSpeed = item
            .GetAttribute(CatalogueAPIfields.UploadSpeed)?
            .GetStringValue();
        Free5gCpeRouter = item
            .GetAttribute(CatalogueAPIfields.Free5gCpeRouter)?
            .GetBoolValue();
        Image = item
            .GetAttribute(CatalogueAPIfields.Image)?
            .GetImageUrl();
        UrlKey = item
            .GetAttribute(CatalogueAPIfields.UrlKey)?
            .GetStringValue();
        SpecialPrice = item
            .GetAttribute(CatalogueAPIfields.SpecialPrice)?
            .GetDecimalValue();
        PageLayout = item
            .GetAttribute(CatalogueAPIfields.PageLayout)?
            .GetStringValue();
        GiftMessageAvailable = item
            .GetAttribute(CatalogueAPIfields.GiftMessageAvailable)?
            .GetIntValue();
        Data_MbS = item
            .GetAttribute(CatalogueAPIfields.Data_MbS)?
            .GetStringValue();
        DownloadSpeed = item
            .GetAttribute(CatalogueAPIfields.DownloadSpeed)?
            .GetStringValue();
        Free5gRouter = item
            .GetAttribute(CatalogueAPIfields.Free5gRouter)?
            .GetBoolValue();
        SmallImage = item
            .GetAttribute(CatalogueAPIfields.SmallImage)?
            .GetImageUrl();
        MetaTitle = item
            .GetAttribute(CatalogueAPIfields.MetaTitle)?
            .GetStringValue();
        SpecialFromDate = item
            .GetAttribute(CatalogueAPIfields.SpecialFromDate)?
            .GetStringValue();
        OptionsContainer = item
            .GetAttribute(CatalogueAPIfields.OptionsContainer)?
            .GetStringValue();
        FreeWifi = item
            .GetAttribute(CatalogueAPIfields.FreeWifi)?
            .GetBoolValue();
        Thumbnail = item
            .GetAttribute(CatalogueAPIfields.Thumbnail)?
            .GetImageUrl();
        MetaKeyword = item
            .GetAttribute(CatalogueAPIfields.MetaKeyword)?
            .GetStringValue();
        SwatchImage = item
            .GetAttribute(CatalogueAPIfields.SwatchImage)?
            .GetImageUrl();
        MetaDescription = item
            .GetAttribute(CatalogueAPIfields.MetaDescription)?
            .GetStringValue();
        TaxClassId = item
            .GetAttribute(CatalogueAPIfields.TaxClassId)?
            .GetIntValue();
        MsrpDisplayActualPriceType = item
            .GetAttribute(CatalogueAPIfields.MsrpDisplayActualPriceType)?
            .GetBoolValue();
        AdditionalDataSim = item
            .GetAttribute(CatalogueAPIfields.AdditionalDataSim)?
            .GetBoolValue();
        RequiredOptions = item
            .GetAttribute(CatalogueAPIfields.RequiredOptions)?
            .GetBoolValue();
        HasOptions = item
            .GetAttribute(CatalogueAPIfields.HasOptions)?
            .GetBoolValue();
        CorrelatedId = item
            .GetAttribute(CatalogueAPIfields.CorrelatedId)?
            .GetStringValue();
        Validity = item
            .GetAttribute(CatalogueAPIfields.Validity)?
            .GetStringValue();

        TrialPeriod = item
         .GetAttribute(CatalogueAPIfields.TrialPeriod)?
         .GetStringValue();

        PlugAndPlay = item
            .GetAttribute(CatalogueAPIfields.PlugAndPlay)?
            .GetBoolValue();

        Plan_Detail_Pdf = item
              .GetAttribute(CatalogueAPIfields.Plan_Detail_Pdf)?
              .GetStringValue();

        //Unlimited5G = item
        // .GetAttribute(CatalogueAPIfields.)?
        // .GetStringValue();

        CategoryIds = item.ExtensionAttributes?.CategoryLinks?
            .Select(link =>
            {
                if (int.TryParse(link.CategoryId, out var id)) return (int?)id;
                return null;
            })
            .Where(id => id.HasValue)
            .Select(id => id.Value)
            .ToList();
        #endregion

        country_notes_2 = item
              .GetAttribute(CatalogueAPIfields.CountryNotes_2)?
              .GetStringValue();

        country_notes_1 = item
      .GetAttribute(CatalogueAPIfields.CountryNotes_1)?
      .GetStringValue();

        buy_now_link = item.GetAttribute(CatalogueAPIfields.buy_now_link)?
            .GetStringValue();

        dedicated_customer_care = item.GetAttribute(CatalogueAPIfields.dedicated_customer_care)?
        .GetStringValue();

        one_fttr_primary_ont = item.GetAttribute(CatalogueAPIfields.one_fttr_primary_ont)?
        .GetStringValue();

        attr_additional_edge_ont_including_ = item.GetAttribute(CatalogueAPIfields.attr_additional_edge_ont_including_)?
         .GetStringValue();

        three_fttr_edge_ont = item.GetAttribute(CatalogueAPIfields.three_fttr_edge_ont)?
         .GetStringValue();

        RelatedProductIDs = item.ExtensionAttributes?.RelatedProducts?
                        .Select(product =>
                        {
                            if (product.Id > 0) return (int?)product.Id;
                            return null;
                        })
                        .Where(id => id.HasValue)
                        .Select(id => id.Value)
                        .ToList();

        nextgear_xr1000 = item.GetAttribute(CatalogueAPIfields.nextgear_xr1000)?
         .GetStringValue();

        free_router = item.GetAttribute(CatalogueAPIfields.free_router)?
                    .GetBoolValue();

        on_net_landline_free_minutes = item.GetAttribute(CatalogueAPIfields.on_net_landline_free_minutes)?
                                        .GetStringValue();

        vouchers_monthly_free = item.GetAttribute(CatalogueAPIfields.vouchers_monthly_free)?
                                .GetStringValue();
        eligible_for = item.GetAttribute(CatalogueAPIfields.eligible_for)?
                        .GetStringValue();

        extra_month_Free = item.GetAttribute(CatalogueAPIfields.extra_month_Free)?
                                .GetStringValue();

        CommitmentPeriod = item.GetAttribute(CatalogueAPIfields.CommitmentPeriod)?
                        .GetStringValue();

        Promotion = item.GetAttribute(CatalogueAPIfields.Promotion)?
        .GetStringValue();

        plan_upgrade = item
            .GetAttribute(CatalogueAPIfields.plan_upgrade)?
            .GetBoolValue();

        plan_downgrade = item
            .GetAttribute(CatalogueAPIfields.plan_downgrade)?
            .GetBoolValue();

        prepaid_bau = item
            .GetAttribute(CatalogueAPIfields.prepaid_bau)?
            .GetBoolValue();

        fair_user_limit = item
            .GetAttribute(CatalogueAPIfields.fair_user_limit)?
            .GetBoolValue();

        contract_period = item.GetAttribute(CatalogueAPIfields.contract_period)?
                          .GetStringValue();
        sales_channel = item.GetAttribute(CatalogueAPIfields.sales_channel)?
                             .GetStringValue();
        vas = item.GetAttribute(CatalogueAPIfields.vas)?
                             .GetStringValue();
        ott = item.GetAttribute(CatalogueAPIfields.ott)?
                             .GetStringValue();
        offer_ends_march_31st = item.GetAttribute(CatalogueAPIfields.offer_ends_march_31st)?
                             .GetBoolValue();
        streamLineServices = item.GetAttribute(CatalogueAPIfields.streamLineServices)?
                             .GetStringValue();
    }
}
