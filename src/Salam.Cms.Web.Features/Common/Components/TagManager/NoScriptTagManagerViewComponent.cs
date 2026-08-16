namespace Salam.Cms.Web.Features.Common.Components.TagManager;

using Microsoft.AspNetCore.Mvc;
using Salam.Cms.Core.Settings.Abstract;
using Salam.Cms.Web.Features.Settings.Models;

public sealed class NoScriptTagManagerViewComponent : ViewComponent
{
    private readonly ISettingsManager _settingsManager;

    public NoScriptTagManagerViewComponent(ISettingsManager settingsManager)
    {
        _settingsManager = settingsManager;
    }

    public IViewComponentResult Invoke()
    {
        var webLayoutSettings = _settingsManager.GetSettings<WebLayoutSettings>();

        if (string.IsNullOrWhiteSpace(webLayoutSettings.TagManagerKey))
        {
            return Content(string.Empty);
        }

        var model = new TagManagerViewModel { TagManagerKey = webLayoutSettings.TagManagerKey };

        return View(model);
    }
}