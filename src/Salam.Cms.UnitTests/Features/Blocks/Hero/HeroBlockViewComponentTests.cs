namespace Salam.Cms.UnitTests.Features.Blocks.Hero;

using EPiServer;
using EPiServer.Core;
using EPiServer.Web.Routing;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Salam.Cms.Shared.Models.Common.Components;
using Salam.Cms.Shared.Models.Common.Enums;
using Salam.Cms.Shared.Models.Media;
using Salam.Cms.Web.Features.Hero.Components;
using Salam.Cms.Web.Features.Hero.Models;
using Salam.Cms.Web.Features.Hero.ViewModels;

[TestFixture]
public sealed class HeroBlockViewComponentTests
{
    private Mock<IContentLoader> _mockContentLoader;
    private Mock<HeroBlock> _mockHeroBlock;
    private Mock<IImageContent> _mockImageContent;
    private Mock<ILinkModelConverter> _mockLinkModelConverter;
    private PageReference _pageReference;
    private Mock<PageData> _mockPage;
    private Mock<IPageRouteHelper> _mockPageRouteHelper;
    private HeroBlockViewComponent _viewComponent;

    [SetUp]
    public void SetUp()
    {
        _mockContentLoader = new Mock<IContentLoader>();
        _mockHeroBlock = new Mock<HeroBlock>(MockBehavior.Loose);
        _mockHeroBlock.SetupAllProperties();
        _mockImageContent = new Mock<IImageContent>();
        _mockLinkModelConverter = new Mock<ILinkModelConverter>();

        _pageReference = new PageReference(123);
        _mockPage = new Mock<PageData>(MockBehavior.Loose);
        _mockPage.Setup(x => x.ContentLink).Returns(_pageReference);

        _mockPageRouteHelper = new Mock<IPageRouteHelper>();
        _mockPageRouteHelper.Setup(x => x.Page).Returns(_mockPage.Object);
        _mockPageRouteHelper.Setup(x => x.PageLink).Returns(_pageReference);

        _viewComponent = new HeroBlockViewComponent(_mockContentLoader.Object, _mockLinkModelConverter.Object, _mockPageRouteHelper.Object);
    }

    [Test]
    [TestCaseSource(typeof(HeroBlockViewComponentTestCases), nameof(HeroBlockViewComponentTestCases.BuildModelTestCases))]
    public void Invoke_BuildsModel(string heading, string badgeText, XhtmlString description, LayoutOption layout)
    {
        // Arrange
        _mockHeroBlock.Setup(x => x.Heading).Returns(heading);
        _mockHeroBlock.Setup(x => x.BadgeText).Returns(badgeText);
        _mockHeroBlock.Setup(x => x.Description).Returns(description);
        _mockHeroBlock.Setup(x => x.Layout).Returns(layout);

        // Set up media reference
        var mediaReference = new ContentReference(456);
        _mockHeroBlock.Setup(x => x.Media).Returns(mediaReference);

        var imageContent = _mockImageContent.Object;
        _mockImageContent.Setup(x => x.ContentLink).Returns(mediaReference);

        // Ensure TryGet returns true and sets the output parameter
        _mockContentLoader.Setup(x => x.TryGet(It.IsAny<ContentReference>(), out imageContent))
                          .Returns(true);

        // Act
        var result = _viewComponent.Invoke(_mockHeroBlock.Object) as ViewViewComponentResult;
        var model = result?.ViewData?.Model as HeroBlockViewModel;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(model, Is.Not.Null);
            Assert.That(model.CurrentBlock, Is.EqualTo(_mockHeroBlock.Object));
            Assert.That(model.CurrentPage, Is.EqualTo(_mockPage.Object));
        });
    }
}