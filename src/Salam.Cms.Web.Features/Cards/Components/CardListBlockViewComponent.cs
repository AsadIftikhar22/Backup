using EPiServer;
using EPiServer.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Salam.Cms.Shared.Models.Extensions;
using Salam.Cms.Web.Features.Cards.Enums;
using Salam.Cms.Web.Features.Cards.Models;
using Salam.Cms.Web.Features.Cards.ViewModels;
using Salam.Cms.Web.Features.Common.Components.Images;

namespace Salam.Cms.Web.Features.Cards.Components
{
    public sealed class CardListBlockViewComponent : AsyncBlockComponent<CardListBlock>
    {
        private readonly IContentLoader _contentLoader;

        public CardListBlockViewComponent(IContentLoader contentLoader)
        {
            _contentLoader = contentLoader;
        }

        protected async override Task<IViewComponentResult> InvokeComponentAsync(CardListBlock currentContent)
        {
            var stylePreset = currentContent.StylePreset;
            var styleClasses = string.Empty;
            var themeClasses = string.Empty;

            var buttonStyle = currentContent.ButtonStyle;

            if (stylePreset == CardListStylePresetOption.Override)
            {
                var styleNumbers = currentContent.Style?
                    .Split(",")
                    .Select(num => int.TryParse(num, out int n) ? (CardListStyleOption?)n : null)
                    .Where(n => n.HasValue)
                    .Select(n => n.Value.GetCssClass());

                styleClasses = string.Join(" ", styleNumbers ?? Enumerable.Empty<string>());

                themeClasses = currentContent.Theme.GetCssClass();
            }

            var cssClasses = string.Join(" ",
                "card-list",
                currentContent.Layout.GetCssClass(),
                themeClasses,
                styleClasses
            );

            var cardBlocks = currentContent.Items?.FilteredItems
                .Select(item => _contentLoader.TryGet(item.ContentLink, out CardBlock? cardBlock) ? cardBlock : null)
                .Where(cardBlock => cardBlock != null)
                .Select((cardBlock, index) =>
                {
                    var vm = new CardBlockViewModel(cardBlock!)
                    {
                        Layout = "card-block" + cardBlock.Layout.GetCssClass(),
                        Theme = stylePreset == CardListStylePresetOption.Override
                                    ? string.Empty
                                    : "card-block" + cardBlock.Theme.GetCssClass(),
                        Style = stylePreset == CardListStylePresetOption.Override
                                    ? string.Empty
                                    : ParseStyleClasses(cardBlock.Style),
                        ButtonStyle = stylePreset == CardListStylePresetOption.Override
                                    ? buttonStyle
                                    : cardBlock.ButtonStyle,
                        ImageProfile = currentContent.Layout switch
                        {
                            CardListLayoutOption.Row => PictureProfiles.CardRow,
                            CardListLayoutOption.Featured => index == 0 ? PictureProfiles.CardFeatured : PictureProfiles.CardAlternating,
                            CardListLayoutOption.AlternatingHorizontalFlip => PictureProfiles.CardAlternating,
                            CardListLayoutOption.FeaturedCentered => PictureProfiles.CardFeaturedLarge,
                            _ => PictureProfiles.CardDefault
                        }
                    };
                    return vm;
                });

            var model = new CardListBlockViewModel(currentContent)
            {
                CssClasses = cssClasses,
                CardBlocks = cardBlocks ?? Enumerable.Empty<CardBlockViewModel>(),
            };

            return View(model);
        }

        private static string ParseStyleClasses(string? style)
        {
            var styleNumbers = style?
                .Split(",", StringSplitOptions.RemoveEmptyEntries)
                .Select(num => int.TryParse(num, out int n) ? (CardStyleOption?)n : null)
                .Where(n => n.HasValue)
                .Select(n => n.Value.GetCssClass());

            return string.Join(" ", styleNumbers ?? Enumerable.Empty<string>());
        }
    }
}
