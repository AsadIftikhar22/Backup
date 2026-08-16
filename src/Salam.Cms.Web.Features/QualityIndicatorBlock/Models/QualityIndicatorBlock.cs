namespace Salam.Cms.Web.Features.SolutionsSectionsBlock.Models;

using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.SpecializedProperties;
using EPiServer.Web;
using Geta.Optimizely.ContentTypeIcons;
using Geta.Optimizely.ContentTypeIcons.Attributes;
using Salam.Cms.Web.Features.Common.Models;
using System.ComponentModel.DataAnnotations;

[ContentType(
    DisplayName = "DXP B2B Quality Indicator Block",
    GUID = "5fb33599-8b71-4759-9e29-506e82459009",
    Description = "DXP B2B Quality Indicator Block",
    GroupName = SystemTabNames.Content)]
[ContentTypeIcon(FontAwesome5Solid.List)]
public class QualityIndicatorBlock : SiteContentBlock
{
    [Display(
    Name = "Year Quarterly Button Text",
    GroupName = SystemTabNames.Content,
    Order = 5)]
    [CultureSpecific]
    public virtual string YearBtnText { get; set; }

    [Display(
        Name = "Heading",
        GroupName = SystemTabNames.Content,
        Order = 10)]
    [CultureSpecific]
    public virtual string? Heading { get; set; }

    [Display(
    Name = "Quarterly Button Text",
    GroupName = SystemTabNames.Content,
    Order = 15)]
    [CultureSpecific]
    public virtual string QuarterlyBtnText { get; set; }

    [Display(
        Name = "Download Button Text",
        GroupName = SystemTabNames.Content,
        Order = 20)]
    [CultureSpecific]
    public virtual string DownloadBtnText { get; set; }

    [Display(
        Name = "Quarterly Reports",
        GroupName = SystemTabNames.Content,
        Order = 25)]
    [CultureSpecific]
    public virtual IList<QuarterReport> Reports { get; set; }
}

[ContentType(
    DisplayName = "Quality Indicator Block",
    GUID = "554b881c-2c63-4023-a321-30d50cf5999e",
    Description = "Displays Quarterly indicator block to be dropped inside Quarter Reports",
    GroupName = SystemTabNames.Content)]
[ContentTypeIcon(FontAwesome5Solid.List)]
public class QuarterReport : SiteContentBlock
{


    [Display(
    Name = "Year Quarterly",
    GroupName = SystemTabNames.Content,
    Order = 10)]
    [CultureSpecific]
    public virtual int Year { get; set; }


    [Display(
        Name = "Quarterly Tab",
        GroupName = SystemTabNames.Content,
        Order = 20)]
    [CultureSpecific]
    public virtual string QuarterlyTab { get; set; }

    [Display(
            Name = "Media Files",
            GroupName = SystemTabNames.Content,
            Order = 30)]
    [CultureSpecific]
    [UIHint(UIHint.MediaFile)]
    public virtual LinkItem FileUrl { get; set; }
}