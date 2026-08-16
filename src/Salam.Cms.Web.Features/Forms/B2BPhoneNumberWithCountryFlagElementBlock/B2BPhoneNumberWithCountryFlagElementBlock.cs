namespace Salam.Cms.Web.Features.Forms.B2BNumberElementBlock;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Forms.Implementation.Elements;
using Geta.Optimizely.ContentTypeIcons;
using Geta.Optimizely.ContentTypeIcons.Attributes;
using System.ComponentModel.DataAnnotations;

[ContentType(DisplayName = "B2B Phone Number With Country Flag Form element",
         GUID = "922c0438-ae2e-41fd-9124-9c97680e49e7",
         Description = "B2B Phone Number With Country Flag Form element")]
[ContentTypeIcon(FontAwesome5Solid.List)]
public class B2BPhoneNumberWithCountryFlagElementBlock : NumberElementBlock
{
    [Display(
    Name = "Field Mapping with Email Template",
    Description = "Field Mapping with Email Template",
    GroupName = SystemTabNames.Content,
    Order = 30)]
    [Required]
    public virtual string FieldMapping { get; set; }

    [Display(
        Name = "Regex Error Message",
        Description = "Regex Error Message",
        GroupName = SystemTabNames.Content,
        Order = 40)]
    [CultureSpecific]
    public virtual string RegexErrorMessage { get; set; }

    [Display(
    Name = "Valid PhoneNumber Error Msg",
    Description = "Valid PhoneNumber Error Msg",
    GroupName = SystemTabNames.Content,
    Order = 40)]
    [CultureSpecific]
    public virtual string ValidPhoneNumberErrorMsg { get; set; }

    [Display(
    Name = "Regex Pattern",
    Description = "Regex Pattern",
    GroupName = SystemTabNames.Content,
    Order = 45)]
    [CultureSpecific]
    public virtual string RegexPattern { get; set; }
}