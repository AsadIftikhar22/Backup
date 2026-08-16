namespace Salam.Cms.Web.Features.Forms.ProtectorChannelFormContainerBlock;
using EPiServer.Forms.Controllers;
using EPiServer.Forms.Implementation.Elements;
using EPiServer.Framework.DataAnnotations;
using EPiServer.Framework.Web;
using Microsoft.AspNetCore.Mvc;

[TemplateDescriptor(AvailableWithoutTag = true,
                    Default = true,
                    ModelType = typeof(ProtectorChannelFormContainerBlock),
                    TemplateTypeCategory = TemplateTypeCategories.MvcPartialController)]
public class ProtectorChannelFormContainerBlockController : FormContainerBlockController
{
    protected override IViewComponentResult InvokeComponent(FormContainerBlock currentBlock)
    {
        return base.InvokeComponent(currentBlock);
    }
}