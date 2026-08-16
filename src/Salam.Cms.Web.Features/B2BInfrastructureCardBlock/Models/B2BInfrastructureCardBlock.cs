namespace Salam.Cms.Web.Features.B2BInfrastructureCardBlock.Models;

using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using Geta.Optimizely.ContentTypeIcons;
using Geta.Optimizely.ContentTypeIcons.Attributes;
using Salam.Cms.Web.Features.Common.Models;
using Salam.Cms.Web.Features.InfrastructuresCardItems.Models;
using System.ComponentModel.DataAnnotations;

[ContentType(
    DisplayName = "DXP B2B Infrastructures Card List Block",
    GUID = "cc0bd054-5531-4d6e-a315-e4a5628d1242",
    Description = "Displays an DXP B2B Infrastructures Card List Block",
    GroupName = SystemTabNames.Content)]
[ContentTypeIcon(FontAwesome5Solid.List)]
public class B2BInfrastructureCardBlock : SiteContentBlock
{
    [Display(
       Name = "Infrastructures Card Items Block",
       Description = "Infrastructures Card Items Block",
       GroupName = SystemTabNames.Content,
       Order = 30)]
    [CultureSpecific]
    public virtual IList<InfrastructuresCardItemsBlock> infrastructureCardItems { get; set; }
}
