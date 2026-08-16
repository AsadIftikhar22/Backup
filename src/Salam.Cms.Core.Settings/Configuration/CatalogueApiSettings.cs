namespace Salam.Cms.Core.Settings.Configuration;

public class CatalogueApiSettings
{
    public string BaseUrl { get; set; } = string.Empty;
    public string BaseUrlForMedia { get; set; } = string.Empty;
    public string PrepaidPostpaidApiUrl { get; set; } = string.Empty;
    public string DataPlanApiUrl { get; set; } = string.Empty;
    public string DeviceApiUrl { get; set; } = string.Empty;
    public string FiberApiUrl { get; set; } = string.Empty;
    public string VisitorApiUrl { get; set; } = string.Empty;
    public string AddOnApiUrl { get; set; } = string.Empty;
    public string FiveGApiUrl { get; set; } = string.Empty;
    public string DeviceHandoffBaseUrl { get; set; } = string.Empty;
    public string PlanHandoffBaseUrl { get; set; } = string.Empty;
    public string FiberHandoffBaseUrl { get; set; } = string.Empty;
    public string FiveGHandoffBaseUrl { get; set; } = string.Empty;
    public List<LanguageSettings> Languages { get; set; } = new List<LanguageSettings>();
}

public class LanguageSettings
{
    public string LanguageCode { get; set; } = string.Empty;
    public string LanguageName { get; set; } = string.Empty;
    public string LanguageStore { get; set; } = string.Empty;
    public int StoreId { get; set; }
}