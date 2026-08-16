namespace Salam.Cms.Web.Features.SolutionsSectionsBlock.ViewModels;
using EPiServer.Core;
using Salam.Cms.Web.Features.Common.ViewModels;
using Salam.Cms.Web.Features.SolutionsSectionsBlock.Models;

public class QualityIndicatorBlockViewModel : BlockViewModel<QualityIndicatorBlock>
{
    public QualityIndicatorBlockViewModel(QualityIndicatorBlock currentBlock)
        : base(currentBlock)
    {
        CurrentBlock = currentBlock;
        if (currentBlock is IContent content)
        {
            BlockId = content.ContentLink.ID;
        }
    }

    public QualityIndicatorBlock CurrentBlock { get; set; }
    public int BlockId { get; set; }
}