namespace Salam.Cms.Web.Features.Forms.ComplaintTabFormContainerBlock;
using EPiServer.Forms.Controllers;
using EPiServer.Forms.Implementation.Elements;
using EPiServer.Framework.DataAnnotations;
using EPiServer.Framework.Web;
using Microsoft.AspNetCore.Mvc;

[TemplateDescriptor(AvailableWithoutTag = true,
                    Default = true,
                    ModelType = typeof(ComplaintTabFormContainerBlock),
                    TemplateTypeCategory = TemplateTypeCategories.MvcPartialController)]
public class ComplaintTabFormContainerBlockController : FormContainerBlockController
{
    protected override IViewComponentResult InvokeComponent(FormContainerBlock currentBlock)
    {
        return base.InvokeComponent(currentBlock);
    }
}