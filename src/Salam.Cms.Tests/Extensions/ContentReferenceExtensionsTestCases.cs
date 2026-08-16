namespace Salam.Cms.Tests.Extensions;

using EPiServer.Core;
using System.Collections.Generic;

public static class ContentReferenceExtensionsTestCases
{
    public static IEnumerable<TestCaseData> IsNullOrEmptyTestCases
    {
        get
        {
            yield return new TestCaseData(null, true);
            yield return new TestCaseData(ContentReference.EmptyReference, true);
            yield return new TestCaseData(new ContentReference(1), false);
        }
    }
}