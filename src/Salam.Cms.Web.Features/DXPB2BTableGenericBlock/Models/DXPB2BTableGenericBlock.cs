using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Shell.ObjectEditing;
using Salam.Cms.Web.Features.Common.Models;
using System.ComponentModel.DataAnnotations;

namespace Salam.Cms.Web.Features.DXPB2BTable.Models
{
    [ContentType(
        DisplayName = "DXP B2B Complete Generic Table Blocks",
        GUID = "19a896c4-204b-41db-8595-529cd0a5de2f",
        Description = "Displays a customizable table DXP B2B Generic  with rows and columns",
        GroupName = SystemTabNames.Content)]
    public class B2BTableGenericBlock : SiteContentBlock
    {
        [Display(Name = "Heading", Order = 10)]
        [CultureSpecific]
        public virtual string? Heading { get; set; }

        [Display(Name = "Description", Order = 20)]
        [CultureSpecific]
        public virtual string? Description { get; set; }

        [Display(Name = "Header Row", Order = 25)]
        [CultureSpecific]
        public virtual IList<B2BRowGenericBlock> HeaderRow { get; set; } = new List<B2BRowGenericBlock>();

        [Display(Name = "Table Rows", Order = 300)]
        [CultureSpecific]
        public virtual IList<B2BRowGenericBlock> Rows { get; set; } = new List<B2BRowGenericBlock>();

        [Display(Name = "Table Body Background Color", Order = 400)]
        [SelectOne(SelectionFactoryType = typeof(BackGroundColorSelectionFactory))]
        [CultureSpecific]
        public virtual string? TableBodyBackGroundColor { get; set; }

        [Display(Name = "Font Color for the entire table", Order = 500)]
        [SelectOne(SelectionFactoryType = typeof(FontColorSelectionFactory))]
        public virtual string? CellFontColor { get; set; }

        [Display(Name = "Font Size for the entire table", Order = 600)]
        [SelectOne(SelectionFactoryType = typeof(FontSizeSelectionFactory))]
        public virtual string? CellFontSize { get; set; }


        [Display(Name = "Font Weight for the entire table", Order = 700)]
        [SelectOne(SelectionFactoryType = typeof(FontWeightSelectionFactory))]
        public virtual string? CellFontWeight { get; set; }

        [Display(Name = "Full Width table", Order = 800)]
        public virtual bool FullWidthTable { get; set; }

        [Display(Name = "Table Body Footer Note", Order = 900)]
        [CultureSpecific]
        public virtual string FooterNote { get; set; }

        [Display(Name = "Footer Font Size", Order = 700)]
        [SelectOne(SelectionFactoryType = typeof(FontSizeSelectionFactory))]
        public virtual string? FooterFontSize { get; set; }

        [Display(Name = "Footer Font Weight", Order = 800)]
        [SelectOne(SelectionFactoryType = typeof(FontWeightSelectionFactory))]
        public virtual string? FooterFontWeight { get; set; }

        [Display(Name = "Footer Font Color", Order = 900)]
        [SelectOne(SelectionFactoryType = typeof(FontColorSelectionFactory))]
        public virtual string? FooterFontColor { get; set; }
    }

    [ContentType(
        DisplayName = "DXP B2B Generic Table Row Inside which Table Cell Will Come",
        GUID = "11814fe4-1fe7-4e6c-b3d6-321b10c402ca",
        Description = "DXP B2B Generic Table Row Inside which Table Cell Will Come")]
    public class B2BRowGenericBlock : SiteBlockData
    {
        [Display(Name = "Background Color", Order = 10)]
        [SelectOne(SelectionFactoryType = typeof(BackGroundColorSelectionFactory))]
        public virtual string? RowBackgroundColor { get; set; }
        [CultureSpecific]
        public virtual IList<B2BCellGenericBlock> Cells { get; set; } = new List<B2BCellGenericBlock>();
    }

    [ContentType(
    DisplayName = "DXP B2B Table Half Cell",
    GUID = "518949c9-73e6-4bbe-bcd5-f9779d4e7738",
    Description = "A Half Cell in the B2B table")]
    public class B2BHalfCellGenericBlock : SiteBlockData
    {
        [Display(Name = "Background Color", Order = 10)]
        [SelectOne(SelectionFactoryType = typeof(BackGroundColorSelectionFactory))]
        public virtual string? RowBackgroundColor { get; set; }

