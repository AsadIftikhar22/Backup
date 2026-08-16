using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Forms.Implementation.Elements;
using EPiServer.SpecializedProperties;
using Geta.Optimizely.ContentTypeIcons;
using Geta.Optimizely.ContentTypeIcons.Attributes;
using System.ComponentModel.DataAnnotations;

[ContentType(DisplayName = "Protector Channel OTP Block",
           GUID = "af1208c0-f76a-4631-8fc2-b0bb497ab90f",
        Description = "Protector Channel OTP Block")]

[ContentTypeIcon(FontAwesome5Solid.List)]
public class WPOTPElementBlock : TextboxElementBlock
{
    [Display(
        Name = "OTP 4 Code sent message",
        Description = "OTP 4 Code sent message",
        GroupName = SystemTabNames.Content,
        Order = 5)]
    public virtual string OTPMessage { get; set; }

    [Display(
    Name = "Enter OTP Message",
    Description = "Enter OTP Message",
    GroupName = SystemTabNames.Content,
    Order = 5)]
    public virtual string EnterOTPMessage { get; set; }

    [Display(
        Name = "Field Mapping with Email Template",
        Description = "Field Mapping with Email Template",
        GroupName = SystemTabNames.Content,
        Order = 10)]
    [Required]
    public virtual string FieldMapping { get; set; }

    [Display(
    Name = "Invalid OTP Messages",
    Description = "Invalid OTP Messages",
    GroupName = SystemTabNames.Content,
    Order = 70)]
    public virtual string InvalidOTPMessage { get; set; }

    [Display(
    Name = "Re Send OTP Link",
    Description = "Re Send OTP Link",
    GroupName = SystemTabNames.Content,
    Order = 70)]
    public virtual LinkItem ReSendOTP { get; set; }

    [Display(
    Name = "Remaining time left message",
    Description = "Remaining time left message",
    GroupName = SystemTabNames.Content,
    Order = 70)]
    public virtual string timeLeftMsg { get; set; }
}