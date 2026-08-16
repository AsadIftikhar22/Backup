namespace Salam.Cms.Core.Settings.Infrastructure;

using EPiServer.DataAnnotations;


[AttributeUsage(validOn: AttributeTargets.Class)]
public sealed class SettingsContentTypeAttribute : ContentTypeAttribute
{
    public string? SettingsName { get; set; } = default;
}
