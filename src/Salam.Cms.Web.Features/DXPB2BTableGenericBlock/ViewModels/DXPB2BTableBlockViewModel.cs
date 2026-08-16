using Salam.Cms.Web.Features.Common.ViewModels;
using Salam.Cms.Web.Features.DXPB2BTable.Models;
using System.Collections.Generic;
using System.Linq;

namespace Salam.Cms.Web.Features.DXPB2BTable.ViewModels
{
    public class DXPB2BTableBlockViewModel : BlockViewModel<B2BTableGenericBlock>
    {
        public DXPB2BTableBlockViewModel(B2BTableGenericBlock currentBlock)
            : base(currentBlock)
        {
            // Ensure HeaderRows is a concrete List<B2BRowBlock>
            HeaderRows = (currentBlock.HeaderRow ?? Enumerable.Empty<B2BRowGenericBlock>()).ToList();

            // Flatten all header cells into a single list
            //HeaderCells = HeaderRows.SelectMany(r => r.Cells ?? Enumerable.Empty<B2BCellGenericBlock>()).ToList();

            // Map rows to TableRowViewModel
            TableRows = (currentBlock.Rows ?? Enumerable.Empty<B2BRowGenericBlock>())
                .Select(r => new TableRowViewModel
                {
                    RowBackgroundColor = r.RowBackgroundColor,
                    Cells = r.Cells?.ToList() ?? new List<B2BCellGenericBlock>()
                })
                .ToList();
        }

        /// <summary>
        /// Flattened list of all header cells (for easier access in view)
        /// </summary>
        public IList<B2BCellGenericBlock> HeaderCells { get; set; }

        /// <summary>
        /// Original header rows (useful if you need row structure)
        /// </summary>
        public IList<B2BRowGenericBlock> HeaderRows { get; set; }

        /// <summary>
        /// Table body rows
        /// </summary>
        public IList<TableRowViewModel> TableRows { get; set; }
    }

    public class TableRowViewModel
    {
        public string? RowBackgroundColor { get; set; }
        public IList<B2BCellGenericBlock> Cells { get; set; } = new List<B2BCellGenericBlock>();
    }
}
