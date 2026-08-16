using EPiServer;
using EPiServer.Core;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;
using EPiServer.Web;
using static Salam.Cms.Shared.Models.SalamConstants;

namespace Salam.Cms.Web.Infrastructure.Initialization;

[InitializableModule]
[ModuleDependency(typeof(InitializationModule))]
public class IconLibraryInitialization : IInitializableModule
{
    public Injected<IContentRepository> ContentRepository { get; set; }

    public void Initialize(InitializationEngine context)
    {
        var globalAssetsRoot = ContentRepository.Service.GetChildren<ContentFolder>(SiteDefinition.Current.GlobalAssetsRoot);

        // check if AssetLibrary folder exists
        if (!globalAssetsRoot.Any(c => c.Name == AssetLibraryConstants.AssetLibrary))
        {
            var assetLibraryFolder = CreateSubfolder(SiteDefinition.Current.GlobalAssetsRoot, AssetLibraryConstants.AssetLibrary);

            var iconLibraryFolder = CreateSubfolder(assetLibraryFolder.ContentLink, AssetLibraryConstants.IconLibrary);
        }
        else
        {
            var assetLibrary = globalAssetsRoot
                .First(c => c.Name == AssetLibraryConstants.AssetLibrary);

            var subfolders = ContentRepository.Service.GetChildren<ContentFolder>(assetLibrary.ContentLink);

            if (!subfolders.Any(c => c.Name == AssetLibraryConstants.IconLibrary))
            {
                var iconLibraryFolder = CreateSubfolder(assetLibrary.ContentLink, AssetLibraryConstants.IconLibrary);
            }
        }
    }

    public void Uninitialize(InitializationEngine context)
    {
    }

    private ContentFolder CreateSubfolder(ContentReference parentLink, string folderName)
    {
        var folder = ContentRepository.Service.GetDefault<ContentFolder>(parentLink);
        folder.Name = folderName;

        ContentRepository.Service.Save(folder, EPiServer.DataAccess.SaveAction.Publish, EPiServer.Security.AccessLevel.NoAccess);

        return folder;
    }
}