        [Display(Name = "First Row Header Cell Content", Order = 400)]
        [CultureSpecific]
        public virtual string? HalfCellContent { get; set; }
    }

    [ContentType(
        DisplayName = "DXP B2B Generic Table Row Item Cell List",
        GUID = "ae61fbb5-572f-4b37-9b5e-2d984b0398a0",
        Description = "A cell in a table row")]
    public class B2BCellGenericBlock : SiteBlockData
    {
        [Display(Name = "Half Cell Block", Order = 5)]
        public virtual B2BHalfCellGenericBlock HalfCellBlock { get; set; }

        [Display(Name = "Content / HTML", Order = 10)]
        [CultureSpecific]
        public virtual string? Content { get; set; }

        [Display(Name = "Row Span", Order = 20)]
        [CultureSpecific]
        public virtual int RowSpan { get; set; } = 1;

        [Display(Name = "Column Span", Order = 30)]
        [CultureSpecific]
        public virtual int ColSpan { get; set; } = 1;

        [Display(Name = "Background Color", Order = 40)]
        [SelectOne(SelectionFactoryType = typeof(BackGroundColorSelectionFactory))]
        public virtual string? CellBackgroundColor { get; set; }

        [Display(Name = "Check Box Is Tick SVG Visible", Order = 50)]
        public virtual bool CheckBoxIsSVGVisible { get; set; }

        #region Over ride Font styles

        [Display(Name = "Over Ride Font Color for the entire table", Order = 500)]
        [SelectOne(SelectionFactoryType = typeof(FontColorSelectionFactory))]
        public virtual string? CellFontColor { get; set; }

        [Display(Name = "Over Ride Font Size for the entire table", Order = 600)]
        [SelectOne(SelectionFactoryType = typeof(FontSizeSelectionFactory))]
        public virtual string? CellFontSize { get; set; }

        [Display(Name = "Over Ride Font Weight for the entire table", Order = 600)]
        [SelectOne(SelectionFactoryType = typeof(FontWeightSelectionFactory))]
        public virtual string? CellFontWeight { get; set; }

 

        #endregion
    }

    public class BackGroundColorSelectionFactory : ISelectionFactory
    {
        public IEnumerable<ISelectItem> GetSelections(ExtendedMetadata metadata)
        {
            yield return new SelectItem { Text = "Gradiant Green", Value = "linear-gradient(to bottom right, #003831, #008208);" };
            yield return new SelectItem { Text = "Dark Green", Value = "#33605a" };
            yield return new SelectItem { Text = "Dark Black", Value = "#001815" };
            yield return new SelectItem { Text = "White", Value = "#fff" };
        }
    }

    public class FontColorSelectionFactory : ISelectionFactory
    {
        public IEnumerable<ISelectItem> GetSelections(ExtendedMetadata metadata)
        {
            yield return new SelectItem { Text = "Black", Value = "#002318" };
            yield return new SelectItem { Text = "Dark Black", Value = "#001815" };
            yield return new SelectItem { Text = "White", Value = "#fff" };
        }
    }

    public class FontWeightSelectionFactory : ISelectionFactory
    {
        public IEnumerable<ISelectItem> GetSelections(ExtendedMetadata metadata)
        {
            yield return new SelectItem { Text = "400", Value = "400" };
            yield return new SelectItem { Text = "600", Value = "600" };
        }
    }

    public class FontSizeSelectionFactory : ISelectionFactory
    {
        public IEnumerable<ISelectItem> GetSelections(ExtendedMetadata metadata)
        {
            yield return new SelectItem { Text = "14px", Value = "14px" };
            yield return new SelectItem { Text = "16px", Value = "16px" };
            yield return new SelectItem { Text = "18px", Value = "18px" };
            yield return new SelectItem { Text = "20px", Value = "20px" };
            yield return new SelectItem { Text = "22px", Value = "22px" };
            yield return new SelectItem { Text = "24px", Value = "24px" };
        }
    }
}
