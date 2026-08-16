namespace Salam.Cms.Shared.Models.Catalogue.Data;

using EPiServer.Find.Api;
using Salam.Cms.Shared.Models.Catalogue.Data.Base;
using static Salam.Cms.Shared.Models.SalamConstants;

public class Visitor : ItemBase
{
    public string? DataMbS { get; set; }
    public string? SocialMediaDataMbS { get; set; }
    public string? FlexMinutes { get; set; }
    public string? FlexSMS { get; set; }
    public int? ValidDays { get; set; }
    public int? CountriesAllowedPerPlan { get; set; }
    // shared
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
    public int? TaxClassId { get; set; }
    public List<int>? CategoryIds { get; set; }
    public string? CorrelatedId { get; set; }
    public string? LocalMinutes { get; set; }
    public string? LocalSMS { get; set; }
    public int? DataRollover { get; set; }
    public int? Voice { get; set; }
    public string? Plan_Detail_Pdf { get; set; }
    public int? Eligible_Countries { get; set; }
    public string on_net_landline_free_minutes { get; set; }
    public string Promotion { get; set; }

    #region Visitor Plan
    public bool? plan_upgrade { get; set; }
    public bool? plan_downgrade { get; set; }
    public bool? prepaid_bau { get; set; }
    public bool? fair_user_limit { get; set; }

    #endregion
    public Visitor() { }

    public Visitor(Item item, string language)
    {
        Language = language;
        LanguageRouting = new LanguageRouting(language);
        Id = item.Id;
        Name = item.Name;
        Price = item.Price;
        ValidDays = item
            .GetAttribute(SalamConstants.CatalogueAPIfields.ValidDays)?
            .GetIntValue();
        DataMbS = item
            .GetAttribute(SalamConstants.CatalogueAPIfields.DataMbS)?
            .GetStringValue();
        FlexMinutes = item
            .GetAttribute(SalamConstants.CatalogueAPIfields.FlexMinutes)?
            .GetStringValue();
        FlexSMS = item
            .GetAttribute(SalamConstants.CatalogueAPIfields.FlexSMS)?
            .GetStringValue();
        SocialMediaDataMbS = item
            .GetAttribute(SalamConstants.CatalogueAPIfields.SocialMediaDataMbS)?
            .GetStringValue();
        Eligible_Countries = item
            .GetAttribute(SalamConstants.CatalogueAPIfields.Eligible_Countries)?
            .GetIntValue();
        // shared
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
        CorrelatedId = item
            .GetAttribute(SalamConstants.CatalogueAPIfields.CorrelatedId)?
            .GetStringValue();
        LocalMinutes = item
            .GetAttribute(SalamConstants.CatalogueAPIfields.LocalMinutes)?
            .GetStringValue();

        LocalSMS = item
     .GetAttribute(SalamConstants.CatalogueAPIfields.LocalSMS)?
     .GetStringValue();

        DataRollover = item
            .GetAttribute(SalamConstants.CatalogueAPIfields.DataRollover)?
            .GetIntValue();
        Voice = item.GetAttribute(SalamConstants.CatalogueAPIfields.Voice)?.GetIntValue();

        Plan_Detail_Pdf = item.GetAttribute(SalamConstants.CatalogueAPIfields.Plan_Detail_Pdf)?
            .GetStringValue();


        on_net_landline_free_minutes = item.GetAttribute(CatalogueAPIfields.on_net_landline_free_minutes)?
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
    }
}
