namespace Salam.Cms.Tests.Extensions;

using EPiServer.Core;
using System.Collections.Generic;

public static class XHtmlStringExtensionsTestCases
{
    public static IEnumerable<TestCaseData> GetNullOrEmptyRichTextTestCases
    {
        get
        {
            yield return new TestCaseData(null);
            yield return new TestCaseData(new XhtmlString());
        }
    }
}