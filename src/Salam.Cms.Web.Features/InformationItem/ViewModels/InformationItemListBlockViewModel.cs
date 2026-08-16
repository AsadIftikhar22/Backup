namespace Salam.Cms.Web.Features.InformationItem.ViewModels;

using Salam.Cms.Web.Features.Common.ViewModels;
using Salam.Cms.Web.Features.InformationItem.Models;

public class InformationItemListBlockViewModel : BlockViewModel<InformationItemListBlock>
{
    public InformationItemListBlockViewModel(InformationItemListBlock? currentBlock) : base(currentBlock)
    {
    }
}