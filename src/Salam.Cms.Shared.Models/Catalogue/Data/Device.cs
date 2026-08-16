namespace Salam.Cms.Shared.Models.Catalogue.Data;
using EPiServer.Find.Api;
using Salam.Cms.Shared.Models.Catalogue.Data.Base;
using System;
using System.Linq;
using static Salam.Cms.Shared.Models.SalamConstants;

public class Device : ItemBase
{
    // color is attribute_id where attribute_code='color' related to AttributeDefinition with AttributeOptions
    public int? ColorId { get; set; }

    // color_code in format "rgb(0-255,0-255,0-255)"
    public string? ColorCode { get; set; }
    public int? Memory { get; set; }
    public int? Capacity { get; set; }
    public string? DeviceId { get; set; }
    public Uri? DeviceImage { get; set; }
    public int? DeviceCategoryId { get; set; }
    public string? DeviceIdEncoded { get; set; }
    public string? Plan_Detail_Pdf { get; set; }
    public string eligible_for { get; set; }
    public string vouchers_monthly_free { get; set; }
    public string extra_month_Free { get; set; }

    public Device() { }

    public Device(Item item, string language)
    {
        Language = language;
        LanguageRouting = new LanguageRouting(language);
        Id = item.Id;
        Name = item.Name;
        Price = item.Price;
        ColorId = item
            .GetAttribute(SalamConstants.CatalogueAPIfields.Color)?
            .GetIntValue();
        ColorCode = item
            .GetAttribute(SalamConstants.CatalogueAPIfields.ColorCode)?
            .GetStringValue();
        Memory = item
            .GetAttribute(SalamConstants.CatalogueAPIfields.Memory)?
            .GetIntValue();
        Capacity = item
            .GetAttribute(SalamConstants.CatalogueAPIfields.Capacity)?
            .GetIntValue();
        DeviceId = item
            .GetAttribute(SalamConstants.CatalogueAPIfields.DeviceId)?
            .GetStringValue();
        DeviceImage = item
            .GetAttribute(SalamConstants.CatalogueAPIfields.Image)?
            .GetImageUrl();

        Plan_Detail_Pdf = item.GetAttribute(SalamConstants.CatalogueAPIfields.Plan_Detail_Pdf)?
                .GetStringValue();

        vouchers_monthly_free = item.GetAttribute(CatalogueAPIfields.vouchers_monthly_free)?
                .GetStringValue();

        eligible_for = item.GetAttribute(CatalogueAPIfields.eligible_for)?
                        .GetStringValue();

        extra_month_Free = item.GetAttribute(CatalogueAPIfields.extra_month_Free)?
                                .GetStringValue();
        string? deviceCategoryStr = item
            .GetAttribute(SalamConstants.CatalogueAPIfields.CategoryIds)?
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

    }

}
