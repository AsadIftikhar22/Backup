namespace Salam.Cms.Tests.Extensions;

using EPiServer;
using EPiServer.Core;
using EPiServer.Web.Routing;
using Microsoft.Extensions.Primitives;
using Salam.Cms.Shared.Models.Extensions;
using Salam.Cms.Shared.Models.Media;
using Salam.Cms.Tests.Common;

[TestFixture]
public class UrlResolverExtensionsTests
{
    private Mock<IUrlResolver> _mockUrlResolver;

    [SetUp]
    public void SetUp()
    {
        _mockUrlResolver = new Mock<IUrlResolver>();
        _mockUrlResolver.Setup(x => x.GetUrl(It.IsAny<UrlBuilder>(), It.IsAny<UrlResolverArguments>()))
                        .Returns<UrlBuilder, UrlResolverArguments>((a, b) => a.ToString());
    }

    [Test]
    [TestCaseSource(typeof(CommonTestCases), nameof(CommonTestCases.NullEmptyOrWhitespaceStringTestCases))]
    public void ContentUrl_WhenPassedANullEmptyOrWhitespaceStringThenAnEmptyStringIsReturned(string url)
    {
        // Act
        var formattedUrl = _mockUrlResolver.Object.ContentUrl(url);

        // Assert
        Assert.That(formattedUrl, Is.EqualTo(string.Empty));
    }

    [Test]
    [TestCase("https://www.example.com/", "https://www.example.com/")]
    [TestCase("mailto:bob@example.com", "mailto:bob@example.com")]
    public void ContentUrl_WhenPassedAValidUrlStringThenAProperlyFormattedUrlShouldBeReturned(string providedUrl, string expectedUrl)
    {
        // Act
        var formattedUrl = _mockUrlResolver.Object.ContentUrl(providedUrl);

        // Assert
        Assert.That(formattedUrl, Is.EqualTo(expectedUrl));
    }

    [Test]
    [TestCaseSource(typeof(UrlResolverExtensionsTestCases), nameof(UrlResolverExtensionsTestCases.NullOrEmptyUrlTestCases))]
    public void ContentUrl_WhenPassedANullEmptyUrlThenAnEmptyStringIsReturned(Url url)
    {
        // Act
        var formattedUrl = _mockUrlResolver.Object.ContentUrl(url);

        // Assert
        Assert.That(formattedUrl, Is.EqualTo(string.Empty));
    }

    [Test]
    [TestCaseSource(typeof(UrlResolverExtensionsTestCases), nameof(UrlResolverExtensionsTestCases.ValidUrlTestCases))]
    public void ContentUrl_WhenPassedAValidUrlThenAnEmptyStringIsReturned(Url url, string expectedUrl)
    {
        // Act
        var formattedUrl = _mockUrlResolver.Object.ContentUrl(url);

        // Assert
        Assert.That(formattedUrl, Is.EqualTo(expectedUrl));
    }

    [Test]
    [TestCaseSource(typeof(UrlResolverExtensionsTestCases), nameof(UrlResolverExtensionsTestCases.ValidUrlTestCases))]
    public void ContentUrl_WhenUrlResolverCannotGetTheUrlThenThePassedUrlShouldBeReturned(Url url, string expectedUrl)
    {
        // Arrange
        _mockUrlResolver.Setup(x => x.GetUrl(It.IsAny<UrlBuilder>(), It.IsAny<UrlResolverArguments>()))
                        .Returns((string)null);

        // Act
        var formattedUrl = _mockUrlResolver.Object.ContentUrl(url);

        // Assert
        Assert.That(formattedUrl, Is.EqualTo(expectedUrl));
    }

