namespace Salam.Cms.Web.Features.TabsFormBlock.Models;

using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using Geta.Optimizely.ContentTypeIcons;
using Geta.Optimizely.ContentTypeIcons.Attributes;
using System.ComponentModel.DataAnnotations;

[ContentType(
    DisplayName = "Tabs Form Card Block",
    GUID = "16dd4e35-a6e4-4778-85c0-e6d2e93b4b3c",
    Description = "TabsFormBlock.",
    GroupName = SystemTabNames.Content)]
[ContentTypeIcon(FontAwesome5Solid.List)]
public class TabsFormBlock : BlockData
{
    [Display(
            Name = "Main Tab title",
            Description = "Main Tab title",
            GroupName = SystemTabNames.Content,
            Order = 5)]
    [CultureSpecific]
    public virtual string? Title { get; set; }

    [Display(
        Name = "Main Tab Descrption",
        Description = "Main Tab Descrption",
        GroupName = SystemTabNames.Content,
        Order = 5)]
    [CultureSpecific]
    public virtual XhtmlString? Descrption { get; set; }

    [CultureSpecific]
    public virtual IList<TabsItemBlock> tabFormItems { get; set; }
}

[ContentType(
    DisplayName = "Tabs Item Form Block",
    GUID = "3b278b3c-1a74-4351-90a7-8a5af31d96c4",
    Description = "TabsFormBlock.",
    GroupName = SystemTabNames.Content)]
[ContentTypeIcon(FontAwesome5Solid.List)]
public class TabsItemBlock : BlockData
{
    [Display(
    Name = "Tab title",
    Description = "Tab title",
    GroupName = SystemTabNames.Content,
    Order = 5)]
    [CultureSpecific]
    public virtual string? Title { get; set; }

    [Display(
    Name = "Form Content Area",
    Description = "Form Content Area",
    GroupName = SystemTabNames.Content,
    Order = 15)]
    [CultureSpecific]
    public virtual ContentArea? FormContentArea { get; set; }

    [Display(
    Name = "Unique Data Tabs ID",
    Description = "Unique Data Tabs ID",
    GroupName = SystemTabNames.Content,
    Order = 20)]
    [CultureSpecific]
    public virtual string? DataTab { get; set; }
}