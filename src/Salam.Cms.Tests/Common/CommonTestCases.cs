namespace Salam.Cms.Tests.Common;

using EPiServer.Core;
using System.Collections.Generic;

public static class CommonTestCases
{
    public static IEnumerable<TestCaseData> NullEmptyOrWhitespaceStringTestCases
    {
        get
        {
            yield return new TestCaseData(null);
            yield return new TestCaseData(string.Empty);
            yield return new TestCaseData(" ");
        }
    }

    public static IEnumerable<TestCaseData> NullOrEmptyContentReferenceTestCases
    {
        get
        {
            yield return new TestCaseData(null);
            yield return new TestCaseData(ContentReference.EmptyReference);
        }
    }

    public static IEnumerable<TestCaseData> VariantContentReferenceTestCases
    {
        get
        {
            yield return new TestCaseData(null);
            yield return new TestCaseData(ContentReference.EmptyReference);
            yield return new TestCaseData(new ContentReference(1));
        }
    }
}
