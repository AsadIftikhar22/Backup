namespace Salam.Cms.Tests.Extensions;

using EPiServer.Core;
using System.Collections.Generic;

public static class ContentAreaExtensionsTestCases
{
    public static IEnumerable<TestCaseData> NullOrEmptyContentAreaTestCases
    {
        get
        {
            yield return new TestCaseData(null);
            yield return new TestCaseData(new ContentArea());
        }
    }
}