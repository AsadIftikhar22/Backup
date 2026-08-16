namespace Salam.Cms.Web.Features.Required.Components;

using EPiServer.Core;
using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Salam.Cms.Web.Features.InformationItem.Models;
using Salam.Cms.Web.Features.Required.Models;
using Salam.Cms.Web.Features.Required.ViewModel;

public class RequirementsBlockViewComponent : BlockComponent<RequirementsBlock>
{
    public RequirementsBlockViewComponent()
    {
    }
    protected override IViewComponentResult InvokeComponent(RequirementsBlock currentContent)
    {
        var model = new RequirementsBlockViewModel(currentContent);

        if (currentContent.Items?.FilteredItems != null)
        {
            model.InformationItems = currentContent.Items.FilteredItems
                .Select(x => x.LoadContent() as InformationItemBlock)
                .Where(x => x != null)
                .ToList()!;
        }
        else
        {
            model.InformationItems = new List<InformationItemBlock>();
        }

        // Set grid layout based on count
        int count = model.InformationItems.Count;
        model.ModifierClass = count > 1 && count <= 6 ? $"grid-layout-{count}" : string.Empty;

        return View(model);
    }
}