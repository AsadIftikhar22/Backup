namespace Salam.Cms.UnitTests.Features.Blocks.IconLinkItem;

using EPiServer.Core;

public static class IconLinkItemBlockTestCases
{
    public static IEnumerable<TestCaseData> CanSetPropertiesTestCases
    {
        get
        {
            yield return new TestCaseData(ContentReference.EmptyReference, "", "").SetName("Empty strings");
            yield return new TestCaseData(new ContentReference(100), "http://example.com", "Example").SetName("Valid properties");
            yield return new TestCaseData(ContentReference.EmptyReference, null, null).SetName("Null link properties");
        }
    }
}