namespace Salam.Cms.UnitTests.Features.Components.Hero;

using EPiServer.Core;

using Microsoft.AspNetCore.Mvc.ViewComponents;
using Salam.Cms.Web.Features.Hero.Components;

[TestFixture]
public sealed class HeroViewComponentTests
{
    private Mock<PageData> _mockSitePageData;

    private Mock<ContentArea> _mockContentArea;

    private HeroViewComponent _viewComponent;

    [SetUp]
    public void SetUp()
    {
        _mockSitePageData = new Mock<PageData>();

        _mockContentArea = new Mock<ContentArea>(MockBehavior.Loose);

        _viewComponent = new HeroViewComponent();
    }

    [Test]
    public void Invoke_WhenGivenANullSitePageReturnsAnEmptyContentResult()
    {
        // Act
        var response = _viewComponent.Invoke(default) as ContentViewComponentResult;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(response, Is.Not.Null);
            Assert.That(response.Content, Is.EqualTo(string.Empty));
        });
    }
}
