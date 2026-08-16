namespace Salam.Cms.Web.Features.Required.ViewModel;

using Salam.Cms.Web.Features.Common.ViewModels;
using Salam.Cms.Web.Features.InformationItem.Models;
using Salam.Cms.Web.Features.Required.Models;

public class RequirementsBlockViewModel : BlockViewModel<RequirementsBlock>
{
    public RequirementsBlockViewModel(RequirementsBlock? currentBlock) : base(currentBlock)
    {
    }

    public List<InformationItemBlock> InformationItems { get; set; }

    public string? ModifierClass { get; set; }
}