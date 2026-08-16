namespace Salam.Cms.Web.Features.CustomerRightsBlock.Components;

using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Salam.Cms.Web.Features.CustomerRightsBlock.Models;
using Salam.Cms.Web.Features.CustomerRightsBlock.ViewModels;

public sealed class CustomerRightsSectionBlockViewComponent : BlockComponent<CustomerRightsSectionBlock>
{
    protected override IViewComponentResult InvokeComponent(CustomerRightsSectionBlock currentContent)
    {
        var model = new CustomerRightsSectionBlockViewModel(currentContent);
        return View(model);
    }
}
