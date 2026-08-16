namespace Salam.Cms.Shared.Models.Catalogue.Data;

using EPiServer.Find.Api;
using Salam.Cms.Shared.Models.Catalogue.Data.Base;
using static Salam.Cms.Shared.Models.SalamConstants;

public class DataPlan : ItemBase
{
    public int? ValidDays { get; set; }
    public string? DataMbS { get; set; }
    public string? SocialMediaDataMbS { get; set; }
    public string? Plan_Detail_Pdf { get; set; }
    public string CountryNotes_1 { get; set; }
    public string CountryNotes_2 { get; set; }
    public string nextgear_xr1000 { get; set; }
    public string free_router { get; set; }
    public string on_net_landline_free_minutes { get; set; }
    public string eligible_for { get; set; }
    public string vouchers_monthly_free { get; set; }
    public string extra_month_Free { get; set; }
    public string Promotion { get; set; }
    public bool? offer_ends_march_31st { get; set; }
    public DataPlan() { }

    public DataPlan(Item item, string language)
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
        SocialMediaDataMbS = item
            .GetAttribute(SalamConstants.CatalogueAPIfields.SocialMediaDataMbS)?
            .GetStringValue();

        Plan_Detail_Pdf = item.GetAttribute(SalamConstants.CatalogueAPIfields.Plan_Detail_Pdf)?
        .GetStringValue();

        CountryNotes_2 = item.GetAttribute(SalamConstants.CatalogueAPIfields.CountryNotes_2)?
        .GetStringValue();

        CountryNotes_1 = item.GetAttribute(SalamConstants.CatalogueAPIfields.CountryNotes_1)?
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
        Promotion= item.GetAttribute(CatalogueAPIfields.Promotion)?
                                .GetStringValue(); 
        offer_ends_march_31st = item.GetAttribute(CatalogueAPIfields.offer_ends_march_31st)?
                .GetBoolValue();
    }
}
