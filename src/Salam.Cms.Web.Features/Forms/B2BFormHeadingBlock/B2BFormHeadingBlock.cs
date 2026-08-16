using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Forms.Implementation.Elements;
using EPiServer.Shell.ObjectEditing;
using Geta.Optimizely.ContentTypeIcons;
using Geta.Optimizely.ContentTypeIcons.Attributes;
using Salam.Cms.Web.Features.Forms.SelectionFactories;
using System.ComponentModel.DataAnnotations;

[ContentType(DisplayName = "B2B Sub Form Heading Block",
           GUID = "9ab3a031-be53-4b87-b9b9-d0a10d5496b4",
        Description = "B2B Text Form element")]
[ContentTypeIcon(FontAwesome5Solid.List)]
public class B2BFormHeadingBlock : ParagraphTextElementBlock
{
    [Display(
            Name = "Heading",
            Description = "Heading",
            GroupName = SystemTabNames.Content,
            Order = 10)]
    public virtual string Heading { get; set; }

    [Display(
        Name = "Color for the Heading",
        Description = "Color for the Heading",
        GroupName = SystemTabNames.Content,
        Order = 20)]
    [SelectOne(SelectionFactoryType = typeof(LabelColorSelectionFactory))]
    public virtual string FontColor { get; set; }

    [Display(
        Name = "Font Size for the Heading",
        Description = "Font Size for the Heading",
        GroupName = SystemTabNames.Content,
        Order = 30)]
    [SelectOne(SelectionFactoryType = typeof(FontSizeSelectionFactory))]
    public virtual string FontSize { get; set; }


    [Display(
        Name = "Mobile Font Size for the Heading",
        Description = "Mobile Font Size for the Heading",
        GroupName = SystemTabNames.Content,
        Order = 30)]
    [SelectOne(SelectionFactoryType = typeof(FontSizeSelectionFactory))]
    public virtual string MobileTextFontSize { get; set; }
}