namespace Salam.Cms.Web.Features.Common.ViewModels;

using EPiServer.Core;
using EPiServer.ServiceLocation;
using EPiServer.Web;
using System.Diagnostics.CodeAnalysis;

public abstract class BlockViewModel<TContent> : IBlockViewModel<TContent>
    where TContent : BlockData
{
    public Injected<IContextModeResolver> ContextModeResolver { get; set; }

    protected BlockViewModel(TContent? currentBlock)
    {
        CurrentBlock = currentBlock;

        try
        {
            // Try to get the context mode resolver service
            IsInEditMode = ContextModeResolver.Service?.CurrentMode == ContextMode.Edit;
        }
        catch (InvalidOperationException)
        {
            // Service not registered (likely in a test environment)
            IsInEditMode = false;
        }
    }

    [NotNull]
    public TContent? CurrentBlock { get; internal set; }

    public bool IsInEditMode { get; private set; }
}

