namespace Salam.Cms.Web.Features.NotFound.ViewModels;

using EPiServer.Core;
using Salam.Cms.Shared.Models.Common.Components;

public sealed class NotFoundHeroBlockViewModel
{
    public string? Title { get; set; }

    public XhtmlString? MainBody { get; set; }
    public XhtmlString? SecondaryBody { get; set; }

    public List<LinkModel> Links { get; set; } = new();

    public string? ImageAltText { get; set; }
}