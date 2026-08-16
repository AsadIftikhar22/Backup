namespace Salam.Cms.Web.Features.Forms.Services;
public class FraudComplaintRequest
{
    public string? ReporterNumber { get; set; }
    public string? ReportedIdentity { get; set; }
    public string? TypeOfComplaint { get; set; }
    public string? Message { get; set; }
    public string? OperatorTcn { get; set; }
    public string? ContentType { get; set; }
    public string? ServiceRating { get; set; }
    public string? ServiceFeedback { get; set; }
}
public class FraudApiResponse
{
    public int code { get; set; }
    public string description { get; set; }
    public FraudApiData data { get; set; }
}

public class FraudApiData
{
    public int complaintId { get; set; }
    public string status { get; set; }
}