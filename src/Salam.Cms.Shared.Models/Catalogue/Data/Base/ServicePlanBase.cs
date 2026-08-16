namespace Salam.Cms.Shared.Models.Catalogue.Data.Base;

using EPiServer.Find.Api;
using Salam.Cms.Shared.Models.Catalogue.Data;
using static Salam.Cms.Shared.Models.SalamConstants;

// base class for Prepaid / Postpaid
public abstract class ServicePlanBase : ItemBase
{
    public int? ValidDays { get; set; }
    public string? DataMbS { get; set; }
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
    public string? Plan_Detail_Pdf { get; set; }
    public string FlexMinutes { get; set; }
    public string nextgear_xr1000 { get; set; }
    public string free_router { get; set; }
    public string on_net_landline_free_minutes { get; set; }
    public string eligible_for { get; set; }
    public string vouchers_monthly_free { get; set; }
    public string extra_month_Free { get; set; }
    public string CommitmentPeriod { get; set; }
    public string Promotion { get; set; }
    public string contract_period { get; set; }
    public string sales_channel { get; set; }
    public string vas { get; set; }
    public string ott { get; set; }
    public bool? offer_ends_march_31st { get; set; }
    public bool? streamLineServices { get; set; }
    public int? Eligible_Countries { get; set; }
    public string? voiceandsms { get; set; }
    public string? ecommerce_partnering_with { get; set; }
    public string? fintech_partnering_with { get; set; }
    public bool? exclusive { get; set; }
    public bool? free_mifi { get; set; }
    public ServicePlanBase() { }

    public ServicePlanBase(Item item, string language)
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
        LocalMinutes = item
            .GetAttribute(SalamConstants.CatalogueAPIfields.LocalMinutes)?
            .GetStringValue();
        LocalSMS = item
            .GetAttribute(SalamConstants.CatalogueAPIfields.LocalSMS)?
            .GetStringValue();
        SocialMediaDataMbS = item
            .GetAttribute(SalamConstants.CatalogueAPIfields.SocialMediaDataMbS)?
            .GetStringValue();
        InternationalMinutes = item
            .GetAttribute(SalamConstants.CatalogueAPIfields.InternationalMinutes)?
            .GetStringValue();
        AdditionalDataSim = item
            .GetAttribute(SalamConstants.CatalogueAPIfields.AdditionalDataSim)?
            .GetBoolValue();
        DataRollover = item
            .GetAttribute(SalamConstants.CatalogueAPIfields.DataRollover)?
            .GetIntValue();
        BssPlanId = item
            .GetAttribute(SalamConstants.CatalogueAPIfields.BssPlanID)?
            .GetStringValue();
        CorrelatedId = item
            .GetAttribute(SalamConstants.CatalogueAPIfields.CorrelatedId)?
            .GetStringValue();
        var categoryIds = item
            .GetAttribute(SalamConstants.CatalogueAPIfields.CategoryIds)?
            .GetStringListValue();
        CategoryId = categoryIds != null && categoryIds.Any()
            ? Int32.Parse(categoryIds.First())
            : 0;
        ShortDescription = item
            .GetAttribute(SalamConstants.CatalogueAPIfields.ShortDescription)?
            .GetStringValue();

        Plan_Detail_Pdf = item.GetAttribute(SalamConstants.CatalogueAPIfields.Plan_Detail_Pdf)?
        .GetStringValue();

        FlexMinutes = item.GetAttribute(SalamConstants.CatalogueAPIfields.FlexMinutes)?
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

        CommitmentPeriod = item.GetAttribute(CatalogueAPIfields.CommitmentPeriod)?
                .GetStringValue();

        Promotion=item.GetAttribute(CatalogueAPIfields.Promotion)?
                .GetStringValue();
        offer_ends_march_31st = item.GetAttribute(CatalogueAPIfields.offer_ends_march_31st)?
                .GetBoolValue(); 
        contract_period = item.GetAttribute(CatalogueAPIfields.contract_period)?
                  .GetStringValue();
        sales_channel = item.GetAttribute(CatalogueAPIfields.sales_channel)?
                             .GetStringValue();
        vas = item.GetAttribute(CatalogueAPIfields.vas)?
                             .GetStringValue();
        ott = item.GetAttribute(CatalogueAPIfields.ott)?
                             .GetStringValue();

        streamLineServices= item
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
