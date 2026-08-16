namespace Salam.Cms.Tests.Extensions;

using EPiServer.DataAbstraction;
using Salam.Cms.Shared.Models.Extensions;

[TestFixture]
public sealed class ContentTypeRepositoryExtensionsTests
{
    private Mock<IContentTypeRepository> _mockRepository;

    private Mock<ContentType> _mockContentType;

    [SetUp]
    public void SetUp()
    {
        _mockRepository = new Mock<IContentTypeRepository>();

        _mockContentType = new Mock<ContentType>(MockBehavior.Loose);
    }

    [Test]
    public void TryGet_ReturnsFalseAndANullContentTypeWhenPassedANullId()
    {
        // Act
        var isSuccess = _mockRepository.Object.TryGet(null, out var contentType);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(isSuccess, Is.False);
            Assert.That(contentType, Is.Null);
        });
    }

    [Test]
    public void TryGet_ReturnsFalseWhenGivenAValidIdThatDoesNotMatchAContentType()
    {
        // Arrange
        _mockRepository.Setup(x => x.Load(It.IsAny<int>())).Returns((ContentType)null);

        // Act
        var isSuccess = _mockRepository.Object.TryGet(1, out var contentType);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(isSuccess, Is.False);
            Assert.That(contentType, Is.Null);
        });
    }

    [Test]
    public void TryGet_ReturnsTrueAndTheContentTypeWhenGivenAValidId()
    {
        // Arrange
        _mockRepository.Setup(x => x.Load(It.IsAny<int>()))
                       .Returns(_mockContentType.Object);

        // Act
        var isSuccess = _mockRepository.Object.TryGet(1, out var contentType);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(isSuccess, Is.True);
            Assert.That(contentType, Is.Not.Null);
        });
    }
}
