namespace Salam.Cms.Web.Features.InternetCardGrid.Components;

using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Salam.Cms.Web.Features.SolutionsSectionsBlock.Models;
using Salam.Cms.Web.Features.SolutionsSectionsBlock.ViewModels;

public sealed class QualityIndicatorBlockViewComponent : BlockComponent<QualityIndicatorBlock>
{
    public QualityIndicatorBlockViewComponent()
    {

    }
    protected override IViewComponentResult InvokeComponent(QualityIndicatorBlock currentContent)
    {
        QualityIndicatorBlockViewModel model = new(currentContent)
        {
        };
        return View(model);
    }
}