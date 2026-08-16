namespace Salam.Cms.Shared.Models.Catalogue.Data;

using EPiServer.Find.Api;
using Salam.Cms.Shared.Models.Catalogue.Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using static Salam.Cms.Shared.Models.SalamConstants;

public class FiveG : ItemBase
{
    public string? UploadSpeed { get; set; }

    public bool? Free5gCpeRouter { get; set; }

    public Uri? Image { get; set; }
    public string? UrlKey { get; set; }
    public decimal? SpecialPrice { get; set; }

    public string? PageLayout { get; set; }

    public int? GiftMessageAvailable { get; set; }

    public string? DownloadSpeed { get; set; }

    public bool? Free5gRouter { get; set; }

    public Uri? SmallImage { get; set; }

    public string? MetaTitle { get; set; }

    public string? SpecialFromDate { get; set; }
    public string? OptionsContainer { get; set; }
    public bool? FreeWifi { get; set; }
    public Uri? Thumbnail { get; set; }

    public string? MetaKeyword { get; set; }
    public Uri? SwatchImage { get; set; }
    public string? MetaDescription { get; set; }
    public int? TaxClassId { get; set; }
    public bool? MsrpDisplayActualPriceType { get; set; }
    public bool? AdditionalDataSim { get; set; }
    public List<int>? CategoryIds { get; set; }
    public bool? RequiredOptions { get; set; }

    public string Data_MbS { get; set; }
    public bool? HasOptions { get; set; }
    public string? CorrelatedId { get; set; }
    public string? Validity { get; set; }
    public string? TrialPeriod { get; set; }
    public bool? PlugAndPlay { get; set; }
    public string? Unlimited5G { get; set; }
    public string? Plan_Detail_Pdf { get; set; }
    public string nextgear_xr1000 { get; set; }
    public string free_router { get; set; }
    public string on_net_landline_free_minutes { get; set; }
    public string CommitmentPeriod { get; set; }
    public string eligible_for { get; set; }
    public string vouchers_monthly_free { get; set; }
    public string extra_month_Free { get; set; }
    public string Promotion { get; set; }
    public string contract_period { get; set; }
    public string sales_channel { get; set; }
    public string vas { get; set; }
    public string ott { get; set; }
    public bool? offer_ends_march_31st { get; set; }

    public FiveG()
    {
    }

    public FiveG(Item item, string languageCode)
    {
        LanguageRouting = new LanguageRouting(languageCode);
        Language = languageCode;
        Id = item.Id;
        Name = item.Name;

        UploadSpeed = item
            .GetAttribute(SalamConstants.CatalogueAPIfields.UploadSpeed)?
            .GetStringValue();

        DownloadSpeed = item
            .GetAttribute(SalamConstants.CatalogueAPIfields.DownloadSpeed)?
            .GetStringValue();

        Free5gCpeRouter = item
            .GetAttribute(SalamConstants.CatalogueAPIfields.Free5gCpeRouter)?
            .GetBoolValue();

        Free5gRouter = item
            .GetAttribute(SalamConstants.CatalogueAPIfields.Free5gRouter)?
            .GetBoolValue();

        FreeWifi = item
            .GetAttribute(SalamConstants.CatalogueAPIfields.FreeWifi)?
            .GetBoolValue();

        Image = item
            .GetAttribute(SalamConstants.CatalogueAPIfields.Image)?
            .GetImageUrl();

        SmallImage = item
            .GetAttribute(SalamConstants.CatalogueAPIfields.SmallImage)?
            .GetImageUrl();

        Thumbnail = item
            .GetAttribute(SalamConstants.CatalogueAPIfields.Thumbnail)?
            .GetImageUrl();

        SwatchImage = item
            .GetAttribute(SalamConstants.CatalogueAPIfields.SwatchImage)?
            .GetImageUrl();

        UrlKey = item
            .GetAttribute(SalamConstants.CatalogueAPIfields.UrlKey)?
            .GetStringValue();

        PageLayout = item
            .GetAttribute(SalamConstants.CatalogueAPIfields.PageLayout)?
            .GetStringValue();

        GiftMessageAvailable = item
            .GetAttribute(SalamConstants.CatalogueAPIfields.GiftMessageAvailable)?
            .GetIntValue();

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

        SpecialPrice = item
            .GetAttribute(SalamConstants.CatalogueAPIfields.SpecialPrice)?
            .GetDecimalValue();

        SpecialFromDate = item
            .GetAttribute(SalamConstants.CatalogueAPIfields.SpecialFromDate)?
            .GetStringValue();

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

        CorrelatedId = item
            .GetAttribute(SalamConstants.CatalogueAPIfields.CorrelatedId)?
            .GetStringValue();

        Validity = item
            .GetAttribute(SalamConstants.CatalogueAPIfields.Validity)?
            .GetStringValue();

        TrialPeriod = item.GetAttribute(SalamConstants.CatalogueAPIfields.TrialPeriod)?
            .GetStringValue();
        Data_MbS = item.GetAttribute(CatalogueAPIfields.Data_MbS)?
                   .GetStringValue();

        PlugAndPlay = item.GetAttribute(SalamConstants.CatalogueAPIfields.PlugAndPlay)?
            .GetBoolValue();

        Unlimited5G = item.GetAttribute(SalamConstants.CatalogueAPIfields.Unlimited5G)?
            .GetStringValue();

        Plan_Detail_Pdf = item.GetAttribute(SalamConstants.CatalogueAPIfields.Plan_Detail_Pdf)?
            .GetStringValue();

        free_router = item.GetAttribute(SalamConstants.CatalogueAPIfields.free_router)?
    .GetStringValue();

        nextgear_xr1000 = item.GetAttribute(SalamConstants.CatalogueAPIfields.nextgear_xr1000)?
            .GetStringValue();


        on_net_landline_free_minutes = item.GetAttribute(CatalogueAPIfields.on_net_landline_free_minutes)?
                                        .GetStringValue();


        CommitmentPeriod = item.GetAttribute(CatalogueAPIfields.CommitmentPeriod)?
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
    }

}
