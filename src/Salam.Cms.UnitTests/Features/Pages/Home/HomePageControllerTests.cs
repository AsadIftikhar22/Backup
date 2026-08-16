namespace Salam.Cms.UnitTests.Features.Pages.Home;

using Microsoft.AspNetCore.Mvc;
using Salam.Cms.Web.Features.Home.Controllers;
using Salam.Cms.Web.Features.Home.Models;
using Salam.Cms.Web.Features.Home.ViewModels;

[TestFixture]
public class HomePageControllerTests
{
    private Mock<HomePage> _mockHomePage;
    private HomePageController _controller;

    [SetUp]
    public void SetUp()
    {
        _mockHomePage = new Mock<HomePage>(MockBehavior.Loose);
        _controller = new HomePageController();
    }

    [Test]
    public void Index_ReturnsAViewModelContainingTheContent()
    {
        var result = _controller.Index(_mockHomePage.Object) as ViewResult;
        var model = result?.Model as HomePageViewModel;

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(model, Is.Not.Null);
            Assert.That(model.CurrentPage, Is.EqualTo(_mockHomePage.Object));
        });
    }

    [TearDown]
    public void TearDown()
    {
        _controller.Dispose();
        _controller = null;
    }
}