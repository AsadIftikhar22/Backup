namespace Salam.Cms.Web.Features.ClientResources.Media;

using EPiServer.Cms.Shell.UI.ObjectEditing.EditorDescriptors;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Framework.DataAnnotations;
using EPiServer.Shell.ObjectEditing;
using Geta.Optimizely.ContentTypeIcons;
using Geta.Optimizely.ContentTypeIcons.Attributes;
using Salam.Cms.Shared.Models.Common;
using Salam.Cms.Shared.Models.Common.Properties;
using Salam.Cms.Web.Features.ClientResources.Abstract;
using Salam.Cms.Web.Features.ClientResources.Common;
using Salam.Cms.Web.Features.ClientResources.Enums;
using System.ComponentModel.DataAnnotations;

[ContentType(
    DisplayName = "JavaScript Content",
    GUID = "8557580D-4684-426A-BA4A-610A1D150270",
    Description = "JavaScript files can be uploaded as *.js files only.",
    GroupName = GroupNames.EmbedCode)]
[MediaDescriptor(ExtensionString = "js")]
[ContentTypeIcon(FontAwesome5Solid.FileCode)]
public class JavaScriptContent : MediaData, IScriptResourceInclude
{
    [Display(Name = "Preferred Render Location",
        GroupName = SystemTabNames.Content,
        Order = 10)]
    [BackingType(typeof(PropertyNumber))]
    [EditorDescriptor(EditorDescriptorType = typeof(EnumEditorDescriptor<ClientResourceRenderLocationOption>))]
    public virtual ClientResourceRenderLocationOption RenderLocation { get; set; }

    [Display(Name = "Load script in edit mode?",
        GroupName = SystemTabNames.Content,
        Order = 20)]
    public virtual bool IsLoadedInEditMode { get; set; }

    [Display(Name = "Is the resource minified?",
        GroupName = SystemTabNames.Content,
        Order = 30)]
    public virtual bool IsMinified { get; set; }

    [Display(Name = "Element Attributes",
        Description = "Element attributes to render on <link /> or <script /> tags.",
        GroupName = GroupNames.Content,
        Order = 40)]
    [CultureSpecific]
    [EditorDescriptor(EditorDescriptorType = typeof(CollectionEditorDescriptor<ClientResourceAttributeConfiguration>))]
    public virtual IList<ClientResourceAttributeConfiguration>? Attributes { get; set; }

    [ScaffoldColumn(false)]
    public virtual string? SubResourceIntegrity
    {
        get => null;
        set => _ = value;
    }

    public override void SetDefaultValues(ContentType contentType)
    {
        base.SetDefaultValues(contentType);

        RenderLocation = ClientResourceRenderLocationOption.BodyEnd;
    }
}