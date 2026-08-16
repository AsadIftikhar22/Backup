namespace Salam.Cms.Web.Features.Forms.Services;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RestSharp;
using Salam.Cms.Web.Features.Forms.Services.Models;
using System.Text.Json;
using System.Threading.Tasks;

public class ProtectionApiWrapper
{
    private readonly RestClient _client;
    private readonly IConfiguration _configuration;
    private readonly ILogger<ProtectionApiWrapper> _logger;
    public ProtectionApiWrapper(IConfiguration configuration, ILogger<ProtectionApiWrapper> logger)
    {
        _configuration = configuration;
        var options = new RestClientOptions("http://34.166.122.217")
        {
            //MaxTimeout = -1
        };
        _logger = logger;
        _client = new RestClient(options);
    }

    public async Task<ProtectionAddComplaintResponse> AddComplaintAsync(ProtectionAddComplaintRequest requestBody)
    {
        ProtectionAddComplaintResponse result = new();
        try
        {
           
            string ComplaintAPIUrl = _configuration["FraudApi:Url"]!;
            string apiKey = _configuration["FraudApi:ApiKey"]!;
            Console.WriteLine($"Fraud API Url is {ComplaintAPIUrl} and Key {apiKey}");

            // Log request
            var requestJson = JsonSerializer.Serialize(
                requestBody,
                new JsonSerializerOptions { WriteIndented = true });

            _logger.LogInformation(
                "Calling Fraud API {Url}\nRequest:\n{Request}",
                ComplaintAPIUrl,
                requestJson);

                            Console.WriteLine($"""
                ================ REQUEST ================
                URL: {ComplaintAPIUrl}

                {requestJson}
                =========================================
                """);
            var request = new RestRequest(ComplaintAPIUrl, Method.Post);
            request.AddOrUpdateHeader("x-internal-api-key", apiKey);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(requestBody);
            var response = await _client.ExecuteAsync<ProtectionAddComplaintResponse>(request);
            _logger.LogInformation($"Response is {response.Content}");
            Console.WriteLine($"Response is {response.Content}");
            result = JsonSerializer.Deserialize<ProtectionAddComplaintResponse>(
              response?.Content ?? string.Empty,
              new JsonSerializerOptions
              {
                  PropertyNameCaseInsensitive = true
              })!;
            Console.WriteLine($"Response Protector Channel is {JsonSerializer.Serialize(result)}");
            _logger.LogInformation($"Response Protector Channel is {JsonSerializer.Serialize(result)}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception message Protector Channel is {ex.Message} and Stacktrace is {ex.StackTrace}");
        }

        return result;
    }

    public async Task<SearchComplaintResponse> SearchComplaintTicketAsync(SearchComplaintRequest requestBody)
    {
        var result = new SearchComplaintResponse();
        try
        {
            string ComplaintAPIUrl = _configuration["ComplaintAPI:SearchAPI"]!;
            string apiKey = _configuration["ComplaintAPI:ApiKey"]!;
            var request = new RestRequest(ComplaintAPIUrl, Method.Post);
            request.AddOrUpdateHeader("x-internal-api-key", apiKey);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(requestBody);
            var response = await _client.ExecuteAsync<SearchComplaintResponse>(request);
            result = JsonSerializer.Deserialize<SearchComplaintResponse>(
              response?.Content ?? string.Empty,
              new JsonSerializerOptions
              {
                  PropertyNameCaseInsensitive = true
              });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception message in CreateComplaintAsync is {ex.Message} and Stacktrace is {ex.StackTrace}");
            _logger.LogError($"Exception message in CreateComplaintAsync is {ex.Message} and Stacktrace is {ex.StackTrace}");

        }

        return result;
    }

    public async Task<ProtectionAddComplaintResponse> CreateComplaintAsync(ComplaintChannelRequest requestBody)
    {
        var result=new ProtectionAddComplaintResponse();
        try
        {
            string ComplaintAPIUrl = _configuration["ComplaintAPI:CreateUrl"]!;
            string apiKey = _configuration["ComplaintAPI:ApiKey"]!;
            var request = new RestRequest(ComplaintAPIUrl, Method.Post);
            request.AddOrUpdateHeader("x-internal-api-key", apiKey);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(requestBody);
            var response = await _client.ExecuteAsync<ProtectionAddComplaintResponse>(request);
            Console.WriteLine($"Response from CreateComplaintAsync is {response.Content} and source is {response.ErrorMessage}");
            result = JsonSerializer.Deserialize<ProtectionAddComplaintResponse>(
             response?.Content ?? string.Empty,
             new JsonSerializerOptions
             {
                 PropertyNameCaseInsensitive = true
             });

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception message in CreateComplaintAsync is {ex.Message} and Stacktrace is {ex.StackTrace}");
            _logger.LogError($"Exception message in CreateComplaintAsync is {ex.Message} and Stacktrace is {ex.StackTrace}");
        }
        return result;
    }
}