namespace Salam.Cms.Web.Features.Catalogue.Models;

using EPiServer.DataAnnotations;
using Geta.Optimizely.ContentTypeIcons;
using Geta.Optimizely.ContentTypeIcons.Attributes;
using Salam.Cms.Shared.Models.Common;
using Salam.Cms.Web.Features.Common.Interfaces;
using Salam.Cms.Web.Features.Common.Models;
using System.ComponentModel.DataAnnotations;

[ContentType(
    DisplayName = "Product Summary",
    Description = "A block that is used to display a summary of a product.",
    GUID = "68ef7ae2-aa36-4038-8be5-f2c92b926355",
    GroupName = GroupNames.Content)]
[ContentTypeIcon(FontAwesome5Solid.MoneyCheck)]
public class ProductSummaryBlock : SiteBlockData, IPageNavigatorData
{
    [Display(
        Name = "Page Navigator Title",
        Description = "The title displayed in the page navigator when 'Enable Page Navigator' is activated on a page containing this block in the main content area.",
        GroupName = GroupNames.Navigation,
        Order = 5)]
    [CultureSpecific]
    public virtual string? NavigationTitle { get; set; }

    [Display(
    Name = "Is Platform Card Visible",
    Description = "Platform Card visible the end line with some social icons to make them visible true or false",
    GroupName = GroupNames.Content,
    Order = 10)]
    [CultureSpecific]
    public virtual bool IsPlatformCardVisible { get; set; }

    [Display(
        Name = "Current page category",
        Description = "Current page category",
        GroupName = GroupNames.Content,
        Order = 10)]
    [CultureSpecific]
    public virtual string? BatchCategory { get; set; }
}
