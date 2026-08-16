namespace Salam.Cms.UnitTests.Features.Blocks.IconLinkItem;

public static class IconLinkItemListBlockTestCases
{
    public static IEnumerable<TestCaseData> CanSetPropertiesTestCases
    {
        get
        {
            yield return new TestCaseData("Test Heading", "<p>Test</p>", true).SetName("Valid properties with ContentArea");
            yield return new TestCaseData(null, null, false).SetName("All null values");
            yield return new TestCaseData("", "", true).SetName("Empty strings with ContentArea");
            yield return new TestCaseData(" ", " ", false).SetName("Whitespace values with null Items");
        }
    }
}