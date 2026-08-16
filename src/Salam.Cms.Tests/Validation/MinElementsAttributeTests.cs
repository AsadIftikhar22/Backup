namespace Salam.Cms.Tests.Validation;

using EPiServer.Core;
using EPiServer.SpecializedProperties;
using Salam.Cms.Shared.Models.Validation;

[TestFixture]
public class MinElementsAttributeTests
{
    [Test]
    [TestCase(1, 0, false)]
    [TestCase(10, 10, true)]
    [TestCase(20, 21, true)]
    public void IsValid_CorrectlyReturnsAValidationErrorWhenAContentAreaHasTooFewItems(
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
        var minElementsAttribute = new MinElementsAttribute(maxItems);
        var result = minElementsAttribute.IsValid(contentArea.Object);

        // Assert
        Assert.That(result, Is.EqualTo(shouldBeValid));
    }

    [Test]
    [TestCase(1, 0, false)]
    [TestCase(10, 10, true)]
    [TestCase(20, 21, true)]
    public void IsValid_CorrectlyReturnsAValidationErrorWhenAListHasTooFewItems(
        int maxItems,
        int numberOfItems,
        bool shouldBeValid)
    {
        // Arrange
        IList<int> collectionToTest = Enumerable.Range(1, numberOfItems).ToList();

        // Act
        var minElementsAttribute = new MinElementsAttribute(maxItems);
        var result = minElementsAttribute.IsValid(collectionToTest);

        // Assert
        Assert.That(result, Is.EqualTo(shouldBeValid));
    }

    [Test]
    [TestCase(1, 0, false)]
    [TestCase(10, 10, true)]
    [TestCase(20, 21, true)]
    public void IsValid_CorrectlyReturnsAValidationErrorWhenALinkItemCollectionHasTooFewItems(
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
        var minElementsAttribute = new MinElementsAttribute(maxItems);
        var result = minElementsAttribute.IsValid(linkItemCollection);

        // Assert
        Assert.That(result, Is.EqualTo(shouldBeValid));
    }

    [Test]
    public void IsValid_CorrectlyReturnsAValidationErrorWhenPassedANullValue()
    {
        // Act
        var minElementsAttribute = new MinElementsAttribute(10);
        var result = minElementsAttribute.IsValid(null);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsValid_ReturnsATrueForANonCollectionProperty()
    {
        // Arrange
        const string notACollection = "Not a Collection";

        // Act
        var minElementsAttribute = new MinElementsAttribute(10);
        var result = minElementsAttribute.IsValid(notACollection);

        // Assert
        Assert.That(result, Is.True);
    }
}
