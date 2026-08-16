namespace Salam.Cms.Web.Features.Embed.Models;

using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Shell.ObjectEditing;
using EPiServer.Web;
using Geta.Optimizely.ContentTypeIcons;
using Geta.Optimizely.ContentTypeIcons.Attributes;
using Salam.Cms.Shared.Models.Common;
using Salam.Cms.Shared.Models.Common.Properties;
using Salam.Cms.Web.Features.ClientResources.Abstract;
using Salam.Cms.Web.Features.ClientResources.Enums;
using Salam.Cms.Web.Features.Common.Models;
using System.ComponentModel.DataAnnotations;

[ContentType(
    DisplayName = "Embed Block",
    Description = "A block that allows for HTML content to be rendered inline on Embed Pages.",
    GUID = "9407F390-E07E-4D24-87D7-20C8623D2E45",
    GroupName = GroupNames.EmbedCode,
    AvailableInEditMode = false)]

[ContentTypeIcon(FontAwesome5Solid.Code)]
public class EmbedBlock : SiteContentBlock, IEmbedResourceInclude
{
    [Display(
        Name = "HTML Embed Code",
        Description = "HTML Content to be rendered raw and inline with other blocks.",
        GroupName = GroupNames.EmbedCode,
        Order = 10)]
    [CultureSpecific]
    [UIHint(UIHint.Textarea)]
    [Required]
    public virtual string? EmbedContent { get; set; }

    [Display(Name = "Preferred Render Location",
        GroupName = SystemTabNames.Content,
        Order = 20)]
    [BackingType(typeof(PropertyNumber))]
    [EditorDescriptor(EditorDescriptorType = typeof(EnumEditorDescriptor<EmbedRenderLocationOption>))]
    public virtual EmbedRenderLocationOption RenderLocation { get; set; }

    [Display(Name = "Load script in edit mode?",
        GroupName = SystemTabNames.Content,
        Order = 30)]
    public virtual bool IsLoadedInEditMode { get; set; }

    [Display(Name = "Client Resources",
        Description = "A collection of javascript and style sheet files to load for this block.",
        GroupName = GroupNames.Content,
        Order = 40)]
    [AllowedTypes(typeof(IScriptResourceInclude), typeof(IStyleResourceInclude), typeof(IExternalResourceInclude), typeof(IEmbedResourceInclude))]
    public virtual ContentArea? ClientResources { get; set; }
}