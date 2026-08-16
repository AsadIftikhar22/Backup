namespace Salam.Cms.UnitTests.Features.Blocks.ProductSelector;

public static class ProductSelectorBlockViewComponentTestCases
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
