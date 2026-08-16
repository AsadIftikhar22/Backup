using EPiServer.Core;
using EPiServer.ServiceLocation;
using EPiServer.Web;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Html;
using Salam.Cms.Web.Features.ClientResources.Services;
using Salam.Cms.Web.Features.Common.Components.Footer;
using Salam.Cms.Web.Features.Common.Components.Navigation;
using System.Text.Encodings.Web;

/// <summary>
/// WebLayoutResponse
/// </summary>
public class WebLayoutResponse
{
    public string HeaderHtml { get; set; }
    public string FooterHtml { get; set; }
    public string Css { get; set; }
    public string Js { get; set; }
}
public record FormType(string Value)
{
    public static readonly FormType Template1 = new("Template1");
    public static readonly FormType Template2 = new("Template2");
    public static readonly FormType Template3 = new("Template3");
    public static readonly FormType Template4 = new("Template4");
    public static readonly FormType Template5 = new("Template5");
    public static readonly FormType SolutionForm = new("SolutionForm");
}
/// <summary>
/// EmailBodyResponse
/// </summary>
public class EmailBodyResponse
{
    public XhtmlString EmailBody { get; set; }
    public string FromEmail { get; set; }
    public string Emailsubject { get; set; }
    public string ToEmail { get; set; }
    public string APIbaseURL { get; set; }
}
/// <summary>
/// CentralizedLayout
/// </summary>
public class CentralizedLayout
{
    private IInlineCssService _inlineCssService;

    public IInlineCssService ExecuteStaticFiles
    {
        get => _inlineCssService;
        set
        {
            _inlineCssService = value;
            if (_inlineCssService != null)
            {
                LoadCss();
                Javascript = LoadJsArray();
            }
        }
    }

    public NavigationViewModel navigationViewModel { get; set; }
    public FooterViewModel footerViewModel { get; set; }

    private IHtmlContent _css;
    public IHtmlContent Css
    {
        get => _css;
        private set => _css = value;
    }

    private string _javascript;
    public string Javascript
    {
        get => _javascript;
        private set => _javascript = value;
    }


    public void LoadCss()
    {
        if (ExecuteStaticFiles == null)
            throw new InvalidOperationException("InlineCssService is not set.");

        var inlineCriticalCss = ExecuteStaticFiles.LoadInlineCss("critical-*.css");
        var inlineMainCss = ExecuteStaticFiles.LoadInlineCss("main-*.css");

        Css = new HtmlContentBuilder()
            .AppendHtml("<style>")
            .AppendHtml(inlineCriticalCss)
            .AppendHtml(inlineMainCss)
            .AppendHtml("</style>");
    }

    public string LoadJsArray()
    {
        try
        {
            var env = ServiceLocator.Current.GetInstance<IWebHostEnvironment>();
            string webRoot = env.WebRootPath;
            string jsFolderPath = Path.Combine(env.WebRootPath, "static", "assets", "js");

            if (!Directory.Exists(jsFolderPath))
            {
                Console.WriteLine("Folder empty", jsFolderPath);
                return string.Empty;
            }

            var jsFiles = Directory.GetFiles(jsFolderPath, "*.js", SearchOption.TopDirectoryOnly);
            Console.WriteLine("jsFiles", jsFolderPath);
            SiteDefinition _siteDefinition = ServiceLocator.Current.GetInstance<SiteDefinition>();
            var baseUrl = _siteDefinition.SiteUrl.ToString();

            var htmlContentBuilder = new HtmlContentBuilder();
            foreach (var jsFile in jsFiles)
            {
                var scriptTag = new HtmlContentBuilder()
                    .AppendHtml($"<script src=\"{baseUrl}/static/assets/js/{Path.GetFileName(jsFile)}\"></script>");
                htmlContentBuilder.AppendHtml(scriptTag);
            }

            using (var writer = new StringWriter())
            {
                htmlContentBuilder.WriteTo(writer, HtmlEncoder.Default);
                return writer.ToString();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception message is {ex.Message} and stacktrace is {ex.StackTrace}");
            throw ex;
        }
    }
}
