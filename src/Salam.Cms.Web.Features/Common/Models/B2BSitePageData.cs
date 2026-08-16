namespace Salam.Cms.Web.Features.Common.Models;
using EPiServer.Core;
using EPiServer.DataAnnotations;
using Salam.Cms.Shared.Models.Common;
using Salam.Cms.Shared.Models.Common.RichText;
using Salam.Cms.Shared.Models.Pages;
using Salam.Cms.Web.Features.Common.Interfaces;
using System.ComponentModel.DataAnnotations;

public abstract class B2BSitePageData : SitePageData, INavigationItem, IPageNavigatorEnabled
{
    [Display(
    Name = "Enable Category Navigator",
    Description = "Toggle the Category navigator, which links to sections of the main content area. The Page Navigator Title on the block will serve as the link text.",
    GroupName = GroupNames.Navigation,
    Order = 45)]
    public virtual bool EnableCategoryNavigator { get; set; }

    [Display(
        Name = "Start Page Main Content",
        Description = "A content area that allows blocks that have been specifically designed as section content.",
        GroupName = GroupNames.Content,
        Order = 20)]
    [CultureSpecific]
    public virtual ContentArea? MainContent { get; set; }

    [Display(
        Name = "Enable Page Navigator",
        Description = "Toggle the page navigator, which links to sections of the main content area. The Page Navigator Title on the block will serve as the link text.",
        GroupName = GroupNames.Navigation,
        Order = 40)]
    public virtual bool EnablePageNavigator { get; set; }

    [Display(
    Name = "End Page Main Content",
    Description = "A content area that allows blocks that have been specifically designed as section content.",
    GroupName = GroupNames.Content,
    Order = 50)]
    [AllowedTypes(typeof(ISiteContentBlock))]
    [CultureSpecific]
    public virtual ContentArea? EndPageMainContent { get; set; }
    [Display(
        Name = "CSS for the pages",
        Description = "Select the main css for the Page",
        GroupName = GroupNames.Content,
        Order = 25)]
    [CultureSpecific]
    [UIHint(RichTextEditors.HtmlEditor)]
    public virtual XhtmlString? HTMLCSS { get; set; }

    [Display(
    Name = "Main Body",
    Description = "Select the main body for the Page e.g Html for components",
    GroupName = GroupNames.Content,
    Order = 30)]
    [CultureSpecific]
    [UIHint(RichTextEditors.HtmlEditor)]
    public virtual XhtmlString? HTMLBody { get; set; }

    [Display(
     Name = "Show Footer card components from B2B",
     Description = "Show Footer card components from B2B.",
     GroupName = GroupNames.Navigation,
     Order = 40)]
    public virtual bool NotShowFromWebLayoutB2B { get; set; }

    [Display(
         Name = "Selected Product Name needs to be displayed on Selected Product page",
         Description = "Selected Product Name needs to be displayed on Selected Product page",
         GroupName = GroupNames.SelectedProducts,
         Order = 50)]
    [CultureSpecific]
    public virtual string ProductName { get; set; }

    [Display(
     Name = "Selected Product Labels list for B2B",
     Description = "Selected Product Labels list for B2B",
     GroupName = GroupNames.SelectedProducts,
     Order = 60)]
    [CultureSpecific]
    public virtual IList<string> Labels { get; set; }


    [Display(
     Name = "Get Page Name for Selected Product",
     Description = "Get Page Name for Selected Product from child level But from enquire level get the Page name if product Name is not available",
     GroupName = GroupNames.SelectedProducts,
     Order = 60)]

    public virtual bool SelectedProductPageName { get; set; }
}