namespace Salam.Cms.Web.Features.Forms.ComplaintTabFormContainerBlock;

using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Forms.Core;
using EPiServer.Forms.Implementation.Elements;
using EPiServer.ServiceLocation;
using EPiServer.Shell.ObjectEditing;
using Geta.Optimizely.ContentTypeIcons;
using Geta.Optimizely.ContentTypeIcons.Attributes;
using Salam.Cms.Shared.Models.Common.RichText;
using Salam.Cms.Web.Features.Common.Interfaces;
using Salam.Cms.Web.Features.Common.Models;
using Salam.Cms.Web.Features.Forms.SelectionFactories;
using System.ComponentModel.DataAnnotations;

[ContentType(GUID = "{aa4c155b-4b1a-4695-8172-2c25e23a12f8}",
    GroupName = EPiServer.Forms.Constants.FormElementGroup_Container,
    Order = 4000)]
[ServiceConfiguration(typeof(IFormContainerBlock))]
[ContentTypeIcon(FontAwesome5Solid.List)]
public class ComplaintTabFormContainerBlock : FormContainerBlock, ISiteContentBlock
{
    [Display(
          Name = "Accordion Background Color",
          GroupName = SystemTabNames.Content,
          Order = 10)]
    [SelectOne(SelectionFactoryType = typeof(BackGroundColorSelectionFactory))]
    public virtual string SelectedHexColor { get; set; }

    [Display(
      Name = "Heading Font Size for Business and Wholesale",
      GroupName = SystemTabNames.Content,
      Order = 10)]
    [SelectOne(SelectionFactoryType = typeof(FontSizeSelectionFactory))]
    public virtual string HeadingFontSize { get; set; }

    [Display(
          Name = "Heading Font Weight for Business and Wholesale",
          GroupName = SystemTabNames.Content,
          Order = 15)]
    [SelectOne(SelectionFactoryType = typeof(FontWeightSelectionFactory))]
    public virtual string HeadingFontWeight { get; set; }


    [Display(
      Name = "Mobile Heading Font Size for Business and Wholesale",
      GroupName = SystemTabNames.Content,
      Order = 20)]
    [SelectOne(SelectionFactoryType = typeof(FontSizeSelectionFactory))]
    public virtual string MobileHeadingFontSize { get; set; }

    [Display(
          Name = "Mobile Heading Font Weight for Business and Wholesale",
          GroupName = SystemTabNames.Content,
          Order = 25)]
    [SelectOne(SelectionFactoryType = typeof(FontWeightSelectionFactory))]
    public virtual string MobileHeadingFontWeight { get; set; }


    [Display(
          Name = "Paragraph Font Size for Business and Wholesale",
          GroupName = SystemTabNames.Content,
          Order = 30)]
    [SelectOne(SelectionFactoryType = typeof(FontSizeSelectionFactory))]
    public virtual string ParagraphFontSize { get; set; }

    [Display(
      Name = "Mobile Paragraph Font Size for Business and Wholesale",
      GroupName = SystemTabNames.Content,
      Order = 30)]
    [SelectOne(SelectionFactoryType = typeof(FontSizeSelectionFactory))]
    public virtual string MobileParagraphFontSize { get; set; }


    [Display(
          Name = "Form Width Half or full Selection Factory",
          GroupName = SystemTabNames.Content,
          Order = 35)]
    [SelectOne(SelectionFactoryType = typeof(FormWidthSelectionFactory))]
    public virtual string FormWidth { get; set; }

    [Display(
      Name = "Label Font Size for the Form Container",
      GroupName = SystemTabNames.Content,
      Order = 40)]
    [SelectOne(SelectionFactoryType = typeof(FontSizeSelectionFactory))]
    public virtual string LabelFormSize { get; set; }

    [Display(
      Name = "Label Font Color for the Form Container",
      GroupName = SystemTabNames.Content,
      Order = 45)]
    [SelectOne(SelectionFactoryType = typeof(LabelColorSelectionFactory))]
    public virtual string LabelFormColor { get; set; }

    [Display(Name = "Form Type", GroupName = "Information", Order = -8000)]
    [SelectOne(SelectionFactoryType = typeof(FormTypeFactory))]
    public virtual string FormType { get; set; }

    [Display(Name = "Form Footer Message", GroupName = "Information", Order = -8000)]
    [CultureSpecific]
    public virtual string FormFooterMessage { get; set; }

    [Display(Name = "Success Message Title", GroupName = "Information", Order = -8000)]
    [CultureSpecific]
    public virtual string SuccessMessageTitle { get; set; }

    [Display(Name = "Success Message Description", GroupName = "Information", Order = -8000)]
    [CultureSpecific]
    [UIHint(RichTextEditors.HtmlEditor)]
    public virtual XhtmlString SuccessMessageDescription { get; set; }


    [Display(Name = "Error Message Title", GroupName = "Information", Order = -8000)]
    [CultureSpecific]
    public virtual string ErrorMessageTitle { get; set; }

    [Display(Name = "Error Message Description", GroupName = "Information", Order = -8000)]
    [CultureSpecific]
    [UIHint(RichTextEditors.HtmlEditor)]
    public virtual XhtmlString ErrorMessageDescription { get; set; }
}

public class BackGroundColorSelectionFactory : ISelectionFactory
{
    public IEnumerable<ISelectItem> GetSelections(ExtendedMetadata metadata)
    {
        yield return new SelectItem { Text = "Gradiant Green", Value = "linear-gradient(83.63deg, #003831 2.86%, #008208 143.36%);" };
        yield return new SelectItem { Text = "Dark Green", Value = "#03322e" };
    }
}

public class FormWidthSelectionFactory : ISelectionFactory
{
    public IEnumerable<ISelectItem> GetSelections(ExtendedMetadata metadata)
    {
        yield return new SelectItem { Text = "676px", Value = "676px" };
        yield return new SelectItem { Text = "1136px", Value = "1136px" };
    }
}

public class FormTypeFactory : ISelectionFactory
{
    public IEnumerable<ISelectItem> GetSelections(ExtendedMetadata metadata)
    {
        yield return new SelectItem { Text = "Template1", Value = "Template1" };
        yield return new SelectItem { Text = "Template2", Value = "Template2" };
        yield return new SelectItem { Text = "Template3", Value = "Template3" };
        yield return new SelectItem { Text = "Template4", Value = "Template4" };
        yield return new SelectItem { Text = "Template5", Value = "Template5" };
    }
}

[ContentType(
    DisplayName = "DXP Complaint Modal Block",
    GUID = "0c9bbf58-71be-4d31-aaf5-2fe366f5202c",
    Description = "Displays an DXP Complaint Modal Block.",
    GroupName = SystemTabNames.Content)]
[ContentTypeIcon(FontAwesome5Solid.List)]
public class ComplaintModelElementBlock : SiteContentBlock
{
    [Display(
      Name = "Modal ID",
      Description = "Modal ID",
      GroupName = SystemTabNames.Content,
      Order = 10)]
    [Required]
    public virtual string ModalID { get; set; }

    [Display(
      Name = "Modal Title",
      Description = "Modal Title",
      GroupName = SystemTabNames.Content,
      Order = 20)]
    [CultureSpecific]
    public virtual string ModalTitle { get; set; }

    [Display(
          Name = "Modal Description",
          Description = "Modal Description",
          GroupName = SystemTabNames.Content,
          Order = 30)]
    [CultureSpecific]
    public virtual string ModalDescription { get; set; }
}