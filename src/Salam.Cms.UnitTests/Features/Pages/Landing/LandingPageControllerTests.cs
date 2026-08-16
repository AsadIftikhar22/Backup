namespace Salam.Cms.UnitTests.Features.Pages.Landing;

using Microsoft.AspNetCore.Mvc;
using Salam.Cms.Web.Features.Landing.Controllers;
using Salam.Cms.Web.Features.Landing.Models;
using Salam.Cms.Web.Features.Landing.ViewModels;

[TestFixture]
public class LandingPageControllerTests
{
    private Mock<LandingPage> _mockPage;

    private LandingPageController _controller;

    [SetUp]
    public void SetUp()
    {
        _mockPage = new Mock<LandingPage>(MockBehavior.Loose);

        _controller = new LandingPageController();
    }

    [Test]
    public void Index_ReturnsAViewModelContainingTheContent()
    {
        // Act
        var result = _controller.Index(_mockPage.Object) as ViewResult;
        var model = result?.Model as LandingPageViewModel;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(model, Is.Not.Null);
            Assert.That(model.CurrentPage, Is.EqualTo(_mockPage.Object));
        });
    }

    [TearDown]
    public void TearDown()
    {
        _controller.Dispose();
        _controller = null;
    }
}
