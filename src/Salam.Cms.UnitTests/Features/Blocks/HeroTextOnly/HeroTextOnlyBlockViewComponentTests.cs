namespace Salam.Cms.UnitTests.Features.Blocks.HeroTextOnly;

using EPiServer.Core;
using EPiServer.Web.Routing;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Moq;
using NUnit.Framework;
using Salam.Cms.Web.Features.Hero.Components;
using Salam.Cms.Web.Features.Hero.Models;
using Salam.Cms.Web.Features.Hero.ViewModels;

[TestFixture]
public sealed class HeroTextOnlyBlockViewComponentTests
{
    private Mock<HeroTextOnlyBlock> _mockHeroTextOnlyBlock;
    private Mock<IPageRouteHelper> _mockPageRouteHelper;
    private Mock<PageData> _mockPage;
    private PageReference _pageReference;
    private HeroTextOnlyBlockViewComponent _viewComponent;

    [SetUp]
    public void SetUp()
    {
        _mockHeroTextOnlyBlock = new Mock<HeroTextOnlyBlock>(MockBehavior.Loose);
        _mockPageRouteHelper = new Mock<IPageRouteHelper>();

        _mockPage = new Mock<PageData>(MockBehavior.Loose);
        _pageReference = new PageReference(123);
        _mockPage.Setup(x => x.ContentLink).Returns(_pageReference);

        _mockPageRouteHelper.Setup(x => x.Page).Returns(_mockPage.Object);
        _mockPageRouteHelper.Setup(x => x.PageLink).Returns(_pageReference);

        _viewComponent = new HeroTextOnlyBlockViewComponent(_mockPageRouteHelper.Object);
    }

    [Test]
    [TestCaseSource(typeof(HeroTextOnlyBlockViewComponentTestCases), nameof(HeroTextOnlyBlockViewComponentTestCases.BuildModelTestCases))]
    public void Invoke_BuildsModel(string heading, XhtmlString description)
    {
        // Arrange
        _mockHeroTextOnlyBlock.Setup(x => x.Heading).Returns(heading);
        _mockHeroTextOnlyBlock.Setup(x => x.Description).Returns(description);

        // Act
        var result = _viewComponent.Invoke(_mockHeroTextOnlyBlock.Object) as ViewViewComponentResult;
        var model = result?.ViewData?.Model as HeroTextOnlyBlockViewModel;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(model, Is.Not.Null);
            Assert.That(model.CurrentBlock.Heading, Is.EqualTo(heading));
            Assert.That(model.CurrentBlock.Description, Is.EqualTo(description));
            Assert.That(model.CurrentPage, Is.EqualTo(_mockPage.Object));
        });
    }
}
