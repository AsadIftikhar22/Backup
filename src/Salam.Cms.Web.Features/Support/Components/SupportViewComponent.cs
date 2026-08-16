namespace Salam.Cms.Web.Features.Support.Components;

using EPiServer;
using Microsoft.AspNetCore.Mvc;
using Salam.Cms.Core.Settings.Abstract;
using Salam.Cms.Shared.Models.Extensions;
using Salam.Cms.Web.Features.Common.Interfaces;
using Salam.Cms.Web.Features.Settings.Models;
using Salam.Cms.Web.Features.Support.Enums;
using Salam.Cms.Web.Features.Support.Models;
using System.Linq;

public class SupportViewComponent : ViewComponent
{
    private readonly ISettingsManager _settingsManager;
    private readonly IContentLoader _contentLoader;

    public SupportViewComponent(ISettingsManager settingsManager, IContentLoader contentLoader)
    {
        _settingsManager = settingsManager;
        _contentLoader = contentLoader;
    }

    public IViewComponentResult Invoke(ISitePageData? sitePage)
    {
        if (sitePage == null || sitePage.HideSupportContactContent)
        {
            return Content(string.Empty);
        }

        var webLayoutSettings = _settingsManager.GetSettings<WebLayoutSettings>();

        // Ensure the view path is correct and the model is not null
        var model = sitePage.SupportContactContent.IsNullOrEmpty() ?
            webLayoutSettings?.SupportContactContent :
            sitePage.SupportContactContent;

        if (model == null || !model.FilteredItems.Any())
        {
            return Content(string.Empty);
        }

        var block = _contentLoader.Get<SupportBlock>(model.FilteredItems.FirstOrDefault().ContentLink);

        var view = block.Layout switch
        {
            SupportLayoutOption.Default => "~/Views/Shared/Blocks/SupportBlock.cshtml", // Reuse default CMS render cycle block view
            SupportLayoutOption.Featured => "Featured",
            _ => "~/Views/Shared/Blocks/SupportBlock.cshtml"
        };

        return View(view, block);
    }
}
