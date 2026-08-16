namespace Salam.Cms.Web.Features.Common.Models;
using EPiServer.DataAnnotations;
using Salam.Cms.Shared.Models.Common;
using Salam.Cms.Web.Features.Common.Interfaces;
using System.ComponentModel.DataAnnotations;

/// <summary>
/// Base class for all site content blocks that can go in the main content area of most pages.
/// </summary>
public abstract class SiteContentBlock : SiteBlockData, ISiteContentBlock, IPageNavigatorData
{
    [Display(
        Name = "Page Navigator Title",
        Description = "The title displayed in the page navigator when 'Enable Page Navigator' is activated on a page containing this block in the main content area.",
        GroupName = GroupNames.Navigation,
        Order = 5)]
    [CultureSpecific]
    public virtual string? NavigationTitle { get; set; }
}