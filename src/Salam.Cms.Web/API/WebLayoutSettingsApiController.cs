namespace Salam.CMS.Web.Controller;
using EPiServer.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Salam.CMS.Web.Data;
using System.Globalization;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.RegularExpressions;

[ApiController]
[Route("{culture=en}/weblayout-settings")]
public class WebLayoutSettingsApiController : Controller
{
    private readonly IWebLayoutSettingsRepo _webLayoutSettingsRepo;
    public WebLayoutSettingsApiController(IWebLayoutSettingsRepo webLayoutSettingsRepo)
    {
        _webLayoutSettingsRepo = webLayoutSettingsRepo;
    }
    /// <summary>
    /// Language must be En and Ar
    /// </summary>
    /// <param name="Language"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    [HttpGet]
    public async Task<IActionResult> GetHeaderAndFooter()
    {
        try
        {
            var culture = RouteData.Values["culture"]?.ToString();
            if (String.IsNullOrEmpty(culture))
                CultureInfo.DefaultThreadCurrentCulture = ContentLanguage.PreferredCulture;
            else
                CultureInfo.DefaultThreadCurrentCulture = new CultureInfo(culture);

            if (culture != "en" && culture != "ar")
                throw new ArgumentException("Invalid language. Only 'en' (English) and 'ar' (Arabic) are supported.", nameof(culture));

            var cultureInfo = new CultureInfo(culture);
            CultureInfo.CurrentCulture = cultureInfo;
            CultureInfo.CurrentUICulture = cultureInfo;

            var settings = _webLayoutSettingsRepo.GetAllWebLayoutSettings(cultureInfo);
            var viewPath = "~/Views/Shared/Components/Navigation/Default.cshtml";
            var viewModel = settings;

            ViewResult viewResult = base.View(viewPath, viewModel);

            string headerHtml = await RenderViewToStringAsync(
                "~/Views/Shared/Components/Navigation/Default.cshtml",
                settings.navigationViewModel,
                this.ControllerContext
            );

            string footerHtml = await RenderViewToStringAsync(
                "~/Views/Shared/Components/Footer/Default.cshtml",
                settings.footerViewModel,
                       this.ControllerContext
            );

            var CentralizedHeaderFooter = new StringBuilder();
            if (settings.Css != null)
            {
                using (var writer = new StringWriter())
                {
                    settings.Css.WriteTo(writer, HtmlEncoder.Default);
                    CentralizedHeaderFooter.Append(writer.ToString());
                }
            }
            //CentralizedHeaderFooter.Append(headerHtml);
            //CentralizedHeaderFooter.Append(footerHtml);
            //CentralizedHeaderFooter.Append(settings.Javascript);

            var options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };
            var objResponsedto = new WebLayoutResponse
            {
                HeaderHtml = CleanHtml(headerHtml),
                FooterHtml = CleanHtml(footerHtml),
                Css = CentralizedHeaderFooter.ToString(),
                Js = settings.Javascript
            };
            return Ok(objResponsedto);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception Message is {ex.Message} and StackTrace is {ex.StackTrace}");
            throw new ArgumentException($"Exception message is {ex.Message} and Stacktrace is {ex.StackTrace}");
        }
    }


    // Helper: render Razor View to string
    private async Task<string> RenderViewToStringAsync(string viewPath, object model, ActionContext actionContext)
    {
        try
        {
            var viewEngine = actionContext.HttpContext.RequestServices.GetRequiredService<IRazorViewEngine>();
            var tempProv = actionContext.HttpContext.RequestServices.GetRequiredService<ITempDataProvider>();

            var viewResult = viewEngine.GetView(executingFilePath: null, viewPath, isMainPage: false)
                             ?? viewEngine.FindView(actionContext, viewPath, isMainPage: false);

            if (!viewResult.Success)
                throw new InvalidOperationException($"View not found: {string.Join(", ", viewResult.SearchedLocations)}");

            var vData = new ViewDataDictionary(new EmptyModelMetadataProvider(), actionContext.ModelState)
            {
                Model = model
            };

            using var sw = new StringWriter();
            var ctx = new ViewContext(
                actionContext,
                viewResult.View,
                vData,
                new TempDataDictionary(actionContext.HttpContext, tempProv),
                sw,
                new HtmlHelperOptions()
            );

            await viewResult.View.RenderAsync(ctx);
            return sw.ToString();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception Message is {ex.Message} and StackTrace is {ex.StackTrace}");
            throw new ArgumentException($"Exception message is {ex.Message} and Stacktrace is {ex.StackTrace}");
        }
    }

    string CleanHtml(string html) =>
        Regex.Replace(html, @"\r?\n\s*", "");
}

