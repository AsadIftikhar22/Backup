using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using System.Text;
using System.Xml.Linq;

namespace YourApp.Controllers
{
    [Route("api/Complaint")]
    public class ComplaintController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public ComplaintController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        [HttpPost("send-email")]
        public async Task<IActionResult> SendEmail(
            [FromForm] string to,
            [FromForm] string subject,
            [FromForm] string title,
            [FromForm] string number,
            [FromForm] string tier1,
            [FromForm] string message,
            [FromForm] string lang)
        {
            try
            {
                var htmlMessage = message.Replace("#TicketNumber", $"<b class=\"green-text\">{number}</b>")
                                         .Replace("#Tier1Category", tier1);

                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(_configuration["Email:FromAddress"]));
                email.To.Add(MailboxAddress.Parse(to));
                email.Subject = subject;

                var builder = new BodyBuilder
                {
                    HtmlBody = htmlMessage
                };
                email.Body = builder.ToMessageBody();

                using var smtp = new SmtpClient();
                await smtp.ConnectAsync(_configuration["Email:SmtpHost"], int.Parse(_configuration["Email:SmtpPort"]), false);
                await smtp.AuthenticateAsync(_configuration["Email:SmtpUser"], _configuration["Email:SmtpPass"]);
                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);

                return Ok(new { Mail = "Message has been sent" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost("submit-complaint")]
        public async Task<IActionResult> SubmitComplaint(
       [FromForm] string number,
       [FromForm] string description,
       [FromForm] string summary,
       [FromForm] string tier_1,
       [FromForm] string tier_2,
       [FromForm] string tier_3)
        {
            // The base SOAP URL, e.g. https://example.com/arsys/CreateTicket_Salam
            string baseSoapUrl = _configuration["Complaint:CreateTicketUrl"];
            string endpointPath = _configuration["Complaint:CreateTicket_Salam"];
            string soapUrl = $"{baseSoapUrl.TrimEnd('/')}{endpointPath.TrimStart('/')}";

            // Build SOAP XML as a string (just like the PHP version)
            string soapXml = $@"
<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:urn=""urn:CreateTicket_Salam"">
   <soapenv:Header>
      <urn:AuthenticationInfo>
         <urn:userName>{_configuration["Complaint:USER"]}</urn:userName>
         <urn:password>{_configuration["Complaint:PASSWORD"]}</urn:password>
      </urn:AuthenticationInfo>
   </soapenv:Header>
   <soapenv:Body>
      <urn:Create_Incident>
         <urn:MSISDN>{number}</urn:MSISDN>
         <urn:First_Name>Adil</urn:First_Name>
         <urn:Last_Name>test1</urn:Last_Name>
         <urn:ServiceType>User Service Restoration</urn:ServiceType>
         <urn:Detailed_Decription>{description}</urn:Detailed_Decription>
         <urn:Severity>Minor</urn:Severity>
         <urn:Summary>{summary}</urn:Summary>
         <urn:Assigned_Support_Company>SALAM-MOBILE</urn:Assigned_Support_Company>
         <urn:Assigned_Support_Organization>MVNO-Call Center</urn:Assigned_Support_Organization>
         <urn:Assigned_Group>{_configuration["Complaint:SOAP_ASSIGNED_GROUP"]}</urn:Assigned_Group>
         <urn:Assigned_Group_ID>{_configuration["Complaint:SOAP_ASSIGNED_GROUP_ID"]}</urn:Assigned_Group_ID>
         <urn:ReportedSourceType>Web</urn:ReportedSourceType>
         <urn:Operational_Categorization_Tier_1>{tier_1}</urn:Operational_Categorization_Tier_1>
         <urn:Operational_Categorization_Tier_2>{tier_2}</urn:Operational_Categorization_Tier_2>
         <urn:Operational_Categorization_Tier_3>{tier_3}</urn:Operational_Categorization_Tier_3>
      </urn:Create_Incident>
   </soapenv:Body>
</soapenv:Envelope>";

            try
            {
                using var client = _httpClientFactory.CreateClient();

                // Create StringContent with text/xml exactly like PHP cURL
                var content = new StringContent(soapXml, Encoding.UTF8, "text/xml");

                // Add the same SOAPAction header
                content.Headers.Add("SOAPAction", "#POST");


                // Send request
                var response = await client.PostAsync(soapUrl, content);
                string responseXml = await response.Content.ReadAsStringAsync();

                // If status is not OK, treat like your PHP
                if (!response.IsSuccessStatusCode)
                {
                    return BadRequest(new
                    {
                        error = "Something went wrong",
                        whole_response = responseXml,
                        tier1 = tier_1,
                        tier2 = tier_2,
                        tier3 = tier_3
                    });
                }

                // Extract reference ID same way PHP did (string search)
                int start = responseXml.IndexOf("<ns0:CustomerServiceNo>");
                string refId = "";
                if (start >= 0)
                {
                    refId = responseXml.Substring(start + "<ns0:CustomerServiceNo>".Length, 15);
                }

                return Ok(new
                {
                    reference_id = refId,
                    whole_response = responseXml,
                    tier1 = tier_1
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }


        [HttpPost("search-complaint")]
        public async Task<IActionResult> SearchComplaint([FromForm] string number)
        {
            string soapUrl = _configuration["Complaint:CreateTicketUrl"] + _configuration["Complaint:SearchStatusUrl"];

            var xml = new XDocument(
                new XElement("soapenv:Envelope",
                    new XAttribute(XNamespace.Xmlns + "soapenv", "http://schemas.xmlsoap.org/soap/envelope/"),
                    new XAttribute(XNamespace.Xmlns + "urn", "urn:SearchStatus_Salam"),
                    new XElement("soapenv:Header",
                        new XElement("urn:AuthenticationInfo",
                            new XElement("urn:userName", _configuration["Complaint:USER"]),
                            new XElement("urn:password", _configuration["Complaint:PASSWORD"])
                        )
                    ),
                    new XElement("soapenv:Body",
                        new XElement("urn:Search_Status",
                            new XElement("urn:Qualification", number)
                        )
                    )
                )
            );

            var client = _httpClientFactory.CreateClient();
            var content = new StringContent(xml.ToString(), Encoding.UTF8, "text/xml");
            content.Headers.Add("SOAPAction", "#GET");

            var response = await client.PostAsync(soapUrl, content);
            var responseXml = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                return BadRequest(new { error = "Something went wrong", whole_response = responseXml });

            var doc = XDocument.Parse(responseXml);
            var status = doc.Descendants().FirstOrDefault(e => e.Name.LocalName == "Status")?.Value;

            return Ok(new { status });
        }
    }
}
