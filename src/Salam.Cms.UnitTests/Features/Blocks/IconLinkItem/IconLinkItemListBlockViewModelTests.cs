namespace Salam.Cms.UnitTests.Features.Blocks.IconLinkItem;

using EPiServer.Core;
using Salam.Cms.Web.Features.IconLinks.Models;
using Salam.Cms.Web.Features.IconLinks.ViewModels;

[TestFixture]
public class IconLinkItemListBlockViewModelTests
{
    [Test]
    public void Constructor_ShouldSetCurrentBlock()
    {
        // Arrange
        var mockBlock = new Mock<IconLinkItemListBlock>();
        mockBlock.Setup(x => x.Heading).Returns("Test Heading");
        mockBlock.Setup(x => x.Description).Returns(new XhtmlString("<p>Test Description</p>"));
        mockBlock.Setup(x => x.Items).Returns(new ContentArea());

        // Act
        var viewModel = new IconLinkItemListBlockViewModel(mockBlock.Object);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(viewModel.CurrentBlock, Is.Not.Null);
            Assert.That(viewModel.CurrentBlock, Is.EqualTo(mockBlock.Object));
            Assert.That(viewModel.CurrentBlock.Heading, Is.EqualTo("Test Heading"));
            Assert.That(viewModel.CurrentBlock.Description.ToString(), Is.EqualTo("<p>Test Description</p>"));
            Assert.That(viewModel.CurrentBlock.Items, Is.Not.Null);
        });
    }
}