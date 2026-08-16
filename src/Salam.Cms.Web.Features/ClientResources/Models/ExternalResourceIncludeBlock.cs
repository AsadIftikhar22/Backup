namespace Salam.Cms.Web.Features.ClientResources.Models;

using EPiServer.Cms.Shell.UI.ObjectEditing.EditorDescriptors;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Shell.ObjectEditing;
using Geta.Optimizely.ContentTypeIcons;
using Geta.Optimizely.ContentTypeIcons.Attributes;
using Salam.Cms.Shared.Models.Common;
using Salam.Cms.Shared.Models.Common.Properties;
using Salam.Cms.Web.Features.ClientResources.Abstract;
using Salam.Cms.Web.Features.ClientResources.Common;
using Salam.Cms.Web.Features.ClientResources.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

[ContentType(
    DisplayName = "Scripts (External)",
    Description = "Use to add any 3rd party script to the page source",
    GUID = "4e6872a0-0dd7-40f1-8b41-dfb8d2ddf2d1",
    GroupName = GroupNames.EmbedCode)]
[ContentTypeIcon(FontAwesome.NewspaperO)]
public class ExternalResourceIncludeBlock : BlockData, IExternalResourceInclude
{
    [Display(Name = "Resource Type",
        GroupName = SystemTabNames.Content,
        Order = 10)]
    [BackingType(typeof(PropertyNumber))]
    [EditorDescriptor(EditorDescriptorType = typeof(EnumEditorDescriptor<ClientResourceTypeOption>))]
    public virtual ClientResourceTypeOption ResourceType { get; set; }

    [Display(Name = "External Resource Url",
        Order = 20)]
    [Required]
    public virtual string? ExternalUrl { get; set; }

    [Display(Name = "Subresource Integrity (SRI)",
        Description = "A Subresource Integrity value allows the browser to verify that the External File has not be compromised or changed unexpectedly.",
        Order = 30)]
    public virtual string? SubResourceIntegrity { get; set; }

    [Display(Name = "Preferred Render Location",
        GroupName = SystemTabNames.Content,
        Order = 40)]
    [BackingType(typeof(PropertyNumber))]
    [EditorDescriptor(EditorDescriptorType = typeof(EnumEditorDescriptor<ClientResourceRenderLocationOption>))]
    public virtual ClientResourceRenderLocationOption RenderLocation { get; set; }

    [Display(Name = "Load script in edit mode?",
        GroupName = SystemTabNames.Content,
        Order = 50)]
    public virtual bool IsLoadedInEditMode { get; set; }

    [Display(Name = "Is the resource minified?",
        GroupName = SystemTabNames.Content,
        Order = 60)]
    public virtual bool IsMinified { get; set; }

    [Display(Name = "Element Attributes",
        Description = "Element attributes to render on <link /> or <script /> tags.",
        GroupName = GroupNames.Content,
        Order = 70)]
    [CultureSpecific]
    [EditorDescriptor(EditorDescriptorType = typeof(CollectionEditorDescriptor<ClientResourceAttributeConfiguration>))]
    public virtual IList<ClientResourceAttributeConfiguration>? Attributes { get; set; }

    public override void SetDefaultValues(ContentType contentType)
    {
        base.SetDefaultValues(contentType);

        RenderLocation = ClientResourceRenderLocationOption.Head;
    }
}
