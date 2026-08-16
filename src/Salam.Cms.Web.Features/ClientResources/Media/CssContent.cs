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
    DisplayName = "CSS Content",
    GUID = "88FF5E2F-8BC8-4B56-A905-25E55AC832E7",
    Description = "Cascading Style Sheet files can be uploaded as .css files only.",
    GroupName = GroupNames.EmbedCode)]
[MediaDescriptor(ExtensionString = "css")]
[ContentTypeIcon(FontAwesome5Solid.FileCode)]
public class CssContent : MediaData, IStyleResourceInclude
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
    }
}