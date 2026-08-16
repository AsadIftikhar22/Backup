namespace Salam.Cms.Web.Features.Catalogue.Models;

using EPiServer.Cms.Shell.UI.ObjectEditing.EditorDescriptors;
using EPiServer.Core;
using EPiServer.DataAnnotations;
using EPiServer.Shell.ObjectEditing;
using EPiServer.SpecializedProperties;
using Geta.Optimizely.ContentTypeIcons;
using Geta.Optimizely.ContentTypeIcons.Attributes;
using Salam.Cms.Shared.Models.Catalogue.Data;
using Salam.Cms.Shared.Models.Catalogue.Editor;
using Salam.Cms.Shared.Models.Catalogue.Enums;
using Salam.Cms.Shared.Models.Catalogue.Models;
using Salam.Cms.Shared.Models.Common;
using Salam.Cms.Shared.Models.Common.Properties;
using Salam.Cms.Shared.Models.Validation;
using Salam.Cms.Web.Features.Common.Models;
using System.ComponentModel.DataAnnotations;

[ContentType(
    DisplayName = "Featured Product List",
    Description = "A block that allows for a featured product list to be rendered inline on pages.",
    GUID = "2753001c-6d5c-4901-86a1-77f7c9b10d06",
    GroupName = GroupNames.Content)]
[ContentTypeIcon(FontAwesome5Solid.ListAlt)]
public class FeaturedProductListBlock : SiteContentBlock
{
    [Display(
        Name = "Heading",
        Description = "The heading to show above the product list.",
        GroupName = GroupNames.Content,
        Order = 10)]
    [CultureSpecific]
    [Required]
    public virtual string? Heading { get; set; }

    [Display(
        Name = "Query Behaviour",
        Description = "The query behaviour to use when fetching products."
                        + SalamConstants.PropertyDescriptions.LineBreak
                        + "ManualOnly: Only show products that are manually selected."
                        + SalamConstants.PropertyDescriptions.LineBreak
                        + "QueryOnly: Only show products that are fetched from the query."
                        + SalamConstants.PropertyDescriptions.LineBreak
                        + "ManualAndQuery: Show products that are manually selected and fetched from the query.",
        GroupName = GroupNames.Content,
        Order = 20)]
    [BackingType(typeof(PropertyNumber))]
    [EditorDescriptor(EditorDescriptorType = typeof(EnumEditorDescriptor<QueryBehaviourOption>))]
    public virtual QueryBehaviourOption QueryBehaviour { get; set; }

    [Display(
        Name = "Query Parameters",
        Description = "The query parameters to use for filtering products.",
        GroupName = GroupNames.Content,
        Order = 30)]
    [BackingType(typeof(QueryParameterProperty))]
    [EditorDescriptor(EditorDescriptorType = typeof(CollectionEditorDescriptor<QueryParameter>))]
    public virtual IList<QueryParameter> QueryParamaters { get; set; } = new List<QueryParameter>();

    [Display(
        Name = "Product Type",
        Description = "The product type to use for filtering products.",
        GroupName = GroupNames.Content,
        Order = 40)]
    [BackingType(typeof(PropertyNumber))]
    [SelectOne(SelectionFactoryType = typeof(EnumSelectionFactory<ProductType>))]
    public virtual ProductType ProductType { get; set; }

    [Display(
        Name = "Product List",
        Description = "Select product IDs to display.",
        GroupName = GroupNames.Content,
        Order = 50)]
    [AutoSuggestSelection(typeof(ProductSelectionQuery), AllowCustomValues = false)]
    [MaxElements(5)]
    public virtual IList<string>? ProductIds { get; set; }

    [Display(
    Name = "Handoff Behaviour",
    Description = "The handoff behaviour to use when handing off products."
                    + SalamConstants.PropertyDescriptions.LineBreak
                    + "None: No handoff behaviour is applied."
                    + SalamConstants.PropertyDescriptions.LineBreak
                    + "Plan: Initiate plan handoff."
                    + SalamConstants.PropertyDescriptions.LineBreak
                    + "Device: Initiate device handoff.",
    GroupName = GroupNames.ProductSelector,
    Order = 70)]
    [BackingType(typeof(PropertyNumber))]
    [EditorDescriptor(EditorDescriptorType = typeof(EnumEditorDescriptor<HandoffOption>))]
    public virtual HandoffOption HandoffBehavior { get; set; }

    [Display(
        Name = "View More Link",
        Description = "View more link definition.",
        GroupName = GroupNames.Content,
        Order = 60)]
    public virtual LinkItem? ViewMoreLink { get; set; }
}
