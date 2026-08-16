namespace Salam.Cms.UnitTests.Features.Blocks.FeaturedProductList;

public static class FeaturedProductListBlockViewComponentTestCases
{
    public static IEnumerable<TestCaseData> BuildModelTestCases
    {
        get
        {
            yield return new TestCaseData("Heading Line One");

            yield return new TestCaseData(null);
        }
    }
}
