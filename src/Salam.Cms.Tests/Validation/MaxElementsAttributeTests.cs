namespace Salam.Cms.Tests.Validation;

using EPiServer.Core;
using EPiServer.SpecializedProperties;
using Salam.Cms.Shared.Models.Validation;

[TestFixture]
public class MaxElementsAttributeTests
{
    [Test]
    [TestCase(5, 4, true)]
    [TestCase(10, 10, true)]
    [TestCase(20, 21, false)]
    public void IsValid_CorrectlyReturnsAValidationErrorWhenAContentAreaHasTooManyItems(
        int maxItems,
        int numberOfItems,
        bool shouldBeValid)
    {
        // Arrange
        var contentAreaItems = Enumerable.Range(1, numberOfItems)
                                         .Select(x => new ContentAreaItem { ContentLink = new ContentReference(x) })
                                         .ToList();
        var contentArea = new Mock<ContentArea>(MockBehavior.Loose);
        contentArea.Setup(x => x.Items).Returns(contentAreaItems);

        // Act
        var maxElementsAttribute = new MaxElementsAttribute(maxItems);
        var result = maxElementsAttribute.IsValid(contentArea.Object);

        // Assert
        Assert.That(result, Is.EqualTo(shouldBeValid));
    }

    [Test]
    [TestCase(5, 4, true)]
    [TestCase(10, 10, true)]
    [TestCase(20, 21, false)]
    public void IsValid_CorrectlyReturnsAValidationErrorWhenAListHasTooManyItems(
        int maxItems,
        int numberOfItems,
        bool shouldBeValid)
    {
        // Arrange
        IList<int> collectionToTest = Enumerable.Range(1, numberOfItems).ToList();

        // Act
        var maxElementsAttribute = new MaxElementsAttribute(maxItems);
        var result = maxElementsAttribute.IsValid(collectionToTest);

        // Assert
        Assert.That(result, Is.EqualTo(shouldBeValid));
    }

    [Test]
    [TestCase(5, 4, true)]
    [TestCase(10, 10, true)]
    [TestCase(20, 21, false)]
    public void IsValid_CorrectlyReturnsAValidationErrorWhenALinkItemCollectionHasTooManyItems(
        int maxItems,
        int numberOfItems,
        bool shouldBeValid)
    {
        // Arrange
        var linkItems = Enumerable.Range(1, numberOfItems)
                                  .Select(x => new LinkItem { Href = $"https://www.example.com/{x}", Text = x.ToString() })
                                  .ToList();
        var linkItemCollection = new LinkItemCollection(linkItems);

        // Act
        var maxElementsAttribute = new MaxElementsAttribute(maxItems);
        var result = maxElementsAttribute.IsValid(linkItemCollection);

        // Assert
        Assert.That(result, Is.EqualTo(shouldBeValid));
    }

    [Test]
    public void IsValid_ReturnsATrueForANonCollectionProperty()
    {
        // Arrange
        const string notACollection = "Not a Collection";

        // Act
        var maxElementsAttribute = new MaxElementsAttribute(10);
        var result = maxElementsAttribute.IsValid(notACollection);

        // Assert
        Assert.That(result, Is.True);
    }
}