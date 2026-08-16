namespace Salam.Cms.Web.Features.Common.Components.MetaData;

using EPiServer.Core;

public class SocialMetaDataViewModel
{
    public string? Title { get; set; }

    public string? Description { get; set; }

    public string? Creator { get; set; }

    public ContentReference? PageReference { get; set; }

    public ContentReference? ImageReference { get; set; }

    public string? ImageAltText { get; set; }

    public string? SiteName { get; set; }

    public string? TypeName { get; set; }
}