namespace Salam.Cms.Tests.Extensions;

using EPiServer;
using EPiServer.Core;
using EPiServer.ServiceLocation;
using Salam.Cms.Shared.Models.Extensions;
using Salam.Cms.Shared.Models.Media;
using Salam.Cms.Tests.Common;

[TestFixture]
public sealed class ContentReferenceExtensionsTests
{
    private Mock<IServiceProvider> _mockIServiceProvider;

    private Mock<IContentLoader> _mockContentLoader;

    [SetUp]
    public void SetUp()
    {
        _mockContentLoader = new Mock<IContentLoader>();

        _mockIServiceProvider = new Mock<IServiceProvider>();
        _mockIServiceProvider.Setup(x => x.GetService(typeof(IContentLoader))).Returns(_mockContentLoader.Object);

        ServiceLocator.SetServiceProvider(_mockIServiceProvider.Object);
    }

    [Test]
    [TestCaseSource(typeof(ContentReferenceExtensionsTestCases), nameof(ContentReferenceExtensionsTestCases.IsNullOrEmptyTestCases))]
    public void IsNullOrEmpty_CorrectlyValidatesContentReferences(
        ContentReference contentReference,
        bool expectedValue)
    {
        // Act
        var isNullOrEmpty = contentReference.IsNullOrEmpty();

        // Assert
        Assert.That(isNullOrEmpty, Is.EqualTo(expectedValue));
    }

    [Test]
    [TestCaseSource(typeof(CommonTestCases), nameof(CommonTestCases.NullOrEmptyContentReferenceTestCases))]
    public void GetContent_ReturnsNullGivenAnInvalidContentReference(ContentReference contentReference)
    {
        // Act
        var content = contentReference.GetContent<PageData>();

        // Assert
        Assert.That(content, Is.Null);
    }

    [Test]
    public void GetContent_ReturnsNullWhenGivenAnValidContentReferenceThatCannotBeLoaded()
    {
        // Arrange
        var contentReference = new ContentReference(1);
        var mockPage = new Mock<PageData>(MockBehavior.Loose);
        var mockedPage = mockPage.Object;

        _mockContentLoader.Setup(x => x.TryGet(It.IsAny<ContentReference>(), out mockedPage)).Returns(false);

        // Act
        var content = contentReference.GetContent<PageData>();

        // Assert
        Assert.That(content, Is.Null);
    }

    [Test]
    public void GetContent_ReturnsPageDataWhenGivenAnValidContentReferenceThatCanBeLoaded()
    {
        // Arrange
        var contentReference = new ContentReference(1);
        var mockPage = new Mock<PageData>(MockBehavior.Loose);
        var mockedPage = mockPage.Object;

        _mockContentLoader.Setup(x => x.TryGet(It.IsAny<ContentReference>(), out mockedPage)).Returns(true);

        // Act
        var content = contentReference.GetContent<PageData>();

        // Assert
        Assert.That(content, Is.EqualTo(mockedPage));
    }

    [Test]
    [TestCaseSource(typeof(CommonTestCases), nameof(CommonTestCases.NullOrEmptyContentReferenceTestCases))]
    public void GetImageAltText_ReturnsAnEmptyStringGivenAnInvalidContentReference(ContentReference imageReference)
    {
        // Act
        var result = imageReference.GetImageAltText();

        // Assert
        Assert.That(result, Is.EqualTo(string.Empty));
    }

    [Test]
    public void GetImageAltText_WhenTheImageCannotBeLoadedThenAnEmptyStringIsReturned()
    {
        // Arrange
        var imageReference = new ContentReference(1);

        var mockImage = new Mock<IImageContent>();
        mockImage.Setup(x => x.AltText).Returns("Something about the image");
        var image = mockImage.Object;

        _mockContentLoader.Setup(x => x.TryGet(It.IsAny<ContentReference>(), out image)).Returns(false);

        // Act
        var result = imageReference.GetImageAltText();

        // Assert
        Assert.That(result, Is.EqualTo(string.Empty));
    }

    [Test]
    [TestCase("Something about the image", "Something about the image")]
    [TestCase(" ", "")]
    [TestCase("", "")]
    [TestCase(null, "")]
    public void GetImageAltText_WhenTheImageCanBeLoadedThenTheAltTextDefaultingToAnEmptyStringIsReturned(
        string altText,
        string expectedValue)
    {
        // Arrange
        var imageReference = new ContentReference(1);

        var mockImage = new Mock<IImageContent>();
        mockImage.Setup(x => x.AltText).Returns(altText);
        var image = mockImage.Object;

        _mockContentLoader.Setup(x => x.TryGet(It.IsAny<ContentReference>(), out image)).Returns(true);

        // Act
        var result = imageReference.GetImageAltText();

        // Assert
        Assert.That(result, Is.EqualTo(expectedValue));
    }
}