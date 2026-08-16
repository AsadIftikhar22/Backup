namespace Salam.Cms.Web.Features.XHtmlBlock.Models;

using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Web;
using Geta.Optimizely.ContentTypeIcons;
using Geta.Optimizely.ContentTypeIcons.Attributes;
using Salam.Cms.Shared.Models.Common;
using Salam.Cms.Shared.Models.Common.RichText;
using Salam.Cms.Web.Features.CallToAction.Abstract;
using Salam.Cms.Web.Features.Common.Interfaces;
using Salam.Cms.Web.Features.Common.Models;
using System.ComponentModel.DataAnnotations;

[ContentType(
    DisplayName = "XHTML Block",
    GUID = "b4191c94-073a-43a3-bf07-642a287fa2cd",
    Description = "Displays an XHTML Block.",
    GroupName = SystemTabNames.Content)]
[ContentTypeIcon(FontAwesome5Solid.Icons)]
public class XHtmlBlock : SiteContentBlock, IXHtmlBlock, IPageNavigatorData
{
    [Display(
        Name = "Block Navigator Title",
        Description = "The title displayed in the page navigator when 'Enable Page Navigator' is activated on a page containing this block in the main content area.",
        GroupName = GroupNames.Navigation,
        Order = 5)]
    [CultureSpecific]
    public virtual string? NavigationTitle { get; set; }

    [Display(
            Name = "Block Styles",
            Description = "Select the Styles for the Components",
            GroupName = GroupNames.Content,
            Order = 10)]
    [CultureSpecific]
    [UIHint(RichTextEditors.HtmlEditor)]
    public virtual XhtmlString? CSSBody { get; set; }

    [Display(
    Name = "Main Body",
    Description = "Select the main body for the Page e.g Html for components",
    GroupName = GroupNames.Content,
    Order = 15)]
    [CultureSpecific]
    [UIHint(RichTextEditors.HtmlEditor)]
    public virtual XhtmlString? HTMLBody { get; set; }
}
