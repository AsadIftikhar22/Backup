namespace Salam.Cms.Core.Settings.Models
{
    public class SettingsApiResponse<T>
    {
        public bool Error { get; set; } = false;

        public T? Data { get; set; } = default;

        public string? Message { get; set; } = default;
    }
}