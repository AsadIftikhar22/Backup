namespace Salam.Cms.Web.Features.NotFound.Components;

using EPiServer;
using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Salam.Cms.Shared.Models.Common.Components;
using Salam.Cms.Shared.Models.Helpers;
using Salam.Cms.Shared.Models.Media;
using Salam.Cms.Web.Features.NotFound.Models;
using Salam.Cms.Web.Features.NotFound.ViewModels;

public sealed class NotFoundHeroBlockViewComponent : BlockComponent<NotFoundHeroBlock>
{
    private readonly IValueFallbackHandler _valueFallbackHandler;

    private readonly IContentLoader _contentLoader;

    private readonly ILinkModelConverter _linkModelConverter;

    public NotFoundHeroBlockViewComponent(
        IValueFallbackHandler valueFallbackHandler,
        IContentLoader contentLoader,
        ILinkModelConverter linkModelConverter)
    {
        _valueFallbackHandler = valueFallbackHandler;
        _contentLoader = contentLoader;
        _linkModelConverter = linkModelConverter;
    }

    protected override IViewComponentResult InvokeComponent(NotFoundHeroBlock currentContent)
    {
        var model = BuildModel(currentContent);

        return View(model);
    }

    private NotFoundHeroBlockViewModel BuildModel(NotFoundHeroBlock block)
    {
        var imageReference = _valueFallbackHandler.GetBest(block.Image);

        var links = _linkModelConverter.ConvertToModelCollection(block.Links);

        var model = new NotFoundHeroBlockViewModel
        {
            Title = block.Title,
            MainBody = block.MainBody,
            SecondaryBody = block.SecondaryBody,
            Links = links
        };

        if (_contentLoader.TryGet<IImageContent>(imageReference, out var imageContent))
        {
            model.ImageAltText = imageContent.AltText;
        }

        return model;
    }
}