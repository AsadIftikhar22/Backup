namespace Salam.Cms.Tests.Extensions;

using Castle.Core.Internal;
using EPiServer.SpecializedProperties;

[TestFixture]
public sealed class LinkItemCollectionExtensionsTests
{
    [Test]
    [TestCaseSource(typeof(LinkItemCollectionExtensionsTestCases), nameof(LinkItemCollectionExtensionsTestCases.IsNullOrEmptyTestCases))]
    public void IsNullOrEmpty_CorrectlyValidatesLinkItemCollections(
        LinkItemCollection linkItemCollection,
        bool expectedValue)
    {
        // Act
        var isNullOrEmpty = linkItemCollection.IsNullOrEmpty();

        // Assert
        Assert.That(isNullOrEmpty, Is.EqualTo(expectedValue));
    }
}