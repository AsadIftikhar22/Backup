namespace Salam.Cms.Tests.Validation;

public static class NotNullOrEmptyAttributeTestCases
{
    public static IEnumerable<TestCaseData> StringTestCases
    {
        get
        {
            yield return new TestCaseData(null, false);
            yield return new TestCaseData("", false);
            yield return new TestCaseData(" ", false);
            yield return new TestCaseData("A Value", true);
        }
    }

    public static IEnumerable<TestCaseData> GuidTestCases
    {
        get
        {
            yield return new TestCaseData(null, false);
            yield return new TestCaseData(Guid.Empty, false);
            yield return new TestCaseData(new Guid("689B81C1-C94B-4470-A8F9-B563EDD1ADDE"), true);
        }
    }

    public static IEnumerable<TestCaseData> OtherObjectTestCases
    {
        get
        {
            yield return new TestCaseData(null, false);
            yield return new TestCaseData(123, true);
            yield return new TestCaseData(123M, true);
            yield return new TestCaseData(new { Name = "Anonymous" }, true);
        }
    }
}