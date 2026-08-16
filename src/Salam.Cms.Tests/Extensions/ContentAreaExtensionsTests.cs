namespace Salam.Cms.Tests.Extensions;

using EPiServer.Core;
using Salam.Cms.Shared.Models.Extensions;
using System.Collections.Generic;

[TestFixture]
public sealed class ContentAreaExtensionsTests
{
    [Test]
    [TestCaseSource(typeof(ContentAreaExtensionsTestCases), nameof(ContentAreaExtensionsTestCases.NullOrEmptyContentAreaTestCases))]
    public void GetAllowedReferences_ReturnsAnEmptyCollectionForNullOrEmptyContentAreas(ContentArea contentArea)
    {
        // Act
        var allowedReferences = contentArea.GetAllowedReferences();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(allowedReferences, Is.Not.Null);
            Assert.That(allowedReferences, Is.Empty);
        });
    }

    [Test]
    public void GetAllowedReferences_ReturnsFilteredItemsWhenProductalizationIsInUse()
    {
        // Act
        var contentReferenceOne = new ContentReference(1);
        var contentReferenceTwo = new ContentReference(2);
        var contentReferenceThree = new ContentReference(3);

        var allItems = new List<ContentAreaItem>
        {
            new() { ContentLink = contentReferenceOne },
            new() { ContentLink = contentReferenceTwo },
            new() { ContentLink = contentReferenceThree }
        };

        var filteredItems = new List<ContentAreaItem>
        {
            new() { ContentLink = contentReferenceTwo },
            new() { ContentLink = contentReferenceThree }
        };

        var contentArea = new Mock<ContentArea>(MockBehavior.Loose);
        contentArea.Setup(x => x.Items).Returns(allItems);
        contentArea.Setup(x => x.FilteredItems).Returns(filteredItems);

        var allowedReferences = contentArea.Object.GetAllowedReferences();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(allowedReferences, Is.Not.Null);
            Assert.That(allowedReferences, Has.Count.EqualTo(2));
        });
    }

    [Test]
    public void GetAllowedReferences_ReturnsItemsWhenProductalizationIsNotInUse()
    {
        // Act
        var contentReferenceOne = new ContentReference(1);
        var contentReferenceTwo = new ContentReference(2);
        var contentReferenceThree = new ContentReference(3);

        var allItems = new List<ContentAreaItem>
        {
            new() { ContentLink = contentReferenceOne },
            new() { ContentLink = contentReferenceTwo },
            new() { ContentLink = contentReferenceThree }
        };

        var contentArea = new Mock<ContentArea>(MockBehavior.Loose);
        contentArea.Setup(x => x.Items).Returns(allItems);
        contentArea.Setup(x => x.FilteredItems).Returns((List<ContentAreaItem>)null);

        var allowedReferences = contentArea.Object.GetAllowedReferences();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(allowedReferences, Is.Not.Null);
            Assert.That(allowedReferences, Has.Count.EqualTo(3));
        });
    }

    [Test]
    [TestCaseSource(typeof(ContentAreaExtensionsTestCases), nameof(ContentAreaExtensionsTestCases.NullOrEmptyContentAreaTestCases))]
    public void IsNullOrEmpty_ReturnsTrueWhenContentAreaIsNullOrEmpty(ContentArea contentArea)
    {
        // Act
        var isNullOrEmpty = contentArea.IsNullOrEmpty();

        // Assert
        Assert.That(isNullOrEmpty, Is.True);
    }

    [Test]
    public void IsNullOrEmpty_ReturnsFalseWhenContentAreaIsItems()
    {
        // Arrange
        var mockContentArea = new Mock<ContentArea>();
        mockContentArea.Setup(x => x.Items).Returns(new List<ContentAreaItem> { new() });

        var contentArea = mockContentArea.Object;

        // Act
        var isNullOrEmpty = contentArea.IsNullOrEmpty();

        // Assert
        Assert.That(isNullOrEmpty, Is.False);
    }
}
