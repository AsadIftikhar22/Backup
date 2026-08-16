namespace Salam.Cms.Web.Features.Common.ViewModels;

using EPiServer.Core;

public interface IBlockViewModel<out TContent>
    where TContent : BlockData
{
    TContent? CurrentBlock { get; }
}