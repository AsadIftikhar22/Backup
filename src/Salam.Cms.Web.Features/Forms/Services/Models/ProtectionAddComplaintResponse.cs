namespace Salam.Cms.Web.Features.Forms.Services.Models;
using System.Text.Json.Serialization;

public class ProtectionAddComplaintResponse
{
    [JsonPropertyName("success")]
    public bool Success { get; set; }

    [JsonPropertyName("error")]
    public ErrorDetail Error { get; set; }
}
public class SearchComplaintData
{
    public string referenceId { get; set; }
    public string ticketStatus { get; set; }
}

public class SearchComplaintResponse
{
    public SearchComplaintData data { get; set; }
    public string message { get; set; }
    public bool success { get; set; }
    public int statusCode { get; set; }
}

public class OTPResponse
{
    public string status { get; set; }
    public int code { get; set; }
    public string error { get; set; }
    public string source { get; set; }
}

public class ErrorDetail
{
    [JsonPropertyName("code")]
    public string Code { get; set; }

    [JsonPropertyName("message")]
    public string Message { get; set; }
}

public class Data
{
    public string referenceId { get; set; }
    public string status { get; set; }
}