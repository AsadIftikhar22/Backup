namespace Salam.CMS.Web.Data;

using System.Globalization;
public interface IWebLayoutSettingsRepo
{
    CentralizedLayout GetAllWebLayoutSettings(CultureInfo cultureInfo);
    EmailBodyResponse GetFormEmailBody(CultureInfo cultureInfo, string RequestForm);
    int GetProductEnquireLimit();
}
