namespace Salam.Cms.Core.Settings.Components;

using EPiServer.Shell;
using EPiServer.Shell.ViewComposition;
using Salam.Cms.Core.Settings.Infrastructure;

[Component]
public class GlobalSettingsComponent : ComponentDefinitionBase
{
    public GlobalSettingsComponent()
        : base("epi-cms/component/MainNavigationComponent")
    {
        LanguagePath = "/episerver/cms/components/globalsettings";
        Title = "Site Settings";
        SortOrder = 1000;
        PlugInAreas = new[] { PlugInArea.AssetsDefaultGroup };
        Settings.Add(new Setting("repositoryKey", value: GlobalSettingsRepositoryDescriptor.RepositoryKey));
    }
}
