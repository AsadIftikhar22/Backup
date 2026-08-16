namespace Salam.Cms.Web.Features.Forms.WPFormStepBlock;

using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Forms.Implementation.Elements;
using Geta.Optimizely.ContentTypeIcons;
using Geta.Optimizely.ContentTypeIcons.Attributes;
using System.ComponentModel.DataAnnotations;

[ContentType(DisplayName = "WP Migration Form step End Point",
           GUID = "a2992c3d-e45b-4a97-a7b9-bcd54221a525",
        Description = "WP Migration Form step End Point")]
[ContentTypeIcon(FontAwesome5Solid.List)]
public class WPFormStepBlock : FormStepBlock
{
    [Display(
        Name = "Form Step API Points",
        Description = "Form Step API Points",
        GroupName = SystemTabNames.Content,
        Order = 80)]
    public virtual string ApiEndpoint { get; set; }

    [Display(
    Name = "Form Step Button Text For Each Step",
    Description = "Form Step Button Text For Each Step",
    GroupName = SystemTabNames.Content,
    Order = 80)]
    public virtual string btnTextNew { get; set; }
}
