namespace Salam.Cms.Web.Features.Embed.ViewModels;

using Salam.Cms.Web.Features.Common.ViewModels;
using Salam.Cms.Web.Features.Embed.Models;

public sealed class EmbedPageViewModel : SitePageViewModel<EmbedPage>
{
    public EmbedPageViewModel(EmbedPage currentPage) : base(currentPage)
    {
    }
}