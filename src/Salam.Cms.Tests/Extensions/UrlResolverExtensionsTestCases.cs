namespace Salam.Cms.Tests.Extensions;

using EPiServer;
using EPiServer.Core;
using Microsoft.Extensions.Primitives;

public static class UrlResolverExtensionsTestCases
{
    static Dictionary<string, StringValues> existingParameters = new Dictionary<string, StringValues>
        {
            { "term", new StringValues("insurance") },
        };
    public static IEnumerable<TestCaseData> NullOrEmptyUrlTestCases
    {
        get
        {
            yield return new TestCaseData(args: null);
            yield return new TestCaseData(new Url(""));
        }
    }

    public static IEnumerable<TestCaseData> ValidUrlTestCases
    {
        get
        {
            yield return new TestCaseData(new Url("my-page/index"), "my-page/index");
            yield return new TestCaseData(new Url("https://www.example.com"), "https://www.example.com/");
            yield return new TestCaseData(new Url("mailto:bob@example.com"), "mailto:bob@example.com");
        }
    }

    public static IEnumerable<TestCaseData> ContentUrlWithQueryTestCases
    {

        get
        {
            yield return new TestCaseData(new ContentReference(1), " ", new ContentReference(100), "https://www.example.com/", null);
            yield return new TestCaseData(new ContentReference(1), "", new ContentReference(100), "https://www.example.com/", null);
            yield return new TestCaseData(new ContentReference(1), null, new ContentReference(100), "https://www.example.com/", null);
            yield return new TestCaseData(new ContentReference(1), "string", "a-string", "https://www.example.com/?string=a-string", null);
            yield return new TestCaseData(new ContentReference(1), "context", "cars", "https://www.example.com/?term=insurance&context=cars", existingParameters);
            yield return new TestCaseData(new ContentReference(1), "integer", 1, "https://www.example.com/?integer=1", null);
            yield return new TestCaseData(new ContentReference(1), "content", new ContentReference(123), "https://www.example.com/?content=123", null);
            yield return new TestCaseData(new ContentReference(1), "unsupported", 1.25M, "https://www.example.com/", null);
        }
    }
}