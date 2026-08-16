namespace Salam.Cms.Tests.Extensions;

using System.Collections.Generic;

public static class ListExtensionsTestCases
{
    public static IEnumerable<TestCaseData> NullOrEmptyTestCases
    {
        get
        {
            yield return new TestCaseData(null, true);
            yield return new TestCaseData(new List<string>(0), true);
            yield return new TestCaseData(new List<string> { "Test" }, false);
        }
    }
}