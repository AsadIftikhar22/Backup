namespace Salam.Cms.Shared.Models.Common.Components;

public sealed class LinkModel
{
    public string? Text { get; set; }

    public string? Title { get; set; }

    public string? Target { get; set; }

    public string? Url { get; set; }

    public bool Display => !string.IsNullOrWhiteSpace(Text) && !string.IsNullOrWhiteSpace(Url);
}