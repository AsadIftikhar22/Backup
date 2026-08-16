namespace Salam.Cms.Shared.Models.Catalogue.Data;
using EPiServer.Find;
using EPiServer.Find.Api;
using Salam.Cms.Shared.Models.Catalogue.Data.Base;
using Salam.Cms.Shared.Models.Catalogue.Enums;
using System.Collections.Generic;
using static Salam.Cms.Shared.Models.SalamConstants;

public class Fiber : ItemBase
{
    public string? DownloadSpeed { get; set; }
    public string? UploadSpeed { get; set; }
    public string? ShortDescription { get; set; }
    public Uri? Image { get; set; }
    public Uri? SmallImage { get; set; }
    public Uri? Thumbnail { get; set; }
    public Uri? SwatchImage { get; set; }
    public bool? Free5gCpeRouter { get; set; }
    public bool? FreeWifi { get; set; }
    public bool? Free5gRouter { get; set; }
    public bool? MsrpDisplayActualPriceType { get; set; }
    public bool? AdditionalDataSim { get; set; }
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
    public decimal? InstallationFee { get; set; }
    public bool? WiFiExtender { get; set; }
    public string? DedicatedCustomerCare { get; set; }
    public int? TaxClassId { get; set; }
    public List<int>? CategoryIds { get; set; }
    public string? CorrelatedId { get; set; }
    public string? Plan_Detail_Pdf { get; set; }
    public IEnumerable<PricingType>? PricingType { get; set; }
    public string FlexMinutes { get; set; }

