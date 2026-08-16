namespace Salam.Cms.Tests.Extensions;

using EPiServer.SpecializedProperties;

public static class LinkItemCollectionExtensionsTestCases
{
    public static IEnumerable<TestCaseData> IsNullOrEmptyTestCases
    {
        get
        {
            yield return new TestCaseData(null, true);
            yield return new TestCaseData(new LinkItemCollection(), true);
            yield return new TestCaseData(new LinkItemCollection(new List<LinkItem> { new() }), false);
        }
    }
}