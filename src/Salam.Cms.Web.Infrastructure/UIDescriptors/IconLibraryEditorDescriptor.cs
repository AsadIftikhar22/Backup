using EPiServer;
using EPiServer.Core;
using EPiServer.Shell.ObjectEditing.EditorDescriptors;
using EPiServer.Web;
using Salam.Cms.Shared.Models;
using static Salam.Cms.Shared.Models.SalamConstants;

namespace Salam.Cms.Web.Infrastructure.UIDescriptors;

[EditorDescriptorRegistration(TargetType = typeof(ContentReference), UIHint = SalamUIHint.IconLibrary)]
public class IconLibraryEditorDescriptor : ContentReferenceEditorDescriptor<ImageData>
{
    private readonly IContentLoader _contentLoader;

    public IconLibraryEditorDescriptor(IContentLoader contentLoader)
    {
        _contentLoader = contentLoader;
    }

    public override IEnumerable<ContentReference> Roots
    {
        get
        {
            var assetRoot = _contentLoader.GetChildren<ContentFolder>(SiteDefinition.Current.GlobalAssetsRoot)
                            .First(c => c.Name == AssetLibraryConstants.AssetLibrary);

            var iconLibraryRoot = _contentLoader.GetChildren<ContentFolder>(assetRoot.ContentLink)
                            .First(c => c.Name == AssetLibraryConstants.IconLibrary);

            return new ContentReference[] { new ContentReference(iconLibraryRoot.ContentLink.ID) };
        }
    }
}