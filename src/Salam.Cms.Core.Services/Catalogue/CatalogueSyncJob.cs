namespace Salam.Cms.Core.Services.Catalogue;

using EPiServer.Find;
using EPiServer.PlugIn;
using EPiServer.Scheduler;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Salam.Cms.Core.Settings.Configuration;
using Salam.Cms.Shared.Models.Catalogue.Data;
using System.Text;
using System.Web;

[ScheduledPlugIn(DisplayName = "Product Catalogue Sync Job", Description = "Syncs data from external catalogue API", DefaultEnabled = true)]
public class CatalogueSyncJob : ScheduledJobBase
{
    private readonly ILogger<CatalogueSyncJob> _logger;
    private readonly ICatalogueApiService _apiService;
    private readonly string _prepaidPostpaidApiUrl;
    private readonly string _dataPlanApiUrl;
    private readonly string _deviceApiUrl;
    private readonly string _fiberApiUrl;
    private readonly string _visitorApiUrl;
    private readonly string _addOnApiUrl;
    private readonly string _5gApiUrl;
    private readonly List<LanguageSettings> _languages;

    public CatalogueSyncJob(ILogger<CatalogueSyncJob> logger, IClient findClient, IOptions<CatalogueApiSettings> apiSettings, ICatalogueApiService apiService)
    {
        _logger = logger;
        IsStoppable = true;
        _apiService = apiService;
        _prepaidPostpaidApiUrl = apiSettings.Value.PrepaidPostpaidApiUrl;
        _dataPlanApiUrl = apiSettings.Value.DataPlanApiUrl;
        _deviceApiUrl = apiSettings.Value.DeviceApiUrl;
        _fiberApiUrl = apiSettings.Value.FiberApiUrl;
        _visitorApiUrl = apiSettings.Value.VisitorApiUrl;
        _addOnApiUrl = apiSettings.Value.AddOnApiUrl;
        _5gApiUrl = apiSettings.Value.FiveGApiUrl;
        _languages = apiSettings.Value.Languages;
    }

