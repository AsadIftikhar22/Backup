namespace Salam.Cms.Tests.Extensions;

using Salam.Cms.Shared.Models.Extensions;
using System.Collections.Generic;

[TestFixture]
public sealed class ListExtensionsTests
{
    [Test]
    [TestCaseSource(typeof(ListExtensionsTestCases), nameof(ListExtensionsTestCases.NullOrEmptyTestCases))]
    public void IsNullOrEmpty_ReturnsTrueForANullCollection(List<string> listUnderTest, bool expectedValue)
    {
        Assert.That(listUnderTest.IsNullOrEmpty(), Is.EqualTo(expectedValue));
    }
}