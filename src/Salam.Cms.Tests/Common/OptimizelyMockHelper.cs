namespace Salam.Cms.Tests.Common;

using EPiServer.Core;
using Geta.Optimizely.Categories;
using System.Collections.Generic;
using System.Linq;

public static class OptimizelyMockHelper
{
    public static PageData CreateMockedSitePageData(int identity)
    {
        return CreateSitePageDataMock(identity).Object;
    }

    public static Mock<PageData> CreateSitePageDataMock(int identity)
    {
        var mockSitePageData = new Mock<PageData>(MockBehavior.Loose);
        mockSitePageData.SetupGet(x => x.ContentLink).Returns(new ContentReference(identity));
        mockSitePageData.SetupGet(x => x.PageName).Returns($"Page {identity}");

        return mockSitePageData;
    }

    public static ContentArea CreateMockedContentArea(params int[] identities)
    {
        return CreateMockedContentArea(identities.ToList());
    }

    public static ContentArea CreateMockedContentArea(IEnumerable<int> identities)
    {
        return CreateContentAreaMock(identities).Object;
    }

    public static Mock<ContentArea> CreateContentAreaMock(IEnumerable<int> identities)
    {
        var contentAreaItems = identities.Select(x => new ContentAreaItem { ContentLink = new ContentReference(x) })
                                         .ToList();
        var contentArea = new Mock<ContentArea>(MockBehavior.Loose);
        contentArea.Setup(x => x.Items).Returns(contentAreaItems);
        contentArea.Setup(x => x.FilteredItems).Returns(contentAreaItems);
        contentArea.Setup(x => x.Count).Returns(contentAreaItems.Count);

        return contentArea;
    }

    public static CategoryData CreateMockedCategoryData(int identity)
    {
        return CreateCategoryDataMock(identity).Object;
    }

    public static Mock<CategoryData> CreateCategoryDataMock(int identity)
    {
        var mockcategoryData = new Mock<CategoryData>(MockBehavior.Loose);
        mockcategoryData.Setup(x => x.ContentLink).Returns(new ContentReference(identity));
        mockcategoryData.Setup(x => x.Name).Returns($"Category {identity}");

        return mockcategoryData;
    }
}