    public override string Execute()
    {
        _logger.LogInformation("Scheduled job started.");
        var startTime = DateTime.Now;
        var totalIndexed = 0;
        var results = new Dictionary<string, Dictionary<string, (int Count, bool Success, string? ErrorMessage, string? StackTrace)>>();

        try
        {
            foreach (LanguageSettings language in _languages)
            {
                if (!results.ContainsKey(language.LanguageCode))
                {
                    results[language.LanguageCode] = new Dictionary<string, (int Count, bool Success, string? ErrorMessage, string? StackTrace)>();
                }

                // PrepaidPostpaid sync
                try
                {
                    var prepaidPostpaidItems = _apiService.FetchAndIndexDataAsync<PrepaidPostpaid>(_prepaidPostpaidApiUrl, language.LanguageCode, language.LanguageStore).Result;
                    results[language.LanguageCode]["PrepaidPostpaid"] = (prepaidPostpaidItems.Count, true, null, null);
                    totalIndexed += prepaidPostpaidItems.Count;
                    _logger.LogInformation($"Successfully indexed {prepaidPostpaidItems.Count} PrepaidPostpaid items for {language.LanguageCode}.");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "PrepaidPostpaid sync failed for lang {Lang}", language.LanguageCode);
                    results[language.LanguageCode]["PrepaidPostpaid"] = (0, false, ex.Message, ex.ToString());
                }

                // //Device sync
                try
                {
                    var devicesItems = _apiService.FetchAndIndexDataAsync<Device>(_deviceApiUrl, language.LanguageCode, language.LanguageStore).Result;
                    results[language.LanguageCode]["Device"] = (devicesItems.Count, true, null, null);
                    totalIndexed += devicesItems.Count;
                    _logger.LogInformation($"Successfully indexed {devicesItems.Count} Device items for {language.LanguageCode}.");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Device sync failed for lang {Lang}", language.LanguageCode);
                    results[language.LanguageCode]["Device"] = (0, false, ex.Message, ex.ToString());
                }

                //// Visitor sync
               try
               {
                   var visitorsItems = _apiService.FetchAndIndexDataAsync<Visitor>(_visitorApiUrl, language.LanguageCode, language.LanguageStore).Result;
                   results[language.LanguageCode]["Visitor"] = (visitorsItems.Count, true, null, null);
                   totalIndexed += visitorsItems.Count;
                   _logger.LogInformation($"Successfully indexed {visitorsItems.Count} Visitor items for {language.LanguageCode}.");
               }
               catch (Exception ex)
               {
                   _logger.LogError(ex, "Visitor sync failed for lang {Lang}", language.LanguageCode);
                   results[language.LanguageCode]["Visitor"] = (0, false, ex.Message, ex.ToString());
               }

                // DataPlan sync
               try
               {
                   var dataPlansItems = _apiService.FetchAndIndexDataAsync<DataPlan>(_dataPlanApiUrl, language.LanguageCode, language.LanguageStore).Result;
                   results[language.LanguageCode]["DataPlan"] = (dataPlansItems.Count, true, null, null);
                   totalIndexed += dataPlansItems.Count;
                   _logger.LogInformation($"Successfully indexed {dataPlansItems.Count} DataPlan items for {language.LanguageCode}.");
               }
               catch (Exception ex)
               {
                   _logger.LogError(ex, "DataPlan sync failed for lang {Lang}", language.LanguageCode);
                   results[language.LanguageCode]["DataPlan"] = (0, false, ex.Message, ex.ToString());
               }

                // Fiber sync
                try
                {
                    var fiberItems = _apiService.FetchAndIndexDataAsync<Fiber>(_fiberApiUrl, language.LanguageCode, language.LanguageStore).Result;
                    results[language.LanguageCode]["Fiber"] = (fiberItems.Count, true, null, null);
                    totalIndexed += fiberItems.Count;
                    _logger.LogInformation($"Successfully indexed {fiberItems.Count} Fiber items for {language.LanguageCode}.");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Fiber sync failed for lang {Lang}", language.LanguageCode);
                    results[language.LanguageCode]["Fiber"] = (0, false, ex.Message, ex.ToString());
                }

                // AddOn sync
                try
               {
                   var addOnItems = _apiService.FetchAndIndexAddOnsAsync(_addOnApiUrl, language.LanguageCode, language.LanguageStore).Result;
                   results[language.LanguageCode]["AddOn"] = (addOnItems.Count, true, null, null);
                   totalIndexed += addOnItems.Count;
                   _logger.LogInformation($"Successfully indexed {addOnItems.Count} AddOn items for {language.LanguageCode}.");
               }
               catch (Exception ex)
               {
                   _logger.LogError(ex, "AddOn sync failed for lang {Lang}", language.LanguageCode);
                   results[language.LanguageCode]["AddOn"] = (0, false, ex.Message, ex.ToString());
               }

                //5G sync
               try
               {
                   var fiveGItems = _apiService.FetchAndIndexDataAsync<FiveG>(_5gApiUrl, language.LanguageCode, language.LanguageStore).Result;
                   results[language.LanguageCode]["5G"] = (fiveGItems.Count, true, null, null);
                   totalIndexed += fiveGItems.Count;
                   _logger.LogInformation($"Successfully indexed {fiveGItems.Count} 5G items for {language.LanguageCode}.");
               }
               catch (Exception ex)
               {
                   _logger.LogError(ex, "5G sync failed for lang {Lang}", language.LanguageCode);
                   results[language.LanguageCode]["5G"] = (0, false, ex.Message, ex.ToString());
               }

                Thread.Sleep(1000); //Add slight delay when switching languages
            }

            return GenerateHtmlReport(results, startTime, totalIndexed);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while executing scheduled job.");
            return $@"<div style='color:red;font-weight:bold;'>Job failed: {ex.Message}</div>";
        }
    }

    public override void Stop()
    {
        base.Stop();
    }

    private string GenerateHtmlReport(Dictionary<string, Dictionary<string, (int Count, bool Success, string? ErrorMessage, string? StackTrace)>> results, DateTime startTime, int totalIndexed)
    {
        var duration = (DateTime.Now - startTime).TotalSeconds;
        var sb = new StringBuilder();

        sb.AppendLine(@"<div style='font-family: Arial, sans-serif;'>");
        sb.AppendLine($@"<b>{totalIndexed} Items indexed. Expand for results.</b>");
        sb.AppendLine($@"<p><strong>Start time:</strong> {startTime:yyyy-MM-dd HH:mm:ss}</p>");
        sb.AppendLine($@"<p><strong>Duration:</strong> {duration:0.00} seconds</p>");
        sb.AppendLine($@"<p><strong>Total items indexed:</strong> {totalIndexed}</p>");

        sb.AppendLine(@"<table border='1' cellpadding='4' cellspacing='0' style='border-collapse: collapse; width: 100%;'>");
        sb.AppendLine(@"<tr style='background-color: #f2f2f2; font-weight: bold;'>");
        sb.AppendLine(@"<th>Product Type</th>");

        // Header row with language codes
        foreach (var language in results.Keys)
        {
            sb.AppendLine($@"<th>{language}</th>");
        }
        sb.AppendLine(@"</tr>");

        // Get all product types
        var productTypes = results.Values
            .SelectMany(dict => dict.Keys)
            .Distinct()
            .OrderBy(type => type)
            .ToList();

        // Add rows for each product type
        foreach (var productType in productTypes)
        {
            sb.AppendLine($@"<tr>");
            sb.AppendLine($@"<td style='font-weight: bold;'>{productType}</td>");

            foreach (var language in results.Keys)
            {
                if (results[language].TryGetValue(productType, out var resultData))
                {
                    var (count, success, errorMessage, stackTrace) = resultData;

                    if (success)
                    {
                        sb.AppendLine($@"<td style='text-align: center; background-color: #e6ffe6;'>{count}</td>");
                    }
                    else
                    {
                        sb.AppendLine($@"<td style='text-align: center; background-color: #ffe6e6;' title='{HttpUtility.HtmlEncode(errorMessage)}'>Failed</td>");
                    }
                }
                else
                {
                    sb.AppendLine(@"<td style='text-align: center;'>N/A</td>");
                }
            }

            sb.AppendLine(@"</tr>");
        }

        // Add summary row
        sb.AppendLine(@"<tr style='background-color: #f2f2f2; font-weight: bold;'>");
        sb.AppendLine(@"<td>Total per language</td>");

        foreach (var language in results.Keys)
        {
            var languageTotal = results[language].Sum(r => r.Value.Count);
            sb.AppendLine($@"<td style='text-align: center;'>{languageTotal}</td>");
        }

        sb.AppendLine(@"</tr>");
        sb.AppendLine(@"</table>");

        sb.AppendLine(@"<p style='font-size: 0.8em; color: #666;'>Green cells indicate successful indexing. Red cells indicate failures (hover for error details).</p>");

        // Add error details section if there are any errors
        bool hasErrors = results.Values.SelectMany(dict => dict.Values).Any(result => !result.Success);
        if (hasErrors)
        {
            sb.AppendLine(@"<h3>Error Details</h3>");
            sb.AppendLine(@"<div style='margin-top: 20px;'>");

            foreach (var language in results.Keys)
            {
                bool languageHasErrors = results[language].Values.Any(result => !result.Success);
                if (!languageHasErrors) continue;

                sb.AppendLine($@"<details style='margin-bottom: 10px;'>");
                sb.AppendLine($@"<summary style='font-weight: bold; cursor: pointer; padding: 8px; background-color: #f2f2f2;'>Errors for {language}</summary>");
                sb.AppendLine($@"<div style='border: 1px solid #ddd; padding: 10px; margin-top: 5px;'>");

                foreach (var productType in results[language].Keys)
                {
                    var (count, success, errorMessage, stackTrace) = results[language][productType];
                    if (success) continue;

                    sb.AppendLine($@"<details style='margin-bottom: 10px;'>");
                    sb.AppendLine($@"<summary style='font-weight: bold; cursor: pointer; color: #c00;'>{productType} Error</summary>");
                    sb.AppendLine($@"<div style='background-color: #f8f8f8; padding: 10px; border-left: 4px solid #c00; font-family: monospace; white-space: pre-wrap; overflow-x: auto;'>");
                    sb.AppendLine(HttpUtility.HtmlEncode(stackTrace));
                    sb.AppendLine($@"</div>");
                    sb.AppendLine($@"</details>");
                }

                sb.AppendLine($@"</div>");
                sb.AppendLine($@"</details>");
            }

            sb.AppendLine(@"</div>");
        }

        sb.AppendLine(@"</div>");

        return sb.ToString();
    }
}