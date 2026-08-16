namespace Salam.Cms.Web.Features.Home.Models;

using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using Geta.Optimizely.ContentTypeIcons;
using Geta.Optimizely.ContentTypeIcons.Attributes;
using Salam.Cms.Shared.Models.Common;
using Salam.Cms.Shared.Models.Common.RichText;
using Salam.Cms.Web.Features.Common.Models;
using System.ComponentModel.DataAnnotations;

[ContentType(
    DisplayName = "Investor Relation Page",
    GUID = "091cb35c-7fba-4f55-a7a1-d78ee0639e63",
    Description = "A page designed as a Investor Relation Page.",
    GroupName = GroupNames.Content)]
[ContentTypeIcon(FontAwesome.Flag)]
public class InvestorRelationPage : SitePageData
{
    [Display(
        Name = "Main Content",
        Description = "A content area that allows blocks that have been specifically designed as section content.",
        GroupName = GroupNames.Content,
        Order = 10)]
    public virtual ContentArea? MainContent { get; set; }

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
}