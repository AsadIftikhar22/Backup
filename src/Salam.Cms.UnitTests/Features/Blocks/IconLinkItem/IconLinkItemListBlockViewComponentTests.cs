namespace Salam.Cms.UnitTests.Features.Blocks.IconLinkItem;

using EPiServer;
using EPiServer.Core;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Moq;
using NUnit.Framework;
using Salam.Cms.Tests.Common;
using Salam.Cms.Web.Features.IconLinks.Components;
using Salam.Cms.Web.Features.IconLinks.Models;
using Salam.Cms.Web.Features.IconLinks.ViewModels;

[TestFixture]
public class IconLinkItemListBlockViewComponentTests
{
    private Mock<IContentLoader> _mockContentLoader;
    private Mock<IconLinkItemListBlock> _mockBlock;
    private IconLinkItemListBlockViewComponent _component;

    [SetUp]
    public void Setup()
    {
        _mockBlock = new Mock<IconLinkItemListBlock>();
        _mockContentLoader = new Mock<IContentLoader>();
        _component = new IconLinkItemListBlockViewComponent(_mockContentLoader.Object);
    }

    [Test]
    public void Invoke_ShouldReturnViewWithViewModel()
    {
        // Arrange
        _mockBlock.Setup(x => x.Heading).Returns("Test Heading");
        _mockBlock.Setup(x => x.Description).Returns(new XhtmlString("<p>Test Description</p>"));
        _mockBlock.Setup(x => x.Items).Returns((ContentArea)null);

        // Act
        var result = _component.Invoke(_mockBlock.Object) as ViewViewComponentResult;
        var model = result?.ViewData?.Model as IconLinkItemListBlockViewModel;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(model, Is.Not.Null);
            Assert.That(model.CurrentBlock, Is.EqualTo(_mockBlock.Object));
            Assert.That(model.CurrentBlock.Heading, Is.EqualTo("Test Heading"));
            Assert.That(model.CurrentBlock.Description.ToString(), Is.EqualTo("<p>Test Description</p>"));
            Assert.That(model.CurrentBlock.Items, Is.Null);
        });
    }

    [Test]
    public void Invoke_WithContentAreaItems_ShouldReturnViewModelWithItems()
    {
        // Arrange
        var mockIconLinkItem = new Mock<IconLinkItemBlock>(MockBehavior.Loose);
        var contentItem = mockIconLinkItem.Object;

        var contentArea = OptimizelyMockHelper.CreateMockedContentArea(1);

        _mockBlock.Setup(x => x.Heading).Returns("Test Heading");
        _mockBlock.Setup(x => x.Description).Returns(new XhtmlString("<p>Test Description</p>"));
        _mockBlock.Setup(x => x.Items).Returns(contentArea);

        _mockContentLoader.Setup(x => x.TryGet(It.IsAny<ContentReference>(), out contentItem)).Returns(true);

        // Act
        var result = _component.Invoke(_mockBlock.Object) as ViewViewComponentResult;
        var model = result?.ViewData?.Model as IconLinkItemListBlockViewModel;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(model, Is.Not.Null);
            Assert.That(model.CurrentBlock, Is.EqualTo(_mockBlock.Object));
            Assert.That(model.CurrentBlock.Items, Is.Not.Null);
            Assert.That(model.CurrentBlock.Items, Has.Count.EqualTo(1));
        });
    }
}
