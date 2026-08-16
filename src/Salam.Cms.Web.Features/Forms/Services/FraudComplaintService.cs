namespace Salam.Cms.Web.Features.Forms.Services;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
public class FraudComplaintService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<FraudComplaintService> _logger;
    private readonly IConfiguration _config;

    public FraudComplaintService(
        IHttpClientFactory httpClientFactory,
        ILogger<FraudComplaintService> logger,
        IConfiguration config)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
        _config = config;
    }
    public async Task<object> AddAsync(FraudComplaintRequest request)
    {
        if (request == null)
            return new { code = 0, description = "Invalid JSON payload." };


        return await CallFraudReportingApiAsync(request);
    }
    private async Task<object> CallFraudReportingApiAsync(FraudComplaintRequest payload)
    {
        var url = _config["FraudApi:Url"];
        var apiKey = _config["FraudApi:ApiKey"];
        payload.OperatorTcn = _config["FraudApi:operatorTcn"]+1;
        var client = _httpClientFactory.CreateClient();

        client.DefaultRequestHeaders.Add("api-key", apiKey);

        client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

        var json = JsonSerializer.Serialize(payload,
            new JsonSerializerOptions
            {
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            });

        var content = new StringContent(json, Encoding.UTF8, "application/json");

        try
        {
            var response = await client.PostAsync(url, content);
            var raw = await response.Content.ReadAsStringAsync();

            _logger.LogInformation("🔁 Fraud API HTTP Code: {code}", response.StatusCode);
            _logger.LogInformation("📥 API Raw Response: {response}", raw);

            if (!response.IsSuccessStatusCode)
            {
                return new
                {
                    code = 0,
                    description = "API Error",
                    httpCode = (int)response.StatusCode,
                    raw
                };
            }

            return JsonSerializer.Deserialize<object>(raw)
                   ?? new { code = 0, description = "Invalid JSON from API" };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "❌ Fraud API call failed");
            return new { code = 0, description = ex.Message };
        }
    }
}