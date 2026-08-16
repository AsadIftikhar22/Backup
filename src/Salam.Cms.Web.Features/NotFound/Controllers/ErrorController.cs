using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Stott.Security.Optimizely.Common.Validation;

namespace Salam.Cms.Web.Features.NotFound.Controllers;
using Newtonsoft.Json;
using Salam.Cms.Web.Features.NotFound.ViewModels;

[ApiExplorerSettings(IgnoreApi = true)]
public class ErrorController : Controller
{
    private readonly INotFoundViewModelBuilder _viewModelBuilder;
    private readonly ILogger<ErrorController> _logger;

    public ErrorController(INotFoundViewModelBuilder viewModelBuilder, ILogger<ErrorController> logger)
    {
        _viewModelBuilder = viewModelBuilder;
        _logger = logger;
    }

    [Route("/error")]
    [AcceptVerbs("GET", "HEAD")]
    public IActionResult PageNotFound(int statusCode)
    {
        if (!ModelState.IsValid)
        {
            var validationModel = new ValidationModel(ModelState);
            _logger.LogError("Invalid model state. Model: {ValidationModel}", validationModel);
            throw new InvalidOperationException(JsonConvert.SerializeObject(validationModel));
        }

        // Please note that this error page should only execute for 400 range error ranges.
        // This view model builder will resolve the Not Found Page and does not need to call .WithContent(...)
        var model = _viewModelBuilder.WithStatusCode(statusCode).Build();

        return View(model);
    }
}