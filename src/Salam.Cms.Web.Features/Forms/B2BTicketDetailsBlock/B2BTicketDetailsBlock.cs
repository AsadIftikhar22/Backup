using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Forms.Implementation.Elements;
using EPiServer.SpecializedProperties;
using Geta.Optimizely.ContentTypeIcons;
using Geta.Optimizely.ContentTypeIcons.Attributes;
using System.ComponentModel.DataAnnotations;

[ContentType(DisplayName = "B2B Ticket Details Block",
           GUID = "a1438cb8-98be-4d47-9bf3-250e0e0abdbd",
        Description = "B2B Hidden Fields")]
[ContentTypeIcon(FontAwesome5Solid.List)]
public class B2BTicketDetailsBlock : PredefinedHiddenElementBlock
{
    [Display(
            Name = "Hidden Field Name",
            Description = "Hidden Field Name",
            GroupName = SystemTabNames.Content,
            Order = 10)]
    public virtual string HiddenFieldName { get; set; }

    [Display(
        Name = "Field Mapping with Email Template",
        Description = "Field Mapping with Email Template",
        GroupName = SystemTabNames.Content,
        Order = 30)]
    [Required]
    public virtual string FieldMapping { get; set; }

    [CultureSpecific]
    public virtual string ComplaintStatus { get; set; }

    [CultureSpecific]
    public virtual string TicketNumber { get; set; }

    [CultureSpecific]
    public virtual string Closed { get; set; }

    [CultureSpecific]
    public virtual string Opened { get; set; }

    [CultureSpecific]
    public virtual string MobileNumber { get; set; }

    [CultureSpecific]
    public virtual LinkItem ContinueHomePage { get; set; }
}