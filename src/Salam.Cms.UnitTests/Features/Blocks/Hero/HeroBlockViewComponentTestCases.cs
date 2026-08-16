namespace Salam.Cms.UnitTests.Features.Blocks.Hero;

using EPiServer.Core;
using NUnit.Framework;
using Salam.Cms.Shared.Models.Common.Enums;

public static class HeroBlockViewComponentTestCases
{
    public static IEnumerable<TestCaseData> BuildModelTestCases()
    {
        yield return new TestCaseData("Test Heading", "Badge Text", new XhtmlString("<p>Test description</p>"), LayoutOption.MediaThenContent);
        yield return new TestCaseData(null, null, null, LayoutOption.ContentThenMedia);
        yield return new TestCaseData("Another Heading", "Featured", new XhtmlString("<p>Another description</p>"), LayoutOption.ContentThenMedia);
    }
}