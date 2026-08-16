namespace Salam.Cms.UnitTests.Features.Blocks.CallToAction;

using EPiServer.Core;
using EPiServer.SpecializedProperties;
using Salam.Cms.Shared.Models.Common.Enums;

public static class CallToActionBlockViewComponentTestCases
{
    public static IEnumerable<TestCaseData> BuildModelTestCases
    {
        get
        {
            yield return new TestCaseData(
                LayoutOption.MediaThenContent,
                ContentReference.EmptyReference,
                "Badge Text",
                "Heading Line One",
                "Heading Line Two",
                new XhtmlString("<p>Main body text</p>"),
                new LinkItemCollection());

            yield return new TestCaseData(
                LayoutOption.ContentThenMedia,
                ContentReference.EmptyReference,
                null,
                "Heading Line One",
                null,
                new XhtmlString("<p>Another main body text</p>"),
                null);
        }
    }
}
