namespace Salam.Cms.Web.Features.UserRightsBlock.Components;

using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Salam.Cms.Web.Features.UserRightsBlock.Models;
using Salam.Cms.Web.Features.UserRightsBlock.ViewModels;

public sealed class UserRightsSectionBlockViewComponent : BlockComponent<UserRightsSectionBlock>
{
    protected override IViewComponentResult InvokeComponent(UserRightsSectionBlock currentContent)
    {
        var model = new UserRightsSectionBlockViewModel(currentContent);
        return View(model);
    }
}
