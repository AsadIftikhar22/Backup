namespace Salam.Cms.Web.Features.ClientResources.Services;

using Microsoft.AspNetCore.Html;

public interface IInlineCssService
{
    HtmlString LoadInlineCss(string pattern);
}
