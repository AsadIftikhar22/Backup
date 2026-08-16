namespace Salam.Cms.UnitTests.Features.Blocks.CallToAction;

using EPiServer;
using EPiServer.Core;
using EPiServer.SpecializedProperties;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Salam.Cms.Shared.Models.Common.Enums;
using Salam.Cms.Web.Features.CallToAction.Components;
using Salam.Cms.Web.Features.CallToAction.Models;
using Salam.Cms.Web.Features.CallToAction.ViewModels;

[TestFixture]
public class CallToActionBlockViewComponentTests
{
    private Mock<IContentLoader> _mockContentLoader;
    private Mock<CallToActionBlock> _mockBlock;
    private CallToActionBlockViewComponent _component;

    [SetUp]
    public void Setup()
    {
        _mockBlock = new Mock<CallToActionBlock>();
        _mockContentLoader = new Mock<IContentLoader>();
        _component = new CallToActionBlockViewComponent(_mockContentLoader.Object);
    }

    [Test]
    [TestCaseSource(typeof(CallToActionBlockViewComponentTestCases), nameof(CallToActionBlockViewComponentTestCases.BuildModelTestCases))]
    public void Invoke_CorrectlyBuildsTheModelAndReturnsItWithAViewResponse(
        LayoutOption layoutOption,
        ContentReference media,
        string badgeText,
        string headingLineOne,
        string headingLineTwo,
        XhtmlString mainBody,
        LinkItemCollection linkItems)
    {
        // Arrange
        _mockBlock.Setup(x => x.Media).Returns(media);
        _mockBlock.Setup(x => x.BadgeText).Returns(badgeText);
        _mockBlock.Setup(x => x.HeadingLineOne).Returns(headingLineOne);
        _mockBlock.Setup(x => x.HeadingLineTwo).Returns(headingLineTwo);
        _mockBlock.Setup(x => x.MainBody).Returns(mainBody);
        _mockBlock.Setup(x => x.LinkItems).Returns(linkItems);

        // Act
        var result = _component.Invoke(_mockBlock.Object) as ViewViewComponentResult;
        var model = result.ViewData.Model as CallToActionBlockViewModel;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(model, Is.Not.Null);
            Assert.That(model.CurrentBlock.Media, Is.EqualTo(media));
            Assert.That(model.CurrentBlock.BadgeText, Is.EqualTo(badgeText));
            Assert.That(model.CurrentBlock.HeadingLineOne, Is.EqualTo(headingLineOne));
            Assert.That(model.CurrentBlock.HeadingLineTwo, Is.EqualTo(headingLineTwo));
            Assert.That(model.CurrentBlock.MainBody, Is.EqualTo(mainBody));
            Assert.That(model.CurrentBlock.LinkItems, Is.EqualTo(linkItems));
        });
    }
}
