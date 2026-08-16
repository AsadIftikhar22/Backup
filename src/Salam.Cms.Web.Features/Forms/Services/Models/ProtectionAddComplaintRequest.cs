namespace Salam.Cms.Web.Features.Forms.Services.Models;
public class ProtectionAddComplaintRequest
{
    public string reporterNumber { get; set; }
    public string reportedIdentity { get; set; }
    public string typeOfComplaint { get; set; }
    public string message { get; set; }
    public string operatorTcn { get; set; }
    public string serviceRating { get; set; }
    public string serviceFeedback { get; set; }
}

public class ComplaintChannelRequest
{
    public string number { get; set; }
    public string description { get; set; }
    public string summary { get; set; }
    public string tier1 { get; set; }
    public string tier2 { get; set; }
    public string tier3 { get; set; }
}


public class SearchComplaintRequest{

    public string number { get; set; }
}
