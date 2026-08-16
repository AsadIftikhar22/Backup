namespace Salam.Cms.Tests.Extensions;

using EPiServer;

public static class UrlExtensionsTestCases
{
    public static IEnumerable<TestCaseData> IsNullOrEmptyTestCases
    {
        get
        {
            yield return new TestCaseData(null, true);
            yield return new TestCaseData(new Url(string.Empty), true);
            yield return new TestCaseData(new Url("https://www.example.com"), false);
        }
    }
}