using Salam.Cms.Web.Features.Common.ViewModels;
using Salam.Cms.Web.Features.Embed.Abstract;
using Salam.Cms.Web.Features.Embed.Models;

namespace Salam.Cms.Web.Features.Embed.ViewModels;

public sealed class EmbedPageViewModelBuilder : SitePageViewModelBuilder<EmbedPage, EmbedPageViewModel>, IEmbedPageViewModelBuilder
{
    public override EmbedPageViewModel Build()
    {
        return Model;
    }
}