    [Test]
    [TestCaseSource(typeof(UrlResolverExtensionsTestCases), nameof(UrlResolverExtensionsTestCases.ContentUrlWithQueryTestCases))]
    public void ContentUrlWithQuery_OnlyAppendsSupportedValuesWhenKeyAndValueIsProvided(
        ContentReference contentReference,
        string queryStringKey,
        object queryStringValue,
        string expectedUrl,
        Dictionary<string, StringValues> existingParameters = null)
    {
        // Arrange
        _mockUrlResolver.Setup(x => x.GetUrl(It.IsAny<ContentReference>(), It.IsAny<string>(), It.IsAny<UrlResolverArguments>()))
                        .Returns("https://www.example.com/");

        // Act
        var formattedUrl = _mockUrlResolver.Object.ContentUrlWithQuery(contentReference, queryStringKey, queryStringValue, existingParameters);

        // Assert
        Assert.That(formattedUrl, Is.EqualTo(expectedUrl));
    }

    [Test]
    public void ImageUrl_WhenGivenANullImageThenAnEmptyStringWillBeReturned()
    {
        // Act
        var url = _mockUrlResolver.Object.ImageUrl(null, 500, 500, 80);

        // Assert
        Assert.That(url, Is.EqualTo(string.Empty));
    }

    [Test]
    [TestCase("0.5|0.5", 500, 300, 95, "?width=500&height=300&quality=95&rxy=0.5%2c0.5&rmode=crop")]
    [TestCase("0.6|0.7", 789, null, 73, "?width=789&quality=73&rxy=0.6%2c0.7")]
    [TestCase("0.2|0.1", null, 456, 62, "?height=456&quality=62&rxy=0.2%2c0.1")]
    [TestCase("", 500, 300, 95, "?width=500&height=300&quality=95&rmode=crop")]
    [TestCase(null, 500, 300, 95, "?width=500&height=300&quality=95&rmode=crop")]
    [TestCase("0.65|0.25", 123, 234, null, "?width=123&height=234&rxy=0.65%2c0.25&rmode=crop")]
    public void ImageUrl_WhenGivenAnImageContentInstanceThenAnTheUrlWillContainResizingInformation(
        string focalPoint,
        int? width,
        int? height,
        int? quality,
        string expectedQuery)
    {
        // Act
        var imageContent = new Mock<ImageContent>(MockBehavior.Loose);
        imageContent.Setup(x => x.ContentLink).Returns(new ContentReference(123));
        imageContent.Setup(x => x.ImageFocalPoint).Returns(focalPoint);

        _mockUrlResolver.Setup(x => x.GetUrl(It.IsAny<ContentReference>(), It.IsAny<string>(), It.IsAny<UrlResolverArguments>()))
                        .Returns("https://www.salam.sa/some-image.jpg");

        // Act
        var url = _mockUrlResolver.Object.ImageUrl(imageContent.Object, width, height, quality);

        // Assert
        Assert.That(url, Does.Contain(expectedQuery));
    }

    [Test]
    public void ImageUrl_WhenGivenAVectorImageContentInstanceThenAnTheUrlWillNotContainResizingInformation()
    {
        // Act
        var vectorImageContent = new Mock<VectorImageContent>(MockBehavior.Loose);
        vectorImageContent.Setup(x => x.ContentLink).Returns(new ContentReference(123));

        _mockUrlResolver.Setup(x => x.GetUrl(It.IsAny<ContentReference>(), It.IsAny<string>(), It.IsAny<UrlResolverArguments>()))
                        .Returns("https://www.salam.sa/some-image.jpg");

        // Act
        var url = _mockUrlResolver.Object.ImageUrl(vectorImageContent.Object, 500, 300, 95);

        // Assert
        Assert.That(url, Does.Not.Contain('?'));
    }

    [Test]
    public void ImageUrl_WhenGivenANeitherAnImageContentOrVectorImageContentThenAnEmptyStringWillBeReturned()
    {
        // Act
        var image = new Mock<IImageContent>(MockBehavior.Loose);
        image.Setup(x => x.ContentLink).Returns(new ContentReference(123));

        _mockUrlResolver.Setup(x => x.GetUrl(It.IsAny<ContentReference>(), It.IsAny<string>(), It.IsAny<UrlResolverArguments>()))
                        .Returns("https://www.salam.sa/some-image.jpg");

        // Act
        var url = _mockUrlResolver.Object.ImageUrl(image.Object, 500, 300, 95);

        // Assert
        Assert.That(url, Is.EqualTo(string.Empty));
    }
}