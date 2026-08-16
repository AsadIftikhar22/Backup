namespace Salam.Cms.UnitTests.Features.Blocks.Accordion;

using EPiServer;
using EPiServer.Core;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Salam.Cms.Tests.Common;
using Salam.Cms.Web.Features.Accordion.Components;
using Salam.Cms.Web.Features.Accordion.Models;
using Salam.Cms.Web.Features.Accordion.ViewModels;

[TestFixture]
public class AccordionBlockViewComponentTests
{
    private Mock<IContentLoader> _mockContentLoader;
    private Mock<AccordionBlock> _mockBlock;
    private AccordionBlockViewComponent _component;

    [SetUp]
    public void Setup()
    {
        _mockBlock = new Mock<AccordionBlock>();
        _mockContentLoader = new Mock<IContentLoader>();
        _component = new AccordionBlockViewComponent(_mockContentLoader.Object);
    }

    [Test]
    public void Invoke_ShouldReturnEmptyContentNoItems()
    {
        // Arrange
        _mockBlock.Setup(x => x.Items).Returns((ContentArea)null);

        // Act
        var result = _component.Invoke(_mockBlock.Object) as ViewViewComponentResult;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            var model = result.ViewData.Model as AccordionBlockViewModel;
            Assert.That(model, Is.Not.Null);
            Assert.That(model.CurrentBlock.Items, Is.Null);
        });
    }

    [Test]
    public void Invoke_CorrectlyBuildsTheModelAndReturnsItWithAViewResponse()
    {
        // Arrange         
        var mockedAccordionItem = new Mock<AccordionItemBlock>(MockBehavior.Loose);
        mockedAccordionItem.Setup(x => x.Heading).Returns("A heading.");
        mockedAccordionItem.Setup(x => x.Description).Returns(new XhtmlString("Some content"));
        var item = mockedAccordionItem.Object;

        var mockedItemsContentArea = OptimizelyMockHelper.CreateMockedContentArea(1);

        _mockBlock.Setup(x => x.Items).Returns(mockedItemsContentArea);
        _mockContentLoader.Setup(x => x.TryGet(It.IsAny<ContentReference>(), out item)).Returns(true);

        // Act
        var result = _component.Invoke(_mockBlock.Object) as ViewViewComponentResult;
        var model = result?.ViewData?.Model as AccordionBlockViewModel;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(model, Is.Not.Null);
            Assert.That(model.CurrentBlock.Items, Is.Not.Null);
            Assert.That(model.CurrentBlock.Items, Has.Count.EqualTo(1));
        });
    }
}
