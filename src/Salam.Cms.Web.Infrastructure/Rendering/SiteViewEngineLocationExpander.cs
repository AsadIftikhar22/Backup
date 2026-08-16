namespace Salam.Cms.Web.Infrastructure.Rendering;

using Microsoft.AspNetCore.Mvc.Razor;

public class SiteViewEngineLocationExpander : IViewLocationExpander
{
    public const string BlockFolder = "~/Views/Shared/Blocks/";
    public const string PagePartialsFolder = "~/Views/Shared/PagePartials/";
    public const string FormFolder = "~/Views/Shared/ElementBlocks/";

    private static readonly string[] AdditionalPartialViewFormats =
    [
        BlockFolder + "{0}.cshtml",
        PagePartialsFolder + "{0}.cshtml",
        FormFolder + "{0}.cshtml"
    ];

    public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
    {
        foreach (var location in viewLocations)
        {
            yield return location;
        }

        for (var i = 0; i < AdditionalPartialViewFormats.Length; i++)
        {
            yield return AdditionalPartialViewFormats[i];
        }
    }

    public void PopulateValues(ViewLocationExpanderContext context) { }
}
