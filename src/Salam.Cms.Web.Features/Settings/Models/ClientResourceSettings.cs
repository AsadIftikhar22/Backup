namespace Salam.Cms.Web.Features.Settings.Models;

using EPiServer.Core;
using EPiServer.DataAnnotations;
using Geta.Optimizely.ContentTypeIcons;
using Geta.Optimizely.ContentTypeIcons.Attributes;
using Salam.Cms.Core.Settings.Infrastructure;
using Salam.Cms.Core.Settings.Models;
using Salam.Cms.Shared.Models.Common;
using Salam.Cms.Web.Features.ClientResources.Abstract;
using System.ComponentModel.DataAnnotations;

[SettingsContentType(
    DisplayName = "Client Resource Settings",
    GUID = "0cde2c94-cf1f-45ff-8853-47b2bcfe85a3",
    Description = "Client Resource Settings",
    AvailableInEditMode = true,
    SettingsName = "Client Resource Settings")]
[ContentTypeIcon(FontAwesome5Solid.Cogs)]
public class ClientResourceSettings : SettingsBase
{
    [Display(
        Name = "Client Resources",
        Description = "A collection of javascript and style sheet files to load for every page.",
        GroupName = GroupNames.Content,
        Order = 10)]
    [AllowedTypes(typeof(IScriptResourceInclude), typeof(IStyleResourceInclude), typeof(IExternalResourceInclude), typeof(IEmbedResourceInclude))]
    public virtual ContentArea? ClientResources { get; set; }
}
