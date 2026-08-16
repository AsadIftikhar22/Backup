namespace Salam.Cms.Web.Features.Preview.ViewModels;

using EPiServer.Core;
using Salam.Cms.Web.Features.Common.Models;
using Salam.Cms.Web.Features.Common.ViewModels;
using System.Collections.Generic;

public class PreviewModel : SitePageViewModel<SitePageData>
{
    public PreviewModel(SitePageData currentPage, ContentAreaItem previewContent)
        : base(currentPage)
    {
        PreviewContent = previewContent;
        Areas = new List<PreviewArea>();
    }

    public ContentAreaItem PreviewContent { get; set; }

    public List<PreviewArea> Areas { get; set; } = [];

    public class PreviewArea
    {
        public bool Supported { get; set; }

        public string AreaName { get; set; }

        public string AreaTag { get; set; }

        public ContentArea ContentArea { get; set; }
    }
}