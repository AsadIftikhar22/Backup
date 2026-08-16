namespace Salam.Cms.UnitTests.Features.Blocks.HeroLanding;

using EPiServer;
using EPiServer.Core;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Moq;
using NUnit.Framework;
using Salam.Cms.Shared.Models.Common.Components;
using Salam.Cms.Shared.Models.Media;
using Salam.Cms.Tests.Common;
using Salam.Cms.Web.Features.Hero.Components;
using Salam.Cms.Web.Features.Hero.Models;
using Salam.Cms.Web.Features.Hero.ViewModels;

[TestFixture]
public sealed class HeroLandingBlockViewComponentTests
{
    private Mock<IContentLoader> _mockContentLoader;

    private Mock<HeroLandingBlock> _mockHeroLandingBlock;

    private Mock<IImageContent> _mockImageContent;

    private Mock<ILinkModelConverter> _mockLinkModelConverter;

    private HeroLandingBlockViewComponent _viewComponent;

    [SetUp]
    public void SetUp()
    {
        _mockContentLoader = new Mock<IContentLoader>();
        _mockHeroLandingBlock = new Mock<HeroLandingBlock>(MockBehavior.Loose);
        _mockImageContent = new Mock<IImageContent>();
        _mockLinkModelConverter = new Mock<ILinkModelConverter>();

        _viewComponent = new HeroLandingBlockViewComponent(_mockContentLoader.Object);
    }

    [Test]
    public void Invoke_BuildsModel()
    {
        // Act
        var result = _viewComponent.Invoke(_mockHeroLandingBlock.Object) as ViewViewComponentResult;
        var model = result?.ViewData;

        // Assert
        Assert.That(model, Is.Not.Null);
    }

    [Test]
    public void BuildModel_LoadsHeroBlocksFromContentArea()
    {
        // Arrange
        var imageContent = _mockImageContent.Object;

        var mockHeroLandingItemsContentArea = OptimizelyMockHelper.CreateMockedContentArea(1);

        var mockHeroLandingItemBlock = new Mock<HeroBlock>(MockBehavior.Loose);
        mockHeroLandingItemBlock.Setup(b => b.Heading).Returns("Heading");
        mockHeroLandingItemBlock.Setup(b => b.BadgeText).Returns("Badge Text");

        var heroBlock = mockHeroLandingItemBlock.Object;

        // This setup is crucial - the component calls Get<HeroBlock> on the ContentLoader
        _mockContentLoader.Setup(x => x.Get<HeroBlock>(It.IsAny<ContentReference>()))
                         .Returns(heroBlock);

        _mockHeroLandingBlock.Setup(x => x.Items).Returns(mockHeroLandingItemsContentArea);

        //  Act
        var result = _viewComponent.Invoke(_mockHeroLandingBlock.Object) as ViewViewComponentResult;
        var model = result?.ViewData?.Model as HeroLandingBlockViewModel;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(model, Is.Not.Null);
            Assert.That(model.CurrentBlock.Items, Has.Count.EqualTo(1));
            Assert.That(model.HeroHeadings, Has.Count.EqualTo(1));
            Assert.That(model.HeroHeadings[0], Is.EqualTo("Badge Text"));
            Assert.That(model.HeroBlocks, Has.Count.EqualTo(1));
            Assert.That(model.HeroBlocks[0], Is.EqualTo(heroBlock));
        });
    }
}
