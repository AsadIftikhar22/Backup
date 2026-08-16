using EPiServer.ContentApi.Core.Serialization.Models;
using EPiServer.Web;

namespace Salam.Cms.Core.Settings.Models
{
    public class SettingsGroupResponse
    {
        public string SiteName { get; set; }

        public Guid SiteId { get; set; }

        public string SettingGroup { get; set; }

        public IContentApiModel Settings { get; set; }

        public SettingsGroupResponse(SiteDefinition site, string settingGroup, IContentApiModel settings)
        {
            SiteName = site.Name;
            SiteId = site.Id;
            SettingGroup = settingGroup;
            Settings = settings;
        }
    }
}