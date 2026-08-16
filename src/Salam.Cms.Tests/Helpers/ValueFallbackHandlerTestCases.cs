namespace Salam.Cms.Tests.Helpers;

using EPiServer.Core;

public static class ValueFallbackHandlerTestCases
{
    public static IEnumerable<TestCaseData> TextTestCases
    {
        get
        {
            yield return new TestCaseData("Value One", "Value Two", "Value One");
            yield return new TestCaseData(" ", "Value Two", "Value Two");
            yield return new TestCaseData("", "Value Two", "Value Two");
            yield return new TestCaseData(null, "Value Two", "Value Two");
            yield return new TestCaseData(null, " ", "");
            yield return new TestCaseData(null, "", "");
            yield return new TestCaseData(null, null, "");
        }
    }

    public static IEnumerable<TestCaseData> ContentReferenceTestCases
    {
        get
        {
            var contentReferenceOne = new ContentReference(1);
            var contentReferenceTwo = new ContentReference(1);

            yield return new TestCaseData(contentReferenceOne, contentReferenceTwo, contentReferenceOne);
            yield return new TestCaseData(contentReferenceOne, ContentReference.EmptyReference, contentReferenceOne);
            yield return new TestCaseData(contentReferenceOne, null, contentReferenceOne);
            yield return new TestCaseData(ContentReference.EmptyReference, contentReferenceTwo, contentReferenceTwo);
            yield return new TestCaseData(null, contentReferenceTwo, contentReferenceTwo);
        }
    }
}