    public string? dedicated_customer_care { get; set; }
    public string? one_fttr_primary_ont { get; set; }
    public string? attr_additional_edge_ont_including_ { get; set; }
    public string? three_fttr_edge_ont { get; set; }
    public string nextgear_xr1000 { get; set; }
    public string on_net_landline_free_minutes { get; set; }
    public string free_router { get; set; }
    public string eligible_for { get; set; }
    public string vouchers_monthly_free { get; set; }
    public string extra_month_Free { get; set; }
    public string Promotion { get; set; }
    public string contract_period { get; set; }
    public string sales_channel { get; set; }
    public string vas { get; set; }
    public string ott { get; set; }
    public bool? offer_ends_march_31st { get; set; }
    public int? Eligible_Countries { get; set; }
    public bool? streamLineServices { get; set; }
    public string? voiceandsms { get; set; }
    public string? ecommerce_partnering_with { get; set; }
    public string? fintech_partnering_with { get; set; }
    public bool? exclusive { get; set; }
    public bool? free_mifi { get; set; }
    public Fiber() { }
    public Fiber(Item item, string languageCode)
    {
        LanguageRouting = new LanguageRouting(languageCode);
        Language = languageCode;
        Id = item.Id;
        Name = item.Name;
        DownloadSpeed = item
            .GetAttribute(SalamConstants.CatalogueAPIfields.DownloadSpeed)?
            .GetStringValue();
        UploadSpeed = item
            .GetAttribute(SalamConstants.CatalogueAPIfields.UploadSpeed)?
            .GetStringValue();
        PricingType = item
            .GetAttribute(SalamConstants.CatalogueAPIfields.PricingTypes)?
            .GetPricingTypes();
        ShortDescription = item
            .GetAttribute(SalamConstants.CatalogueAPIfields.ShortDescription)?
            .GetStringValue();
        Image = item
            .GetAttribute(SalamConstants.CatalogueAPIfields.Image)?
            .GetImageUrl();
        SmallImage = item
            .GetAttribute(SalamConstants.CatalogueAPIfields.SmallImage)?
            .GetImageUrl();
        Free5gCpeRouter = item
            .GetAttribute(SalamConstants.CatalogueAPIfields.Free5gCpeRouter)?
            .GetBoolValue();
        FreeWifi = item
            .GetAttribute(SalamConstants.CatalogueAPIfields.FreeWifi)?
            .GetBoolValue();
        UrlKey = item
            .GetAttribute(SalamConstants.CatalogueAPIfields.UrlKey)?
            .GetStringValue();
        PageLayout = item
            .GetAttribute(SalamConstants.CatalogueAPIfields.PageLayout)?
            .GetStringValue();
        GiftMessageAvailable = item
            .GetAttribute(SalamConstants.CatalogueAPIfields.GiftMessageAvailable)?
            .GetIntValue();
        Free5gRouter = item
            .GetAttribute(SalamConstants.CatalogueAPIfields.Free5gRouter)?
            .GetBoolValue();
        MetaTitle = item
            .GetAttribute(SalamConstants.CatalogueAPIfields.MetaTitle)?
            .GetStringValue();
        MetaKeyword = item
            .GetAttribute(SalamConstants.CatalogueAPIfields.MetaKeyword)?
            .GetStringValue();
        MetaDescription = item
            .GetAttribute(SalamConstants.CatalogueAPIfields.MetaDescription)?
            .GetStringValue();
        OptionsContainer = item
            .GetAttribute(SalamConstants.CatalogueAPIfields.OptionsContainer)?
            .GetStringValue();
        Thumbnail = item
            .GetAttribute(SalamConstants.CatalogueAPIfields.Thumbnail)?
            .GetImageUrl();
        SwatchImage = item
            .GetAttribute(SalamConstants.CatalogueAPIfields.SwatchImage)?
            .GetImageUrl();
        TaxClassId = item
            .GetAttribute(SalamConstants.CatalogueAPIfields.TaxClassId)?
            .GetIntValue();
        MsrpDisplayActualPriceType = item
            .GetAttribute(SalamConstants.CatalogueAPIfields.MsrpDisplayActualPriceType)?
            .GetBoolValue();
        AdditionalDataSim = item
            .GetAttribute(SalamConstants.CatalogueAPIfields.AdditionalDataSim)?
            .GetBoolValue();
        CategoryIds = item
            .GetAttribute(SalamConstants.CatalogueAPIfields.CategoryIds)?
            .GetStringListValue()?
            .Select(id =>
            {
                if (int.TryParse(id, out var parsedId))
                    return (int?)parsedId;
                return null;
            })
            .Where(id => id.HasValue)
            .Select(id => id.Value)
            .ToList();
        RequiredOptions = item
            .GetAttribute(SalamConstants.CatalogueAPIfields.RequiredOptions)?
            .GetBoolValue();
        HasOptions = item
            .GetAttribute(SalamConstants.CatalogueAPIfields.HasOptions)?
            .GetBoolValue();
        Validity = item
            .GetAttribute(SalamConstants.CatalogueAPIfields.Validity)?
            .GetStringValue();
        VanityLandlineNumber = item
            .GetAttribute(SalamConstants.CatalogueAPIfields.VanityLandlineNumber)?
            .GetStringValue();
        LandlineSetupFee = item
            .GetAttribute(SalamConstants.CatalogueAPIfields.LandlineSetupFee)?
            .GetStringValue();
        LandlineMonthlyFee = item
            .GetAttribute(SalamConstants.CatalogueAPIfields.LandlineMonthlyFee)?
            .GetStringValue();
        MobileFreeMinutes = item
            .GetAttribute(SalamConstants.CatalogueAPIfields.MobileFreeMinutes)?
            .GetIntValue();
        OffNetLandlineFreeMinutes = item
            .GetAttribute(SalamConstants.CatalogueAPIfields.OffNetLandlineFreeMinutes)?
            .GetIntValue();
        OneFttrEdgeOnt = item
            .GetAttribute(SalamConstants.CatalogueAPIfields.OneFttrEdgeOnt)?
            .GetStringValue();
        OneFttrPrimaryOnt = item
            .GetAttribute(SalamConstants.CatalogueAPIfields.OneFttrPrimaryOnt)?
            .GetStringValue();
        AttrAdditionalEdgeOntIncluding = item
            .GetAttribute(SalamConstants.CatalogueAPIfields.AttrAdditionalEdgeOntIncluding)?
            .GetStringValue();
        SpecialPrice = item
            .GetAttribute(SalamConstants.CatalogueAPIfields.SpecialPrice)?
            .GetDecimalValue();
        SpecialFromDate = item
            .GetAttribute(SalamConstants.CatalogueAPIfields.SpecialFromDate)?
            .GetStringValue();
        InstallationFee = item
            .GetAttribute(SalamConstants.CatalogueAPIfields.InstallationFee)?
            .GetDecimalValue();
        WiFiExtender = item
            .GetAttribute(SalamConstants.CatalogueAPIfields.WiFiExtender)?
            .GetBoolValue();
        CorrelatedId = item
            .GetAttribute(SalamConstants.CatalogueAPIfields.CorrelatedId)?
            .GetStringValue();
        Plan_Detail_Pdf = item.GetAttribute(SalamConstants.CatalogueAPIfields.Plan_Detail_Pdf)?
            .GetStringValue();
        FlexMinutes = item.GetAttribute(SalamConstants.CatalogueAPIfields.FlexMinutes)?
          .GetStringValue();


        dedicated_customer_care = item.GetAttribute(CatalogueAPIfields.dedicated_customer_care)?
        .GetStringValue();

        one_fttr_primary_ont = item.GetAttribute(CatalogueAPIfields.one_fttr_primary_ont)?
        .GetStringValue();

        attr_additional_edge_ont_including_ = item.GetAttribute(CatalogueAPIfields.attr_additional_edge_ont_including_)?
         .GetStringValue();

        three_fttr_edge_ont = item.GetAttribute(CatalogueAPIfields.three_fttr_edge_ont)?
         .GetStringValue();

        free_router = item.GetAttribute(SalamConstants.CatalogueAPIfields.free_router)?
            .GetStringValue();

        nextgear_xr1000 = item.GetAttribute(SalamConstants.CatalogueAPIfields.nextgear_xr1000)?
            .GetStringValue();


        on_net_landline_free_minutes = item.GetAttribute(CatalogueAPIfields.on_net_landline_free_minutes)?
                                        .GetStringValue();


        vouchers_monthly_free = item.GetAttribute(CatalogueAPIfields.vouchers_monthly_free)?
                                .GetStringValue();
        eligible_for = item.GetAttribute(CatalogueAPIfields.eligible_for)?
                        .GetStringValue();

        extra_month_Free = item.GetAttribute(CatalogueAPIfields.extra_month_Free)?
                                .GetStringValue();

        Promotion = item.GetAttribute(CatalogueAPIfields.Promotion)?
                        .GetStringValue();

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

        streamLineServices = item
            .GetAttribute(SalamConstants.CatalogueAPIfields.streamLineServices)?
            .GetBoolValue();
        Eligible_Countries = item
            .GetAttribute(SalamConstants.CatalogueAPIfields.Eligible_Countries)?
            .GetIntValue();
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
    }
}
