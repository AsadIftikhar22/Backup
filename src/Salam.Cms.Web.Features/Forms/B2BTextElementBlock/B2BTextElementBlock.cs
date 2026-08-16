using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Forms.Implementation.Elements;
using Geta.Optimizely.ContentTypeIcons;
using Geta.Optimizely.ContentTypeIcons.Attributes;
using Salam.Cms.Web.Features.Forms.ComplaintTabFormContainerBlock;
using System.ComponentModel.DataAnnotations;

[ContentType(DisplayName = "B2B Text Element",
           GUID = "84064982-ccdb-43d9-a55b-d984c3261c7b",
        Description = "B2B Text Form element")]

[ContentTypeIcon(FontAwesome5Solid.List)]
public class B2BTextElementBlock : TextboxElementBlock
{
    [Display(
            Name = "Hint Message for the field",
            Description = "Hint Message for the field",
            GroupName = SystemTabNames.Content,
            Order = 30)]
    public virtual string HintMessage { get; set; }

    [Display(
        Name = "Hint Message Color for the field",
        Description = "Hint Message Color for the field",
        GroupName = SystemTabNames.Content,
        Order = 30)]
    public virtual string HintFontColor { get; set; }

    [Display(
        Name = "Hint Message Font Size for the field",
        Description = "Hint Message Font Size for the field",
        GroupName = SystemTabNames.Content,
        Order = 30)]
    public virtual string HintFontSize { get; set; }

    [Display(
        Name = "Field Mapping with Email Template",
        Description = "Field Mapping with Email Template",
        GroupName = SystemTabNames.Content,
        Order = 30)]
    [Required]
    public virtual string FieldMapping { get; set; }

    public virtual ComplaintModelElementBlock ComplaintModelElementBlock { get; set; }
}