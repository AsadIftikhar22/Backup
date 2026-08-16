namespace Salam.Cms.Web.Features.Forms.B2BRecaptchaBlock;

using EPiServer.DataAnnotations;
using EPiServer.Forms.Implementation.Elements;
using Geta.Optimizely.ContentTypeIcons;
using Geta.Optimizely.ContentTypeIcons.Attributes;
using System.ComponentModel.DataAnnotations;

[ContentType(DisplayName = "B2B Recaptcha Element Recapthca",
           GUID = "754cb132-3f99-4d79-8a07-6f2472aea9ec",
        Description = "B2B Recaptcha Element Recapthca")]
[ContentTypeIcon(FontAwesome5Solid.List)]
public class B2BRecaptchaBlock : TextboxElementBlock
{
    [Display(Name = "Recaptcha Token", GroupName = "Information", Order = -8000)]
    public virtual string recaptchaToken { get; set; }
}
