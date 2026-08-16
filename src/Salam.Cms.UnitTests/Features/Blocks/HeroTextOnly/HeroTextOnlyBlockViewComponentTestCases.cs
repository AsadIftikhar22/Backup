namespace Salam.Cms.UnitTests.Features.Blocks.HeroTextOnly;

using EPiServer.Core;
using NUnit.Framework;

public static class HeroTextOnlyBlockViewComponentTestCases
{
    public static IEnumerable<TestCaseData> BuildModelTestCases()
    {
        yield return new TestCaseData("Test Heading", new XhtmlString("<p>Test Description</p>"));
        yield return new TestCaseData("Test Heading", null);
        yield return new TestCaseData(null, new XhtmlString("<p>Test Description</p>"));
        yield return new TestCaseData(null, null);
    }
}
