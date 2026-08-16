using EPiServer.Web;

namespace Salam.Cms.Core.Settings.Models
{
    public class SettingsGroupsResponse
    {
        public string SiteName { get; set; }

        public Guid SiteId { get; set; }

        public IEnumerable<string> SettingGroups { get; set; }

        public SettingsGroupsResponse(SiteDefinition site, IEnumerable<string> settingGroups)
        {
            SiteName = site.Name;
            SiteId = site.Id;
            SettingGroups = settingGroups;
        }
    }
}