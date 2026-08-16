namespace Salam.Cms.Shared.Models.Common.Robots;

using Microsoft.AspNetCore.Mvc.Filters;

public sealed class DisallowRobotsActonFilterAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        base.OnActionExecuting(context);

        context.HttpContext.Response.Headers["X-Robots-Tag"] = "noindex, nofollow";
    }
}